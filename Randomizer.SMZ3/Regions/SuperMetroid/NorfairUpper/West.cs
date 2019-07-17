using System.Collections.Generic;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairUpper {

    class West : SMRegion {

        public override string Name => "Norfair Upper West";
        public override string Area => "Norfair Upper";

        public West(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 50, 0xC78B24, LocationType.Chozo, "Ice Beam", Config.Logic switch {
                    Casual => items => items.Super && items.CanPassBombPassages() && items.Varia && items.SpeedBooster,
                    _ => new Requirement(items => items.Super && items.Morph && (items.Varia || items.HasEnergyReserves(3)))
                }),
                new Location(this, 51, 0xC78B46, LocationType.Hidden, "Missile (below Ice Beam)", Config.Logic switch {
                    Casual => items => items.Super && items.CanUsePowerBombs() && items.Varia && items.SpeedBooster,
                    _ => new Requirement(items => items.Super && items.CanUsePowerBombs() && (items.Varia || items.HasEnergyReserves(3)) ||
                        items.Varia && items.SpeedBooster && items.Super)
                }),
                new Location(this, 53, 0xC78BAC, LocationType.Chozo, "Hi-Jump Boots", Config.Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors() && items.CanPassBombPassages())
                }),
                new Location(this, 55, 0xC78BE6, LocationType.Visible, "Missile (Hi-Jump Boots)", Config.Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors() && items.Morph)
                }),
                new Location(this, 56, 0xC78BEC, LocationType.Visible, "Energy Tank (Hi-Jump Boots)", Config.Logic switch {
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
