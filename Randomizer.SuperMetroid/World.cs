using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SuperMetroid {

    class World {

        public List<Location> Locations { get; set; }
        public List<Region> Regions { get; set; }
        public List<Item> Items { get; set; }
        public Logic Logic { get; set; }
        public string Player { get; set; }

        public World(Logic logic, string player) {
            Logic = logic;
            Player = player;

            Regions = new List<Region> {
                new Regions.Crateria.Central(this, Logic),
                new Regions.Crateria.West(this, Logic),
                new Regions.Crateria.East(this, Logic),
                new Regions.Brinstar.Blue(this, Logic),
                new Regions.Brinstar.Green(this, Logic),
                new Regions.Brinstar.Kraid(this, Logic),
                new Regions.Brinstar.Pink(this, Logic),
                new Regions.Brinstar.Red(this, Logic),
                new Regions.Maridia.Outer(this, Logic),
                new Regions.Maridia.Inner(this, Logic),
                new Regions.NorfairUpper.West(this, Logic),
                new Regions.NorfairUpper.East(this, Logic),
                new Regions.NorfairUpper.Crocomire(this, Logic),
                new Regions.NorfairLower.West(this, Logic),
                new Regions.NorfairLower.East(this, Logic),
                new Regions.WreckedShip(this, Logic)
            };

            Locations = Regions.SelectMany(x => x.Locations).ToList();
            Items = new List<Item>();
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
