using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Randomizer.Shared.Models {

    public enum SessionState {
        Created,
        Running,
        Done,
    }

    [Index(nameof(Guid))]
    public class Session {
        public int Id { get; set; }
        public string Guid { get; set; }
        public SessionState State { get; set; }
        public List<Client> Clients { get; set; }
        public Seed Seed { get; set; }
        public List<SessionEvent> Events { get; set; }
    }

}
