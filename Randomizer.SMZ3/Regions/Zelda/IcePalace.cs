using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class IcePalace : Region {

        public override string Name => "Ice Palace";
        public override string Area => "Ice Palace";

        public IcePalace(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 256+161, 0xE9D4, LocationType.Regular, "Ice Palace - Compass Chest"),
                new Location(this, 256+162, 0xE9E0, LocationType.Regular, "Ice Palace - Spike Room",
                    items => items.Has(Hookshot) || items.Has(BigKeyIP) && items.Has(KeyIP)),
                new Location(this, 256+163, 0xE9DD, LocationType.Regular, "Ice Palace - Map Chest",
                    items => items.Has(Hammer) && items.CanLiftLight() &&
                        items.Has(Hookshot) || items.Has(BigKeyIP) && items.Has(KeyIP)),
                new Location(this, 256+164, 0xE9A4, LocationType.Regular, "Ice Palace - Big Key Chest",
                    items => items.Has(Hammer) && items.CanLiftLight() &&
                        items.Has(Hookshot) || items.Has(BigKeyIP) && items.Has(KeyIP)),
                new Location(this, 256+165, 0xE9E3, LocationType.Regular, "Ice Palace - Iced T Room"),
                new Location(this, 256+166, 0xE995, LocationType.Regular, "Ice Palace - Freezor Chest"),
                new Location(this, 256+167, 0xE9AA, LocationType.Regular, "Ice Palace - Big Chest",
                    items => items.Has(BigKeyIP)),
                new Location(this, 256+168, 0x180157, LocationType.Regular, "Ice Palace - Kholdstare",
                    items => items.Has(BigKeyIP) && items.Has(Hammer) && items.CanLiftLight() &&
                        items.Has(KeyIP, items.Has(Somaria) ? 1 : 2)),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return items.Has(MoonPearl) && items.Has(Flippers) && items.CanLiftHeavy() && items.CanMeltFreezors();
        }

    }

}
