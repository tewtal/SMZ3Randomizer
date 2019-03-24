using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SuperMetroid {

    class World {

        public List<Location> Locations { get; set; }
        public List<Region> Regions { get; set; }
        public Logic Logic { get; set; }

        public World(Logic logic) {
            Logic = logic;

            Regions = new List<Region> {
                new Regions.Crateria.Central(this, Logic),
                new Regions.Crateria.West(this, Logic)
            };

            Locations = Regions.SelectMany(x => x.Locations).ToList().Shuffle();
        }

        internal bool CanEnter(string regionName, List<Item> items) {
            var region = Regions.Find(r => r.Name == regionName);
            if (region != null)
                return region.CanEnter(items);
            else
                throw new ArgumentException("World.CanEnter: Invalid region name " + regionName);
        }

    }

}
