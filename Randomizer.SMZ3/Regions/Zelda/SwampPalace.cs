using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class SwampPalace : Z3Region, IReward {

        public override string Name => "Swamp Palace";

        public RewardType Reward { get; set; } = RewardType.None;

        public SwampPalace(World world, Config config) : base(world, config) {
            Weight = 3;
            RegionItems = new[] { KeySP, BigKeySP, MapSP, CompassSP };

            Locations = new List<Location> {
                new Location(this, 256+135, 0x1EA9D, LocationType.Regular, "Swamp Palace - Entrance")
                    .Allow((item, items) => Config.Keysanity || item.Is(KeySP, World)),
                new Location(this, 256+136, 0x1E986, LocationType.Regular, "Swamp Palace - Map Chest",
                    items => items.KeySP),
                new Location(this, 256+137, 0x1E989, LocationType.Regular, "Swamp Palace - Big Chest",
                    items => items.BigKeySP && items.KeySP && items.Hammer)
                    .AlwaysAllow((item, items) => item.Is(BigKeySP, World)),
                new Location(this, 256+138, 0x1EAA0, LocationType.Regular, "Swamp Palace - Compass Chest",
                    items => items.KeySP && items.Hammer),
                new Location(this, 256+139, 0x1EAA3, LocationType.Regular, "Swamp Palace - West Chest",
                    items => items.KeySP && items.Hammer),
                new Location(this, 256+140, 0x1EAA6, LocationType.Regular, "Swamp Palace - Big Key Chest",
                    items => items.KeySP && items.Hammer),
                new Location(this, 256+141, 0x1EAA9, LocationType.Regular, "Swamp Palace - Flooded Room - Left",
                    items => items.KeySP && items.Hammer && items.Hookshot),
                new Location(this, 256+142, 0x1EAAC, LocationType.Regular, "Swamp Palace - Flooded Room - Right",
                    items => items.KeySP && items.Hammer && items.Hookshot),
                new Location(this, 256+143, 0x1EAAF, LocationType.Regular, "Swamp Palace - Waterfall Room",
                    items => items.KeySP && items.Hammer && items.Hookshot),
                new Location(this, 256+144, 0x308154, LocationType.Regular, "Swamp Palace - Arrghus",
                    items => items.KeySP && items.Hammer && items.Hookshot),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && items.Mirror && items.Flippers && World.CanEnter("Dark World South", items);
        }

        public bool CanComplete(Progression items) {
            return GetLocation("Swamp Palace - Arrghus").Available(items);
        }

    }

}
