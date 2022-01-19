using System.Collections.Generic;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.Zelda.LightWorld {

    class NorthWest : Z3Region {

        public override string Name => "Light World North West";
        public override string Area => "Light World";

        public NorthWest(World world, Config config) : base(world, config) {
            var sphereOne = -14;
            Locations = new List<Location> {
                new Location(this, 256+14, 0x589B0, LocationType.Pedestal, "Master Sword Pedestal",
                    items => World.CanAcquireAll(items, AnyPendant)),
                new Location(this, 256+15, 0x308013, LocationType.Regular, "Mushroom").Weighted(sphereOne),
                new Location(this, 256+16, 0x308000, LocationType.Regular, "Lost Woods Hideout").Weighted(sphereOne),
                new Location(this, 256+17, 0x308001, LocationType.Regular, "Lumberjack Tree",
                    items => World.CanAcquire(items, Agahnim) && items.Boots),
                new Location(this, 256+18, 0x1EB3F, LocationType.Regular, "Pegasus Rocks",
                    items => items.Boots),
                new Location(this, 256+19, 0x308004, LocationType.Regular, "Graveyard Ledge",
                    items => items.Mirror && items.MoonPearl && World.CanEnter("Dark World North West", items)),
                new Location(this, 256+20, 0x1E97A, LocationType.Regular, "King's Tomb",
                    items => items.Boots && (
                        items.CanLiftHeavy() ||
                        items.Mirror && items.MoonPearl && World.CanEnter("Dark World North West", items))),
                new Location(this, 256+21, 0x1EA8E, LocationType.Regular, "Kakariko Well - Top").Weighted(sphereOne),
                new Location(this, 256+22, 0x1EA91, LocationType.Regular, "Kakariko Well - Left").Weighted(sphereOne),
                new Location(this, 256+23, 0x1EA94, LocationType.Regular, "Kakariko Well - Middle").Weighted(sphereOne),
                new Location(this, 256+24, 0x1EA97, LocationType.Regular, "Kakariko Well - Right").Weighted(sphereOne),
                new Location(this, 256+25, 0x1EA9A, LocationType.Regular, "Kakariko Well - Bottom").Weighted(sphereOne),
                new Location(this, 256+26, 0x1EB0F, LocationType.Regular, "Blind's Hideout - Top").Weighted(sphereOne),
                new Location(this, 256+27, 0x1EB18, LocationType.Regular, "Blind's Hideout - Far Left").Weighted(sphereOne),
                new Location(this, 256+28, 0x1EB12, LocationType.Regular, "Blind's Hideout - Left").Weighted(sphereOne),
                new Location(this, 256+29, 0x1EB15, LocationType.Regular, "Blind's Hideout - Right").Weighted(sphereOne),
                new Location(this, 256+30, 0x1EB1B, LocationType.Regular, "Blind's Hideout - Far Right").Weighted(sphereOne),
                new Location(this, 256+31, 0x5EB18, LocationType.Regular, "Bottle Merchant").Weighted(sphereOne),
                new Location(this, 256+250, 0x1E9E9, LocationType.Regular, "Chicken House").Weighted(sphereOne),
                new Location(this, 256+33, 0x6B9CF, LocationType.Regular, "Sick Kid",
                    items => items.Bottle),
                new Location(this, 256+34, 0x1E9CE, LocationType.Regular, "Kakariko Tavern").Weighted(sphereOne),
                new Location(this, 256+35, 0x308015, LocationType.Regular, "Magic Bat",
                    items => items.Powder && (items.Hammer || items.MoonPearl && items.Mirror && items.CanLiftHeavy())),
            };
        }

    }

}
