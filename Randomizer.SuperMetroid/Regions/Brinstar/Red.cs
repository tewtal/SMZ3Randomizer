using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;

namespace Randomizer.SuperMetroid.Regions.Brinstar {

    class Red : Region {

        public override string Name => "Brinstar Red";
        public override string Area => "Brinstar";

        public Red(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, "X-Ray Scope", LocationType.Chozo, 0x78876, Logic switch {
                    Casual => items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && (items.Has(Grapple) || items.Has(SpaceJump)),
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && (
                        items.Has(Grapple) || items.Has(SpaceJump) ||
                        (items.CanIbj() || items.Has(HiJump) && items.Has(SpeedBooster) || items.CanSpringBallJump()) &&
                            (items.Has(Varia) && items.HasEnergyReserves(3) || items.HasEnergyReserves(5))))
                }),
                new Location(this, "Power Bomb (red Brinstar sidehopper room)", LocationType.Visible, 0x788CA, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(Super))
                }),
                new Location(this, "Power Bomb (red Brinstar spike room)", LocationType.Chozo, 0x7890E, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(Super))
                }),
                new Location(this, "Missile (red Brinstar spike room)", LocationType.Visible, 0x78914, Logic switch {
                    Casual => items => (items.CanUsePowerBombs() || items.Has(Ice)) && items.Has(Super),
                    _ => new Requirement(items => items.Has(Super))
                }),
                new Location(this, "Spazer", LocationType.Chozo, 0x7896E, Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages() && items.Has(Super))
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph);
        }

    }

}
