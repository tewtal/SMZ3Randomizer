using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;
using static Randomizer.SuperMetroid.LocationType;
using static Randomizer.SuperMetroid.ItemClass;

namespace Randomizer.SuperMetroid.Regions.NorfairUpper {

    class West : Region {

        public override string Name => "Norfair Upper West";
        public override string Area => "Norfair Upper";

        public West(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 50, "Ice Beam", Chozo, Major, 0x78B24, Logic switch {
                    Casual => items => items.Has(Super) && items.CanPassBombPassages() && items.Has(Varia) && items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Super) && items.Has(Morph) && (items.Has(Varia) || items.HasEnergyReserves(3)))
                }),
                new Location(this, 51, "Missile (below Ice Beam)", Hidden, Minor, 0x78B46, Logic switch {
                    Casual => items => items.Has(Super) && items.CanUsePowerBombs() && items.Has(Varia) && items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Super) && items.CanUsePowerBombs() && (items.Has(Varia) || items.HasEnergyReserves(3)) ||
                        items.Has(Varia) && items.Has(SpeedBooster) && items.Has(Super))
                }),
                new Location(this, 53, "Hi-Jump Boots", Chozo, Major, 0x78BAC, Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors() && items.CanPassBombPassages())
                }),
                new Location(this, 55, "Missile (Hi-Jump Boots)", Visible, Minor, 0x78BE6, Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors())
                }),
                new Location(this, 56, "Energy Tank (Hi-Jump Boots)", Visible, Minor, 0x78BEC, Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors())
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph);
        }

    }

}
