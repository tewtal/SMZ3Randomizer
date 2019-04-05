using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Crateria {

    class Central : Region {

        public override string Name => "Crateria Central";
        public override string Area => "Crateria";

        public Central(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 0, 0x781CC, LocationType.Visible, "Power Bomb (Crateria surface)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(SpeedBooster) && items.CanFly())
                }),
                new Location(this, 12, 0x78486, LocationType.Visible, "Missile (Crateria middle)", Config.Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages())
                }),
                new Location(this, 6, 0x783EE, LocationType.Visible, "Missile (Crateria bottom)", Config.Logic switch {
                    _ => new Requirement(items => items.CanDestroyBombWalls())
                }),
                new Location(this, 11, 0x78478, LocationType.Visible, "Super Missile (Crateria)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.HasEnergyReserves(2) && items.Has(SpeedBooster))
                }),
                new Location(this, 7, 0x78404, LocationType.Chozo, "Bombs", Config.Logic switch {
                    Casual => items => items.CanPassBombPassages() && items.CanOpenRedDoors(),
                    _ => new Requirement(items => items.Has(Morph) && items.CanOpenRedDoors())
                })
            };
        }

    }

}
