using Grpc.Core;
using Randomizer.Service;
using Randomizer.Shared.Models;
using Microsoft.EntityFrameworkCore;
using static Randomizer.SMZ3.ItemType;

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

        public override async Task<GetReportResponse> GetReport(GetReportRequest request, ServerCallContext context)
        {
            /* Get all events relevant to specified world */
            var events = await _context.Clients
                .Where(c => c.ConnectionId == request.ClientToken)
                .Join(_context.SessionEvents, c => c.SessionId, e => e.SessionId, (c, e) => e)
                .Where(x =>
                    (x.Id >= request.FromEventId) && (
                        x.FromWorldId == request.WorldId || 
                        (x.ToWorldId == request.WorldId || x.ToWorldId == -1)
                    ) && request.EventTypes.Contains((EventType)x.EventType))
                .ToListAsync();

            if (request.ClientToken != "" && events == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The specified client is not registered any session"));
            }

            var worlds = await _context.Worlds
                .Where(w => w.SeedId == request.SeedId)
                .OrderBy(w => w.WorldId)
                .ToListAsync();

            var response = new GetReportResponse();
            
            if (events != null)
            {
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
            }

            response.Worlds.AddRange(worlds.Select(w => new World
            {
                Id = w.Id,
                Guid = w.Guid,
                WorldId = w.WorldId,
                ClientState = (ClientState)w.State,
                PlayerName = w.Player,
                Settings = w.Settings
            }));

            return response;

        }

        public override async Task<GetEventsResponse> GetEvents(GetEventsRequest request, ServerCallContext context)
        {
            var events = await _context.Clients
                .Where(c => c.ConnectionId == request.ClientToken)
                .Join(_context.SessionEvents, c => c.SessionId, e => e.SessionId, (c, e) => e)
                .Where(x =>
                    (!request.HasFromEventId || (x.Id >= request.FromEventId)) &&
                    (!request.HasToEventId || (x.Id <= request.ToEventId)) &&
                    (!request.HasFromWorldId || (x.FromWorldId == request.FromWorldId || x.FromWorldId == -1)) &&
                    (!request.HasToWorldId || (x.ToWorldId == request.ToWorldId || x.ToWorldId == -1)) &&
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
            
            if (sessionEvent.EventType == SessionEventType.ItemFound && sessionEvent.ItemLocation >= 0)
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
            else if (sessionEvent.EventType == SessionEventType.Forfeit)
            {
                /* Only let the actual owner of a world Id forfeit for themselves */
                if (client.WorldId == sessionEvent.FromWorldId)
                {
                    ok = sessionEvent.Confirmed = await Forfeit(client);
                }
            }

            return (sessionEvent, ok);
        }

        private async Task<Shared.Models.SessionEvent> PostProcessEvent(Client client, Shared.Models.SessionEvent sessionEvent)
        {
            /* An item event with itemLocation -1 is a re-sent item, broadcast a system message to let all players know this */
            if (sessionEvent.EventType == SessionEventType.ItemFound && sessionEvent.ItemLocation == -1)
            {
                try
                {
                    var worlds = await _context.Sessions
                    .Where(s => s.Id == client.SessionId)
                    .Include(s => s.Seed).ThenInclude(s => s.Worlds)
                    .SelectMany(s => s.Seed.Worlds)
                    .ToListAsync();

                    var ev = new Shared.Models.SessionEvent
                    {
                        EventType = SessionEventType.SystemMessage,
                        FromWorldId = -1,
                        ToWorldId = -1,
                        ItemId = sessionEvent.ItemId,
                        ItemLocation = 0,
                        SequenceNum = -1,
                        SessionId = client.SessionId,
                        TimeStamp = DateTime.UtcNow,
                        Confirmed = true,
                        Message = $"{worlds.Find(w => w.WorldId == sessionEvent.FromWorldId)?.Player ?? "Unknown"} re-sent <itemId> to {worlds.Find(w => w.WorldId == sessionEvent.ToWorldId)?.Player ?? "Unknown"}"
                    };

                    _context.SessionEvents.Add(ev);
                    await _context.SaveChangesAsync();
                }
                catch { }                
            } 
            else if (sessionEvent.EventType == SessionEventType.ForfeitVote)
            {
                /* If there's players - 1 unique votes, have that player forfeit */
                var votes = await _context.SessionEvents.Where(e =>
                    e.SessionId == client.SessionId &&
                    e.ToWorldId == sessionEvent.ToWorldId &&
                    e.EventType == SessionEventType.ForfeitVote)
                    .GroupBy(e => e.FromWorldId)
                    .CountAsync();
               
                var worlds = await _context.Sessions
                        .Where(s => s.Id == client.SessionId)
                        .Include(s => s.Seed).ThenInclude(s => s.Worlds)
                        .SelectMany(s => s.Seed.Worlds)
                        .ToListAsync();

                var players = worlds.Where(w => w.State < Shared.Models.ClientState.Completed).Count();

                if(votes == (players - 1))
                {
                    var forfeitClient = await _context.Clients.FirstOrDefaultAsync(c => c.SessionId == client.SessionId && c.WorldId == sessionEvent.ToWorldId);
                    if (forfeitClient != null)
                    {
                        sessionEvent.Confirmed = await Forfeit(forfeitClient);
                    }
                } 
                else
                {
                    try
                    {
                        var ev = new Shared.Models.SessionEvent
                        {
                            EventType = SessionEventType.SystemMessage,
                            FromWorldId = -1,
                            ToWorldId = -1,
                            ItemId = 0,
                            ItemLocation = 0,
                            SequenceNum = -1,
                            SessionId = client.SessionId,
                            TimeStamp = DateTime.UtcNow,
                            Confirmed = true,
                            Message = $"{worlds.Find(w => w.WorldId == sessionEvent.FromWorldId)?.Player ?? "Unknown"} voted to remove {worlds.Find(w => w.WorldId == sessionEvent.ToWorldId)?.Player ?? "Unknown"}. ({(players - votes - 1)} more votes needed)"
                        };

                        _context.SessionEvents.Add(ev);
                        await _context.SaveChangesAsync();
                    }
                    catch { }
                }
            }
            
            return sessionEvent;
        }

        private async Task<bool> Forfeit(Client client)
        {
            var world = await _context.Sessions
                .Where(s => s.Id == client.SessionId)
                .Join(_context.Worlds, s => s.Seed.Id, w => w.SeedId, (s, w) => w)
                .Where(w => w.WorldId == client.WorldId).FirstOrDefaultAsync();

            if (world != null)
            {

                int[] excludeItems = new[] { (int)Arrow, (int)ThreeBombs, (int)OneRupee, (int)FiveRupees, (int)TwentyRupees, (int)TenArrows };

                // All locations available for this player that doesn't belong to themselves
                var locations = await _context.Locations
                    .Where(l =>
                        l.WorldId == world.Id &&
                        l.ItemWorldId != world.WorldId &&
                        !excludeItems.Contains(l.ItemId) &&
                        !_context.SessionEvents.Any(e =>
                            e.FromWorldId == world.WorldId &&
                            e.SessionId == client.SessionId &&
                            e.ItemId == l.ItemId &&
                            (e.ItemLocation == (l.LocationId * 8) || (e.ItemLocation == (((l.LocationId - 256) * 8) + 0x8000))))
                        )
                    .ToListAsync();

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

                client.State = Shared.Models.ClientState.Completed;
                world.State = client.State;
                _context.Update(client);
                _context.Update(world);

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