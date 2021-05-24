using System.Collections.Generic;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.Zelda.DarkWorld {

    class NorthEast : Z3Region {

        public override string Name => "Dark World North East";
        public override string Area => "Dark World";

        public NorthEast(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+78, 0x1DE185, LocationType.Regular, "Catfish",
                    items => items.MoonPearl && items.CanLiftLight()),
                new Location(this, 256+79, 0x308147, LocationType.Regular, "Pyramid"),
                new Location(this, 256+80, 0x1E980, LocationType.Regular, "Pyramid Fairy - Left",
                    items => World.CanAcquireAll(items, CrystalRed) && items.MoonPearl && World.CanEnter("Dark World South", items) &&
                        (items.Hammer || items.Mirror && World.CanAcquire(items, Agahnim))),
                new Location(this, 256+81, 0x1E983, LocationType.Regular, "Pyramid Fairy - Right",
                    items => World.CanAcquireAll(items, CrystalRed) && items.MoonPearl && World.CanEnter("Dark World South", items) &&
                        (items.Hammer || items.Mirror && World.CanAcquire(items, Agahnim)))
            };
        }

        public override bool CanEnter(Progression items) {
            return World.CanAcquire(items, Agahnim) || items.MoonPearl && (
                items.Hammer && items.CanLiftLight() ||
                items.CanLiftHeavy() && items.Flippers ||
                items.CanAccessDarkWorldPortal(Config) && items.Flippers
            );
        }

    }

}
