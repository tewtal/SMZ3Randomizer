using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;
using static Randomizer.SuperMetroid.LocationType;
using static Randomizer.SuperMetroid.ItemClass;


namespace Randomizer.SuperMetroid.Regions.Brinstar {

    class Green : Region {

        public override string Name => "Brinstar Green";
        public override string Area => "Brinstar";

        public Green(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 13, "Power Bomb (green Brinstar bottom)", Chozo, Minor, 0x784AC, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 15, "Missile (green Brinstar below super missile)", Visible, Minor, 0x78518, Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages() && items.CanOpenRedDoors())
                }),
                new Location(this, 16, "Super Missile (green Brinstar top)", Visible, Minor, 0x7851E, Logic switch {
                    Casual => items => items.CanOpenRedDoors() && items.Has(SpeedBooster),
                    _ => new Requirement(items => items.CanOpenRedDoors() && (items.Has(Morph) || items.Has(SpeedBooster)))
                }),
                new Location(this, 17, "Reserve Tank, Brinstar", Chozo, Major, 0x7852C, Logic switch {
                    Casual => items => items.CanOpenRedDoors() && items.Has(SpeedBooster),
                    _ => new Requirement(items => items.CanOpenRedDoors() && (items.Has(Morph) || items.Has(SpeedBooster)))
                }),
                new Location(this, 18, "Missile (green Brinstar behind missile)", Hidden, Minor, 0x78532, Logic switch {
                    Casual => items => items.Has(SpeedBooster) && items.CanPassBombPassages() && items.CanOpenRedDoors(),
                    _ => new Requirement(items => (items.CanPassBombPassages() || items.Has(Morph) && items.Has(ScrewAttack)) &&
                        items.CanOpenRedDoors())
                }),
                new Location(this, 19, "Missile (green Brinstar behind reserve tank)", Visible, Minor, 0x78538, Logic switch {
                    Casual => items => items.Has(SpeedBooster) && items.CanOpenRedDoors() && items.Has(Morph),
                    _ => new Requirement(items => items.CanOpenRedDoors() && items.Has(Morph))
                }),
                new Location(this, 30, "Energy Tank, Etecoons", Visible, Major, 0x787C2, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 31, "Super Missile (green Brinstar bottom)", Visible, Minor, 0x787D0, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(Super))
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return items.CanDestroyBombWalls() || items.Has(SpeedBooster);
        }

    }

}
