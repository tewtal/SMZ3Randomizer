using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class SwampPalace : Region, Reward {

        public override string Name => "Swamp Palace";
        public override string Area => "Swamp Palace";

        public RewardType Reward { get; set; } = RewardType.None;

        public SwampPalace(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeySP, BigKeySP, MapSP, CompassSP };

            Locations = new List<Location> {
                new Location(this, 256+135, 0xEA9D, LocationType.Regular, "Swamp Palace - Entrance")
                    .Allow((item, items) => Config.Keysanity || item.Type == KeySP),
                new Location(this, 256+136, 0xE986, LocationType.Regular, "Swamp Palace - Map Chest",
                    items => items.KeySP),
                new Location(this, 256+137, 0xE989, LocationType.Regular, "Swamp Palace - Big Chest",
                    items => items.BigKeySP && items.KeySP && items.Hammer)
                    .AlwaysAllow((item, items) => item.Type == BigKeySP),
                new Location(this, 256+138, 0xEAA0, LocationType.Regular, "Swamp Palace - Compass Chest",
                    items => items.KeySP && items.Hammer),
                new Location(this, 256+139, 0xEAA3, LocationType.Regular, "Swamp Palace - West Chest",
                    items => items.KeySP && items.Hammer),
                new Location(this, 256+140, 0xEAA6, LocationType.Regular, "Swamp Palace - Big Key Chest",
                    items => items.KeySP && items.Hammer),
                new Location(this, 256+141, 0xEAA9, LocationType.Regular, "Swamp Palace - Flooded Room - Left",
                    items => items.KeySP && items.Hammer && items.Hookshot),
                new Location(this, 256+142, 0xEAAC, LocationType.Regular, "Swamp Palace - Flooded Room - Right",
                    items => items.KeySP && items.Hammer && items.Hookshot),
                new Location(this, 256+143, 0xEAAF, LocationType.Regular, "Swamp Palace - Waterfall Room",
                    items => items.KeySP && items.Hammer && items.Hookshot),
                new Location(this, 256+144, 0x180154, LocationType.Regular, "Swamp Palace - Arrghus",
                    items => items.KeySP && items.Hammer && items.Hookshot),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && items.Mirror && items.Flippers && World.CanEnter("Dark World South", items);
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Swamp Palace - Arrghus").Available(items);
        }

    }

}
