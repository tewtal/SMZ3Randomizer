using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;

namespace Randomizer.SuperMetroid.Regions.NorfairLower {

    class West : Region {

        public override string Name => "Norfair Lower West";
        public override string Area => "Norfair Lower";

        public West(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 70, "Missile (Gold Torizo)", LocationType.Visible, 0x78E6E, Logic switch {
                    Casual => items => items.CanUsePowerBombs() && items.Has(SpaceJump) && items.Has(Super),
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(SpaceJump) && items.Has(Varia) &&
                        (items.Has(HiJump) || items.Has(Gravity)))
                }),
                new Location(this, 71, "Super Missile (Gold Torizo)", LocationType.Hidden, 0x78E74, Logic switch {
                    Casual => new Requirement(items => items.CanDestroyBombWalls() && (items.Has(Super) || items.Has(Charge)) &&
                        items.Has(SpaceJump) && items.CanUsePowerBombs()),
                    _ => new Requirement(items => items.CanDestroyBombWalls() && items.Has(Varia) && (items.Has(Super) || items.Has(Charge)))
                }),
                new Location(this, 79, "Screw Attack", LocationType.Chozo, 0x79110, Logic switch {
                    Casual => items => items.CanDestroyBombWalls() && items.Has(SpaceJump) && items.CanUsePowerBombs(),
                    _ => new Requirement(items => items.CanDestroyBombWalls() && items.Has(Varia))
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Logic switch {
                Casual =>
                    items.Has(Varia) && World.CanEnter("Norfair Upper East", items) && items.CanUsePowerBombs() &&
                    items.Has(SpaceJump) && items.Has(Gravity),
                _ =>
                    items.Has(Varia) && World.CanEnter("Norfair Upper East", items) && items.CanUsePowerBombs() &&
                    (items.Has(HiJump) || items.Has(Gravity))
            };
        }

    }

}
