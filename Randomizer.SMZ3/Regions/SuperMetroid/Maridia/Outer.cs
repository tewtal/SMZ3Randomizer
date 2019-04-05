using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Maridia {

    class Outer : Region {

        public override string Name => "Maridia Outer";
        public override string Area => "Maridia";

        public Outer(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 136, 0x7C437, LocationType.Visible, "Missile (green Maridia shinespark)", Logic switch {
                    Casual => items => items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Gravity) && items.Has(SpeedBooster))
                }),
                new Location(this, 137, 0x7C43D, LocationType.Visible, "Super Missile (green Maridia)"),
                new Location(this, 138, 0x7C47D, LocationType.Visible, "Energy Tank, Mama turtle", Logic switch {
                    Casual => items => items.CanFly() || items.Has(SpeedBooster) || items.Has(Grapple),
                    _ => new Requirement(items => items.CanFly() || items.Has(SpeedBooster) || items.Has(Grapple) ||
                        items.CanSpringBallJump() && (items.Has(Gravity) || items.Has(HiJump)))
                }),
                new Location(this, 139, 0x7C483, LocationType.Hidden, "Missile (green Maridia tatori)"),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Logic switch {
                Casual => (
                        World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() ||
                        items.CanAccessMaridiaPortal(World)
                    ) && items.Has(Gravity),
                _ =>
                    World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() &&
                    (items.Has(Gravity) || items.Has(HiJump) && (items.CanSpringBallJump() || items.Has(Ice)))
                    || items.CanAccessMaridiaPortal(World)
            };
        }

    }

}
