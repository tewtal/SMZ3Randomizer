using System.Collections.Generic;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Red : Region {

        public override string Name => "Brinstar Red";
        public override string Area => "Brinstar";

        public Red(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 38, 0x78876, LocationType.Chozo, "X-Ray Scope", Config.Logic switch {
                    Casual => items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && (items.Grapple || items.SpaceJump),
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && (
                        items.Grapple || items.SpaceJump ||
                        (items.CanIbj() || items.HiJump && items.SpeedBooster || items.CanSpringBallJump()) &&
                            (items.Varia && items.HasEnergyReserves(3) || items.HasEnergyReserves(5))))
                }),
                new Location(this, 39, 0x788CA, LocationType.Visible, "Power Bomb (red Brinstar sidehopper room)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Super)
                }),
                new Location(this, 40, 0x7890E, LocationType.Chozo, "Power Bomb (red Brinstar spike room)", Config.Logic switch {
                    Casual => items => (items.CanUsePowerBombs() || items.Ice) && items.Super,
                    _ => new Requirement(items => items.Super)
                }),
                new Location(this, 41, 0x78914, LocationType.Visible, "Missile (red Brinstar spike room)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Super)
                }),
                new Location(this, 42, 0x7896E, LocationType.Chozo, "Spazer", Config.Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages() && items.Super)
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return Config.Logic switch {
                Casual =>
                    (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                    items.CanAccessNorfairUpperPortal() && (items.Ice || items.HiJump || items.SpaceJump),
                _ =>
                    (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                    items.CanAccessNorfairUpperPortal() && (items.Ice || items.CanSpringBallJump() || items.HiJump || items.CanFly())
            };
        }

    }

}
