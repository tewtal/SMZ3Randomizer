using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebRandomizer.Models {
    public class Seed {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Type { get; set; }
        public string SeedNumber { get; set; }
        public string Spoiler { get; set; }
        public string GameName { get; set; }
        public int Players { get; set; }
        public List<World> Worlds { get; set; }

    }
}
