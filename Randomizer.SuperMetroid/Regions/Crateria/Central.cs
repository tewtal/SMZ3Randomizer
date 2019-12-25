using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;
using static Randomizer.SuperMetroid.LocationType;
using static Randomizer.SuperMetroid.ItemClass;

namespace Randomizer.SuperMetroid.Regions.Crateria {

    class Central : Region {

        public override string Name => "Crateria Central";
        public override string Area => "Crateria";

        public Central(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 0, "Power Bomb (Crateria surface)", Visible, Minor, 0x781CC, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && (items.Has(SpeedBooster) || items.CanFly()))
                }),
                new Location(this, 12, "Missile (Crateria middle)", Visible, Minor, 0x78486, Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages())
                }),
                new Location(this, 6, "Missile (Crateria bottom)", Visible, Minor, 0x783EE, Logic switch {
                    _ => new Requirement(items => items.CanDestroyBombWalls())
                }),
                new Location(this, 11, "Super Missile (Crateria)", Visible, Minor, 0x78478, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.HasEnergyReserves(2) && items.Has(SpeedBooster))
                }),
                new Location(this, 7, "Bombs", Chozo, Major, 0x78404, Logic switch {
                    Casual => items => items.CanPassBombPassages() && items.CanOpenRedDoors(),
                    _ => new Requirement(items => items.Has(Morph) && items.CanOpenRedDoors())
                })
            };
        }

    }

}
