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
                .Include(s => s.Clients)
                .Include(s => s.Seed).ThenInclude(s => s.Worlds)
                .FirstOrDefaultAsync(s => s.Guid == request.SessionGuid);

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
                    ClientState = (ClientState)(session.Clients.FirstOrDefault(c => c.WorldId == w.WorldId)?.State ?? Shared.Models.ClientState.Disconnected),
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
                    Events = new List<Shared.Models.Event>(),                    
                    Name = session.Seed.Worlds.FirstOrDefault(w => w.WorldId == request.WorldId)?.Player ?? "Unknown",
                    RecievedSeq = 0,
                    SentSeq = 0,
                    SessionId = session.Id,
                    State = Shared.Models.ClientState.Registered,
                    WorldId = request.WorldId
                };
                
                _context.Add(client);
                await _context.SaveChangesAsync();

                return new RegisterPlayerResponse
                {
                    ClientId = client.Id,
                    ClientGuid = client.Guid,
                    ClientToken = client.ConnectionId,
                    WorldId = client.WorldId
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