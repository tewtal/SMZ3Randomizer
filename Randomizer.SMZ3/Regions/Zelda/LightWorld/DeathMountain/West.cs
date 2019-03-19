using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda.LightWorld.DeathMountain {

    class West : Region {

        public override string Name => "Light World Death Mountain West";
        public override string Area => "Light World";

        public West(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 256+0, 0x180016, LocationType.Ether, "Ether Tablet",
                    items => items.Has(Book) && items.HasMasterSword() && (items.Has(Mirror) || items.Has(Hammer) && items.Has(Hookshot))),
                new Location(this, 256+1, 0x180140, LocationType.Regular, "Spectacle Rock",
                    items => items.Has(Mirror)),
                new Location(this, 256+2, 0x180002, LocationType.Regular, "Spectacle Rock Cave"),
                new Location(this, 256+3, 0xF69FA, LocationType.Regular, "Old Man",
                    items => items.Has(Lamp)),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return items.Has(Flute) || items.CanLiftLight() && items.Has(Lamp) || items.CanAccessDeathMountainPortal();
        }

    }

}
