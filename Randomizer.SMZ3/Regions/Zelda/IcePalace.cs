using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class IcePalace : Z3Region, IReward {

        public override string Name => "Ice Palace";

        public RewardType Reward { get; set; } = RewardType.None;

        public IcePalace(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyIP, BigKeyIP, MapIP, CompassIP };

            Locations = new List<Location> {
                new Location(this, 256+161, 0x1E9D4, LocationType.Regular, "Ice Palace - Compass Chest"),
                new Location(this, 256+162, 0x1E9E0, LocationType.Regular, "Ice Palace - Spike Room",
                    items => items.Hookshot || items.BigKeyIP && items.KeyIP >= 1),
                new Location(this, 256+163, 0x1E9DD, LocationType.Regular, "Ice Palace - Map Chest",
                    items => items.Hammer && items.CanLiftLight() && Locations.Get("Ice Palace - Spike Room").Available(items)),
                new Location(this, 256+164, 0x1E9A4, LocationType.Regular, "Ice Palace - Big Key Chest",
                    items => items.Hammer && items.CanLiftLight() && Locations.Get("Ice Palace - Spike Room").Available(items)),
                new Location(this, 256+165, 0x1E9E3, LocationType.Regular, "Ice Palace - Iced T Room"),
                new Location(this, 256+166, 0x1E995, LocationType.Regular, "Ice Palace - Freezor Chest"),
                new Location(this, 256+167, 0x1E9AA, LocationType.Regular, "Ice Palace - Big Chest",
                    items => items.BigKeyIP),
                new Location(this, 256+168, 0x308157, LocationType.Regular, "Ice Palace - Kholdstare",
                    items => items.BigKeyIP && items.Hammer && items.CanLiftLight() &&
                        items.KeyIP >= (items.Somaria ? 1 : 2)),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && items.Flippers && items.CanLiftHeavy() && items.CanMeltFreezors();
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Ice Palace - Kholdstare").Available(items);
        }

    }

}
