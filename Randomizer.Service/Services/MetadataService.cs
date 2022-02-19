using Grpc.Core;
using Randomizer.Service;
using Randomizer.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Randomizer.Service.Services
{
    public class MetadataService : Metadata.MetadataBase
    {
        private readonly ILogger<MetadataService> _logger;
        private readonly RandomizerContext _context;

        public MetadataService(ILogger<MetadataService> logger, RandomizerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override async Task<GetPatchResponse> GetPatch(GetPatchRequest request, ServerCallContext context)
        {

            var patch = await _context.Sessions
                .Join(_context.Clients, s => s.Id, c => c.SessionId, (s, c) => new { Client = c, Session = s })
                .Join(_context.Worlds, x => x.Session.Seed.Id, w => w.SeedId, (x, w) => new { ClientSession = x, World = w })
                .Where(x => x.ClientSession.Client.ConnectionId == request.ClientToken && x.World.WorldId == x.ClientSession.Client.WorldId)
                .Select(x => x.World.Patch)
                .FirstOrDefaultAsync();

            if (patch == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Could not retrieve a patch for this client token"));
            }

            return new GetPatchResponse
            {
                PatchData = Google.Protobuf.ByteString.CopyFrom(patch)
            };
        }

        public override Task<GetSpoilerResponse> GetSpoiler(GetSpoilerRequest request, ServerCallContext context)
        {
            //TODO: Implement this properly, although it's not needed right now since the web-ui can call into the
            //      regular spoiler API to retrieve the spoiler.
            return Task.FromResult(
                new GetSpoilerResponse
                {
                    Spoiler = "\"[]\""
                }
            );
        }
    }
}