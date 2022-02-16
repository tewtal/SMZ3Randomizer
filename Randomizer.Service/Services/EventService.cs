using Grpc.Core;
using Randomizer.Service;
using Randomizer.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Randomizer.Service.Services
{
    public class EventService : Event.EventBase
    {
        private readonly ILogger<EventService> _logger;
        private readonly RandomizerContext _context;

        public EventService(ILogger<EventService> logger, RandomizerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override async Task<GetEventsResponse> GetEvents(GetEventsRequest request, ServerCallContext context)
        {
            var events = await _context.Clients
                .Where(c => c.ConnectionId == request.ClientToken)
                .Join(_context.SessionEvents, c => c.SessionId, e => e.SessionId, (c, e) => e)
                .Where(x =>
                    (!request.HasFromEventId || (x.Id >= request.FromEventId)) &&
                    (!request.HasToEventId || (x.Id <= request.ToEventId)) &&
                    (!request.HasFromWorldId || (x.FromWorldId == request.FromWorldId)) &&
                    (!request.HasToWorldId || (x.ToWorldId == request.ToWorldId)) &&
                    request.EventTypes.Contains((EventType)x.EventType))
                .ToListAsync();

            if (events == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The specified client is not registered any session"));
            }

            var response = new GetEventsResponse();
            response.Events.AddRange(events.Select(e => new SessionEvent
            {
                Id = e.Id,
                SequenceNum = e.SequenceNum,
                FromWorldId = e.FromWorldId,
                ToWorldId = e.ToWorldId,
                ItemId = e.ItemId,
                ItemLocation = e.ItemLocation,
                EventType = (EventType)e.EventType,
                TimeStamp = e.TimeStamp.ToUniversalTime().ToString(),
                Message = e.Message,
                Confirmed = e.Confirmed
            }));

            return response;
        }

        public override async Task<ConfirmEventsResponse> ConfirmEvents(ConfirmEventsRequest request, ServerCallContext context)
        {
            var clientSession = await _context.Clients.Where(c => c.ConnectionId == request.ClientToken)
                .Join(_context.Sessions, c => c.SessionId, s => s.Id, (c, s) => new { Client = c, Session = s })
                .FirstOrDefaultAsync();

            if (clientSession == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The specified client is not registered any session"));
            }

            var events = await _context.SessionEvents.Where(e => request.EventIds.Contains(e.Id)).ToListAsync();
            foreach (var ev in events)
            {
                ev.Confirmed = true;
                _context.Update(ev);
            }

            try
            {
                await _context.SaveChangesAsync();
            } catch
            {
                throw new RpcException(new Status(StatusCode.Aborted, "An error occured during event update, please try again"));
            }

            var response = new ConfirmEventsResponse();
            response.EventIds.AddRange(request.EventIds);
            return response;            
        }

        public override async Task<SendEventResponse> SendEvent(SendEventRequest request, ServerCallContext context)
        {
            var clientSession = await _context.Clients.Where(c => c.ConnectionId == request.ClientToken)
                .Join(_context.Sessions, c => c.SessionId, s => s.Id, (c, s) => new { Client = c, Session = s })
                .FirstOrDefaultAsync();

            if (clientSession == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The specified client is not registered any session"));
            }

            var sessionEvent = new Shared.Models.SessionEvent {
                SessionId = clientSession.Session.Id,
                SequenceNum = request.Event.SequenceNum,
                FromWorldId = clientSession.Client.WorldId, // We're not allowing clients to spoof their worldId (maybe if we implement some kind of permission system later)
                ToWorldId = request.Event.ToWorldId,
                ItemId = request.Event.ItemId,
                ItemLocation = request.Event.ItemLocation,
                EventType = (SessionEventType)request.Event.EventType,
                Message = request.Event.Message,
                Confirmed = request.Event.Confirmed,
                TimeStamp = DateTime.UtcNow
            };


            try
            {
                /* Handle event pre-save if needed */
                (sessionEvent, var ok) = await PreProcessEvent(clientSession.Client, sessionEvent);
                
                if (ok)
                {                  
                    _context.Add(sessionEvent);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                throw new RpcException(new Status(StatusCode.Aborted, "An error occured during event creation, please try again"));
            }

            /* Handle event post-save if needed */
            sessionEvent = await PostProcessEvent(clientSession.Client, sessionEvent);

            var response = new SendEventResponse
            {
                Event = new SessionEvent
                {
                    Id = sessionEvent.Id,
                    SequenceNum = sessionEvent.SequenceNum,
                    FromWorldId = sessionEvent.FromWorldId,
                    ToWorldId = sessionEvent.ToWorldId,
                    ItemId = sessionEvent.ItemId,
                    ItemLocation = sessionEvent.ItemLocation,
                    EventType = (EventType)sessionEvent.EventType,
                    TimeStamp = sessionEvent.TimeStamp.ToUniversalTime().ToString(),
                    Message = sessionEvent.Message,
                    Confirmed = sessionEvent.Confirmed
                }
            };

            return response;
        }

        private async Task<(Shared.Models.SessionEvent, bool)> PreProcessEvent(Client client, Shared.Models.SessionEvent sessionEvent)
        {
            bool ok = true;
            
            if (sessionEvent.EventType == SessionEventType.ItemFound)
            {
                /* Reject any duplicate item event */
                ok = !await _context.SessionEvents.AnyAsync(e => 
                    e.SessionId == sessionEvent.SessionId &&
                    e.FromWorldId == sessionEvent.FromWorldId &&
                    e.ToWorldId == sessionEvent.ToWorldId &&
                    e.ItemId == sessionEvent.ItemId &&
                    e.ItemLocation == sessionEvent.ItemLocation &&
                    e.EventType == sessionEvent.EventType
                );
            }

            if (sessionEvent.EventType == SessionEventType.Forfeit)
            {
                /* Only let the actual owner of a world Id forfeit for themselves */
                if (client.WorldId == sessionEvent.FromWorldId)
                {
                    ok = sessionEvent.Confirmed = await Forfeit(client);
                }
            }

            return await Task.FromResult((sessionEvent, ok));
        }

        private async Task<Shared.Models.SessionEvent> PostProcessEvent(Client client, Shared.Models.SessionEvent sessionEvent)
        {
            return await Task.FromResult(sessionEvent);
        }

        private async Task<bool> Forfeit(Client client)
        {
            var world = await _context.Sessions
                .Where(s => s.Id == client.SessionId)
                .Join(_context.Worlds, s => s.Seed.Id, w => w.SeedId, (s, w) => w)
                .Where(w => w.WorldId == client.WorldId).FirstOrDefaultAsync();

            if (world != null)
            {
                var locations = await _context.Locations
                    .Where(l => l.WorldId == world.Id && 
                    !_context.SessionEvents.Where(e => 
                        e.EventType == SessionEventType.ItemFound &&
                        e.FromWorldId == client.WorldId &&
                        e.ToWorldId != client.WorldId &&
                        e.SessionId == client.SessionId
                     )
                    .Select(e => e.ItemId).Contains(l.ItemId)).ToListAsync();

                foreach (var location in locations)
                {
                    var sessionEvent = new Shared.Models.SessionEvent
                    {
                        FromWorldId = client.WorldId,
                        EventType = SessionEventType.ItemFound,
                        ItemId = location.ItemId,
                        ItemLocation = location.LocationId,
                        SequenceNum = 0,
                        SessionId = client.SessionId,
                        Confirmed = false,
                        ToWorldId = location.ItemWorldId,
                        Message = $"Item {location.ItemId} forfeited to player {location.ItemWorldId}",
                        TimeStamp = DateTime.UtcNow
                    };

                    _context.Add(sessionEvent);
                }

                try
                {
                    await _context.SaveChangesAsync();
                    return true;
                } catch {
                    return false;
                }               
            }

            return false;
        }
    }
}