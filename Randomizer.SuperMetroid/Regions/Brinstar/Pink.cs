using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;
using static Randomizer.SuperMetroid.LocationType;
using static Randomizer.SuperMetroid.ItemClass;

namespace Randomizer.SuperMetroid.Regions.Brinstar {

    class Pink : Region {

        public override string Name => "Brinstar Pink";
        public override string Area => "Brinstar";

        public Pink(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 14, "Super Missile (pink Brinstar)", Chozo, Minor, 0x784E4, Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages() && items.Has(Super))
                }),
                new Location(this, 21, "Missile (pink Brinstar top)", Visible, Minor, 0x78608),
                new Location(this, 22, "Missile (pink Brinstar bottom)", Visible, Minor, 0x7860E),
                new Location(this, 23, "Charge Beam", Chozo, Major, 0x78614, Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages())
                }),
                new Location(this, 24, "Power Bomb (pink Brinstar)", Visible, Minor, 0x7865C, Logic switch {
                    Casual => items => items.CanUsePowerBombs() && items.Has(Super) && items.HasEnergyReserves(1),
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(Super))
                }),
                new Location(this, 25, "Missile (green Brinstar pipe)", Visible, Minor, 0x78676, Logic switch {
                    _ => new Requirement(items => items.Has(Morph) && (items.Has(PowerBomb) || items.Has(Super)))
                }),
                new Location(this, 33, "Energy Tank, Waterway", Visible, Major, 0x787FA, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && items.Has(SpeedBooster) &&
                        (items.HasEnergyReserves(1) || items.Has(Gravity)))
                }),
                new Location(this, 35, "Energy Tank, Brinstar Gate", Visible, Major, 0x78824, Logic switch {
                    Casual => items => items.CanUsePowerBombs() && items.Has(Wave) && items.HasEnergyReserves(1),
                    _ => new Requirement(items => items.CanUsePowerBombs() && (items.Has(Wave) || items.Has(Super)))
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return items.CanOpenRedDoors() && (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) || items.CanUsePowerBombs();
        }

    }

}
