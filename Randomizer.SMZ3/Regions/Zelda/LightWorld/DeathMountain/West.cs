using System.Collections.Generic;
using static Randomizer.SMZ3.Z3Logic;

namespace Randomizer.SMZ3.Regions.Zelda.LightWorld.DeathMountain {

    class West : Z3Region {

        public override string Name => "Light World Death Mountain West";
        public override string Area => "Death Mountain";

        public West(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+0, 0x308016, LocationType.Ether, "Ether Tablet",
                    items => items.Book && items.MasterSword && (items.Mirror || items.Hammer && items.Hookshot)),
                new Location(this, 256+1, 0x308140, LocationType.Regular, "Spectacle Rock",
                    items => items.Mirror),
                new Location(this, 256+2, 0x308002, LocationType.Regular, "Spectacle Rock Cave"),
                new Location(this, 256+3, 0x1EE9FA, LocationType.Regular, "Old Man", Logic switch {
                    Normal => items => items.Lamp,
                    _ => new Requirement(items => items.Lamp || items.Sword || items.Hookshot),
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Normal => items.Flute || items.CanLiftLight() && items.Lamp || items.CanAccessDeathMountainPortal(),
                _ => items.Flute || (items.CanLiftLight() && (items.Lamp || items.Sword || items.Hookshot)) || items.CanAccessDeathMountainPortal(),
            };
        }

    }

}
