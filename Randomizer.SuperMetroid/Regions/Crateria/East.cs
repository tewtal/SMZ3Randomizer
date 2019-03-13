using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Difficulty;

namespace Randomizer.SuperMetroid.Regions.Crateria {

    class East : Region {

        public override string Name => "East Crateria";
        public override string Area => "Crateria";

        public East(World world, Difficulty difficulty) : base(world, difficulty) {
            Locations = new List<Location> {

            };
        }

        public override bool CanEnter(List<Item> items) {
            return base.CanEnter(items);
        }

    }

}
