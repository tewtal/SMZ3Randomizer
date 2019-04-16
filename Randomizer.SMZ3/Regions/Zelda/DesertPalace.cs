using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class DesertPalace : Z3Region, Reward {

        public override string Name => "Desert Palace";
        public override string Area => "Desert Palace";

        public RewardType Reward { get; set; } = RewardType.None;

        public DesertPalace(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyDP, BigKeyDP, MapDP, CompassDP };

            Locations = new List<Location> {
                new Location(this, 256+109, 0xE98F, LocationType.Regular, "Desert Palace - Big Chest",
                    items => items.BigKeyDP),
                new Location(this, 256+110, 0x180160, LocationType.Regular, "Desert Palace - Torch",
                    items => items.Boots),
                new Location(this, 256+111, 0xE9B6, LocationType.Regular, "Desert Palace - Map Chest"),
                new Location(this, 256+112, 0xE9C2, LocationType.Regular, "Desert Palace - Big Key Chest",
                    items => items.KeyDP),
                new Location(this, 256+113, 0xE9CB, LocationType.Regular, "Desert Palace - Compass Chest",
                    items => items.KeyDP),
                new Location(this, 256+114, 0x180151, LocationType.Regular, "Desert Palace - Lanmolas",
                    items => (
                        items.CanLiftLight() ||
                        items.CanAccessMiseryMirePortal(Config) && items.Mirror
                    ) && items.BigKeyDP && items.KeyDP && items.CanLightTorches() && CanBeatBoss(items)),
            };
        }

        static bool CanBeatBoss(Progression items) {
            return items.Sword || items.Hammer || items.Bow ||
                items.Firerod || items.Icerod ||
                items.Byrna || items.Somaria;
        }

        public override bool CanEnter(Progression items) {
            return items.Book ||
                items.Mirror && items.CanLiftHeavy() && items.Flute ||
                items.CanAccessMiseryMirePortal(Config) && items.Mirror;
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Desert Palace - Lanmolas").Available(items);
        }

    }

}
