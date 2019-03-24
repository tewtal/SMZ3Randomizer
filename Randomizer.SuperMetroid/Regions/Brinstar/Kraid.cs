using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;

namespace Randomizer.SuperMetroid.Regions.Brinstar {

    class Kraid : Region {

        public override string Name => "Brinstar Kraid";
        public override string Area => "Brinstar";

        public Kraid(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, "Energy Tank, Kraid", LocationType.Hidden, 0x7899C),
                new Location(this, "Varia Suit", LocationType.Chozo, 0x78ACA),
                new Location(this, "Missile (Kraid)", LocationType.Hidden, 0x789EC, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.CanPassBombPassages();
        }

    }

}
