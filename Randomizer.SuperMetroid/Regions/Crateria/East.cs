using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;

namespace Randomizer.SuperMetroid.Regions.Crateria {

    class East : Region {

        public override string Name => "Crateria East";
        public override string Area => "Crateria";

        public East(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 1, "Missile (outside Wrecked Ship bottom)", LocationType.Visible, 0x781E8, Logic switch {
                    Casual => items => items.Has(SpaceJump) || items.Has(SpeedBooster) || items.Has(Grapple),
                    _ => new Requirement(items => true)
                }),
                new Location(this, 2, "Missile (outside Wrecked Ship top)", LocationType.Hidden, 0x781EE, Logic switch {
                    Casual => items => items.Has(Super) && items.CanPassBombPassages() && (items.Has(SpaceJump) || items.Has(SpeedBooster) || items.Has(Grapple)),
                    _ => new Requirement(items => items.Has(Super) && items.CanPassBombPassages())
                }),
                new Location(this, 3, "Missile (outside Wrecked Ship middle)", LocationType.Visible, 0x781F4, Logic switch {
                    Casual => items => items.Has(Super) && items.CanPassBombPassages() && (items.Has(SpaceJump) || items.Has(SpeedBooster) || items.Has(Grapple)),
                    _ => new Requirement(items => items.Has(Super) && items.CanPassBombPassages())
                }),
                new Location(this, 4, "Missile (Crateria moat)", LocationType.Visible, 0x78248, Logic switch {
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
