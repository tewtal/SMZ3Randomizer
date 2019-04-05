using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Red : Region {

        public override string Name => "Brinstar Red";
        public override string Area => "Brinstar";

        public Red(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 38, 0x78876, LocationType.Chozo, "X-Ray Scope", Config.Logic switch {
                    Casual => items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && (items.Has(Grapple) || items.Has(SpaceJump)),
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && (
                        items.Has(Grapple) || items.Has(SpaceJump) ||
                        (items.CanIbj() || items.Has(HiJump) && items.Has(SpeedBooster) || items.CanSpringBallJump()) &&
                            (items.Has(Varia) && items.HasEnergyReserves(3) || items.HasEnergyReserves(5))))
                }),
                new Location(this, 39, 0x788CA, LocationType.Visible, "Power Bomb (red Brinstar sidehopper room)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(Super))
                }),
                new Location(this, 40, 0x7890E, LocationType.Chozo, "Power Bomb (red Brinstar spike room)", Config.Logic switch {
                    Casual => items => (items.CanUsePowerBombs() || items.Has(Ice)) && items.Has(Super),
                    _ => new Requirement(items => items.Has(Super))
                }),
                new Location(this, 41, 0x78914, LocationType.Visible, "Missile (red Brinstar spike room)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(Super))
                }),
                new Location(this, 42, 0x7896E, LocationType.Chozo, "Spazer", Config.Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages() && items.Has(Super))
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Config.Logic switch {
                Casual =>
                    (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph) ||
                    items.CanAccessNorfairUpperPortal() && (items.Has(Ice) || items.Has(HiJump) || items.Has(SpaceJump)),
                _ =>
                    (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph) ||
                    items.CanAccessNorfairUpperPortal() && (items.Has(Ice) || items.CanSpringBallJump() || items.Has(HiJump) || items.CanFly())
            };
        }

    }

}
