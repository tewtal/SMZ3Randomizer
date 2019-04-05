using System.Collections.Generic;
using System.Linq;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.Zelda.LightWorld {

    class NorthWest : Region {

        public override string Name => "Light World North West";
        public override string Area => "Light World";

        public NorthWest(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+14, 0x289B0, LocationType.Pedestal, "Master Sword Pedestal",
                    items => World.CanAquireAll(items, PendantGreen, PendantNonGreen)),
                new Location(this, 256+15, 0x180013, LocationType.Regular, "Mushroom"),
                new Location(this, 256+16, 0x180000, LocationType.Regular, "Lost Woods Hideout"),
                new Location(this, 256+17, 0x180001, LocationType.Regular, "Lumberjack Tree",
                    items => World.CanAquire(items, Agahnim) && items.Has(Boots)),
                new Location(this, 256+18, 0xEB3F, LocationType.Regular, "Pegasus Rocks",
                    items => items.Has(Boots)),
                new Location(this, 256+19, 0x180004, LocationType.Regular, "Graveyard Ledge",
                    items => items.Has(Mirror) && items.Has(MoonPearl) && World.CanEnter("Dark World North West", items)),
                new Location(this, 256+20, 0xE97A, LocationType.Regular, "King's Tomb",
                    items => items.Has(Boots) && (
                        items.CanLiftHeavy() ||
                        items.Has(Mirror) && items.Has(MoonPearl) && World.CanEnter("Dark World North West", items))),
                new Location(this, 256+21, 0xEA8E, LocationType.Regular, "Kakariko Well - Top"),
                new Location(this, 256+22, 0xEA91, LocationType.Regular, "Kakariko Well - Left"),
                new Location(this, 256+23, 0xEA94, LocationType.Regular, "Kakariko Well - Middle"),
                new Location(this, 256+24, 0xEA97, LocationType.Regular, "Kakariko Well - Right"),
                new Location(this, 256+25, 0xEA9A, LocationType.Regular, "Kakariko Well - Bottom"),
                new Location(this, 256+26, 0xEB0F, LocationType.Regular, "Blind's Hideout - Top"),
                new Location(this, 256+27, 0xEB18, LocationType.Regular, "Blind's Hideout - Far Left"),
                new Location(this, 256+28, 0xEB12, LocationType.Regular, "Blind's Hideout - Left"),
                new Location(this, 256+29, 0xEB15, LocationType.Regular, "Blind's Hideout - Right"),
                new Location(this, 256+30, 0xEB1B, LocationType.Regular, "Blind's Hideout - Far Right"),
                new Location(this, 256+31, 0x2EB18, LocationType.Regular, "Bottle Merchant"),
                new Location(this, 256+32, 0xE9E9, LocationType.Regular, "Chicken House"),
                new Location(this, 256+33, 0x339CF, LocationType.Regular, "Sick Kid",
                    items => items.Has(Bottle)),
                new Location(this, 256+34, 0xE9CE, LocationType.Regular, "Kakariko Tavern"),
                new Location(this, 256+35, 0x180015, LocationType.Regular, "Magic Bat",
                    items => items.Has(Powder) && (items.Has(Hammer) || items.Has(MoonPearl) && items.Has(Mirror) && items.CanLiftHeavy())),
            };
        }

    }

}
