using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class ThievesTown : Region, Reward {

        public override string Name => "Thieves' Town";
        public override string Area => "Thieves' Town";

        public RewardType Reward { get; set; } = RewardType.None;

        public ThievesTown(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyTT, BigKeyTT, MapTT, CompassTT };

            Locations = new List<Location> {
                new Location(this, 256+153, 0xEA01, LocationType.Regular, "Thieves' Town - Map Chest"),
                new Location(this, 256+154, 0xEA0A, LocationType.Regular, "Thieves' Town - Ambush Chest"),
                new Location(this, 256+155, 0xEA07, LocationType.Regular, "Thieves' Town - Compass Chest"),
                new Location(this, 256+156, 0xEA04, LocationType.Regular, "Thieves' Town - Big Key Chest"),
                new Location(this, 256+157, 0xEA0D, LocationType.Regular, "Thieves' Town - Attic",
                    items => items.Has(BigKeyTT) && items.Has(KeyTT)),
                new Location(this, 256+158, 0xEA13, LocationType.Regular, "Thieves' Town - Blind's Cell",
                    items => items.Has(BigKeyTT)),
                new Location(this, 256+159, 0xEA10, LocationType.Regular, "Thieves' Town - Big Chest",
                    items => items.Has(BigKeyTT) && items.Has(Hammer) &&
                        (Locations.Get("Thieves' Town - Big Chest").ItemType == KeyTT || items.Has(KeyTT)))
                    .AlwaysAllow((item, items) => item.Type == KeyTT && items.Has(Hammer)),
                new Location(this, 256+160, 0x180156, LocationType.Regular, "Thieves' Town - Blind",
                    items => items.Has(BigKeyTT) && items.Has(KeyTT) && CanBeatBoss(items)),
            };
        }

        static bool CanBeatBoss(List<Item> items) {
            return items.HasSword() || items.Has(Hammer) ||
                items.Has(Somaria) || items.Has(Byrna);
        }

        public override bool CanEnter(List<Item> items) {
            return items.Has(MoonPearl) && World.CanEnter("Dark World North West", items);
        }

        public bool CanComplete(List<Item> items) {
            return Locations.Get("Thieves' Town - Blind").Available(items);
        }

    }

}
