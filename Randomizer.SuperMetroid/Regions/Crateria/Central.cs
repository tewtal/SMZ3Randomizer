using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;

namespace Randomizer.SuperMetroid.Regions.Crateria {

    class Central : Region {

        public override string Name => "Crateria Central";
        public override string Area => "Crateria";

        public Central(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, "Power Bomb (Crateria surface)", LocationType.Visible, 0x781CC, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(SpeedBooster) && items.CanFly())
                }),
                new Location(this, "Missile (Crateria middle)", LocationType.Visible, 0x78486, Logic switch {
                    _ => new Requirement(items => true)
                }),
                new Location(this, "Missile (Crateria bottom)", LocationType.Visible, 0x783EE, Logic switch {
                    _ => new Requirement(items => items.CanDestroyBombWalls())
                }),
                new Location(this, "Super Missile (Crateria)", LocationType.Visible, 0x78468, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.HasEnergyReserves(2) && items.Has(SpeedBooster))
                }),
                new Location(this, "Bombs", LocationType.Chozo, 0x78404, Logic switch {
                    Casual => items => items.CanPassBombPassages() && items.CanOpenRedDoors(),
                    _ => new Requirement(items => items.Has(Morph) && items.CanOpenRedDoors())
                })
            };
        }

    }

}
