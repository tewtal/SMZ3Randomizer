using System;
using System.Collections.Generic;
using System.Linq;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class GanonTower : Z3Region {

        public override string Name => "Ganon Tower";
        public override string Area => "Ganon Tower";

        public GanonTower(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyGT, BigKeyGT, MapGT, CompassGT };

            Locations = new List<Location> {
                new Location(this, 256+189, 0x180161, LocationType.Regular, "Ganon's Tower - Bob's Torch",
                    items => items.Boots),
                new Location(this, 256+190, 0xEAB8, LocationType.Regular, "Ganon's Tower - DMs Room - Top Left",
                    items => items.Hammer && items.Hookshot),
                new Location(this, 256+191, 0xEABB, LocationType.Regular, "Ganon's Tower - DMs Room - Top Right",
                    items => items.Hammer && items.Hookshot),
                new Location(this, 256+192, 0xEABE, LocationType.Regular, "Ganon's Tower - DMs Room - Bottom Left",
                    items => items.Hammer && items.Hookshot),
                new Location(this, 256+193, 0xEAC1, LocationType.Regular, "Ganon's Tower - DMs Room - Bottom Right",
                    items => items.Hammer && items.Hookshot),
                new Location(this, 256+194, 0xEAD3, LocationType.Regular, "Ganon's Tower - Map Chest",
                    items => items.Hammer && (items.Hookshot || items.Boots) && items.KeyGT >=
                        (new[] { BigKeyGT, KeyGT }.Contains(Locations.Get("Ganon's Tower - Map Chest").ItemType) ? 3 : 4))
                    .AlwaysAllow((item, items) => item.Type == KeyGT && items.KeyGT >= 3),
                new Location(this, 256+195, 0xEAD0, LocationType.Regular, "Ganon's Tower - Firesnake Room",
                    items => items.Hammer && items.Hookshot && items.KeyGT >= (new[] {
                            Locations.Get("Ganon's Tower - Randomizer Room - Top Right"),
                            Locations.Get("Ganon's Tower - Randomizer Room - Top Left"),
                            Locations.Get("Ganon's Tower - Randomizer Room - Bottom Left"),
                            Locations.Get("Ganon's Tower - Randomizer Room - Bottom Right")
                        }.Any(l => l.ItemType == BigKeyGT) ||
                        Locations.Get("Ganon's Tower - Firesnake Room").ItemType == KeyGT ? 2 : 3)),
                new Location(this, 256+196, 0xEAC4, LocationType.Regular, "Ganon's Tower - Randomizer Room - Top Left",
                    items => LeftSide(items, new[] {
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Right"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Left"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Right")
                    })),
                new Location(this, 256+197, 0xEAC7, LocationType.Regular, "Ganon's Tower - Randomizer Room - Top Right",
                    items => LeftSide(items, new[] {
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Left"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Left"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Right")
                    })),
                new Location(this, 256+198, 0xEACA, LocationType.Regular, "Ganon's Tower - Randomizer Room - Bottom Left",
                    items => LeftSide(items, new[] {
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Right"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Left"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Right")
                    })),
                new Location(this, 256+199, 0xEACD, LocationType.Regular, "Ganon's Tower - Randomizer Room - Bottom Right",
                    items => LeftSide(items, new[] {
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Right"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Top Left"),
                        Locations.Get("Ganon's Tower - Randomizer Room - Bottom Left")
                    })),
                new Location(this, 256+200, 0xEAD9, LocationType.Regular, "Ganon's Tower - Hope Room - Left"),
                new Location(this, 256+201, 0xEADC, LocationType.Regular, "Ganon's Tower - Hope Room - Right"),
                new Location(this, 256+202, 0xEAE2, LocationType.Regular, "Ganon's Tower - Tile Room",
                    items => items.Somaria),
                new Location(this, 256+203, 0xEAE5, LocationType.Regular, "Ganon's Tower - Compass Room - Top Left",
                    items => RightSide(items, new[] {
                        Locations.Get("Ganon's Tower - Compass Room - Top Right"),
                        Locations.Get("Ganon's Tower - Compass Room - Bottom Left"),
                        Locations.Get("Ganon's Tower - Compass Room - Bottom Right")
                    })),
                new Location(this, 256+204, 0xEAE8, LocationType.Regular, "Ganon's Tower - Compass Room - Top Right",
                    items => RightSide(items, new[] {
                        Locations.Get("Ganon's Tower - Compass Room - Top Left"),
                        Locations.Get("Ganon's Tower - Compass Room - Bottom Left"),
                        Locations.Get("Ganon's Tower - Compass Room - Bottom Right")
                    })),
                new Location(this, 256+205, 0xEAEB, LocationType.Regular, "Ganon's Tower - Compass Room - Bottom Left",
                    items => RightSide(items, new[] {
                        Locations.Get("Ganon's Tower - Compass Room - Top Right"),
                        Locations.Get("Ganon's Tower - Compass Room - Top Left"),
                        Locations.Get("Ganon's Tower - Compass Room - Bottom Right")
                    })),
                new Location(this, 256+206, 0xEAEE, LocationType.Regular, "Ganon's Tower - Compass Room - Bottom Right",
                    items => RightSide(items, new[] {
                        Locations.Get("Ganon's Tower - Compass Room - Top Right"),
                        Locations.Get("Ganon's Tower - Compass Room - Top Left"),
                        Locations.Get("Ganon's Tower - Compass Room - Bottom Left")
                    })),
                new Location(this, 256+207, 0xEADF, LocationType.Regular, "Ganon's Tower - Bob's Chest",
                    items => items.KeyGT >= 3 && (
                        items.Hammer && items.Hookshot ||
                        items.Somaria && items.Firerod)),
                new Location(this, 256+208, 0xEAD6, LocationType.Regular, "Ganon's Tower - Big Chest",
                    items => items.BigKeyGT && items.KeyGT >= 3 && (
                        items.Hammer && items.Hookshot ||
                        items.Somaria && items.Firerod))
                    .Allow((item, items) => item.Type != BigKeyGT),
                new Location(this, 256+209, 0xEAF1, LocationType.Regular, "Ganon's Tower - Big Key Chest", BigKeyRoom),
                new Location(this, 256+210, 0xEAF4, LocationType.Regular, "Ganon's Tower - Big Key Room - Left", BigKeyRoom),
                new Location(this, 256+211, 0xEAF7, LocationType.Regular, "Ganon's Tower - Big Key Room - Right", BigKeyRoom),
                new Location(this, 256+212, 0xEAFD, LocationType.Regular, "Ganon's Tower - Mini Helmasaur Room - Left", TowerAscend)
                    .Allow((item, items) => item.Type != BigKeyGT),
                new Location(this, 256+213, 0xEB00, LocationType.Regular, "Ganon's Tower - Mini Helmasaur Room - Right", TowerAscend)
                    .Allow((item, items) => item.Type != BigKeyGT),
                new Location(this, 256+214, 0xEB03, LocationType.Regular, "Ganon's Tower - Pre-Moldorm Chest", TowerAscend)
                    .Allow((item, items) => item.Type != BigKeyGT),
                new Location(this, 256+215, 0xEB06, LocationType.Regular, "Ganon's Tower - Moldorm Chest",
                    items => items.BigKeyGT && items.KeyGT >= 4 &&
                        items.Bow && items.CanLightTorches() &&
                        CanBeatMoldorm(items) && items.Hookshot)
                    .Allow((item, items) => new[] { KeyGT, BigKeyGT }.Contains(item.Type) == false),
            };
        }

        private bool LeftSide(Progression items, IList<Location> locations) {
            return items.Hammer && items.Hookshot && items.KeyGT >= (locations.Any(l => l.ItemType == BigKeyGT) ? 3 : 4);
        }

        private bool RightSide(Progression items, IList<Location> locations) {
            return items.Somaria && items.Firerod && items.KeyGT >= (locations.Any(l => l.ItemType == BigKeyGT) ? 3 : 4);
        }

        private bool BigKeyRoom(Progression items) {
            return items.KeyGT >= 3 && CanBeatArmos(items) 
                && (items.Hammer && items.Hookshot || items.Firerod && items.Somaria);
        }

        private bool TowerAscend(Progression items) {
            return items.BigKeyGT && items.KeyGT >= 3 && items.Bow && items.CanLightTorches();
        }   
            
        private bool CanBeatArmos(Progression items) {
            return items.Sword || items.Hammer || items.Bow ||
                items.CanExtendMagic(2) && (items.Somaria || items.Byrna) ||
                items.CanExtendMagic(4) && (items.Firerod || items.Icerod);
        }

        private bool CanBeatMoldorm(Progression items) {
            return items.Sword || items.Hammer;
        }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && World.CanEnter("Dark World Death Mountain East", items) &&
                World.CanAquireAll(items, new[] { CrystalBlue, CrystalRed, GoldenFourBoss });
        }

        public override bool CanFill(Item item) {
            if (Config.GameMode == GameMode.Multiworld) {
                if (item.World != World || item.Progression)
                    return false;
            }

            return base.CanFill(item);
        }

    }

}
