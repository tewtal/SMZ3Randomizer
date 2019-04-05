using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Green : Region {

        public override string Name => "Brinstar Green";
        public override string Area => "Brinstar";

        public Green(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 13, 0x784AC, LocationType.Chozo, "Power Bomb (green Brinstar bottom)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 15, 0x78518, LocationType.Visible, "Missile (green Brinstar below super missile)", Config.Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages() && items.CanOpenRedDoors())
                }),
                new Location(this, 16, 0x7851E, LocationType.Visible, "Super Missile (green Brinstar top)", Config.Logic switch {
                    Casual => items => items.CanOpenRedDoors() && items.Has(SpeedBooster),
                    _ => new Requirement(items => items.CanOpenRedDoors() && (items.Has(Morph) || items.Has(SpeedBooster)))
                }),
                new Location(this, 17, 0x7852C, LocationType.Chozo, "Reserve Tank, Brinstar", Config.Logic switch {
                    Casual => items => items.CanOpenRedDoors() && items.Has(SpeedBooster),
                    _ => new Requirement(items => items.CanOpenRedDoors() && (items.Has(Morph) || items.Has(SpeedBooster)))
                }),
                new Location(this, 18, 0x78532, LocationType.Hidden, "Missile (green Brinstar behind missile)", Config.Logic switch {
                    Casual => items => items.Has(SpeedBooster) && items.CanPassBombPassages() && items.CanOpenRedDoors(),
                    _ => new Requirement(items => (items.CanPassBombPassages() || items.Has(Morph) && items.Has(ScrewAttack)) &&
                        items.CanOpenRedDoors())
                }),
                new Location(this, 19, 0x78538, LocationType.Visible, "Missile (green Brinstar behind reserve tank)", Config.Logic switch {
                    Casual => items => items.Has(SpeedBooster) && items.CanOpenRedDoors() && items.Has(Morph),
                    _ => new Requirement(items => items.CanOpenRedDoors() && items.Has(Morph))
                }),
                new Location(this, 30, 0x787C2, LocationType.Visible, "Energy Tank, Etecoons", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 31, 0x787D0, LocationType.Visible, "Super Missile (green Brinstar bottom)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(Super))
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return items.CanDestroyBombWalls() || items.Has(SpeedBooster);
        }

    }

}
