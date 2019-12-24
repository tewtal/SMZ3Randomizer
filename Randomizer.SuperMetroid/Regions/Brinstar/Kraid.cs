using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.ItemClass;

namespace Randomizer.SuperMetroid.Regions.Brinstar {

    class Kraid : Region {

        public override string Name => "Brinstar Kraid";
        public override string Area => "Brinstar";

        public Kraid(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 43, "Energy Tank, Kraid", LocationType.Hidden, Major, 0x7899C),
                new Location(this, 48, "Varia Suit", LocationType.Chozo, Major, 0x78ACA),
                new Location(this, 44, "Missile (Kraid)", LocationType.Hidden, Minor, 0x789EC, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.CanPassBombPassages();
        }

    }

}
