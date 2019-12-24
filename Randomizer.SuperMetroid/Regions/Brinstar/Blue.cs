using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;
using static Randomizer.SuperMetroid.LocationType;
using static Randomizer.SuperMetroid.ItemClass;

namespace Randomizer.SuperMetroid.Regions.Brinstar {

    class Blue : Region {

        public override string Name => "Blue Brinstar";
        public override string Area => "Brinstar";

        public Blue(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 26, "Morphing Ball", Visible, Major, 0x786DE),
                new Location(this, 27, "Power Bomb (blue Brinstar)", Visible, Minor, 0x7874C, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 28, "Missile (blue Brinstar middle)", Visible, Minor, 0x78798, Logic switch {
                    _ => new Requirement(items => items.Has(Morph))
                }),
                new Location(this, 29, "Energy Tank, Brinstar Ceiling", Hidden, Major, 0x7879E, Logic switch {
                    Casual => items => items.CanFly() || items.Has(HiJump) ||items.Has(SpeedBooster) || items.Has(Ice),
                    _ => new Requirement(items => true)
                }),
                new Location(this, 34, "Missile (blue Brinstar bottom)", Chozo, Minor, 0x78802, Logic switch {
                    _ => new Requirement(items => items.Has(Morph))
                }),
                new Location(this, 36, "Missile (blue Brinstar top)", Visible, Minor, 0x78836, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 37, "Missile (blue Brinstar behind missile)", Hidden, Minor, 0x7883C, Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
            };
        }
    }
}
