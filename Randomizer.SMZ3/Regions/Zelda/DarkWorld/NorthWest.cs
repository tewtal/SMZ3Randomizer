using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.Zelda.DarkWorld {

    class NorthWest : Region {

        public override string Name => "Dark World North West";
        public override string Area => "Dark World";

        public NorthWest(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 256+71, 0x180146, LocationType.Regular, "Bumper Cave",
                    items => items.CanLiftLight() && items.Has(Cape)),
                new Location(this, 256+72, 0xEDA8, LocationType.Regular, "Chest Game"),
                new Location(this, 256+73, 0xE9EF, LocationType.Regular, "C-Shaped House"),
                new Location(this, 256+74, 0xE9EC, LocationType.Regular, "Brewery"),
                new Location(this, 256+75, 0x180006, LocationType.Regular, "Hammer Pegs",
                    items => items.CanLiftHeavy() && items.Has(Hammer)),
                new Location(this, 256+76, 0x18002A, LocationType.Regular, "Blacksmith",
                    items => items.CanLiftHeavy()),
                new Location(this, 256+77, 0x33D68, LocationType.Regular, "Purple Chest",
                    items => items.CanLiftHeavy()),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return items.Has(MoonPearl) && ((
                    World.CanAquire(items, Agahnim) ||
                    items.CanAccessDarkWorldPortal(Logic) && items.Has(Flippers)
                ) && items.Has(Hookshot) && (items.Has(Flippers) || items.CanLiftLight() || items.Has(Hammer)) ||
                items.Has(Hammer) && items.CanLiftLight() ||
                items.CanLiftHeavy()
            );
        }

    }

}
