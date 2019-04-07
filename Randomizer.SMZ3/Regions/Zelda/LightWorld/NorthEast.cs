using System.Collections.Generic;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.Zelda.LightWorld {

    class NorthEast : Region {

        public override string Name => "Light World North East";
        public override string Area => "Light World";

        public NorthEast(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+36, 0xEE1C3, LocationType.Regular, "King Zora",
                    items => items.CanLiftLight() || items.Flippers),
                new Location(this, 256+37, 0x180149, LocationType.Regular, "Zora's Ledge",
                    items => items.Flippers),
                new Location(this, 256+254, 0xE9B0, LocationType.Regular, "Waterfall Fairy - Left",
                    items => items.Flippers),
                new Location(this, 256+39, 0xE9D1, LocationType.Regular, "Waterfall Fairy - Right",
                    items => items.Flippers),
                new Location(this, 256+40, 0x180014, LocationType.Regular, "Potion Shop",
                    items => items.Mushroom),
                new Location(this, 256+41, 0xEA82, LocationType.Regular, "Sahasrahla's Hut - Left"),
                new Location(this, 256+42, 0xEA85, LocationType.Regular, "Sahasrahla's Hut - Middle"),
                new Location(this, 256+43, 0xEA88, LocationType.Regular, "Sahasrahla's Hut - Right"),
                new Location(this, 256+44, 0x2F1FC, LocationType.Regular, "Sahasrahla",
                    items => World.CanAquire(items, PendantGreen))
            };
        }

    }

}
