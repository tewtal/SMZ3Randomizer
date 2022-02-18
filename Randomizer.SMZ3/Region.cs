using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static Randomizer.SMZ3.WorldState;

namespace Randomizer.SMZ3 {

    [Flags]
    enum RewardType {
        [Description("None")]
        None,
        [Description("Agahnim")]
        Agahnim = 1 << 0,
        [Description("Green Pendant")]
        PendantGreen = 1 << 1,
        [Description("Blue/Red Pendant")]
        PendantNonGreen = 1 << 2,
        [Description("Blue Crystal")]
        CrystalBlue = 1 << 3,
        [Description("Red Crystal")]
        CrystalRed = 1 << 4,
        [Description("Kraid Boss Token")]
        BossTokenKraid = 1 << 5,
        [Description("Phantoon Boss Token")]
        BossTokenPhantoon = 1 << 6,
        [Description("Draygon Boss Token")]
        BossTokenDraygon = 1 << 7,
        [Description("Ridley Boss Token")]
        BossTokenRidley = 1 << 8,

        AnyPendant = PendantGreen | PendantNonGreen,
        AnyCrystal = CrystalBlue | CrystalRed,
        AnyBossToken = BossTokenKraid | BossTokenPhantoon
            | BossTokenDraygon | BossTokenRidley,
    }

    interface IReward {
        RewardType Reward { get; set; }
        bool CanComplete(Progression items);
    }

    interface IMedallionAccess {
        Medallion Medallion { get; set; }
    }

    abstract class SMRegion : Region {
        public SMLogic Logic => Config.SMLogic;
        public SMRegion(World world, Config config) : base(world, config) { }
    }

    abstract class Z3Region : Region {
        public Z3Region(World world, Config config)
            : base(world, config) { }
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
