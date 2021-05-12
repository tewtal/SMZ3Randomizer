using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Randomizer.SMZ3 {

    enum RewardType {
        [Description("None")]
        None,
        [Description("Agahnim")]
        Agahnim,
        [Description("Green Pendant")]
        PendantGreen,
        [Description("Blue/Red Pendant")]
        PendantNonGreen,
        [Description("Blue Crystal")]
        CrystalBlue,
        [Description("Red Crystal")]
        CrystalRed,
        [Description("Golden Four Boss")]
        GoldenFourBoss
    }

    interface IReward {
        RewardType Reward { get; set; }
        bool CanComplete(Progression items);
    }

    interface IMedallionAccess {
        ItemType Medallion { get; set; }
    }

    abstract class SMRegion : Region {
        public SMLogic Logic => Config.SMLogic;
        public SMRegion(World world, Config config) : base(world, config) { }
    }

    abstract class Z3Region : Region {
        public Z3Logic Logic => Config.Z3Logic;
        public Z3Region(World world, Config config) : base(world, config) { }
    }

    abstract class Region {

        public virtual string Name { get; }
        public virtual string Area => Name;

        public List<Location> Locations { get; set; }
        public World World { get; set; }
        public int Weight { get; set; } = 0;

        protected Config Config { get; set; }
        protected IList<ItemType> RegionItems { get; set; } = new List<ItemType>();

        private Dictionary<string, Location> locationLookup { get; set; }
        public Location GetLocation(string name) => locationLookup[name];

        public Region(World world, Config config) {
            Config = config;
            World = world;
            locationLookup = new Dictionary<string, Location>();
        }

        public void GenerateLocationLookup() {
            locationLookup = Locations.ToDictionary(l => l.Name, l => l);
        }

        public bool IsRegionItem(Item item) {
            return RegionItems.Contains(item.Type);
        }

        public virtual bool CanFill(Item item, Progression items) {
            return Config.Keysanity || !item.IsDungeonItem || IsRegionItem(item);
        }

        public virtual bool CanEnter(Progression items) {
            return true;
        }

    }

}
