using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebRandomizer.Models {
    public class Location {
        public int Id { get; set; }
        public int WorldId { get; set; }
        public int LocationId { get; set; }
        public int ItemId { get; set; }
        public int ItemWorldId { get; set; }
    }
}
