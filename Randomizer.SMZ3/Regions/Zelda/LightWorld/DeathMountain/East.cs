using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda.LightWorld.DeathMountain {

    class East : Region {

        public override string Name => "Light World Death Mountain East";
        public override string Area => "Light World";

        public East(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 256+4, 0x180141, LocationType.Regular, "Floating Island",
                    items => items.Has(Mirror) && items.Has(MoonPearl) && items.CanLiftHeavy()),
                new Location(this, 256+5, 0xE9BF, LocationType.Regular, "Spiral Cave"),
                new Location(this, 256+6, 0xEB39, LocationType.Regular, "Paradox Cave Upper - Left"),
                new Location(this, 256+7, 0xEB3C, LocationType.Regular, "Paradox Cave Upper - Right"),
                new Location(this, 256+8, 0xEB2A, LocationType.Regular, "Paradox Cave Lower - Far Left"),
                new Location(this, 256+9, 0xEB2D, LocationType.Regular, "Paradox Cave Lower - Left"),
                new Location(this, 256+10, 0xEB36, LocationType.Regular, "Paradox Cave Lower - Middle"),
                new Location(this, 256+11, 0xEB30, LocationType.Regular, "Paradox Cave Lower - Right"),
                new Location(this, 256+12, 0xEB33, LocationType.Regular, "Paradox Cave Lower - Far Right"),
                new Location(this, 256+13, 0xE9C5, LocationType.Regular, "Mimic Cave",
                    items => items.Has(Mirror) && items.Has(KeyTR, 2) && World.CanEnter("Turtle Rock", items)),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return World.CanEnter("Light World Death Mountain West", items) && (
                items.Has(Hammer) && items.Has(Mirror) ||
                items.Has(Hookshot)
            );
        }

    }

}
