using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairUpper {

    class Crocomire : SMRegion {

        public override string Name => "Norfair Upper Crocomire";
        public override string Area => "Norfair Upper";

        public Crocomire(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 52, 0xC78BA4, LocationType.Visible, "Energy Tank, Crocomire", Logic switch {
                    Normal => items => items.HasEnergyReserves(1) || items.SpaceJump || items.Grapple,
                    _ => new Requirement(items => true)
                }),
                new Location(this, 54, 0xC78BC0, LocationType.Visible, "Missile (above Crocomire)", Logic switch {
                    Normal => items => items.CanFly() || items.Grapple || items.HiJump && items.SpeedBooster,
                    _ => new Requirement(items => (items.CanFly() || items.Grapple || items.HiJump &&
                        (items.SpeedBooster || items.CanSpringBallJump() || items.Varia && items.Ice)) && items.CanHellRun())
                }),
                new Location(this, 57, 0xC78C04, LocationType.Visible, "Power Bomb (Crocomire)", Logic switch {
                    Normal => items => items.CanFly() || items.HiJump || items.Grapple,
                    _ => new Requirement(items => true)
                }),
                new Location(this, 58, 0xC78C14, LocationType.Visible, "Missile (below Crocomire)", Logic switch {
                    _ => new Requirement(items => items.Morph)
                }),
                new Location(this, 59, 0xC78C2A, LocationType.Visible, "Missile (Grapple Beam)", Logic switch {
                    Normal => items => items.Morph && (items.CanFly() || items.SpeedBooster && items.CanUsePowerBombs()),
                    _ => new Requirement(items => items.SpeedBooster || items.Morph && (items.CanFly() || items.Grapple))
                }),
                new Location(this, 60, 0xC78C36, LocationType.Chozo, "Grapple Beam", Logic switch {
                    Normal => items => items.Morph && (items.CanFly() || items.SpeedBooster && items.CanUsePowerBombs()),
                    _ => new Requirement(items => items.SpaceJump || items.Morph || items.Grapple ||
                        items.HiJump && items.SpeedBooster)
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Normal => (
                        (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                        items.CanAccessNorfairUpperPortal()
                    ) &&
                    items.Varia && items.Super && (
                        items.CanUsePowerBombs() && items.SpeedBooster ||
                        items.SpeedBooster && items.Wave ||
                        items.Morph && (items.CanFly() || items.HiJump) && items.Wave
                    ),
                _ => (
                        (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                        items.CanAccessNorfairUpperPortal()
                    ) &&
                    items.Super &&
                    (items.HasEnergyReserves(2) && items.SpeedBooster || items.CanHellRun()) &&
                    (items.CanFly() || items.HiJump || items.CanSpringBallJump() || items.Varia && items.Ice || items.SpeedBooster) &&
                    (items.CanPassBombPassages() || items.SpeedBooster || items.Varia && items.Morph)
                    || items.CanAccessNorfairLowerPortal() && items.ScrewAttack && items.SpaceJump && items.Varia && items.Super && items.HasEnergyReserves(2)
            };
        }

    }

}
