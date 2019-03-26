using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;

namespace Randomizer.SuperMetroid.Regions.NorfairLower {

    class East : Region {

        public override string Name => "Norfair Lower East";
        public override string Area => "Norfair Lower";

        public East(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 73, "Missile (Mickey Mouse room)", LocationType.Visible, 0x78F30, Logic switch {
                    Casual => new Requirement(items => true),
                    _ => items => items.Has(Morph) && items.CanDestroyBombWalls()
                }),
                new Location(this, 74, "Missile (lower Norfair above fire flea room)", LocationType.Visible, 0x78FCA),
                new Location(this, 75, "Power Bomb (lower Norfair above fire flea room)", LocationType.Visible, 0x78FD2, Logic switch {
                    Casual => new Requirement(items => true),
                    _ => items => items.CanPassBombPassages()
                }),
                new Location(this, 76, "Power Bomb (Power Bombs of shame)", LocationType.Visible, 0x790C0, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 77, "Missile (lower Norfair near Wave Beam)", LocationType.Visible, 0x79100, Logic switch {
                    Casual => new Requirement(items => true),
                    _ => items => items.Has(Morph) && items.CanDestroyBombWalls()
                }),
                new Location(this, 78, "Energy Tank, Ridley", LocationType.Hidden, 0x79108, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(Super))
                }),
                new Location(this, 80, "Energy Tank, Firefleas", LocationType.Visible, 0x79184)
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Logic switch {
                Casual =>
                    items.Has(Varia) && World.CanEnter("Norfair Upper East", items) && items.CanUsePowerBombs() &&
                    items.Has(SpaceJump) && items.Has(Gravity),
                _ =>
                    items.Has(Varia) && World.CanEnter("Norfair Upper East", items) && items.CanUsePowerBombs() &&
                    (items.Has(HiJump) || items.Has(Gravity)) &&
                    (items.CanFly() || items.Has(HiJump) || items.CanSpringBallJump() || items.Has(Ice) && items.Has(Charge)) &&
                    (items.CanPassBombPassages() || items.Has(ScrewAttack) && items.Has(SpaceJump)) &&
                    (items.Has(Morph) || items.HasEnergyReserves(5))
            };
        }

    }

}
