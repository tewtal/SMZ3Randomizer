using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Crateria {

    class Central : SMRegion {

        public override string Name => "Crateria Central";
        public override string Area => "Crateria";

        public Central(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 0, 0xC781CC, LocationType.Visible, "Power Bomb (Crateria surface)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.SpeedBooster && items.CanFly())
                }),
                new Location(this, 12, 0xC78486, LocationType.Visible, "Missile (Crateria middle)", Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages())
                }),
                new Location(this, 6, 0xC783EE, LocationType.Visible, "Missile (Crateria bottom)", Logic switch {
                    _ => new Requirement(items => items.CanDestroyBombWalls())
                }),
                new Location(this, 11, 0xC78478, LocationType.Visible, "Super Missile (Crateria)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.HasEnergyReserves(2) && items.SpeedBooster)
                }),
                new Location(this, 7, 0xC78404, LocationType.Chozo, "Bombs", Logic switch {
                    Casual => items => items.CanPassBombPassages() && items.CanOpenRedDoors(),
                    _ => new Requirement(items => items.Morph && items.CanOpenRedDoors())
                })
            };
        }

    }

}
