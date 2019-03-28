using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Pink : Region {

        public override string Name => "Brinstar Pink";
        public override string Area => "Brinstar";

        public Pink(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 14, 0x784E4, LocationType.Chozo, "Super Missile (pink Brinstar)", Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages() && items.Has(Super))
                }),
                new Location(this, 21, 0x78608, LocationType.Visible, "Missile (pink Brinstar top)"),
                new Location(this, 22, 0x7860E, LocationType.Visible, "Missile (pink Brinstar bottom)"),
                new Location(this, 23, 0x78614, LocationType.Chozo, "Charge Beam", Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages())
                }),
                new Location(this, 24, 0x7865C, LocationType.Visible, "Power Bomb (pink Brinstar)", Logic switch {
                    Casual => items => items.CanUsePowerBombs() && items.Has(Super) && items.HasEnergyReserves(1),
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(Super))
                }),
                new Location(this, 25, 0x78676, LocationType.Visible, "Missile (green Brinstar pipe)", Logic switch {
                    _ => new Requirement(items => items.Has(Morph) && (items.Has(PowerBomb) || items.Has(Super)))
                }),
                new Location(this, 33, 0x787FA, LocationType.Visible, "Energy Tank, Waterway", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && items.Has(SpeedBooster) &&
                        (items.HasEnergyReserves(1) || items.Has(Gravity)))
                }),
                new Location(this, 35, 0x78824, LocationType.Visible, "Energy Tank, Brinstar Gate", Logic switch {
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
