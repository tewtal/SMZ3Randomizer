using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Pink : SMRegion {

        public override string Name => "Brinstar Pink";
        public override string Area => "Brinstar";

        public Pink(World world, Config config) : base(world, config) {
            Weight = -4;

            Locations = new List<Location> {
                new Location(this, 14, 0xC784E4, LocationType.Chozo, "Super Missile (pink Brinstar)", Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages() && items.Super)
                }),
                new Location(this, 21, 0xC78608, LocationType.Visible, "Missile (pink Brinstar top)"),
                new Location(this, 22, 0xC7860E, LocationType.Visible, "Missile (pink Brinstar bottom)"),
                new Location(this, 23, 0xC78614, LocationType.Chozo, "Charge Beam", Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages())
                }),
                new Location(this, 24, 0xC7865C, LocationType.Visible, "Power Bomb (pink Brinstar)", Logic switch {
                    Casual => items => items.CanUsePowerBombs() && items.Super && items.HasEnergyReserves(1),
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Super)
                }),
                new Location(this, 25, 0xC78676, LocationType.Visible, "Missile (green Brinstar pipe)", Logic switch {
                    _ => new Requirement(items => items.Morph &&
                        (items.PowerBomb || items.Super || items.CanAccessNorfairUpperPortal()))
                }),
                new Location(this, 33, 0xC787FA, LocationType.Visible, "Energy Tank, Waterway", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && items.SpeedBooster &&
                        (items.HasEnergyReserves(1) || items.Gravity))
                }),
                new Location(this, 35, 0xC78824, LocationType.Visible, "Energy Tank, Brinstar Gate", Logic switch {
                    Casual => items => items.CanUsePowerBombs() && items.Wave && items.HasEnergyReserves(1),
                    _ => new Requirement(items => items.CanUsePowerBombs() && (items.Wave || items.Super))
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Casual =>
                    items.CanOpenRedDoors() && (items.CanDestroyBombWalls() || items.SpeedBooster) ||
                    items.CanUsePowerBombs() ||
                    items.CanAccessNorfairUpperPortal() && items.Morph && items.Wave &&
                        (items.Ice || items.HiJump || items.SpaceJump),
                _ =>
                    items.CanOpenRedDoors() && (items.CanDestroyBombWalls() || items.SpeedBooster) ||
                    items.CanUsePowerBombs() ||
                    items.CanAccessNorfairUpperPortal() && items.Morph && (items.CanOpenRedDoors() || items.Wave) &&
                        (items.Ice || items.HiJump || items.CanSpringBallJump() || items.CanFly())
            };
        }

    }

}
