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
        bool CanComplete(Progression items);
    }

    interface MedallionAccess {
        ItemType Medallion { get; set; }
    }

    abstract class SMRegion : Region {
        public SMRegion(World world, Config config)
            : base(world, config) { }
    }

    abstract class Z3Region : Region {
        public Z3Region(World world, Config config)
            : base(world, config) { }
    }

    abstract class Region {

        public virtual string Name { get; }
        public virtual string Area { get; }
        public List<Location> Locations { get; set; }
        public World World { get; set; }

        protected Config Config { get; set; }
        protected IList<ItemType> RegionItems { get; set; } = new List<ItemType>();

        public Region(World world, Config config) {
            Config = config;
            World = world;
        }

        public virtual bool CanFill(Item item) {
            return Config.Keysanity || !item.IsDungeonItem || RegionItems.Contains(item.Type);
        }

        public virtual bool CanEnter(Progression items) {
            return true;
        }

    }

}
