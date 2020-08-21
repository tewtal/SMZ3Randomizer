using System.Collections.Generic;

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

    public class Client {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Device { get; set; }
        public ClientState State { get; set; }
        public string ConnectionId { get; set; }
        public int SessionId { get; set; }
        public int WorldId { get; set; }
        public int RecievedSeq { get; set; }
        public int SentSeq { get; set; }
        public List<Event> Events { get; set; }
    }

}
