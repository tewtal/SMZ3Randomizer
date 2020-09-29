using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Crateria {

    class Central : SMRegion {

        public override string Name => "Crateria Central";
        public override string Area => "Crateria";

        public Central(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 0, 0x8F81CC, LocationType.Visible, "Power Bomb (Crateria surface)", Logic switch {
                    _ => new Requirement(items => (config.Keysanity ? items.CardCrateriaL1 : items.CanUsePowerBombs()) && (items.SpeedBooster || items.CanFly()))
                }),
                new Location(this, 12, 0x8F8486, LocationType.Visible, "Missile (Crateria middle)", Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages())
                }),
                new Location(this, 6, 0x8F83EE, LocationType.Visible, "Missile (Crateria bottom)", Logic switch {
                    _ => new Requirement(items => items.CanDestroyBombWalls())
                }),
                new Location(this, 11, 0x8F8478, LocationType.Visible, "Super Missile (Crateria)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.HasEnergyReserves(2) && items.SpeedBooster)
                }),
                new Location(this, 7, 0x8F8404, LocationType.Chozo, "Bombs", Logic switch {
                    Normal => items => (config.Keysanity ? items.CardCrateriaBoss : items.CanOpenRedDoors()) && items.CanPassBombPassages(),
                    _ => new Requirement(items => (config.Keysanity ? items.CardCrateriaBoss : items.CanOpenRedDoors()) && items.Morph)
                })
            };
        }

    }

}
