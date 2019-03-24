using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;

namespace Randomizer.SuperMetroid.Regions.Maridia {

    class Outer : Region {

        public override string Name => "Maridia Outer";
        public override string Area => "Maridia";

        public Outer(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, "Missile (green Maridia shinespark)", LocationType.Visible, 0x7C437, Logic switch {
                    Casual => items => items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Gravity) && items.Has(SpeedBooster))
                }),
                new Location(this, "Super Missile (green Maridia)", LocationType.Visible, 0x7C43D),
                new Location(this, "Energy Tank, Mama turtle", LocationType.Visible, 0x7C47D, Logic switch {
                    Casual => items => items.CanFly() || items.Has(SpeedBooster) || items.Has(Grapple),
                    _ => new Requirement(items => items.CanFly() || items.Has(SpeedBooster) || items.Has(Grapple) ||
                        items.CanSpringBallJump() && (items.Has(Gravity) || items.Has(HiJump)))
                }),
                new Location(this, "Missile (green Maridia tatori)", LocationType.Hidden, 0x7C483),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Logic switch {
                Casual => 
                    World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() && items.Has(Gravity),
                _ =>
                    World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() &&
                    (items.Has(Gravity) || items.Has(HiJump) && (items.CanSpringBallJump() || items.Has(Ice)))
            };
        }

    }

}
