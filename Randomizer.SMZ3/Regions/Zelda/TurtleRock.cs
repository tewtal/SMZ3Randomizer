using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class TurtleRock : Region {

        public override string Name => "Turtle Rock";
        public override string Area => "Turtle Rock";

        public ItemType Medallion { get; set; }

        public TurtleRock(World world, Logic logic) : base(world, logic) {
            RegionItems = new[] { KeyTR, BigKeyTR, MapTR, CompassTR };

            Requirement laserBridge = items => items.Has(BigKeyTR) && items.Has(KeyTR, 3) &&
                items.Has(Lamp) && (items.Has(Cape) || items.Has(Byrna) || items.CanBlockLasers());

            Locations = new List<Location> {
                new Location(this, 256+177, 0xEA22, LocationType.Regular, "Turtle Rock - Compass Chest"),
                new Location(this, 256+178, 0xEA1C, LocationType.Regular, "Turtle Rock - Roller Room - Left",
                    items => items.Has(Firerod)),
                new Location(this, 256+179, 0xEA1F, LocationType.Regular, "Turtle Rock - Roller Room - Right",
                    items => items.Has(Firerod)),
                new Location(this, 256+180, 0xEA16, LocationType.Regular, "Turtle Rock - Chain Chomps",
                    items => items.Has(KeyTR)),
                new Location(this, 256+181, 0xEA25, LocationType.Regular, "Turtle Rock - Big Key Chest",
                    items => items.Has(KeyTR,
                        /*!config.keysanity*/true || Locations.Get("Turtle Rock - Big Key Chest").ItemType == BigKeyTR ? 2 :
                            Locations.Get("Turtle Rock - Big Key Chest").ItemType == KeyTR ? 3 : 4))
                    .AlwaysAllow((item, items) => item.Type == KeyTR && items.Has(KeyTR, 3)),
                new Location(this, 256+182, 0xEA19, LocationType.Regular, "Turtle Rock - Big Chest",
                    items => items.Has(BigKeyTR) && items.Has(KeyTR, 2))
                    .Allow((item, items) => item.Type != BigKeyTR),
                new Location(this, 256+183, 0xEA34, LocationType.Regular, "Turtle Rock - Crystaroller Room",
                    items => items.Has(BigKeyTR) && items.Has(KeyTR, 2)),
                new Location(this, 256+184, 0xEA28, LocationType.Regular, "Turtle Rock - Eye Bridge - Top Right", laserBridge),
                new Location(this, 256+185, 0xEA2B, LocationType.Regular, "Turtle Rock - Eye Bridge - Top Left", laserBridge),
                new Location(this, 256+186, 0xEA2E, LocationType.Regular, "Turtle Rock - Eye Bridge - Bottom Right", laserBridge),
                new Location(this, 256+187, 0xEA31, LocationType.Regular, "Turtle Rock - Eye Bridge - Bottom Left", laserBridge),
                new Location(this, 256+188, 0x180159, LocationType.Regular, "Turtle Rock - Trinexx",
                    items => items.Has(BigKeyTR) && items.Has(KeyTR, 4) && items.Has(Lamp) && CanBeatBoss(items)),
            };
        }

        static bool CanBeatBoss(List<Item> items) {
            return items.Has(Firerod) && items.Has(Icerod) && (
                items.HasSword(3) || items.Has(Hammer) ||
                items.CanExtendMagic(2) && items.HasSword(2) ||
                items.CanExtendMagic(4) && items.HasSword());
        }

        public override bool CanEnter(List<Item> items) {
            return items.Has(Medallion) && items.HasSword() &&
                items.Has(MoonPearl) && items.CanLiftHeavy() && items.Has(Hammer) && items.Has(Somaria) &&
                World.CanEnter("Light World Death Mountain East", items);
        }

    }

}
