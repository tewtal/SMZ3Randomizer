using System.Collections.Generic;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairUpper {

    class East : SMRegion {

        public override string Name => "Norfair Upper East";
        public override string Area => "Norfair Upper";

        public East(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 49, 0xC78AE4, LocationType.Hidden, "Missile (lava room)", Config.Logic switch {
                    _ => new Requirement(items => items.Morph)
                }),
                new Location(this, 61, 0xC78C3E, LocationType.Chozo, "Reserve Tank, Norfair", Config.Logic switch {
                    Casual => items => items.Morph && (
                        items.CanFly() || items.Grapple && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice),
                    _ => new Requirement(items => items.Morph && items.Super)
                }),
                new Location(this, 62, 0xC78C44, LocationType.Hidden, "Missile (Norfair Reserve Tank)", Config.Logic switch {
                    Casual => items => items.Morph && (
                        items.CanFly() || items.Grapple && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice),
                    _ => new Requirement(items => items.Morph && items.Super)
                }),
                new Location(this, 63, 0xC78C52, LocationType.Visible, "Missile (bubble Norfair green door)", Config.Logic switch {
                    Casual => items => items.CanFly() ||
                        items.Grapple && items.Morph && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice,
                    _ => new Requirement(items => items.Super)
                }),
                new Location(this, 64, 0xC78C66, LocationType.Visible, "Missile (bubble Norfair)"),
                new Location(this, 65, 0xC78C74, LocationType.Hidden, "Missile (Speed Booster)", Config.Logic switch {
                    Casual => items => items.CanFly() || items.Morph && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice,
                    _ => new Requirement(items => items.Super)
                }),
                new Location(this, 66, 0xC78C82, LocationType.Chozo, "Speed Booster", Config.Logic switch {
                    Casual => items => items.CanFly() || items.Morph && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice,
                    _ => new Requirement(items => items.Super)
                }),
                new Location(this, 67, 0xC78CBC, LocationType.Visible, "Missile (Wave Beam)", Config.Logic switch {
                    Casual => items => items.CanFly() || items.Morph && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice,
                    _ => new Requirement(items => true)
                }),
                new Location(this, 68, 0xC78CCA, LocationType.Chozo, "Wave Beam", Config.Logic switch {
                    Casual => items => items.Morph && (
                        items.CanFly() || items.Morph && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice),
                    _ => new Requirement(items => items.CanOpenRedDoors() &&
                        (items.Morph || items.Grapple || items.HiJump && items.Varia || items.SpaceJump))
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return Config.Logic switch {
                Casual => (
                        (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                        items.CanAccessNorfairUpperPortal()
                    ) &&
                    items.Varia && items.Super &&
                    (items.CanFly() || items.HiJump || items.SpeedBooster),
                _ => (
                        // Norfair Main Street Access
                        (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                        items.CanAccessNorfairUpperPortal()
                    ) &&
                    // Hell Run and Green Door
                    items.CanHellRun() && (
                        items.Super && (
                            // Cathedral Route
                            items.CanFly() || items.HiJump || items.SpeedBooster || items.CanSpringBallJump() ||
                            items.Varia && (items.Ice || items.SpeedBooster)
                        ) ||
                        // Speedway route
                        items.SpeedBooster && items.CanUsePowerBombs())
            };
        }

    }

}
