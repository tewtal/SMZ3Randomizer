using System.Collections.Generic;

namespace Randomizer.SMZ3 {

    enum RewardType {
        None,
        Agahnim,
        PendantGreen,
        PendantNonGreen,
        CrystalBlue,
        CrystalRed,
        GoldenFourBoss
    }

    interface Reward {
        RewardType Reward { get; set; }
        bool CanComplete(List<Item> items);
    }

    interface MedallionAccess {
        ItemType Medallion { get; set; }
    }

    abstract class Region {

        public virtual string Name { get; }
        public virtual string Area { get; }
        public List<Location> Locations { get; set; }
        public World World { get; set; }

        protected Logic Logic { get; set; }
        protected IList<ItemType> RegionItems { get; set; } = new List<ItemType>();

        public Region(World world, Logic logic) {
            Logic = logic;
            World = world;
        }

        public bool CanFill(Item item) {
            return /*config.keysanity*/false || !item.IsDungeonItem || RegionItems.Contains(item.Type);
        }

        public virtual bool CanEnter(List<Item> items) {
            return true;
        }

    }

}
