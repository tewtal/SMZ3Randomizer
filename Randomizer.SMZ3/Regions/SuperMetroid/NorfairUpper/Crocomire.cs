using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairUpper {

    class Crocomire : Region {

        public override string Name => "Norfair Upper Crocomire";
        public override string Area => "Norfair Upper";

        public Crocomire(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 52, 0x78BA4, LocationType.Visible, "Energy Tank, Crocomire", Logic switch {
                    Casual => items => items.HasEnergyReserves(1) || items.Has(SpaceJump) || items.Has(Grapple),
                    _ => new Requirement(items => true)
                }),
                new Location(this, 54, 0x78BC0, LocationType.Visible, "Missile (above Crocomire)", Logic switch {
                    Casual => items => items.CanFly() || items.Has(Grapple) || items.Has(HiJump) && items.Has(SpeedBooster),
                    _ => new Requirement(items => (items.CanFly() || items.Has(Grapple) || items.Has(HiJump) &&
                        (items.Has(SpeedBooster) || items.CanSpringBallJump() || items.Has(Varia) && items.Has(Ice))) && items.CanHellRun())
                }),
                new Location(this, 57, 0x78C04, LocationType.Visible, "Power Bomb (Crocomire)", Logic switch {
                    Casual => items => items.CanFly() || items.Has(HiJump) || items.Has(Grapple),
                    _ => new Requirement(items => true)
                }),
                new Location(this, 58, 0x78C14, LocationType.Visible, "Missile (below Crocomire)", Logic switch {
                    _ => new Requirement(items => items.Has(Morph))
                }),
                new Location(this, 59, 0x78C2A, LocationType.Visible, "Missile (Grapple Beam)", Logic switch {
                    Casual => items => items.Has(Morph) && (items.CanFly() || items.Has(SpeedBooster) && items.CanUsePowerBombs()),
                    _ => new Requirement(items => items.Has(SpeedBooster) || items.Has(Morph) && (items.CanFly() || items.Has(Grapple)))
                }),
                new Location(this, 60, 0x78C36, LocationType.Chozo, "Grapple Beam", Logic switch {
                    Casual => items => items.Has(Morph) && (items.CanFly() || items.Has(SpeedBooster) && items.CanUsePowerBombs()),
                    _ => new Requirement(items => items.Has(SpaceJump) || items.Has(Morph) || items.Has(Grapple) ||
                        items.Has(HiJump) && items.Has(SpeedBooster))
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Logic switch {
                Casual => (
                        (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph) ||
                        items.CanAccessNorfairUpperPortal()
                    ) &&
                    items.Has(Varia) && items.Has(Super) && (
                        items.CanUsePowerBombs() && items.Has(SpeedBooster) ||
                        items.Has(SpeedBooster) && items.Has(Wave) ||
                        items.Has(Morph) && (items.CanFly() || items.Has(HiJump)) && items.Has(Wave)
                    ),
                _ => (
                        (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph) ||
                        items.CanAccessNorfairUpperPortal()
                    ) &&
                    items.Has(Super) &&
                    (items.HasEnergyReserves(2) && items.Has(SpeedBooster) || items.CanHellRun()) &&
                    (items.CanFly() || items.Has(HiJump) || items.CanSpringBallJump() || items.Has(Varia) && items.Has(Ice) || items.Has(SpeedBooster)) &&
                    (items.CanPassBombPassages() || items.Has(SpeedBooster) || items.Has(Varia) && items.Has(Morph))
                    || items.CanAccessNorfairLowerPortal() && items.Has(ScrewAttack) && items.Has(SpaceJump) && items.Has(Varia) && items.Has(Super) && items.HasEnergyReserves(2)
            };
        }

    }

}
