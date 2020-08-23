using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairUpper {

    class Crocomire : SMRegion {

        public override string Name => "Norfair Upper Crocomire";
        public override string Area => "Norfair Upper";

        public Crocomire(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 52, 0x8F8BA4, LocationType.Visible, "Energy Tank, Crocomire", Logic switch {
                    Normal => items => items.CardNorfairBoss && (items.HasEnergyReserves(1) || items.SpaceJump || items.Grapple),
                    _ => new Requirement(items => items.CardNorfairBoss)
                }),
                new Location(this, 54, 0x8F8BC0, LocationType.Visible, "Missile (above Crocomire)", Logic switch {
                    Normal => items => items.CanFly() || items.Grapple || items.HiJump && items.SpeedBooster,
                    _ => new Requirement(items => (items.CanFly() || items.Grapple || items.HiJump &&
                        (items.SpeedBooster || items.CanSpringBallJump() || items.Varia && items.Ice)) && items.CanHellRun())
                }),
                new Location(this, 57, 0x8F8C04, LocationType.Visible, "Power Bomb (Crocomire)", Logic switch {
                    Normal => items => items.CardNorfairBoss && (items.CanFly() || items.HiJump || items.Grapple),
                    _ => new Requirement(items => items.CardNorfairBoss)
                }),
                new Location(this, 58, 0x8F8C14, LocationType.Visible, "Missile (below Crocomire)", Logic switch {
                    _ => new Requirement(items => items.CardNorfairBoss && items.Morph)
                }),
                new Location(this, 59, 0x8F8C2A, LocationType.Visible, "Missile (Grappling Beam)", Logic switch {
                    Normal => items => items.CardNorfairBoss && (items.Morph && (items.CanFly() || items.SpeedBooster && items.CanUsePowerBombs())),
                    _ => new Requirement(items => items.CardNorfairBoss && (items.SpeedBooster || items.Morph && (items.CanFly() || items.Grapple)))
                }),
                new Location(this, 60, 0x8F8C36, LocationType.Chozo, "Grappling Beam", Logic switch {
                    Normal => items => items.CardNorfairBoss && (items.Morph && (items.CanFly() || items.SpeedBooster && items.CanUsePowerBombs())),
                    _ => new Requirement(items => items.CardNorfairBoss && (items.SpaceJump || items.Morph || items.Grapple ||
                        items.HiJump && items.SpeedBooster))
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Normal => (
                        (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                        items.CanAccessNorfairUpperPortal()
                    ) &&
                    items.Varia && (
                        items.CardNorfairL1 && items.CanUsePowerBombs() && items.SpeedBooster ||
                        items.SpeedBooster && items.Wave ||
                        items.CardNorfairL2 && items.Morph && (items.CanFly() || items.HiJump) && (items.CanPassBombPassages() || items.Gravity) && items.Wave
                    ),
                _ => (
                        (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                        items.CanAccessNorfairUpperPortal()
                    ) &&
                    items.Super && (
                    ((items.HasEnergyReserves(2) && items.SpeedBooster || (items.CanHellRun() && items.CardNorfairL2)) &&
                    (items.CanFly() || items.HiJump || items.CanSpringBallJump() || items.Varia && items.Ice || items.SpeedBooster) &&
                    (items.CanPassBombPassages() || items.SpeedBooster || items.Varia && items.Morph))
                    || items.CanAccessNorfairLowerPortal() && items.ScrewAttack && items.SpaceJump && items.Varia && items.Super && items.HasEnergyReserves(2))
            };
        }

    }

}
