using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SuperMetroid {

    class World {

        public List<Location> Locations { get; set; }
        public List<Region> Regions { get; set; }
        public List<Item> Items { get; set; }
        public Config Config { get; set; }
        public string Player { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }

        public World(Config config, string player, int id, string guid) {
            Id = id;
            Config = config;
            Player = player;
            Guid = guid;

            Regions = new List<Region> {
                new Regions.Crateria.Central(this, Config.Logic),
                new Regions.Crateria.West(this, Config.Logic),
                new Regions.Crateria.East(this, Config.Logic),
                new Regions.Brinstar.Blue(this, Config.Logic),
                new Regions.Brinstar.Green(this, Config.Logic),
                new Regions.Brinstar.Kraid(this, Config.Logic),
                new Regions.Brinstar.Pink(this, Config.Logic),
                new Regions.Brinstar.Red(this, Config.Logic),
                new Regions.Maridia.Outer(this, Config.Logic),
                new Regions.Maridia.Inner(this, Config.Logic),
                new Regions.NorfairUpper.West(this, Config.Logic),
                new Regions.NorfairUpper.East(this, Config.Logic),
                new Regions.NorfairUpper.Crocomire(this, Config.Logic),
                new Regions.NorfairLower.West(this, Config.Logic),
                new Regions.NorfairLower.East(this, Config.Logic),
                new Regions.WreckedShip(this, Config.Logic)
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
