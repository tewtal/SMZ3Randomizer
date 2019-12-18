using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class SkullWoods : Z3Region, IReward {

        public override string Name => "Skull Woods";
        public override string Area => "Skull Woods";

        public RewardType Reward { get; set; } = RewardType.None;

        public SkullWoods(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeySW, BigKeySW, MapSW, CompassSW };

            Locations = new List<Location> {
                new Location(this, 256+145, 0xE9A1, LocationType.Regular, "Skull Woods - Pot Prison"),
                new Location(this, 256+146, 0xE992, LocationType.Regular, "Skull Woods - Compass Chest"),
                new Location(this, 256+147, 0xE998, LocationType.Regular, "Skull Woods - Big Chest",
                    items => items.BigKeySW)
                    .AlwaysAllow((item, items) => item.Type == BigKeySW),
                new Location(this, 256+148, 0xE99B, LocationType.Regular, "Skull Woods - Map Chest"),
                new Location(this, 256+149, 0xE9C8, LocationType.Regular, "Skull Woods - Pinball Room")
                    .Allow((item, items) => item.Type == KeySW),
                new Location(this, 256+150, 0xE99E, LocationType.Regular, "Skull Woods - Big Key Chest"),
                new Location(this, 256+151, 0xE9FE, LocationType.Regular, "Skull Woods - Bridge Room",
                    items => items.Firerod),
                new Location(this, 256+152, 0x180155, LocationType.Regular, "Skull Woods - Mothula",
                    items => items.Firerod && items.Sword && items.KeySW >= 3),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && World.CanEnter("Dark World North West", items);
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Skull Woods - Mothula").Available(items);
        }

    }

}
