using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Kraid : Region {

        public override string Name => "Brinstar Kraid";
        public override string Area => "Brinstar";

        public Kraid(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 43, 0x7899C, LocationType.Hidden, "Energy Tank, Kraid"),
                new Location(this, 48, 0x78ACA, LocationType.Chozo, "Varia Suit"),
                new Location(this, 44, 0x789EC, LocationType.Hidden, "Missile (Kraid)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.CanPassBombPassages();
        }

    }

}
