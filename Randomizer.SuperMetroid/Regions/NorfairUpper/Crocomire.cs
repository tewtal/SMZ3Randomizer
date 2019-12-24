using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;
using static Randomizer.SuperMetroid.LocationType;
using static Randomizer.SuperMetroid.ItemClass;

namespace Randomizer.SuperMetroid.Regions.NorfairUpper {

    class Crocomire : Region {

        public override string Name => "Norfair Upper Crocomire";
        public override string Area => "Norfair Upper";

        public Crocomire(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 52, "Energy Tank, Crocomire", Visible, Major, 0x78BA4, Logic switch {
                    Casual => items => items.HasEnergyReserves(1) || items.Has(SpaceJump) || items.Has(Grapple),
                    _ => new Requirement(items => true)
                }),
                new Location(this, 54, "Missile (above Crocomire)", Visible, Minor, 0x78BC0, Logic switch {
                    Casual => items => items.CanFly() || items.Has(Grapple) || items.Has(HiJump) && items.Has(SpeedBooster),
                    _ => new Requirement(items => (items.CanFly() || items.Has(Grapple) || items.Has(HiJump) &&
                        (items.Has(SpeedBooster) || items.CanSpringBallJump() || items.Has(Varia) && items.Has(Ice))) && items.CanHellRun())
                }),
                new Location(this, 57, "Power Bomb (Crocomire)", Visible, Minor, 0x78C04, Logic switch {
                    Casual => items => items.CanFly() || items.Has(HiJump) || items.Has(Grapple),
                    _ => new Requirement(items => true)
                }),
                new Location(this, 58, "Missile (below Crocomire)", Visible, Minor, 0x78C14, Logic switch {
                    _ => new Requirement(items => items.Has(Morph))
                }),
                new Location(this, 59, "Missile (Grapple Beam)", Visible, Minor, 0x78C2A, Logic switch {
                    Casual => items => items.Has(Morph) && (items.CanFly() || items.Has(SpeedBooster) && items.CanUsePowerBombs()),
                    _ => new Requirement(items => items.Has(SpeedBooster) || items.Has(Morph) && (items.CanFly() || items.Has(Grapple)))
                }),
                new Location(this, 60, "Grapple Beam", Chozo, Major, 0x78C36, Logic switch {
                    Casual => items => items.Has(Morph) && (items.CanFly() || items.Has(SpeedBooster) && items.CanUsePowerBombs()),
                    _ => new Requirement(items => items.Has(SpaceJump) || items.Has(Morph) || items.Has(Grapple) ||
                        items.Has(HiJump) && items.Has(SpeedBooster))
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Logic switch {
                Casual => 
                    (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph) &&
                    items.Has(Varia) && items.Has(Super) && (
                        items.CanUsePowerBombs() && items.Has(SpeedBooster) ||
                        items.Has(SpeedBooster) && items.Has(Wave) ||
                        items.Has(Morph) && (items.CanFly() || items.Has(HiJump)) && items.Has(Wave)
                    ),
                _ =>
                    (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph) &&
                    items.Has(Super) &&
                    (items.HasEnergyReserves(2) && items.Has(SpeedBooster) || items.CanHellRun()) &&
                    (items.CanFly() || items.Has(HiJump) || items.CanSpringBallJump() || items.Has(Varia) && items.Has(Ice) || items.Has(SpeedBooster)) &&
                    (items.CanPassBombPassages() || items.Has(SpeedBooster) || items.Has(Varia) && items.Has(Morph))
            };
        }

    }

}
