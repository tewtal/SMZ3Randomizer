using System;
using System.Collections.Generic;
using System.Linq;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class GanonTower : Region {

        public override string Name => "Ganon Tower";
        public override string Area => "Ganon Tower";

        public GanonTower(World world, Logic logic) : base(world, logic) {
            Func<List<Item>, IList<Location>, bool> leftSide = (items, locations) =>
                items.Has(Hammer) && items.Has(Hookshot) &&
                items.Has(KeyGT, locations.Any(l => l.ItemType == BigKeyGT) ? 3 : 4);
            Func<List<Item>, IList<Location>, bool> rightSide = (items, locations) =>
                items.Has(Somaria) && items.Has(Firerod) &&
                items.Has(KeyGT, locations.Any(l => l.ItemType == BigKeyGT) ? 3 : 4);
            Requirement bigKeyRoom = items =>
                items.Has(KeyGT, 3) && CanBeatArmos(items) && (
                    items.Has(Hammer) && items.Has(Hookshot) ||
                    items.Has(Firerod) && items.Has(Somaria));
            Requirement towerAscend = items => items.Has(BigKeyGT) && items.Has(KeyGT, 3) &&
                items.Has(Bow) && items.CanLightTorches();

            Locations = new List<Location> {
                new Location(this, 256+189, 0x180161, LocationType.Regular, "Ganon's Tower - Bob's Torch",
                    items => items.Has(Boots)),
                new Location(this, 256+190, 0xEAB8, LocationType.Regular, "Ganon's Tower - DMs Room - Top Left",
                    items => items.Has(Hammer) && items.Has(Hookshot)),
                new Location(this, 256+191, 0xEABB, LocationType.Regular, "Ganon's Tower - DMs Room - Top Right",
                    items => items.Has(Hammer) && items.Has(Hookshot)),
                new Location(this, 256+192, 0xEABE, LocationType.Regular, "Ganon's Tower - DMs Room - Bottom Left",
                    items => items.Has(Hammer) && items.Has(Hookshot)),
                new Location(this, 256+193, 0xEAC1, LocationType.Regular, "Ganon's Tower - DMs Room - Bottom Right",
                    items => items.Has(Hammer) && items.Has(Hookshot)),
                new Location(this, 256+194, 0xEAD3, LocationType.Regular, "Ganon's Tower - Map Chest",
                    items => items.Has(Hammer) && (items.Has(Hookshot) || items.Has(Boots)) && items.Has(KeyGT,
                        new[] { BigKeyGT, KeyGT }.Contains(Locations.Get("Ganon's Tower - Map Chest").ItemType) ? 3 : 4))
                    .AlwaysAllow((item, items) => item.Type == KeyGT && items.Has(KeyGT, 3)),
                new Location(this, 256+195, 0xEAD0, LocationType.Regular, "Ganon's Tower - Firesnake Room",
                    items => items.Has(Hammer) && items.Has(Hookshot) && items.Has(KeyGT, new[] {
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Right"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Left"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Left"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Right")
                    }.Any(l => l.ItemType == BigKeyGT) ||
                        Locations.Get("Ganon's Tower - Firesnake Room").ItemType == KeyGT ? 2 : 3)),
                new Location(this, 256+196, 0xEAC4, LocationType.Regular, "Ganon's Tower - Randomizer Room - Top Left",
                    items => leftSide(items, new[] {
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Right"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Left"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Right")
                    })),
                new Location(this, 256+197, 0xEAC7, LocationType.Regular, "Ganon's Tower - Randomizer Room - Top Right",
                    items => leftSide(items, new[] {
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Left"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Left"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Right")
                    })),
                new Location(this, 256+198, 0xEACA, LocationType.Regular, "Ganon's Tower - Randomizer Room - Bottom Left",
                    items => leftSide(items, new[] {
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Right"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Left"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Right")
                    })),
                new Location(this, 256+199, 0xEACD, LocationType.Regular, "Ganon's Tower - Randomizer Room - Bottom Right",
                    items => leftSide(items, new[] {
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Right"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Left"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Left")
                    })),
                new Location(this, 256+200, 0xEAD9, LocationType.Regular, "Ganon's Tower - Hope Room - Left"),
                new Location(this, 256+201, 0xEADC, LocationType.Regular, "Ganon's Tower - Hope Room - Right"),
                new Location(this, 256+202, 0xEAE2, LocationType.Regular, "Ganon's Tower - Tile Room",
                    items => items.Has(Somaria)),
                new Location(this, 256+203, 0xEAE5, LocationType.Regular, "Ganon's Tower - Compass Room - Top Left",
                    items => rightSide(items, new[] {
                        Locations.Get("Ganon's Tower - Compass Room - Top Right"),
                        Locations.Get("Ganon's Tower - Compass Room - Bottom Left"),
                        Locations.Get("Ganon's Tower - Compass Room - Bottom Right")
                    })),
                new Location(this, 256+204, 0xEAE8, LocationType.Regular, "Ganon's Tower - Compass Room - Top Right",
                    items => rightSide(items, new[] {
                        Locations.Get("Ganon's Tower - Compass Room - Top Left"),
                        Locations.Get("Ganon's Tower - Compass Room - Bottom Left"),
                        Locations.Get("Ganon's Tower - Compass Room - Bottom Right")
                    })),
                new Location(this, 256+205, 0xEAEB, LocationType.Regular, "Ganon's Tower - Compass Room - Bottom Left",
                    items => rightSide(items, new[] {
                        Locations.Get("Ganon's Tower - Compass Room - Top Right"),
                        Locations.Get("Ganon's Tower - Compass Room - Top Left"),
                        Locations.Get("Ganon's Tower - Compass Room - Bottom Right")
                    })),
                new Location(this, 256+206, 0xEAEE, LocationType.Regular, "Ganon's Tower - Compass Room - Bottom Right",
                    items => rightSide(items, new[] {
                        Locations.Get("Ganon's Tower - Compass Room - Top Right"),
                        Locations.Get("Ganon's Tower - Compass Room - Top Left"),
                        Locations.Get("Ganon's Tower - Compass Room - Bottom Left")
                    })),
                new Location(this, 256+207, 0xEADF, LocationType.Regular, "Ganon's Tower - Bob's Chest",
                    items => items.Has(KeyGT, 3) && (
                        items.Has(Hammer) && items.Has(Hookshot) ||
                        items.Has(Somaria) && items.Has(Firerod))),
                new Location(this, 256+208, 0xEAD6, LocationType.Regular, "Ganon's Tower - Big Chest",
                    items => items.Has(BigKeyGT) && items.Has(KeyGT, 3) && (
                        items.Has(Hammer) && items.Has(Hookshot) ||
                        items.Has(Somaria) && items.Has(Firerod)))
                    .Allow((item, items) => item.Type != BigKeyGT),
                new Location(this, 256+209, 0xEAF1, LocationType.Regular, "Ganon's Tower - Big Key Chest", bigKeyRoom),
                new Location(this, 256+210, 0xEAF4, LocationType.Regular, "Ganon's Tower - Big Key Room - Left", bigKeyRoom),
                new Location(this, 256+211, 0xEAF7, LocationType.Regular, "Ganon's Tower - Big Key Room - Right", bigKeyRoom),
                new Location(this, 256+212, 0xEAFD, LocationType.Regular, "Ganon's Tower - Mini Helmasaur Room - Left", towerAscend)
                    .Allow((item, items) => item.Type != BigKeyGT),
                new Location(this, 256+213, 0xEB00, LocationType.Regular, "Ganon's Tower - Mini Helmasaur Room - Right", towerAscend)
                    .Allow((item, items) => item.Type != BigKeyGT),
                new Location(this, 256+214, 0xEB03, LocationType.Regular, "Ganon's Tower - Pre-Moldorm Chest", towerAscend)
                    .Allow((item, items) => item.Type != BigKeyGT),
                new Location(this, 256+215, 0xEB06, LocationType.Regular, "Ganon's Tower - Moldorm Chest",
                    items => items.Has(BigKeyGT) && items.Has(KeyGT, 4) &&
                        items.Has(Bow) && items.CanLightTorches() &&
                        CanBeatMoldorm(items) && items.Has(Hookshot))
                    .Allow((item, items) => new[] { KeyGT, BigKeyGT }.Contains(item.Type) == false),
            };
        }

        static bool CanBeatArmos(List<Item> items) {
            return items.HasSword() || items.Has(Hammer) || items.Has(Bow) ||
                items.Has(BlueBoomerang) || items.Has(RedBoomerang) ||
                items.CanExtendMagic(4) && (items.Has(Firerod) || items.Has(Icerod)) ||
                items.CanExtendMagic(2) && (items.Has(Somaria) || items.Has(Byrna));
        }

        static bool CanBeatMoldorm(List<Item> items) {
            return items.HasSword() || items.Has(Hammer);
        }

        public override bool CanEnter(List<Item> items) {
            return items.Has(MoonPearl) && World.CanEnter("Dark World Death Mountain East", items);
                /*&& Crystal1 && Crystal2 && Crystal3 && Crystal4 && Crystal5 && Crystal6 && Crystal7
                  && DefeatMotherBrain*/
        }

    }

}
