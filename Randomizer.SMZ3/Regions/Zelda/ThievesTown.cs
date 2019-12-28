using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class ThievesTown : Z3Region, IReward {

        public override string Name => "Thieves' Town";
        public override string Area => "Thieves' Town";

        public RewardType Reward { get; set; } = RewardType.None;

        public ThievesTown(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyTT, BigKeyTT, MapTT, CompassTT };

            Locations = new List<Location> {
                new Location(this, 256+153, 0x1EA01, LocationType.Regular, "Thieves' Town - Map Chest"),
                new Location(this, 256+154, 0x1EA0A, LocationType.Regular, "Thieves' Town - Ambush Chest"),
                new Location(this, 256+155, 0x1EA07, LocationType.Regular, "Thieves' Town - Compass Chest"),
                new Location(this, 256+156, 0x1EA04, LocationType.Regular, "Thieves' Town - Big Key Chest"),
                new Location(this, 256+157, 0x1EA0D, LocationType.Regular, "Thieves' Town - Attic",
                    items => items.BigKeyTT && items.KeyTT),
                new Location(this, 256+158, 0x1EA13, LocationType.Regular, "Thieves' Town - Blind's Cell",
                    items => items.BigKeyTT),
                new Location(this, 256+159, 0x1EA10, LocationType.Regular, "Thieves' Town - Big Chest",
                    items => items.BigKeyTT && items.Hammer &&
                        (Locations.Get("Thieves' Town - Big Chest").ItemType == KeyTT || items.KeyTT))
                    .AlwaysAllow((item, items) => item.Type == KeyTT && items.Hammer),
                new Location(this, 256+160, 0x308156, LocationType.Regular, "Thieves' Town - Blind",
                    items => items.BigKeyTT && items.KeyTT && CanBeatBoss(items)),
            };
        }

        private bool CanBeatBoss(Progression items) {
            return items.Sword || items.Hammer ||
                items.Somaria || items.Byrna;
        }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && World.CanEnter("Dark World North West", items);
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Thieves' Town - Blind").Available(items);
        }

    }

}
