using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairUpper {

    class East : SMRegion {

        public override string Name => "Norfair Upper East";
        public override string Area => "Norfair Upper";

        public East(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 61, 0x8F8C3E, LocationType.Chozo, "Reserve Tank, Norfair", Logic switch {
                    Normal => items => items.CardNorfairL2 && items.Morph && (
                        items.CanFly() ||
                        items.Grapple && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice
                    ),
                    _ => new Requirement(items => items.CardNorfairL2 && items.Morph && items.Super)
                }),
                new Location(this, 62, 0x8F8C44, LocationType.Hidden, "Missile (Norfair Reserve Tank)", Logic switch {
                    Normal => items => items.CardNorfairL2 && items.Morph && (
                        items.CanFly() ||
                        items.Grapple && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice
                    ),
                    _ => new Requirement(items => items.CardNorfairL2 && items.Morph && items.Super)
                }),
                new Location(this, 63, 0x8F8C52, LocationType.Visible, "Missile (bubble Norfair green door)", Logic switch {
                    Normal => items => items.CardNorfairL2 && (
                        items.CanFly() ||
                        items.Grapple && items.Morph && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice
                    ),
                    _ => new Requirement(items => items.CardNorfairL2 && items.Super)
                }),
                new Location(this, 64, 0x8F8C66, LocationType.Visible, "Missile (bubble Norfair)", Logic switch {
                    _ => new Requirement(items => items.CardNorfairL2)
                }),
                new Location(this, 65, 0x8F8C74, LocationType.Hidden, "Missile (Speed Booster)", Logic switch {
                    Normal => items => items.CardNorfairL2 && (
                        items.CanFly() ||
                        items.Morph && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice
                    ),
                    _ => new Requirement(items => items.CardNorfairL2 && items.Super)
                }),
                new Location(this, 66, 0x8F8C82, LocationType.Chozo, "Speed Booster", Logic switch {
                    Normal => items => items.CardNorfairL2 && (
                        items.CanFly() ||
                        items.Morph && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice
                    ),
                    _ => new Requirement(items => items.CardNorfairL2 && items.Super)
                }),
                new Location(this, 67, 0x8F8CBC, LocationType.Visible, "Missile (Wave Beam)", Logic switch {
                    Normal => items => items.CardNorfairL2 && (
                        items.CanFly() ||
                        items.Morph && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice
                    ) ||
                    items.SpeedBooster && items.Wave && items.Morph && items.Super,
                    _ => new Requirement(items => items.CardNorfairL2 || items.Varia)
                }),
                new Location(this, 68, 0x8F8CCA, LocationType.Chozo, "Wave Beam", Logic switch {
                    Normal => items => items.Morph && (
                        items.CardNorfairL2 && (
                            items.CanFly() ||
                            items.Morph && (items.SpeedBooster || items.CanPassBombPassages()) ||
                            items.HiJump || items.Ice
                        ) ||
                        items.SpeedBooster && items.Wave && items.Morph && items.Super
                    ),
                    _ => new Requirement(items => items.CanOpenRedDoors() && (items.CardNorfairL2 || items.Varia) &&
                        (items.Morph || items.Grapple || items.HiJump && items.Varia || items.SpaceJump))
                }),
            };
        }

        // Todo: Super is not actually needed for Frog Speedway, but changing this will affect locations
        // Todo: Ice Beam -> Croc Speedway is not considered
        public override bool CanEnter(Progression items) {
            return Logic switch {
                Normal => (
                        (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                        items.CanAccessNorfairUpperPortal()
                    ) && items.Varia && items.Super && (
                        /* Cathedral */
                        items.CanOpenRedDoors() && (Config.Keysanity ? items.CardNorfairL2 : items.Super) &&
                            (items.CanFly() || items.HiJump || items.SpeedBooster) ||
                        /* Frog Speedway */
                        items.SpeedBooster && (items.CardNorfairL2 || items.Wave /* Blue Gate */) && items.CanUsePowerBombs()
                    ),
                _ => (
                        (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                        items.CanAccessNorfairUpperPortal()
                    ) &&
                    items.CanHellRun() && (
                        /* Cathedral */
                        items.CanOpenRedDoors() && (Config.Keysanity ? items.CardNorfairL2 : items.Super) && (
                            items.CanFly() || items.HiJump || items.SpeedBooster ||
                            items.CanSpringBallJump() || items.Varia && items.Ice
                        ) ||
                        /* Frog Speedway */
                        items.SpeedBooster && (items.CardNorfairL2 || items.Missile || items.Super || items.Wave /* Blue Gate */) && items.CanUsePowerBombs()
                    ),
            };
        }

    }

}
