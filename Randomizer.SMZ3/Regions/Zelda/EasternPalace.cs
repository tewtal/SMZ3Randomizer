using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class EasternPalace : Region {

        public override string Name => "Eastern Palace";
        public override string Area => "Eastern Palace";

        public EasternPalace(World world, Logic logic) : base(world, logic) {
            RegionItems = new[] { BigKeyEP, MapEP, CompassEP };

            Locations = new List<Location> {
                new Location(this, 256+103, 0xE9B3, LocationType.Regular, "Eastern Palace - Cannonball Chest"),
                new Location(this, 256+104, 0xE9F5, LocationType.Regular, "Eastern Palace - Map Chest"),
                new Location(this, 256+105, 0xE977, LocationType.Regular, "Eastern Palace - Compass Chest"),
                new Location(this, 256+106, 0xE97D, LocationType.Regular, "Eastern Palace - Big Chest",
                    items => items.Has(BigKeyEP)),
                new Location(this, 256+107, 0xE9B9, LocationType.Regular, "Eastern Palace - Big Key Chest",
                    items => items.Has(Lamp)),
                new Location(this, 256+108, 0x180150, LocationType.Regular, "Eastern Palace - Armos Knights",
                    items => items.Has(BigKeyEP) && items.Has(Bow) && items.Has(Lamp)),
            };
        }

    }

}
