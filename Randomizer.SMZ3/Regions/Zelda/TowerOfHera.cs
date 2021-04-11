using System.Collections.Generic;
using static Randomizer.SMZ3.Z3Logic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class TowerOfHera : Z3Region, IReward {

        public override string Name => "Tower of Hera";

        public RewardType Reward { get; set; } = RewardType.None;

        public TowerOfHera(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyTH, BigKeyTH, MapTH, CompassTH };

            Locations = new List<Location> {
                new Location(this, 256+115, 0x308162, LocationType.HeraStandingKey, "Tower of Hera - Basement Cage"),
                new Location(this, 256+116, 0x1E9AD, LocationType.Regular, "Tower of Hera - Map Chest"),
                new Location(this, 256+117, 0x1E9E6, LocationType.Regular, "Tower of Hera - Big Key Chest",
                    items => items.KeyTH && items.CanLightTorches())
                    .AlwaysAllow((item, items) => item.Is(KeyTH, World)),
                new Location(this, 256+118, 0x1E9FB, LocationType.Regular, "Tower of Hera - Compass Chest",
                    items => items.BigKeyTH),
                new Location(this, 256+119, 0x1E9F8, LocationType.Regular, "Tower of Hera - Big Chest",
                    items => items.BigKeyTH),
                new Location(this, 256+120, 0x308152, LocationType.Regular, "Tower of Hera - Moldorm", Logic switch {
                    Normal => items => items.BigKeyTH && CanBeatBoss(items),
                    _ => new Requirement(items => (items.BigKeyTH || items.Hookshot) && CanBeatBoss(items)),
                }),
            };
        }

        private bool CanBeatBoss(Progression items) {
            return items.Sword || items.Hammer;
        }

        public override bool CanEnter(Progression items) {
            return (items.Mirror || items.Hookshot && items.Hammer) && World.CanEnter("Light World Death Mountain West", items);
        }

        public bool CanComplete(Progression items) {
            return GetLocation("Tower of Hera - Moldorm").Available(items);
        }

    }

}
