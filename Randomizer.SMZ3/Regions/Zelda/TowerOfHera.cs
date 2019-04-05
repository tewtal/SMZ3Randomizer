using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class TowerOfHera : Region, Reward {

        public override string Name => "Tower of Hera";
        public override string Area => "Tower of Hera";

        public RewardType Reward { get; set; } = RewardType.None;

        public TowerOfHera(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyTH, BigKeyTH, MapTH, CompassTH };

            Locations = new List<Location> {
                new Location(this, 256+115, 0x180162, LocationType.HeraStandingKey, "Tower of Hera - Basement Cage"),
                new Location(this, 256+116, 0xE9AD, LocationType.Regular, "Tower of Hera - Map Chest"),
                new Location(this, 256+117, 0xE9E6, LocationType.Regular, "Tower of Hera - Big Key Chest",
                    items => items.Has(KeyTH) && items.CanLightTorches())
                    .AlwaysAllow((item, items) => item.Type == KeyTH),
                new Location(this, 256+118, 0xE9FB, LocationType.Regular, "Tower of Hera - Compass Chest",
                    items => items.Has(BigKeyTH)),
                new Location(this, 256+119, 0xE9F8, LocationType.Regular, "Tower of Hera - Big Chest",
                    items => items.Has(BigKeyTH)),
                new Location(this, 256+120, 0x180152, LocationType.Regular, "Tower of Hera - Moldorm",
                    items => items.Has(BigKeyTH) && CanBeatBoss(items)),
            };
        }

        static bool CanBeatBoss(List<Item> items) {
            return items.HasSword() || items.Has(Hammer);
        }

        public override bool CanEnter(List<Item> items) {
            return (items.Has(Mirror) || items.Has(Hookshot) && items.Has(Hammer)) && World.CanEnter("Light World Death Mountain West", items);
        }

        public bool CanComplete(List<Item> items) {
            return Locations.Get("Tower of Hera - Moldorm").Available(items);
        }

    }

}
