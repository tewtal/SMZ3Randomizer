using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;

namespace Randomizer.SuperMetroid.Regions.NorfairUpper {

    class West : Region {

        public override string Name => "Norfair Upper West";
        public override string Area => "Norfair Upper";

        public West(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, "Ice Beam", LocationType.Chozo, 0x78B24, Logic switch {
                    Casual => items => items.Has(Super) && items.CanPassBombPassages() && items.Has(Varia) && items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Super) && items.Has(Morph) && (items.Has(Varia) || items.HasEnergyReserves(3)))
                }),
                new Location(this, "Missile (below Ice Beam)", LocationType.Hidden, 0x78B46, Logic switch {
                    Casual => items => items.Has(Super) && items.CanUsePowerBombs() && items.Has(Varia) && items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Super) && items.CanUsePowerBombs() && (items.Has(Varia) || items.HasEnergyReserves(3)) ||
                        items.Has(Varia) && items.Has(SpeedBooster) && items.Has(Super))
                }),
                new Location(this, "Hi-Jump Boots", LocationType.Chozo, 0x78BAC, Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors())
                }),
                new Location(this, "Missile (Hi-Jump Boots)", LocationType.Visible, 0x78BE6, Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors() && items.CanPassBombPassages())
                }),
                new Location(this, "Energy Tank (Hi-Jump Boots)", LocationType.Visible, 0x78BEC, Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors() && items.Has(Morph))
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph);
        }

    }

}
