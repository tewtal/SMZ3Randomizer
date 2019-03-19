using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda.DarkWorld.DeathMountain {

    class East : Region {

        public override string Name => "Dark World Death Mountain East";
        public override string Area => "Dark World";

        public East(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 256+65, 0xEB51, LocationType.Regular, "Hookshot Cave - Top Right",
                    items => items.Has(MoonPearl) && items.Has(Hookshot)),
                new Location(this, 256+66, 0xEB54, LocationType.Regular, "Hookshot Cave - Top Left",
                    items => items.Has(MoonPearl) && items.Has(Hookshot)),
                new Location(this, 256+67, 0xEB57, LocationType.Regular, "Hookshot Cave - Bottom Left",
                    items => items.Has(MoonPearl) && items.Has(Hookshot)),
                new Location(this, 256+68, 0xEB5A, LocationType.Regular, "Hookshot Cave - Bottom Right",
                    items => items.Has(MoonPearl) && (items.Has(Hookshot) || items.Has(Boots))),
                new Location(this, 256+69, 0xEA7C, LocationType.Regular, "Superbunny Cave - Top",
                    items => items.Has(MoonPearl)),
                new Location(this, 256+70, 0xEA7F, LocationType.Regular, "Superbunny Cave - Bottom",
                    items => items.Has(MoonPearl)),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return items.CanLiftHeavy() && World.CanEnter("Light World Death Mountain East", items);
        }

    }

}
