using System.Collections.Generic;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Blue : SMRegion {

        public override string Name => "Brinstar Blue";
        public override string Area => "Brinstar";

        public Blue(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 26, 0xC786EC, LocationType.Visible, "Morphing Ball"),
                new Location(this, 27, 0xC7874C, LocationType.Visible, "Power Bomb (blue Brinstar)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 28, 0xC78798, LocationType.Visible, "Missile (blue Brinstar middle)", Config.Logic switch {
                    _ => new Requirement(items => items.Morph)
                }),
                new Location(this, 29, 0xC7879E, LocationType.Hidden, "Energy Tank, Brinstar Ceiling", Config.Logic switch {
                    Casual => items => items.CanFly() || items.HiJump ||items.SpeedBooster || items.Ice,
                    _ => new Requirement(items => true)
                }),
                new Location(this, 34, 0xC78802, LocationType.Chozo, "Missile (blue Brinstar bottom)", Config.Logic switch {
                    _ => new Requirement(items => items.Morph)
                }),
                new Location(this, 36, 0xC78836, LocationType.Visible, "Missile (blue Brinstar top)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 37, 0xC7883C, LocationType.Hidden, "Missile (blue Brinstar behind missile)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
            };
        }

    }

}
