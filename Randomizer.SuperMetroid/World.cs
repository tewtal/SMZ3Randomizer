using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SuperMetroid {

    class World {

        public List<Location> Locations { get; set; }
        public List<Region> Regions { get; set; }
        public Difficulty Difficulty { get; set; }

        public World(Difficulty difficulty) {
            Difficulty = difficulty;

            Regions = new List<Region> {
                new Regions.Crateria.Central(this, Difficulty),
                new Regions.Crateria.West(this, Difficulty)
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
