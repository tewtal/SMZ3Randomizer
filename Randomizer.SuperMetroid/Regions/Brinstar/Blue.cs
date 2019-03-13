using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Difficulty;

namespace Randomizer.SuperMetroid.Regions.Brinstar {

    class Blue : Region {

        public override string Name => "Blue Brinstar";
        public override string Area => "Brinstar";

        public Blue(World world, Difficulty difficulty) : base(world, difficulty) {
            Locations = new List<Location> {
                new Location(this, "Morphing Ball", LocationType.Visible, 0x786EC),
                new Location(this, "Power Bomb (blue Brinstar)", LocationType.Visible, 0x7874C, Difficulty switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, "Missile (blue Brinstar middle)", LocationType.Visible, 0x78798, Difficulty switch {
                    _ => new Requirement(items => items.Has(Morph))
                }),
                new Location(this, "Energy Tank, Brinstar Ceiling", LocationType.Hidden, 0x7879E, Difficulty switch {
                    Casual => items => items.CanFly() || items.Has(HiJump) ||items.Has(SpeedBooster) || items.Has(IceBeam),
                    _ => new Requirement(items => true)
                }),
                new Location(this, "Missile (blue Brinstar bottom)", LocationType.Chozo, 0x78802, Difficulty switch {
                    _ => new Requirement(items => items.Has(Morph))
                }),
                new Location(this, "Missile (blue Brinstar top)", LocationType.Visible, 0x78836, Difficulty switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, "Missile (blue Brinstar behind missile)", LocationType.Hidden, 0x7883C, Difficulty switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
            };
        }
    }
}
