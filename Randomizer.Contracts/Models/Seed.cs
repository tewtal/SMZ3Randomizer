using System.Collections.Generic;

namespace Randomizer.Shared.Models {

    public class Seed {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Mode { get; set; }
        public string SeedNumber { get; set; }
        public string Spoiler { get; set; }
        public string GameName { get; set; }
        public string GameVersion { get; set; }
        public string GameId { get; set; }
        public string Hash { get; set; }
        public int Players { get; set; }
        public List<World> Worlds { get; set; }
    }

}
