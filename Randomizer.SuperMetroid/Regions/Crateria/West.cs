using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Difficulty;

namespace Randomizer.SuperMetroid.Regions.Crateria {

    class West : Region {

        public override string Name => "West Crateria";
        public override string Area => "Crateria";

        public West(World world, Difficulty difficulty) : base(world, difficulty) {
            Locations = new List<Location> {
                new Location(this, "Energy Tank, Terminator", LocationType.Visible, 0x78432),
                new Location(this, "Energy Tank, Gauntlet", LocationType.Visible, 0x78264, Difficulty switch {
                    Casual  => new Requirement(items => CanEnterAndLeaveGauntlet(items) && items.HasEnergyReserves(1)),
                    _       => new Requirement(items => CanEnterAndLeaveGauntlet(items))
                }),
                new Location(this, "Missile (Crateria gauntlet right)", LocationType.Visible, 0x78464, Difficulty switch {
                    Casual  => new Requirement(items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages() && items.HasEnergyReserves(2)),
                    _       => new Requirement(items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages())
                }),
                new Location(this, "Missile (Crateria gauntlet left)", LocationType.Visible, 0x7846A, Difficulty switch {
                    Casual  => new Requirement(items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages() && items.HasEnergyReserves(2)),
                    _       => new Requirement(items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages())
                })
            };
        }

        public override bool CanEnter(List<Item> items) {
            return (items.CanDestroyBombWalls() || items.Has(SpeedBooster));
        }

        private bool CanEnterAndLeaveGauntlet(List<Item> items) {
            return Difficulty switch {
                Casual =>
                    items.Has(Morph) && (items.CanFly() || items.Has(SpeedBooster)) && (
                        items.CanIbj() ||
                        items.CanUsePowerBombs() && items.Has(PowerBomb, 2) ||
                        items.Has(ScrewAttack)),

                _ =>
                    items.Has(Morph) && (items.Has(Bombs) || items.Has(PowerBomb, 2)) ||
                    items.Has(ScrewAttack) ||
                    items.Has(SpeedBooster) && items.CanUsePowerBombs() && items.HasEnergyReserves(2)
            };
        }

    }

}
