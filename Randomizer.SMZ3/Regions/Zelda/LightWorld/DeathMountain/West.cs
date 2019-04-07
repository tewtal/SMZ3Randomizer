using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.Zelda.LightWorld.DeathMountain {

    class West : Region {

        public override string Name => "Light World Death Mountain West";
        public override string Area => "Light World";

        public West(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+0, 0x180016, LocationType.Ether, "Ether Tablet",
                    items => items.Book && items.MasterSword && (items.Mirror || items.Hammer && items.Hookshot)),
                new Location(this, 256+1, 0x180140, LocationType.Regular, "Spectacle Rock",
                    items => items.Mirror),
                new Location(this, 256+2, 0x180002, LocationType.Regular, "Spectacle Rock Cave"),
                new Location(this, 256+3, 0xF69FA, LocationType.Regular, "Old Man",
                    items => items.Lamp),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.Flute || items.CanLiftLight() && items.Lamp || items.CanAccessDeathMountainPortal();
        }

    }

}
