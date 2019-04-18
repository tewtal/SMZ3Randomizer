using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class TurtleRock : Z3Region, IReward, IMedallionAccess {

        public override string Name => "Turtle Rock";
        public override string Area => "Turtle Rock";

        public RewardType Reward { get; set; } = RewardType.None;
        public ItemType Medallion { get; set; }

        public TurtleRock(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyTR, BigKeyTR, MapTR, CompassTR };

            Locations = new List<Location> {
                new Location(this, 256+177, 0xEA22, LocationType.Regular, "Turtle Rock - Compass Chest"),
                new Location(this, 256+178, 0xEA1C, LocationType.Regular, "Turtle Rock - Roller Room - Left",
                    items => items.Firerod),
                new Location(this, 256+179, 0xEA1F, LocationType.Regular, "Turtle Rock - Roller Room - Right",
                    items => items.Firerod),
                new Location(this, 256+180, 0xEA16, LocationType.Regular, "Turtle Rock - Chain Chomps",
                    items => items.Has(KeyTR)),
                new Location(this, 256+181, 0xEA25, LocationType.Regular, "Turtle Rock - Big Key Chest",
                    items => items.Has(KeyTR,
                        !Config.Keysanity || Locations.Get("Turtle Rock - Big Key Chest").ItemType == BigKeyTR ? 2 :
                            Locations.Get("Turtle Rock - Big Key Chest").ItemType == KeyTR ? 3 : 4))
                    .AlwaysAllow((item, items) => item.Type == KeyTR && items.Has(KeyTR, 3)),
                new Location(this, 256+182, 0xEA19, LocationType.Regular, "Turtle Rock - Big Chest",
                    items => items.BigKeyTR && items.Has(KeyTR, 2))
                    .Allow((item, items) => item.Type != BigKeyTR),
                new Location(this, 256+183, 0xEA34, LocationType.Regular, "Turtle Rock - Crystaroller Room",
                    items => items.BigKeyTR && items.Has(KeyTR, 2)),
                new Location(this, 256+184, 0xEA28, LocationType.Regular, "Turtle Rock - Eye Bridge - Top Right", LaserBridge),
                new Location(this, 256+185, 0xEA2B, LocationType.Regular, "Turtle Rock - Eye Bridge - Top Left", LaserBridge),
                new Location(this, 256+186, 0xEA2E, LocationType.Regular, "Turtle Rock - Eye Bridge - Bottom Right", LaserBridge),
                new Location(this, 256+187, 0xEA31, LocationType.Regular, "Turtle Rock - Eye Bridge - Bottom Left", LaserBridge),
                new Location(this, 256+188, 0x180159, LocationType.Regular, "Turtle Rock - Trinexx",
                    items => items.BigKeyTR && items.Has(KeyTR, 4) && items.Lamp && CanBeatBoss(items)),
            };
        }

        private bool LaserBridge(Progression items) {
            return items.BigKeyTR && items.Has(KeyTR, 3) && items.Lamp && (items.Cape || items.Byrna || items.CanBlockLasers);
        }

        private bool CanBeatBoss(Progression items) {
            return items.Firerod && items.Icerod;
        }

        public override bool CanEnter(Progression items) {
            return Medallion switch {
                    Bombos => items.Bombos,
                    Ether => items.Ether,
                    _ => items.Quake
                } && items.Sword &&
                items.MoonPearl && items.CanLiftHeavy() && items.Hammer && items.Somaria &&
                World.CanEnter("Light World Death Mountain East", items);
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Turtle Rock - Trinexx").Available(items);
        }

    }

}
