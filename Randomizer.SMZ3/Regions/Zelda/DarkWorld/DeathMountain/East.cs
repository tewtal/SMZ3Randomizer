using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.Zelda.DarkWorld.DeathMountain {

    class East : Z3Region {

        public override string Name => "Dark World Death Mountain East";
        public override string Area => "Dark World";

        public East(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+65, 0xEB51, LocationType.Regular, "Hookshot Cave - Top Right",
                    items => items.MoonPearl && items.Hookshot),
                new Location(this, 256+66, 0xEB54, LocationType.Regular, "Hookshot Cave - Top Left",
                    items => items.MoonPearl && items.Hookshot),
                new Location(this, 256+67, 0xEB57, LocationType.Regular, "Hookshot Cave - Bottom Left",
                    items => items.MoonPearl && items.Hookshot),
                new Location(this, 256+68, 0xEB5A, LocationType.Regular, "Hookshot Cave - Bottom Right",
                    items => items.MoonPearl && (items.Hookshot || items.Boots)),
                new Location(this, 256+69, 0xEA7C, LocationType.Regular, "Superbunny Cave - Top",
                    items => items.MoonPearl),
                new Location(this, 256+70, 0xEA7F, LocationType.Regular, "Superbunny Cave - Bottom",
                    items => items.MoonPearl),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.CanLiftHeavy() && World.CanEnter("Light World Death Mountain East", items);
        }

    }

}
