using System.Collections.Generic;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Crateria {

    class West : SMRegion {

        public override string Name => "Crateria West";
        public override string Area => "Crateria";

        public West(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 8, 0x78432, LocationType.Visible, "Energy Tank, Terminator"),
                new Location(this, 5, 0x78264, LocationType.Visible, "Energy Tank, Gauntlet", Config.Logic switch {
                    Casual => items => CanEnterAndLeaveGauntlet(items) && items.HasEnergyReserves(1),
                    _ => new Requirement(items => CanEnterAndLeaveGauntlet(items))
                }),
                new Location(this, 9, 0x78464, LocationType.Visible, "Missile (Crateria gauntlet right)", Config.Logic switch {
                    Casual => items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages() && items.HasEnergyReserves(2),
                    _ => new Requirement(items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages())
                }),
                new Location(this, 10, 0x7846A, LocationType.Visible, "Missile (Crateria gauntlet left)", Config.Logic switch {
                    Casual => items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages() && items.HasEnergyReserves(2),
                    _ => new Requirement(items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages())
                })
            };
        }

        public override bool CanEnter(Progression items) {
            return items.CanDestroyBombWalls() || items.SpeedBooster;
        }

        private bool CanEnterAndLeaveGauntlet(Progression items) {
            return Config.Logic switch {
                Casual =>
                    items.Morph && (items.CanFly() || items.SpeedBooster) && (
                        items.CanIbj() ||
                        items.CanUsePowerBombs() && items.TwoPowerBombs ||
                        items.ScrewAttack),
                _ =>
                    items.Morph && (items.Bombs || items.TwoPowerBombs) ||
                    items.ScrewAttack ||
                    items.SpeedBooster && items.CanUsePowerBombs() && items.HasEnergyReserves(2)
            };
        }

    }

}
