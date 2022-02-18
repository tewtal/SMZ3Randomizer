using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Randomizer.Shared.Models {

    public enum ClientState {
        Disconnected,
        Registering,
        Registered,
        Identifying,
        Patching,
        Ready,
        Playing,
        Completed,
    }

    [Index(nameof(ConnectionId))]
    public class Client {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Device { get; set; }
        public ClientState State { get; set; }
        public string ConnectionId { get; set; }
        public int SessionId { get; set; }
        public int WorldId { get; set; }
    }

}
