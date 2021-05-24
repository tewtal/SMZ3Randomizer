using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Pink : SMRegion {

        public override string Name => "Brinstar Pink";
        public override string Area => "Brinstar";

        public Pink(World world, Config config) : base(world, config) {
            Weight = -4;

            Locations = new List<Location> {
                new Location(this, 14, 0x8F84E4, LocationType.Chozo, "Super Missile (pink Brinstar)", Logic switch {
                    Normal => new Requirement(items => items.CardBrinstarBoss && items.CanPassBombPassages() && items.Super),
                    _ => new Requirement(items => (items.CardBrinstarBoss || items.CardBrinstarL2) && items.CanPassBombPassages() && items.Super)
                }),
                new Location(this, 21, 0x8F8608, LocationType.Visible, "Missile (pink Brinstar top)"),
                new Location(this, 22, 0x8F860E, LocationType.Visible, "Missile (pink Brinstar bottom)"),
                new Location(this, 23, 0x8F8614, LocationType.Chozo, "Charge Beam", Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages())
                }),
                new Location(this, 24, 0x8F865C, LocationType.Visible, "Power Bomb (pink Brinstar)", Logic switch {
                    Normal => items => items.CanUsePowerBombs() && items.Super && items.HasEnergyReserves(1),
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Super)
                }),
                new Location(this, 25, 0x8F8676, LocationType.Visible, "Missile (green Brinstar pipe)", Logic switch {
                    _ => new Requirement(items => items.Morph &&
                        (items.PowerBomb || items.Super || items.CanAccessNorfairUpperPortal()))
                }),
                new Location(this, 33, 0x8F87FA, LocationType.Visible, "Energy Tank, Waterway", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && items.SpeedBooster &&
                        (items.HasEnergyReserves(1) || items.Gravity))
                }),
                new Location(this, 35, 0x8F8824, LocationType.Visible, "Energy Tank, Brinstar Gate", Logic switch {
                    Normal => items => items.CardBrinstarL2 && items.CanUsePowerBombs() && items.Wave && items.HasEnergyReserves(1),
                    _ => new Requirement(items => items.CardBrinstarL2 && items.CanUsePowerBombs() && (items.Wave || items.Super))
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Normal =>
                    items.CanOpenRedDoors() && (items.CanDestroyBombWalls() || items.SpeedBooster) ||
                    items.CanUsePowerBombs() ||
                    items.CanAccessNorfairUpperPortal() && items.Morph && items.Wave &&
                        (items.Ice || items.HiJump || items.SpaceJump),
                _ =>
                    items.CanOpenRedDoors() && (items.CanDestroyBombWalls() || items.SpeedBooster) ||
                    items.CanUsePowerBombs() ||
                    items.CanAccessNorfairUpperPortal() && items.Morph && (items.Missile || items.Super || items.Wave /* Blue Gate */) &&
                        (items.Ice || items.HiJump || items.CanSpringBallJump() || items.CanFly())
            };
        }

    }

}
