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
            var patch = await _context.Clients
                .Where(c => c.ConnectionId == request.ClientToken)
                .Join(_context.Worlds, c => c.WorldId, w => w.WorldId, (c, w) => new { w.Patch })
                .FirstOrDefaultAsync();

            if (patch == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Could not retrieve a patch for this client token"));
            }

            return new GetPatchResponse
            {
                PatchData = Google.Protobuf.ByteString.CopyFrom(patch.Patch)
            };
        }

        public override Task<GetSpoilerResponse> GetSpoiler(GetSpoilerRequest request, ServerCallContext context)
        {
            return Task.FromResult(
                new GetSpoilerResponse
                {
                    Spoiler = "{}"
                }
            );
        }
    }
}