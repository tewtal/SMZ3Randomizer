using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Crateria {

    class East : SMRegion {

        public override string Name => "Crateria East";
        public override string Area => "Crateria";

        public East(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 1, 0xC781E8, LocationType.Visible, "Missile (outside Wrecked Ship bottom)", Logic switch {
                    Normal => items => items.SpaceJump || items.SpeedBooster || items.Grapple,
                    _ => new Requirement(items => true)
                }),
                new Location(this, 2, 0xC781EE, LocationType.Hidden, "Missile (outside Wrecked Ship top)", Logic switch {
                    Normal => items => items.Super && items.CanPassBombPassages() && (items.SpaceJump || items.SpeedBooster || items.Grapple),
                    _ => new Requirement(items => items.Super && items.CanPassBombPassages())
                }),
                new Location(this, 3, 0xC781F4, LocationType.Visible, "Missile (outside Wrecked Ship middle)", Logic switch {
                    Normal => items => items.Super && items.CanPassBombPassages() && (items.SpaceJump || items.SpeedBooster || items.Grapple),
                    _ => new Requirement(items => items.Super && items.CanPassBombPassages())
                }),
                new Location(this, 4, 0xC78248, LocationType.Visible, "Missile (Crateria moat)", Logic switch {
                    Normal => items => items.SpaceJump || items.SpeedBooster || items.Grapple,
                    _ => new Requirement(items => true)
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.CanUsePowerBombs() && items.Super;
        }

    }

}
