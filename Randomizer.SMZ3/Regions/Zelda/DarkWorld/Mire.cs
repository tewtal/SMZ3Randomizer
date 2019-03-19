using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda.DarkWorld {

    class Mire : Region {

        public override string Name => "Dark World Mire";
        public override string Area => "Dark World";

        public Mire(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 256+89, 0xEA73, LocationType.Regular, "Mire Shed - Left",
                    items => items.Has(MoonPearl)),
                new Location(this, 256+90, 0xEA76, LocationType.Regular, "Mire Shed - Right",
                    items => items.Has(MoonPearl)),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return items.Has(Flute) && items.CanLiftHeavy() || items.CanAccessMiseryMirePortal(Logic);
        }

    }

}
