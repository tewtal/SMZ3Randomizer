using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;
using static Randomizer.SuperMetroid.LocationType;
using static Randomizer.SuperMetroid.ItemClass;

namespace Randomizer.SuperMetroid.Regions.Maridia {

    class Outer : Region {

        public override string Name => "Maridia Outer";
        public override string Area => "Maridia";

        public Outer(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 136, "Missile (green Maridia shinespark)", Visible, Minor, 0x7C437, Logic switch {
                    Casual => items => items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Gravity) && items.Has(SpeedBooster))
                }),
                new Location(this, 137, "Super Missile (green Maridia)", Visible, Minor, 0x7C43D),
                new Location(this, 138, "Energy Tank, Mama turtle", Visible, Major, 0x7C47D, Logic switch {
                    Casual => items => items.CanFly() || items.Has(SpeedBooster) || items.Has(Grapple),
                    _ => new Requirement(items => items.CanFly() || items.Has(SpeedBooster) || items.Has(Grapple) ||
                        items.CanSpringBallJump() && (items.Has(Gravity) || items.Has(HiJump)))
                }),
                new Location(this, 139, "Missile (green Maridia tatori)", Hidden, Minor, 0x7C483),
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
