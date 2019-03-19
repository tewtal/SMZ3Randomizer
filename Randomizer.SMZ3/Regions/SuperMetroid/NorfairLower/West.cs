using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairLower {

    class West : Region {

        public override string Name => "Norfair Lower West";
        public override string Area => "Norfair Lower";

        public West(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 70, 0x78E6E, LocationType.Visible, "Missile (Gold Torizo)", Logic switch {
                    Casual => items => items.CanUsePowerBombs() && items.Has(SpaceJump) && items.Has(Super),
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(SpaceJump) && items.Has(Varia) && (
                        items.Has(HiJump) || items.Has(Gravity) ||
                        items.CanAccessNorfairLowerPortal() && (items.CanFly() || items.CanSpringBallJump() || items.Has(SpeedBooster)) && items.Has(Super)))
                }),
                new Location(this, 71, 0x78E74, LocationType.Hidden, "Super Missile (Gold Torizo)", Logic switch {
                    Casual => items => items.CanDestroyBombWalls() && (items.Has(Super) || items.Has(Charge)) &&
                        (items.CanAccessNorfairLowerPortal() || items.Has(SpaceJump) && items.CanUsePowerBombs()),
                    _ => new Requirement(items => items.CanDestroyBombWalls() && items.Has(Varia) && (items.Has(Super) || items.Has(Charge)))
                }),
                new Location(this, 79, 0x79110, LocationType.Chozo, "Screw Attack", Logic switch {
                    Casual => items => items.CanDestroyBombWalls() && (items.Has(SpaceJump) && items.CanUsePowerBombs() || items.CanAccessNorfairLowerPortal()),
                    _ => new Requirement(items => items.CanDestroyBombWalls() && (items.Has(Varia) || items.CanAccessNorfairLowerPortal()))
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Logic switch
            {
                Casual =>
                    items.Has(Varia) && (
                        World.CanEnter("Norfair Upper East", items) && items.CanUsePowerBombs() && items.Has(SpaceJump) && items.Has(Gravity) ||
                        items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls()),
                _ =>
                    World.CanEnter("Norfair Upper East", items) && items.CanUsePowerBombs() && items.Has(Varia) && (items.Has(HiJump) || items.Has(Gravity)) ||
                    items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls()
            };
        }

    }

}
