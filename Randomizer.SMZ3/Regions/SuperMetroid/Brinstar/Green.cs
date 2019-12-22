using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Green : SMRegion {

        public override string Name => "Brinstar Green";
        public override string Area => "Brinstar";

        public Green(World world, Config config) : base(world, config) {
            Weight = -6;

            Locations = new List<Location> {
                new Location(this, 13, 0xC784AC, LocationType.Chozo, "Power Bomb (green Brinstar bottom)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 15, 0xC78518, LocationType.Visible, "Missile (green Brinstar below super missile)", Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages() && items.CanOpenRedDoors())
                }),
                new Location(this, 16, 0xC7851E, LocationType.Visible, "Super Missile (green Brinstar top)", Logic switch {
                    Normal => items => items.CanOpenRedDoors() && items.SpeedBooster,
                    _ => new Requirement(items => items.CanOpenRedDoors() && (items.Morph || items.SpeedBooster))
                }),
                new Location(this, 17, 0xC7852C, LocationType.Chozo, "Reserve Tank, Brinstar", Logic switch {
                    Normal => items => items.CanOpenRedDoors() && items.SpeedBooster,
                    _ => new Requirement(items => items.CanOpenRedDoors() && (items.Morph || items.SpeedBooster))
                }),
                new Location(this, 18, 0xC78532, LocationType.Hidden, "Missile (green Brinstar behind missile)", Logic switch {
                    Normal => items => items.SpeedBooster && items.CanPassBombPassages() && items.CanOpenRedDoors(),
                    _ => new Requirement(items => (items.CanPassBombPassages() || items.Morph && items.ScrewAttack) && items.CanOpenRedDoors())
                }),
                new Location(this, 19, 0xC78538, LocationType.Visible, "Missile (green Brinstar behind reserve tank)", Logic switch {
                    Normal => items => items.SpeedBooster && items.CanOpenRedDoors() && items.Morph,
                    _ => new Requirement(items => items.CanOpenRedDoors() && items.Morph)
                }),
                new Location(this, 30, 0xC787C2, LocationType.Visible, "Energy Tank, Etecoons", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 31, 0xC787D0, LocationType.Visible, "Super Missile (green Brinstar bottom)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Super)
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.CanDestroyBombWalls() || items.SpeedBooster;
        }

    }

}
