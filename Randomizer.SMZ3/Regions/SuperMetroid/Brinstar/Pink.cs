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
                    Normal => items => items.CardBrinstarBoss && items.CanPassBombPassages() && items.Super,
                    _ => new Requirement(items => (items.CardBrinstarBoss || items.CardBrinstarL2) && items.CanPassBombPassages() && items.Super)
                }),
                new Location(this, 21, 0x8F8608, LocationType.Visible, "Missile (pink Brinstar top)", Logic switch {
                    Normal => items => items.CanFly() || items.Grapple,
                    _ => new Requirement(items => true),
                }),
                new Location(this, 22, 0x8F860E, LocationType.Visible, "Missile (pink Brinstar bottom)"),
                new Location(this, 23, 0x8F8614, LocationType.Chozo, "Charge Beam", Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages())
                }),
                new Location(this, 24, 0x8F865C, LocationType.Visible, "Power Bomb (pink Brinstar)", Logic switch {
                    Hard => items => items.CanUsePowerBombs() && items.Super,
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Super && items.HasEnergyReserves(1)),
                }),
                new Location(this, 25, 0x8F8676, LocationType.Visible, "Missile (green Brinstar pipe)", Logic switch {
                    Normal => items => items.Morph && (items.PowerBomb || items.Super ||
                        (items.CanAccessNorfairUpperPortal() && items.CanPassWaveGates(World) && items.SpaceJump || items.Ice)),
                    _ => new Requirement(items => items.Morph && (items.PowerBomb || items.Super ||
                        (items.CanAccessNorfairUpperPortal() && items.CanPassWaveGates(World) && items.HiJump || items.CanFly() || items.Ice)
                    )),
                }),
                new Location(this, 33, 0x8F87FA, LocationType.Visible, "Energy Tank, Waterway", Logic switch {
                    Normal => items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && items.SpeedBooster && items.Gravity,
                    Medium => items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && items.SpeedBooster &&
                        (items.Gravity || (items.HasEnergyReserves(1) && items.HiJump)),
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && items.SpeedBooster &&
                        (items.HasEnergyReserves(1) || items.Gravity)),
                }),
                new Location(this, 35, 0x8F8824, LocationType.Visible, "Energy Tank, Brinstar Gate", Logic switch {
                    Normal => items => items.CardBrinstarL2 && items.CanUsePowerBombs() && items.Wave && items.HasEnergyReserves(1),
                    Medium => items => items.CardBrinstarL2 && items.CanUsePowerBombs() && (items.Wave || items.Super) && items.HasEnergyReserves(1),
                    _ => new Requirement(items => items.CardBrinstarL2 && items.CanUsePowerBombs() && (items.Wave || items.Super))
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Normal =>
                    items.CanOpenRedDoors() && items.CanDestroyBombWalls() || items.CanUsePowerBombs() ||
                    items.CanAccessNorfairUpperPortal() && items.Morph && items.CanPassWaveGates(World) &&
                        (items.Ice || items.HiJump || items.SpaceJump),
                Medium =>
                    items.CanOpenRedDoors() && (items.CanDestroyBombWalls() || items.SpeedBooster) ||
                    items.CanUsePowerBombs() ||
                    items.CanAccessNorfairUpperPortal() && items.Morph && items.CanPassWaveGates(World) &&
                        (items.Ice || items.HiJump || items.CanFly()),
                _ =>
                    items.CanOpenRedDoors() && (items.CanDestroyBombWalls() || items.SpeedBooster) ||
                    items.CanUsePowerBombs() ||
                    items.CanAccessNorfairUpperPortal() && items.Morph && items.CanPassWaveGates(World) &&
                        (items.Ice || items.HiJump || items.CanSpringBallJump() || items.CanFly())
            };
        }

    }

}
