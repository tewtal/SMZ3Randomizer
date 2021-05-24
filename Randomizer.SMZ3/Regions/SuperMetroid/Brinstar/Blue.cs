using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Blue : SMRegion {

        public override string Name => "Brinstar Blue";
        public override string Area => "Brinstar";

        public Blue(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 26, 0x8F86EC, LocationType.Visible, "Morphing Ball"),
                new Location(this, 27, 0x8F874C, LocationType.Visible, "Power Bomb (blue Brinstar)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 28, 0x8F8798, LocationType.Visible, "Missile (blue Brinstar middle)", Logic switch {
                    _ => new Requirement(items => items.CardBrinstarL1 && items.Morph)
                }),
                new Location(this, 29, 0x8F879E, LocationType.Hidden, "Energy Tank, Brinstar Ceiling", Logic switch {
                    Normal => items => items.CardBrinstarL1 && (items.CanFly() || items.HiJump || items.SpeedBooster || items.Ice),
                    _ => new Requirement(items => items.CardBrinstarL1)
                }),
                new Location(this, 34, 0x8F8802, LocationType.Chozo, "Missile (blue Brinstar bottom)", Logic switch {
                    _ => new Requirement(items => items.Morph)
                }),
                new Location(this, 36, 0x8F8836, LocationType.Visible, "Missile (blue Brinstar top)", Logic switch {
                    _ => new Requirement(items => items.CardBrinstarL1 && items.CanUsePowerBombs())
                }),
                new Location(this, 37, 0x8F883C, LocationType.Hidden, "Missile (blue Brinstar behind missile)", Logic switch {
                    _ => new Requirement(items => items.CardBrinstarL1 && items.CanUsePowerBombs())
                }),
            };
        }

    }

}
