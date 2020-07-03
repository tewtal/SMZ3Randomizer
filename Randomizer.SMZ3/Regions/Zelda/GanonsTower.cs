using System.Collections.Generic;
using System.Linq;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class GanonsTower : Z3Region {

        public override string Name => "Ganon's Tower";

        public GanonsTower(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyGT, BigKeyGT, MapGT, CompassGT };

            Locations = new List<Location> {
                new Location(this, 256+189, 0x308161, LocationType.Regular, "Ganon's Tower - Bob's Torch",
                    items => items.Boots),
                new Location(this, 256+190, 0x1EAB8, LocationType.Regular, "Ganon's Tower - DMs Room - Top Left",
                    items => items.Hammer && items.Hookshot),
                new Location(this, 256+191, 0x1EABB, LocationType.Regular, "Ganon's Tower - DMs Room - Top Right",
                    items => items.Hammer && items.Hookshot),
                new Location(this, 256+192, 0x1EABE, LocationType.Regular, "Ganon's Tower - DMs Room - Bottom Left",
                    items => items.Hammer && items.Hookshot),
                new Location(this, 256+193, 0x1EAC1, LocationType.Regular, "Ganon's Tower - DMs Room - Bottom Right",
                    items => items.Hammer && items.Hookshot),
                new Location(this, 256+194, 0x1EAD3, LocationType.Regular, "Ganon's Tower - Map Chest",
                    items => items.Hammer && (items.Hookshot || items.Boots) && items.KeyGT >=
                        (new[] { BigKeyGT, KeyGT }.Any(type => GetLocation("Ganon's Tower - Map Chest").ItemIs(type, World)) ? 3 : 4))
                    .AlwaysAllow((item, items) => item.Is(KeyGT, World) && items.KeyGT >= 3),
                new Location(this, 256+195, 0x1EAD0, LocationType.Regular, "Ganon's Tower - Firesnake Room",
                    items => items.Hammer && items.Hookshot && items.KeyGT >= (new[] {
                            GetLocation("Ganon's Tower - Randomizer Room - Top Right"),
                            GetLocation("Ganon's Tower - Randomizer Room - Top Left"),
                            GetLocation("Ganon's Tower - Randomizer Room - Bottom Left"),
                            GetLocation("Ganon's Tower - Randomizer Room - Bottom Right")
                        }.Any(l => l.ItemIs(BigKeyGT, World)) ||
                        GetLocation("Ganon's Tower - Firesnake Room").ItemIs(KeyGT, World) ? 2 : 3)),
                new Location(this, 256+196, 0x1EAC4, LocationType.Regular, "Ganon's Tower - Randomizer Room - Top Left",
                    items => LeftSide(items, new[] {
                        GetLocation("Ganon's Tower - Randomizer Room - Top Right"),
                        GetLocation("Ganon's Tower - Randomizer Room - Bottom Left"),
                        GetLocation("Ganon's Tower - Randomizer Room - Bottom Right")
                    })),
                new Location(this, 256+197, 0x1EAC7, LocationType.Regular, "Ganon's Tower - Randomizer Room - Top Right",
                    items => LeftSide(items, new[] {
                        GetLocation("Ganon's Tower - Randomizer Room - Top Left"),
                        GetLocation("Ganon's Tower - Randomizer Room - Bottom Left"),
                        GetLocation("Ganon's Tower - Randomizer Room - Bottom Right")
                    })),
                new Location(this, 256+198, 0x1EACA, LocationType.Regular, "Ganon's Tower - Randomizer Room - Bottom Left",
                    items => LeftSide(items, new[] {
                        GetLocation("Ganon's Tower - Randomizer Room - Top Right"),
                        GetLocation("Ganon's Tower - Randomizer Room - Top Left"),
                        GetLocation("Ganon's Tower - Randomizer Room - Bottom Right")
                    })),
                new Location(this, 256+199, 0x1EACD, LocationType.Regular, "Ganon's Tower - Randomizer Room - Bottom Right",
                    items => LeftSide(items, new[] {
                        GetLocation("Ganon's Tower - Randomizer Room - Top Right"),
                        GetLocation("Ganon's Tower - Randomizer Room - Top Left"),
                        GetLocation("Ganon's Tower - Randomizer Room - Bottom Left")
                    })),
                new Location(this, 256+200, 0x1EAD9, LocationType.Regular, "Ganon's Tower - Hope Room - Left"),
                new Location(this, 256+201, 0x1EADC, LocationType.Regular, "Ganon's Tower - Hope Room - Right"),
                new Location(this, 256+202, 0x1EAE2, LocationType.Regular, "Ganon's Tower - Tile Room",
                    items => items.Somaria),
                new Location(this, 256+203, 0x1EAE5, LocationType.Regular, "Ganon's Tower - Compass Room - Top Left",
                    items => RightSide(items, new[] {
                        GetLocation("Ganon's Tower - Compass Room - Top Right"),
                        GetLocation("Ganon's Tower - Compass Room - Bottom Left"),
                        GetLocation("Ganon's Tower - Compass Room - Bottom Right")
                    })),
                new Location(this, 256+204, 0x1EAE8, LocationType.Regular, "Ganon's Tower - Compass Room - Top Right",
                    items => RightSide(items, new[] {
                        GetLocation("Ganon's Tower - Compass Room - Top Left"),
                        GetLocation("Ganon's Tower - Compass Room - Bottom Left"),
                        GetLocation("Ganon's Tower - Compass Room - Bottom Right")
                    })),
                new Location(this, 256+205, 0x1EAEB, LocationType.Regular, "Ganon's Tower - Compass Room - Bottom Left",
                    items => RightSide(items, new[] {
                        GetLocation("Ganon's Tower - Compass Room - Top Right"),
                        GetLocation("Ganon's Tower - Compass Room - Top Left"),
                        GetLocation("Ganon's Tower - Compass Room - Bottom Right")
                    })),
                new Location(this, 256+206, 0x1EAEE, LocationType.Regular, "Ganon's Tower - Compass Room - Bottom Right",
                    items => RightSide(items, new[] {
                        GetLocation("Ganon's Tower - Compass Room - Top Right"),
                        GetLocation("Ganon's Tower - Compass Room - Top Left"),
                        GetLocation("Ganon's Tower - Compass Room - Bottom Left")
                    })),
                new Location(this, 256+207, 0x1EADF, LocationType.Regular, "Ganon's Tower - Bob's Chest",
                    items => items.KeyGT >= 3 && (
                        items.Hammer && items.Hookshot ||
                        items.Somaria && items.Firerod)),
                new Location(this, 256+208, 0x1EAD6, LocationType.Regular, "Ganon's Tower - Big Chest",
                    items => items.BigKeyGT && items.KeyGT >= 3 && (
                        items.Hammer && items.Hookshot ||
                        items.Somaria && items.Firerod))
                    .Allow((item, items) => item.IsNot(BigKeyGT, World)),
                new Location(this, 256+209, 0x1EAF1, LocationType.Regular, "Ganon's Tower - Big Key Chest", BigKeyRoom),
                new Location(this, 256+210, 0x1EAF4, LocationType.Regular, "Ganon's Tower - Big Key Room - Left", BigKeyRoom),
                new Location(this, 256+211, 0x1EAF7, LocationType.Regular, "Ganon's Tower - Big Key Room - Right", BigKeyRoom),
                new Location(this, 256+212, 0x1EAFD, LocationType.Regular, "Ganon's Tower - Mini Helmasaur Room - Left", TowerAscend)
                    .Allow((item, items) => item.IsNot(BigKeyGT, World)),
                new Location(this, 256+213, 0x1EB00, LocationType.Regular, "Ganon's Tower - Mini Helmasaur Room - Right", TowerAscend)
                    .Allow((item, items) => item.IsNot(BigKeyGT, World)),
                new Location(this, 256+214, 0x1EB03, LocationType.Regular, "Ganon's Tower - Pre-Moldorm Chest", TowerAscend)
                    .Allow((item, items) => item.IsNot(BigKeyGT, World)),
                new Location(this, 256+215, 0x1EB06, LocationType.Regular, "Ganon's Tower - Moldorm Chest",
                    items => items.BigKeyGT && items.KeyGT >= 4 &&
                        items.Bow && items.CanLightTorches() &&
                        CanBeatMoldorm(items) && items.Hookshot)
                    .Allow((item, items) => new[] { KeyGT, BigKeyGT }.All(type => item.IsNot(type, World))),
            };
        }

        private bool LeftSide(Progression items, IList<Location> locations) {
            return items.Hammer && items.Hookshot && items.KeyGT >= (locations.Any(l => l.ItemIs(BigKeyGT, World)) ? 3 : 4);
        }

        private bool RightSide(Progression items, IList<Location> locations) {
            return items.Somaria && items.Firerod && items.KeyGT >= (locations.Any(l => l.ItemIs(BigKeyGT, World)) ? 3 : 4);
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

        public override bool CanFill(Item item, Progression items) {
            if (Config.MultiWorld) {
                if (item.World != World || item.Progression) {
                    return false;
                }

                if (Config.Keysanity && !((item.Type == BigKeyGT || item.Type == KeyGT) && item.World == World) && (item.IsKey || item.IsBigKey || item.IsKeycard)) {
                    return false;
                }
            }

            return base.CanFill(item, items);
        }

    }

}
