using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Blue : Region {

        public override string Name => "Brinstar Blue";
        public override string Area => "Brinstar";

        public Blue(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 26, 0x786EC, LocationType.Visible, "Morphing Ball"),
                new Location(this, 27, 0x7874C, LocationType.Visible, "Power Bomb (blue Brinstar)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 28, 0x78798, LocationType.Visible, "Missile (blue Brinstar middle)", Logic switch {
                    _ => new Requirement(items => items.Has(Morph))
                }),
                new Location(this, 29, 0x7879E, LocationType.Hidden, "Energy Tank, Brinstar Ceiling", Logic switch {
                    Casual => items => items.CanFly() || items.Has(HiJump) ||items.Has(SpeedBooster) || items.Has(Ice),
                    _ => new Requirement(items => true)
                }),
                new Location(this, 34, 0x78802, LocationType.Chozo, "Missile (blue Brinstar bottom)", Logic switch {
                    _ => new Requirement(items => items.Has(Morph))
                }),
                new Location(this, 36, 0x78836, LocationType.Visible, "Missile (blue Brinstar top)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 37, 0x7883C, LocationType.Hidden, "Missile (blue Brinstar behind missile)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
            };
        }

    }

}
