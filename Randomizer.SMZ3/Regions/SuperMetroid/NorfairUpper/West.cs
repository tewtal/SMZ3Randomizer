using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairUpper {

    class West : SMRegion {

        public override string Name => "Norfair Upper West";
        public override string Area => "Norfair Upper";

        public West(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 49, 0x8F8AE4, LocationType.Hidden, "Missile (lava room)", Logic switch {
                    Normal => items => items.Varia && (
                            items.CanOpenRedDoors() && (items.CanFly() || items.HiJump || items.SpeedBooster) ||
                            World.CanEnter("Norfair Upper East", items) && items.CardNorfairL2
                        ) && items.Morph,
                    _ => new Requirement(items => items.CanHellRun() && (
                            items.CanOpenRedDoors() && (
                                items.CanFly() || items.HiJump || items.SpeedBooster ||
                                items.CanSpringBallJump() || items.Varia && items.Ice
                            ) ||
                            World.CanEnter("Norfair Upper East", items) && items.CardNorfairL2
                        ) && items.Morph),
                }),
                new Location(this, 50, 0x8F8B24, LocationType.Chozo, "Ice Beam", Logic switch {
                    Normal => items => (config.Keysanity ? items.CardNorfairL1 : items.Super) && items.CanPassBombPassages() && items.Varia && items.SpeedBooster,
                    _ => new Requirement(items => (config.Keysanity ? items.CardNorfairL1 : items.Super) && items.Morph && (items.Varia || items.HasEnergyReserves(3)))
                }),
                new Location(this, 51, 0x8F8B46, LocationType.Hidden, "Missile (below Ice Beam)", Logic switch {
                    Normal => items => (config.Keysanity ? items.CardNorfairL1 : items.Super) && items.CanUsePowerBombs() && items.Varia && items.SpeedBooster,
                    _ => new Requirement(items =>
                        (config.Keysanity ? items.CardNorfairL1 : items.Super) && items.CanUsePowerBombs() && (items.Varia || items.HasEnergyReserves(3)) ||
                        (items.Missile || items.Super || items.Wave /* Blue Gate */) && items.Varia && items.SpeedBooster &&
                            /* Access to Croc's room to get spark */
                            (config.Keysanity ? items.CardNorfairBoss : items.Super) && items.CardNorfairL1)
                }),
                new Location(this, 53, 0x8F8BAC, LocationType.Chozo, "Hi-Jump Boots", Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors() && items.CanPassBombPassages())
                }),
                new Location(this, 55, 0x8F8BE6, LocationType.Visible, "Missile (Hi-Jump Boots)", Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors() && items.Morph)
                }),
                new Location(this, 56, 0x8F8BEC, LocationType.Visible, "Energy Tank (Hi-Jump Boots)", Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors())
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                items.CanAccessNorfairUpperPortal();
        }

    }

}
