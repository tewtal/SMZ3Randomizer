using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.Zelda.DarkWorld {

    class Mire : Region {

        public override string Name => "Dark World Mire";
        public override string Area => "Dark World";

        public Mire(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+89, 0xEA73, LocationType.Regular, "Mire Shed - Left",
                    items => items.MoonPearl),
                new Location(this, 256+90, 0xEA76, LocationType.Regular, "Mire Shed - Right",
                    items => items.MoonPearl),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.Flute && items.CanLiftHeavy() || items.CanAccessMiseryMirePortal(Config);
        }

    }

}
