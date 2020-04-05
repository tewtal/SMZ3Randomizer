using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebRandomizer.Models {

    public class World {
        public int Id { get; set; }
        public int WorldId { get; set; }
        public int SeedId { get; set; }
        public string Guid { get; set; }
        public string Player { get; set; }
        public string Settings { get; set; }
        public byte[] Patch { get; set; }
        public List<Location> Locations { get; set; }
    }

}
