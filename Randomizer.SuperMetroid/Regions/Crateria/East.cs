using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Difficulty;

namespace Randomizer.SuperMetroid.Regions.Crateria {

    class East : Region {

        public override string Name => "East Crateria";
        public override string Area => "Crateria";

        public East(World world, Difficulty difficulty) : base(world, difficulty) {
            Locations = new List<Location> {
                new Location(this, "Missile (outside Wrecked Ship bottom)", LocationType.Visible, 0x781E8, Difficulty switch {
                    Casual => items => items.Has(SpaceJump) || items.Has(SpeedBooster) || items.Has(Grapple),
                    _ => new Requirement(items => true)
                }),
                new Location(this, "Missile (outside Wrecked Ship top)", LocationType.Hidden, 0x781EE, Difficulty switch {
                    Casual => items => items.Has(Super) && items.CanPassBombPassages() && (items.Has(SpaceJump) || items.Has(SpeedBooster) || items.Has(Grapple)),
                    _ => new Requirement(items => items.Has(Super) && items.CanPassBombPassages())
                }),
                new Location(this, "Missile (outside Wrecked Ship middle)", LocationType.Visible, 0x781F4, Difficulty switch {
                    Casual => items => items.Has(Super) && items.CanPassBombPassages() && (items.Has(SpaceJump) || items.Has(SpeedBooster) || items.Has(Grapple)),
                    _ => new Requirement(items => items.Has(Super) && items.CanPassBombPassages())
                }),
                new Location(this, "Missile (Crateria moat)", LocationType.Visible, 0x78248, Difficulty switch {
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
