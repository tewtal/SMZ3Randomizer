using System.Collections.Generic;
using static Randomizer.SMZ3.Z3Logic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda.DarkWorld.DeathMountain {

    class East : Z3Region {

        public override string Name => "Dark World Death Mountain East";
        public override string Area => "Dark World";

        public East(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+65, 0x1EB51, LocationType.Regular, "Hookshot Cave - Top Right",
                    items => items.MoonPearl && items.Hookshot),
                new Location(this, 256+66, 0x1EB54, LocationType.Regular, "Hookshot Cave - Top Left",
                    items => items.MoonPearl && items.Hookshot),
                new Location(this, 256+67, 0x1EB57, LocationType.Regular, "Hookshot Cave - Bottom Left",
                    items => items.MoonPearl && items.Hookshot),
                new Location(this, 256+68, 0x1EB5A, LocationType.Regular, "Hookshot Cave - Bottom Right", Logic switch {
                    Normal => items => items.MoonPearl && items.Hookshot,
                    _ => new Requirement(items => items.MoonPearl && (items.Hookshot || items.Boots)),
                }),
                new Location(this, 256+69, 0x1EA7C, LocationType.Regular, "Superbunny Cave - Top", Logic switch {
                    Normal => items => items.MoonPearl,
                    _ => new Requirement(items => true),
                }),
                new Location(this, 256+70, 0x1EA7F, LocationType.Regular, "Superbunny Cave - Bottom", Logic switch {
                    Normal => items => items.MoonPearl,
                    _ => new Requirement(items => true),
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.CanLiftHeavy() && World.CanEnter("Light World Death Mountain East", items);
        }

    }

}
