using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Crateria {

    class East : Region {

        public override string Name => "Crateria East";
        public override string Area => "Crateria";

        public East(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 1, 0x781E8, LocationType.Visible, "Missile (outside Wrecked Ship bottom)", Config.Logic switch {
                    Casual => items => items.Has(SpaceJump) || items.Has(SpeedBooster) || items.Has(Grapple),
                    _ => new Requirement(items => true)
                }),
                new Location(this, 2, 0x781EE, LocationType.Hidden, "Missile (outside Wrecked Ship top)", Config.Logic switch {
                    Casual => items => items.Has(Super) && items.CanPassBombPassages() && (items.Has(SpaceJump) || items.Has(SpeedBooster) || items.Has(Grapple)),
                    _ => new Requirement(items => items.Has(Super) && items.CanPassBombPassages())
                }),
                new Location(this, 3, 0x781F4, LocationType.Visible, "Missile (outside Wrecked Ship middle)", Config.Logic switch {
                    Casual => items => items.Has(Super) && items.CanPassBombPassages() && (items.Has(SpaceJump) || items.Has(SpeedBooster) || items.Has(Grapple)),
                    _ => new Requirement(items => items.Has(Super) && items.CanPassBombPassages())
                }),
                new Location(this, 4, 0x78248, LocationType.Visible, "Missile (Crateria moat)", Config.Logic switch {
                    Casual => items => items.Has(SpaceJump) || items.Has(SpeedBooster) || items.Has(Grapple),
                    _ => new Requirement(items => true)
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return items.CanUsePowerBombs() && items.Has(Super);
        }

    }

}
