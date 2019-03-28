using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairUpper {

    class West : Region {

        public override string Name => "Norfair Upper West";
        public override string Area => "Norfair Upper";

        public West(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 50, 0x78B24, LocationType.Chozo, "Ice Beam", Logic switch {
                    Casual => items => items.Has(Super) && items.CanPassBombPassages() && items.Has(Varia) && items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Super) && items.Has(Morph) && (items.Has(Varia) || items.HasEnergyReserves(3)))
                }),
                new Location(this, 51, 0x78B46, LocationType.Hidden, "Missile (below Ice Beam)", Logic switch {
                    Casual => items => items.Has(Super) && items.CanUsePowerBombs() && items.Has(Varia) && items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Super) && items.CanUsePowerBombs() && (items.Has(Varia) || items.HasEnergyReserves(3)) ||
                        items.Has(Varia) && items.Has(SpeedBooster) && items.Has(Super))
                }),
                new Location(this, 53, 0x78BAC, LocationType.Chozo, "Hi-Jump Boots", Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors() && items.CanPassBombPassages())
                }),
                new Location(this, 55, 0x78BE6, LocationType.Visible, "Missile (Hi-Jump Boots)", Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors() && items.Has(Morph))
                }),
                new Location(this, 56, 0x78BEC, LocationType.Visible, "Energy Tank (Hi-Jump Boots)", Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors())
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph);
        }

    }

}
