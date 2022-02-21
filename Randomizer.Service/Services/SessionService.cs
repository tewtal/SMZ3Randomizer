using Grpc.Core;
using Randomizer.Service;
using Randomizer.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Randomizer.Service.Services
{
    public class SessionService : Session.SessionBase
    {
        private readonly ILogger<SessionService> _logger;
        private readonly RandomizerContext _context;

        public SessionService(ILogger<SessionService> logger, RandomizerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override async Task<GetSessionResponse> GetSession(GetSessionRequest request, ServerCallContext context)
        {
            var session = await _context.Sessions
                .Where(s => s.Guid == request.SessionGuid)
                .Include(s => s.Clients)
                .Include(s => s.Seed).ThenInclude(s => s.Worlds.OrderBy(w => w.WorldId))
                .FirstOrDefaultAsync();

            if (session != null)
            {
                var reply = new GetSessionResponse
                {
                    Id = session.Id,
                    Guid = session.Guid,
                    State = SessionState.Created,
                    Seed = new Seed {
                        GameId = session.Seed.GameId,
                        GameName = session.Seed.GameName,
                        GameMode = session.Seed.Mode,
                        GameVersion = session.Seed.GameVersion,
                        Guid = session.Seed.Guid,
                        Hash = session.Seed.Hash,
                        Id = session.Seed.Id,
                        Number = session.Seed.SeedNumber,
                        Players = session.Seed.Players,
                    }
                };

                reply.Seed.Worlds.AddRange(session.Seed.Worlds.Select(w => new World
                {
                    Id = w.Id,
                    WorldId = w.WorldId,
                    ClientState = (ClientState)w.State,
                    Guid = w.Guid,
                    PlayerName = w.Player,
                    Settings = w.Settings
                }));

                return reply;
            } else
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Session not found"));
            }
        }

        public override async Task<UpdatePlayerResponse> UpdatePlayer(UpdatePlayerRequest request, ServerCallContext context)
        {
            var session = await _context.Clients
                .Join(_context.Sessions, c => c.SessionId, s => s.Id, (c, s) => new { Client = c, Session = s })
                .Join(_context.Seeds, x => x.Session.Seed.Id, s => s.Id, (x, s) => new { Client = x.Client, Session = x.Session, Seed = s })
                .Where(x => x.Client.ConnectionId == request.ClientToken)
                .FirstOrDefaultAsync();
            
            if (session == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The specified client is not registered any session"));
            }

            var client = session.Client;
            var world = await _context.Worlds.Where(w => w.SeedId == session.Seed.Id && w.WorldId == client.WorldId).FirstOrDefaultAsync();

            if (world == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "There is no world for this client"));
            }

            if (request.HasDeviceName)
            {
                client.Device = request.DeviceName;
            }

            client.State = (Shared.Models.ClientState)request.ClientState;
            world.State = client.State;

            _context.Update(client);
            _context.Update(world);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new RpcException(new Status(StatusCode.Aborted, "An error occured during player update, please try again"));
            }

            return new UpdatePlayerResponse
            {
                Success = true
            };

        }

        public override async Task<UnregisterPlayerResponse> UnregisterPlayer(UnregisterPlayerRequest request, ServerCallContext context)
        {
            var session = await _context.Clients
                .Join(_context.Sessions, c => c.SessionId, s => s.Id, (c, s) => new { Client = c, Session = s })
                .Join(_context.Seeds, x => x.Session.Seed.Id, s => s.Id, (x, s) => new { Client = x.Client, Session = x.Session, Seed = s })
                .Where(x => x.Client.ConnectionId == request.ClientToken)
                .FirstOrDefaultAsync();

            if (session == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The specified client is not registered any session"));
            }

            var client = session.Client;

            var world = await _context.Worlds.Where(w => w.SeedId == session.Seed.Id && w.WorldId == client.WorldId).FirstOrDefaultAsync();

            if (world == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "There is no world for this client"));
            }

            world.State = Shared.Models.ClientState.Disconnected;            

            _context.Remove(client);
            _context.Update(world);

            try
            {
                await _context.SaveChangesAsync();
            } catch
            {
                throw new RpcException(new Status(StatusCode.Aborted, "An error occured during player unregistration, please try again"));
            }

            return new UnregisterPlayerResponse
            {
                Success = true
            };
        }

        public override async Task<RegisterPlayerResponse> RegisterPlayer(RegisterPlayerRequest request, ServerCallContext context)
        {
            /* Get the session and all relevant data for this operation */
            var session = await _context.Sessions               
                .Include(s => s.Clients)
                .Include(s => s.Seed).ThenInclude(s => s.Worlds)
                .FirstOrDefaultAsync(s => s.Guid == request.SessionGuid);

            /* Make sure the session exists */
            if (session == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Session not found"));
            }

            /* Make sure the request world exists */
            if(!session.Seed.Worlds.Any(w => w.WorldId == request.WorldId))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The requested world does not exist for this session"));
            }

            /* Check if there is a client created with this worldId already */
            if(session.Clients.Any(c => c.WorldId == request.WorldId))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The requested world is already registered to another player"));
            }

            try
            {
                var client = new Client
                {
                    Guid = Guid.NewGuid().ToString().Replace("-", ""),
                    ConnectionId = Guid.NewGuid().ToString().Replace("-", ""),
                    Device = "",
                    Name = session.Seed.Worlds.FirstOrDefault(w => w.WorldId == request.WorldId)?.Player ?? "Unknown",
                    SessionId = session.Id,
                    State = Shared.Models.ClientState.Registered,
                    WorldId = request.WorldId
                };
                
                _context.Add(client);

                var clientWorld = session.Seed.Worlds.Find(w => w.WorldId == client.WorldId);
                if (clientWorld != null)
                {
                    clientWorld.State = client.State;
                    _context.Update(clientWorld);
                }                
                
                await _context.SaveChangesAsync();

                return new RegisterPlayerResponse
                {
                    ClientId = client.Id,
                    ClientGuid = client.Guid,
                    ClientToken = client.ConnectionId,
                    WorldId = client.WorldId,
                    PlayerName = client.Name
                };

            } catch {
                throw new RpcException(new Status(StatusCode.Aborted, "An error occured during player registration, please try again"));
            }
        }

        public override async Task<RegisterPlayerResponse> LoginPlayer(LoginPlayerRequest request, ServerCallContext context)
        {
            /* Get the session and all relevant data for this operation */
            var session = await _context.Sessions
                .Include(s => s.Clients)
                .FirstOrDefaultAsync(s => s.Guid == request.SessionGuid);

            /* Make sure the session exists */
            if (session == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Session not found"));
            }

            /* Check if there's a client session with this client guid */
            var client = session.Clients.FirstOrDefault(c => c.Guid == request.ClientGuid);
            if (client != null)
            {
                try
                {
                    client.ConnectionId = Guid.NewGuid().ToString().Replace("-", "");
                    _context.Update(client);
                    await _context.SaveChangesAsync();

                    return new RegisterPlayerResponse
                    {
                        ClientGuid = client.Guid,
                        ClientId = client.Id,
                        ClientToken = client.ConnectionId,
                        PlayerName = client.Name,
                        WorldId = client.WorldId
                    };
                } catch
                {
                    throw new RpcException(new Status(StatusCode.Aborted, "An error occured during player login, please try again"));
                }
            } else
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The specified client is not registered to this session"));
            }
        }
    }
}