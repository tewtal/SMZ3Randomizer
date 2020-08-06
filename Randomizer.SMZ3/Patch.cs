using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Linq.Enumerable;
using Randomizer.SMZ3.Regions.Zelda;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.RewardType;
using static Randomizer.SMZ3.DropPrize;
using Randomizer.SMZ3.Text;

namespace Randomizer.SMZ3 {

    enum DropPrize : byte {
        Heart = 0xD8,
        Green = 0xD9,
        Blue = 0xDA,
        Red = 0xDB,
        Bomb1 = 0xDC,
        Bomb4 = 0xDD,
        Bomb8 = 0xDE,
        Magic = 0xDF,
        FullMagic = 0xE0,
        Arrow5 = 0xE1,
        Arrow10 = 0xE2,
        Fairy = 0xE3,
    }

    class Patch {

        readonly List<World> allWorlds;
        readonly World myWorld;
        readonly string seedGuid;
        readonly int seed;
        readonly Random rnd;
        StringTable stringTable;
        List<(int offset, byte[] bytes)> patches;

        public Patch(World myWorld, List<World> allWorlds, string seedGuid, int seed, Random rnd) {
            this.myWorld = myWorld;
            this.allWorlds = allWorlds;
            this.seedGuid = seedGuid;
            this.seed = seed;
            this.rnd = rnd;
        }

        public Dictionary<int, byte[]> Create(Config config) {
            stringTable = new StringTable();
            patches = new List<(int, byte[])>();

            WriteMedallions();
            WriteRewards();
            WriteDungeonMusic(config.Keysanity);

            WriteDiggingGameRng();

            WritePrizeShuffle();

            WriteOpenModeFlags();

            WriteRemoveEquipmentFromUncle(myWorld.Locations.Get("Link's Uncle").Item);

            WriteLockAgahnimDoorInEscape();
            WriteWishingWellUpgradeFalse();
            WriteRestrictFairyPonds();
            WriteGanonInvicible(config.GanonInvincible);
            WriteRngBlock();
            WriteSmithyQuickItemGive();

            WriteSaveAndQuitFromBossRoom();
            WriteWorldOnAgahnimDeath();

            WriteTexts(config);

            WriteSMLocations(myWorld.Regions.OfType<SMRegion>().SelectMany(x => x.Locations));
            WriteZ3Locations(myWorld.Regions.OfType<Z3Region>().SelectMany(x => x.Locations));

            WriteStringTable();

            WritePlayerNames();
            WriteSeedData();
            WriteGameTitle();
            WriteGameModeData();

            return patches.ToDictionary(x => x.offset, x => x.bytes);
        }

        void WriteMedallions() {
            var turtleRock = myWorld.Regions.OfType<TurtleRock>().First();
            var miseryMire = myWorld.Regions.OfType<MiseryMire>().First();

            var turtleRockAddresses = new int[] { 0x308023, 0xD020, 0xD0FF, 0xD1DE };
            var miseryMireAddresses = new int[] { 0x308022, 0xCFF2, 0xD0D1, 0xD1B0 };

            var turtleRockValues = turtleRock.Medallion switch {
                Bombos => new byte[] { 0x00, 0x51, 0x10, 0x00 },
                Ether => new byte[] { 0x01, 0x51, 0x18, 0x00 },
                Quake => new byte[] { 0x02, 0x14, 0xEF, 0xC4 },
                var x => throw new InvalidOperationException($"Tried using {x} in place of Turtle Rock medallion")
            };

            var miseryMireValues = miseryMire.Medallion switch {
                Bombos => new byte[] { 0x00, 0x51, 0x00, 0x00 },
                Ether => new byte[] { 0x01, 0x13, 0x9F, 0xF1 },
                Quake => new byte[] { 0x02, 0x51, 0x08, 0x00 },
                var x => throw new InvalidOperationException($"Tried using {x} in place of Misery Mire medallion")
            };

            patches.AddRange(turtleRockAddresses.Zip(turtleRockValues, (i, b) => (Snes(i), new byte[] { b })));
            patches.AddRange(miseryMireAddresses.Zip(miseryMireValues, (i, b) => (Snes(i), new byte[] { b })));
        }

        void WriteRewards() {
            var crystalsBlue = new[] { 1, 2, 3, 4, 7 }.Shuffle(rnd);
            var crystalsRed = new[] { 5, 6 }.Shuffle(rnd);
            var crystalRewards = crystalsBlue.Concat(crystalsRed);

            var pendantsGreen = new[] { 1 };
            var pendantsBlueRed = new[] { 2, 3 }.Shuffle(rnd);
            var pendantRewards = pendantsGreen.Concat(pendantsBlueRed);

            var regions = myWorld.Regions.OfType<IReward>();
            var crystalRegions = regions.Where(x => x.Reward == CrystalBlue).Concat(regions.Where(x => x.Reward == CrystalRed));
            var pendantRegions = regions.Where(x => x.Reward == PendantGreen).Concat(regions.Where(x => x.Reward == PendantNonGreen));

            patches.AddRange(RewardPatches(crystalRegions, crystalRewards, CrystalValues));
            patches.AddRange(RewardPatches(pendantRegions, pendantRewards, PendantValues));
        }

        IEnumerable<(int, byte[])> RewardPatches(IEnumerable<IReward> regions, IEnumerable<int> rewards, Func<int, byte[]> rewardValues) {
            var addresses = regions.Select(RewardAddresses);
            var values = rewards.Select(rewardValues);
            var associations = addresses.Zip(values, (a, v) => (a, v));
            return associations.SelectMany(x => x.a.Zip(x.v, (i, b) => (Snes(i), new byte[] { b })));
        }

        int[] RewardAddresses(IReward region) {
            return region switch {
                EasternPalace _ => new[] { 0x2A09D, 0xABEF8, 0xABEF9, 0x308052, 0x30807C, 0x1C6FE },
                DesertPalace _ => new[] { 0x2A09E, 0xABF1C, 0xABF1D, 0x308053, 0x308078, 0x1C6FF },
                TowerOfHera _ => new[] { 0x2A0A5, 0xABF0A, 0xABF0B, 0x30805A, 0x30807A, 0x1C706 },
                PalaceOfDarkness _ => new[] { 0x2A0A1, 0xABF00, 0xABF01, 0x308056, 0x30807D, 0x1C702 },
                SwampPalace _ => new[] { 0x2A0A0, 0xABF6C, 0xABF6D, 0x308055, 0x308071, 0x1C701 },
                SkullWoods _ => new[] { 0x2A0A3, 0xABF12, 0xABF13, 0x308058, 0x30807B, 0x1C704 },
                ThievesTown _ => new[] { 0x2A0A6, 0xABF36, 0xABF37, 0x30805B, 0x308077, 0x1C707 },
                IcePalace _ => new[] { 0x2A0A4, 0xABF5A, 0xABF5B, 0x308059, 0x308073, 0x1C705 },
                MiseryMire _ => new[] { 0x2A0A2, 0xABF48, 0xABF49, 0x308057, 0x308075, 0x1C703 },
                TurtleRock _ => new[] { 0x2A0A7, 0xABF24, 0xABF25, 0x30805C, 0x308079, 0x1C708 },
                var x => throw new InvalidOperationException($"Region {x} should not be a dungeon reward region")
            };
        }

        byte[] CrystalValues(int crystal) {
            return crystal switch {
                1 => new byte[] { 0x02, 0x34, 0x64, 0x40, 0x7F, 0x06 },
                2 => new byte[] { 0x10, 0x34, 0x64, 0x40, 0x79, 0x06 },
                3 => new byte[] { 0x40, 0x34, 0x64, 0x40, 0x6C, 0x06 },
                4 => new byte[] { 0x20, 0x34, 0x64, 0x40, 0x6D, 0x06 },
                5 => new byte[] { 0x04, 0x32, 0x64, 0x40, 0x6E, 0x06 },
                6 => new byte[] { 0x01, 0x32, 0x64, 0x40, 0x6F, 0x06 },
                7 => new byte[] { 0x08, 0x34, 0x64, 0x40, 0x7C, 0x06 },
                var x => throw new InvalidOperationException($"Tried using {x} as a crystal number")
            };
        }

        byte[] PendantValues(int pendant) {
            return pendant switch {
                1 => new byte[] { 0x04, 0x38, 0x62, 0x00, 0x69, 0x01 },
                2 => new byte[] { 0x01, 0x32, 0x60, 0x00, 0x69, 0x03 },
                3 => new byte[] { 0x02, 0x34, 0x60, 0x00, 0x69, 0x02 },
                var x => throw new InvalidOperationException($"Tried using {x} as a pendant number")
            };
        }

        void WriteSMLocations(IEnumerable<Location> locations) {
            foreach (var location in locations) {
                if (myWorld.Config.GameMode == GameMode.Multiworld) {
                    patches.Add((Snes(location.Address), UshortBytes(GetSMItemPLM(location))));
                    patches.Add(ItemTablePatch(location, GetZ3ItemId(location)));
                } else {
                    ushort plmId = GetSMItemPLM(location);
                    patches.Add((Snes(location.Address), UshortBytes(plmId)));
                    if (plmId >= 0xEFE0) {
                        patches.Add((Snes(location.Address + 5), new byte[] { GetZ3ItemId(location) }));
                    }
                }
            }
        }

        ushort GetSMItemPLM(Location location) {
            int plmId = myWorld.Config.GameMode == GameMode.Multiworld ?
                0xEFE0 :
                location.Item.Type switch {
                    ETank => 0xEED7,
                    Missile => 0xEEDB,
                    Super => 0xEEDF,
                    PowerBomb => 0xEEE3,
                    Bombs => 0xEEE7,
                    Charge => 0xEEEB,
                    Ice => 0xEEEF,
                    HiJump => 0xEEF3,
                    SpeedBooster => 0xEEF7,
                    Wave => 0xEEFB,
                    Spazer => 0xEEFF,
                    SpringBall => 0xEF03,
                    Varia => 0xEF07,
                    Plasma => 0xEF13,
                    Grapple => 0xEF17,
                    Morph => 0xEF23,
                    ReserveTank => 0xEF27,
                    Gravity => 0xEF0B,
                    XRay => 0xEF0F,
                    SpaceJump => 0xEF1B,
                    ScrewAttack => 0xEF1F,
                    _ => 0xEFE0,
                };

            plmId += plmId switch {
                0xEFE0 => location.Type switch {
                    LocationType.Chozo => 4,
                    LocationType.Hidden => 8,
                    _ => 0
                },
                _ => location.Type switch {
                    LocationType.Chozo => 0x54,
                    LocationType.Hidden => 0xA8,
                    _ => 0
                }
            };

            return (ushort)plmId;
        }

        void WriteZ3Locations(IEnumerable<Location> locations) {
            foreach (var location in locations) {
                if (location.Type == LocationType.HeraStandingKey) {
                    patches.Add((Snes(0x9E3BB), location.Item.Type == KeyTH ? new byte[] { 0xE4 } : new byte[] { 0xEB }));
                } else if (new[] { LocationType.Pedestal, LocationType.Ether, LocationType.Bombos }.Contains(location.Type)) {
                    var text = Texts.ItemTextbox(location.Item);
                    var dialog = Dialog.Simple(text);
                    if (location.Type == LocationType.Pedestal) {
                        stringTable.SetPedestalText(text);
                        patches.Add((Snes(0x308300), dialog));
                    }
                    else if (location.Type == LocationType.Ether) {
                        stringTable.SetEtherText(text);
                        patches.Add((Snes(0x308F00), dialog));
                    }
                    else if (location.Type == LocationType.Bombos) {
                        stringTable.SetBombosText(text);
                        patches.Add((Snes(0x309000), dialog));
                    }
                }

                if (myWorld.Config.GameMode == GameMode.Multiworld) {
                    patches.Add((Snes(location.Address), new byte[] { (byte)(location.Id - 256) }));
                    patches.Add(ItemTablePatch(location, GetZ3ItemId(location)));
                } else {
                    patches.Add((Snes(location.Address), new byte[] { GetZ3ItemId(location) }));
                }
            }
        }

        byte GetZ3ItemId(Location location) {
            var item = location.Item;
            var value = location.Type == LocationType.NotInDungeon ||
                !(item.IsDungeonItem && location.Region.IsRegionItem(item)) ? item.Type : item switch {
                    _ when item.IsKey => Key,
                    _ when item.IsBigKey => BigKey,
                    _ when item.IsMap => Map,
                    _ when item.IsCompass => Compass,
                    _ => throw new InvalidOperationException($"Tried replacing {item} with a dungeon region item"),
                };
            return (byte)value;
        }

        (int, byte[]) ItemTablePatch(Location location, byte itemId) {
            var type = location.Item.World == location.Region.World ? 0 : 1;
            var owner = location.Item.World.Id;
            return (0x386000 + (location.Id * 8), new[] { type, itemId, owner, 0 }.SelectMany(UshortBytes).ToArray());
        }

        void WriteDungeonMusic(bool keysanity) {
            var regions = myWorld.Regions.OfType<IReward>();
            IEnumerable<byte> music;
            if (keysanity) {
                regions = regions.Where(x => new[] { PendantGreen, PendantNonGreen, CrystalBlue, CrystalRed }.Contains(x.Reward));
                music = RandomDungeonMusic().Take(regions.Count());
            } else {
                var pendantRegions = regions.Where(x => new[] { PendantGreen, PendantNonGreen }.Contains(x.Reward));
                var crystalRegions = regions.Where(x => new[] { CrystalBlue, CrystalRed }.Contains(x.Reward));
                regions = pendantRegions.Concat(crystalRegions);
                music = new byte[] {
                    0x11, 0x11, 0x11, 0x16, 0x16,
                    0x16, 0x16, 0x16, 0x16, 0x16,
                };
            }
            patches.AddRange(MusicPatches(regions, music));
        }

        IEnumerable<byte> RandomDungeonMusic() {
            while (true) yield return rnd.Next(2) == 0 ? (byte)0x11 : (byte)0x16;
        }

        IEnumerable<(int, byte[])> MusicPatches(IEnumerable<IReward> regions, IEnumerable<byte> music) {
            var addresses = regions.Select(MusicAddresses);
            var associations = addresses.Zip(music, (a, b) => (a, b));
            return associations.SelectMany(x => x.a.Select(i => (Snes(i), new byte[] { x.b })));
        }

        int[] MusicAddresses(IReward region) {
            return region switch {
                EasternPalace _ => new[] { 0x2D59A },
                DesertPalace _ => new[] { 0x2D59B, 0x2D59C, 0x2D59D, 0x2D59E },
                TowerOfHera _ => new[] { 0x2D5C5, 0x2907A, 0x28B8C },
                PalaceOfDarkness _ => new[] { 0x2D5B8 },
                SwampPalace _ => new[] { 0x2D5B7 },
                SkullWoods _ => new[] { 0x2D5BA, 0x2D5BB, 0x2D5BC, 0x2D5BD, 0x2D608, 0x2D609, 0x2D60A, 0x2D60B },
                ThievesTown _ => new[] { 0x2D5C6 },
                IcePalace _ => new[] { 0x2D5BF },
                MiseryMire _ => new[] { 0x2D5B9 },
                TurtleRock _ => new[] { 0x2D5C7, 0x2D5A7, 0x2D5AA, 0x2D5AB },
                var x => throw new InvalidOperationException($"Region {x} should not be a dungeon music region"),
            };
        }

        void WritePrizeShuffle() {
            const int prizePackItems = 56;
            const int treePullItems = 3;

            IEnumerable<byte> bytes;
            byte drop, final;

            var pool = new DropPrize[] {
                Heart, Heart, Heart, Heart, Green, Heart, Heart, Green,         // pack 1
                Blue, Green, Blue, Red, Blue, Green, Blue, Blue,                // pack 2
                FullMagic, Magic, Magic, Blue, FullMagic, Magic, Heart, Magic,  // pack 3
                Bomb1, Bomb1, Bomb1, Bomb4, Bomb1, Bomb1, Bomb8, Bomb1,         // pack 4
                Arrow5, Heart, Arrow5, Arrow10, Arrow5, Heart, Arrow5, Arrow10, // pack 5
                Magic, Green, Heart, Arrow5, Magic, Bomb1, Green, Heart,        // pack 6
                Heart, Fairy, FullMagic, Red, Bomb8, Heart, Red, Arrow10,       // pack 7
                Green, Blue, Red, // from pull trees
                Green, Red, // from prize crab
                Green, // stunned prize
                Red, // saved fish prize
            }.AsEnumerable();

            var prizes = pool.Shuffle(rnd).Cast<byte>();

            /* prize pack drop order */
            (bytes, prizes) = prizes.SplitOff(prizePackItems);
            patches.Add((Snes(0x6FA78), bytes.ToArray()));

            /* tree pull prizes */
            (bytes, prizes) = prizes.SplitOff(treePullItems);
            patches.Add((Snes(0x1DFBD4), bytes.ToArray()));

            /* crab prizes */
            (drop, final, prizes) = prizes;
            patches.Add((Snes(0x6A9C8), new[] { drop }));
            patches.Add((Snes(0x6A9C4), new[] { final }));

            /* stun prize */
            (drop, prizes) = prizes;
            patches.Add((Snes(0x6F993), new[] { drop }));

            /* fish prize */
            (drop, _) = prizes;
            patches.Add((Snes(0x1D82CC), new[] { drop }));

            patches.AddRange(EnemyPrizePackDistribution());

            /* Pack drop chance */
            /* Normal difficulty is 50%. 0 => 100%, 1 => 50%, 3 => 25% */
            const int nrPacks = 7;
            const byte probability = 1;
            patches.Add((Snes(0x6FA62), Repeat(probability, nrPacks).ToArray()));
        }

        IEnumerable<(int, byte[])> EnemyPrizePackDistribution() {
            var (prizePacks, duplicatePacks) = EnemyPrizePacks();

            var n = prizePacks.Sum(x => x.bytes.Length);
            var randomization = PrizePackRandomization(n, 1);

            var patches = prizePacks.Select(x => {
                IEnumerable<byte> packs;
                (packs, randomization) = randomization.SplitOff(x.bytes.Length);
                return (x.offset, bytes: x.bytes.Zip(packs, (b, p) => (byte)(b | p)).ToArray());
            }).ToList();

            var duplicates =
                from d in duplicatePacks
                from p in patches
                where p.offset == d.src
                select (d.dest, p.bytes);
            patches.AddRange(duplicates.ToList());

            return patches.Select(x => (Snes(x.offset), x.bytes));
        }

        /* Guarantees at least s of each prize pack, over a total of n packs.
         * In each iteration, from the product n * m, use the guaranteed number
         * at k, where k is the "row" (integer division by m), when k falls
         * within the list boundary. Otherwise use the "column" (modulo by m)
         * as the random element.
         */
        IEnumerable<byte> PrizePackRandomization(int n, int s) {
            const int m = 7;
            var g = Repeat(Range(0, m), s).SelectMany(x => x).ToList();

            IEnumerable<int> randomization(int n) {
                n = m * n;
                while (n > 0) {
                    var r = rnd.Next(n);
                    var k = r / m;
                    yield return k < g.Count ? g[k] : r % m;
                    if (k < g.Count) g.RemoveAt(k);
                    n -= m;
                }
            }

            return randomization(n).Select(x => (byte)(x + 1)).ToList();
        }

        /* Todo: Deadrock turns into $8F Blob when powdered, but those "onion blobs" always drop prize pack 1. */
        (IList<(int offset, byte[] bytes)>, IList<(int src, int dest)>) EnemyPrizePacks() {
            const int offset = 0xDB632;
            var patches = new[] {
                /* sprite_prep */
                (0x6888D, new byte[] { 0x00 }), // Keese DW
                (0x688A8, new byte[] { 0x00 }), // Rope
                (0x68967, new byte[] { 0x00, 0x00 }), // Crow/Dacto
                (0x69125, new byte[] { 0x00, 0x00 }), // Red/Blue Hardhat Bettle
                /* sprite properties */
                (offset+0x01, new byte[] { 0x90 }), // Vulture
                (offset+0x08, new byte[] { 0x00 }), // Octorok (One Way)
                (offset+0x0A, new byte[] { 0x00 }), // Octorok (Four Way)
                (offset+0x0D, new byte[] { 0x80, 0x90 }), // Buzzblob, Snapdragon
                (offset+0x11, new byte[] { 0x90, 0x90, 0x00 }), // Hinox, Moblin, Mini Helmasaur
                (offset+0x18, new byte[] { 0x90, 0x90 }), // Mini Moldorm, Poe/Hyu
                (offset+0x20, new byte[] { 0x00 }), // Sluggula
                (offset+0x22, new byte[] { 0x80, 0x00, 0x00 }), // Ropa, Red Bari, Blue Bari
                // Blue Soldier/Tarus, Green Soldier, Red Spear Soldier
                // Blue Assault Soldier, Red Assault Spear Soldier/Tarus
                // Blue Archer, Green Archer
                // Red Javelin Soldier, Red Bush Javelin Soldier
                // Red Bomb Soldiers, Green Soldier Recruits,
                // Geldman, Toppo
                (offset+0x41, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x10, 0x90, 0x90, 0x80 }),
                (offset+0x4F, new byte[] { 0x80 }), // Popo 2
                (offset+0x51, new byte[] { 0x80 }), // Armos
                (offset+0x55, new byte[] { 0x00, 0x00 }), // Ku, Zora
                (offset+0x58, new byte[] { 0x90 }), // Crab
                (offset+0x64, new byte[] { 0x80 }), // Devalant (Shooter)
                (offset+0x6A, new byte[] { 0x90, 0x90 }), // Ball N' Chain Trooper, Cannon Soldier
                (offset+0x6D, new byte[] { 0x80, 0x80 }), // Rat/Buzz, (Stal)Rope
                (offset+0x71, new byte[] { 0x80 }), // Leever
                (offset+0x7C, new byte[] { 0x90 }), // Initially Floating Stal
                (offset+0x81, new byte[] { 0xC0 }), // Hover
                // Green Eyegore/Mimic, Red Eyegore/Mimic
                // Detached Stalfos Body, Kodongo
                (offset+0x83, new byte[] { 0x10, 0x10, 0x10, 0x00 }),
                (offset+0x8B, new byte[] { 0x10 }), // Gibdo
                (offset+0x8E, new byte[] { 0x00, 0x00 }), // Terrorpin, Blob
                (offset+0x91, new byte[] { 0x10 }), // Stalfos Knight
                (offset+0x99, new byte[] { 0x10 }), // Pengator
                (offset+0x9B, new byte[] { 0x10 }), // Wizzrobe
                // Blue Zazak, Red Zazak, Stalfos
                // Green Zirro, Blue Zirro, Pikit
                (offset+0xA5, new byte[] { 0x10, 0x10, 0x10, 0x80, 0x80, 0x80 }),
                (offset+0xC7, new byte[] { 0x10 }), // Hokku-Bokku
                (offset+0xC9, new byte[] { 0x10 }), // Tektite
                (offset+0xD0, new byte[] { 0x10 }), // Lynel
                (offset+0xD3, new byte[] { 0x00 }), // Stal
            };
            var duplicates = new[] {
                /* Popo2 -> Popo. Popo is not used in vanilla Z3, but we duplicate from Popo2 just to be sure */
                (offset + 0x4F, offset + 0x4E),
            };
            return (patches, duplicates);
        }

        void WriteTexts(Config config) {
            var regions = myWorld.Regions.OfType<IReward>();
            var greenPendantDungeon = regions.Where(x => x.Reward == PendantGreen).Cast<Region>().First();
            var redCrystalDungeons = regions.Where(x => x.Reward == CrystalRed).Cast<Region>();

            var sahasrahla = Texts.SahasrahlaReveal(greenPendantDungeon);
            patches.Add((Snes(0x308A00), Dialog.Simple(sahasrahla)));
            stringTable.SetSahasrahlaRevealText(sahasrahla);

            var bombShop = Texts.BombShopReveal(redCrystalDungeons);
            patches.Add((Snes(0x308E00), Dialog.Simple(bombShop)));
            stringTable.SetBombShopRevealText(bombShop);

            var blind = Texts.Blind(rnd);
            patches.Add((Snes(0x308800), Dialog.Simple(blind)));
            stringTable.SetBlindText(blind);

            var tavernMan = Texts.TavernMan(rnd);
            patches.Add((Snes(0x308C00), Dialog.Simple(tavernMan)));
            stringTable.SetTavernManText(tavernMan);

            var ganon = Texts.GanonFirstPhase(rnd);
            patches.Add((Snes(0x308600), Dialog.Simple(ganon)));
            stringTable.SetGanonFirstPhaseText(ganon);

            // Todo: Verify these two are correct if ganon invincible patch is ever added
            // ganon_fall_in_alt in v30
            var ganonFirstPhaseInvincible = "You think you\nare ready to\nface me?\n\nI will not die\n\nunless you\ncomplete your\ngoals. Dingus!";
            patches.Add((Snes(0x309100), Dialog.Simple(ganonFirstPhaseInvincible)));

            // ganon_phase_3_alt in v30
            var ganonThirdPhaseInvincible = "Got wax in\nyour ears?\nI cannot die!";
            patches.Add((Snes(0x309200), Dialog.Simple(ganonThirdPhaseInvincible)));
            // ---

            var silversLocation = allWorlds.SelectMany(world => world.Locations).Where(l => l.ItemIs(SilverArrows, myWorld)).First();
            var silvers = config.GameMode == GameMode.Multiworld ?
                Texts.GanonThirdPhaseMulti(silversLocation.Region, myWorld) :
                Texts.GanonThirdPhaseSingle(silversLocation.Region);
            patches.Add((Snes(0x308700), Dialog.Simple(silvers)));
            stringTable.SetGanonThirdPhaseText(silvers);

            var triforceRoom = Texts.TriforceRoom(rnd);
            patches.Add((Snes(0x308400), Dialog.Simple(triforceRoom)));
            stringTable.SetTriforceRoomText(triforceRoom);
        }

        void WriteStringTable() {
            patches.Add((Snes(0x1C8000), stringTable.GetPaddedBytes()));
        }

        void WritePlayerNames() {
            patches.AddRange(allWorlds.Select(world => (0x385000 + (world.Id * 16), PlayerNameBytes(world.Player))));
        }

        byte[] PlayerNameBytes(string name) {
            name = name.Length > 12 ? name[..12] : name;
            int padding = 12 - name.Length;
            if (padding > 0) {
                double pad = padding / 2.0;
                name = name.PadLeft(name.Length + (int)Math.Ceiling(pad));
                name = name.PadRight(name.Length + (int)Math.Floor(pad));
            }
            return AsAscii(name.ToUpper()).Concat(UintBytes(0)).ToArray();
        }

        void WriteSeedData() {
            var configField =
                ((myWorld.Config.GameMode == GameMode.Multiworld ? 1 : 0) << 12) |
                ((int)myWorld.Config.Z3Logic << 10) |
                ((int)myWorld.Config.SMLogic << 8) |
                (Randomizer.version.Major << 4) |
                (Randomizer.version.Minor << 0);

            patches.Add((Snes(0x80FF50), UshortBytes(myWorld.Id)));
            patches.Add((Snes(0x80FF52), UshortBytes(configField)));
            patches.Add((Snes(0x80FF54), UintBytes(seed)));
            /* Reserve the rest of the space for future use */
            patches.Add((Snes(0x80FF58), Repeat<byte>(0x00, 8).ToArray()));
            patches.Add((Snes(0x80FF60), AsAscii(seedGuid)));
            patches.Add((Snes(0x80FF80), AsAscii(myWorld.Guid)));
        }

        void WriteGameModeData() {
            if (myWorld.Config.GameMode == GameMode.Multiworld) {
                patches.Add((Snes(0xF47000), UshortBytes(0x0001)));
            }
        }

        void WriteGameTitle() {
            var z3Glitch = myWorld.Config.Z3Logic switch {
                Z3Logic.Nmg => "N",
                Z3Logic.Owg => "O",
                _ => "C",
            };
            var smGlitch = myWorld.Config.SMLogic switch {
                SMLogic.Normal => "N",
                SMLogic.Hard => "H",
                _ => "X",
            };
            var title = AsAscii($"ZSM{Randomizer.version}{z3Glitch}{smGlitch}{seed:X8}".PadRight(21)[..21]);
            patches.Add((Snes(0x00FFC0), title));
            patches.Add((Snes(0x80FFC0), title));
        }

        void WriteDiggingGameRng() {
            byte digs = (byte)(rnd.Next(30) + 1);
            patches.Add((Snes(0x308020), new byte[] { digs }));
            patches.Add((Snes(0x1DFD95), new byte[] { digs }));
        }

        void WriteOpenModeFlags() {
            patches.AddRange(new[] {
                (Snes(0x308032), new byte[] { 0x01 }),
                (Snes(0x308038), new byte[] { 0x00 }),
                (Snes(0x308039), new byte[] { 0x00 }),
                (Snes(0x30803A), new byte[] { 0x00 }),
            });
        }

        // Removes Sword/Shield from Uncle by moving the tiles for
        // sword/shield to his head and replaces them with his head.
        void WriteRemoveEquipmentFromUncle(Item item) {
            if (item.Type != ProgressiveSword) {
                patches.AddRange(new[] {
                    (Snes(0xDD263), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (Snes(0xDD26B), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (Snes(0xDD293), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (Snes(0xDD29B), new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x00, 0x0E }),
                    (Snes(0xDD2B3), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x02, 0x0E }),
                    (Snes(0xDD2BB), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x02, 0x0E }),
                    (Snes(0xDD2E3), new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x02, 0x0E }),
                    (Snes(0xDD2EB), new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x02, 0x0E }),
                    (Snes(0xDD31B), new byte[] { 0x00, 0x00, 0xE4, 0xFF, 0x08, 0x0E }),
                    (Snes(0xDD323), new byte[] { 0x00, 0x00, 0xE4, 0xFF, 0x08, 0x0E }),
                });
            }
            if (item.Type != ProgressiveShield) {
                patches.AddRange(new[] {
                    (Snes(0xDD253), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (Snes(0xDD25B), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (Snes(0xDD283), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (Snes(0xDD28B), new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x00, 0x0E }),
                    (Snes(0xDD2CB), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x02, 0x0E }),
                    (Snes(0xDD2FB), new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x02, 0x0E }),
                    (Snes(0xDD313), new byte[] { 0x00, 0x00, 0xE4, 0xFF, 0x08, 0x0E }),
                });
            }
        }

        void WriteLockAgahnimDoorInEscape() {
            patches.Add((Snes(0x308169), new byte[] { 0x01 }));
        }

        void WriteWishingWellUpgradeFalse() {
            patches.Add((Snes(0x6C8DB), new byte[] { 0x2A }));
            patches.Add((Snes(0x6C8EB), new byte[] { 0x05 }));
        }

        void WriteRestrictFairyPonds() {
            patches.Add((Snes(0x30817E), new byte[] { 0x01 }));
        }

        void WriteGanonInvicible(GanonInvincible invincible) {
            var value = invincible switch {
                GanonInvincible.Never => 0x00,
                GanonInvincible.Always => 0x01,
                GanonInvincible.BeforeAllDungeons => 0x02,
                GanonInvincible.BeforeCrystals => 0x03,
                var x => throw new ArgumentException($"Unknown Ganon invincible value {x}", nameof(invincible))
            };
            patches.Add((Snes(0x30803E), new byte[] { (byte)value }));
        }

        void WriteRngBlock() {
            /* Repoint RNG Block */
            patches.Add((0x420000, Range(0, 1024).Select(x => (byte)rnd.Next(0x100)).ToArray()));
        }

        void WriteSmithyQuickItemGive() {
            patches.Add((Snes(0x308029), new byte[] { 0x01 }));
        }

        void WriteSaveAndQuitFromBossRoom() {
            patches.Add((Snes(0x308042), new byte[] { 0x01 }));
        }

        void WriteWorldOnAgahnimDeath() {
            patches.Add((Snes(0x3080A3), new byte[] { 0x01 }));
        }

        int Snes(int addr) {
            addr = addr switch {
                /* Redirect hi bank $30 access into ExHiRom lo bank $40 */
                _ when (addr & 0xFF8000) == 0x308000 => 0x400000 | (addr & 0x7FFF),
                /* General case, add ExHi offset for banks < $80, and collapse mirroring */
                _ => (addr < 0x800000 ? 0x400000 : 0) | (addr & 0x3FFFFF),
            };
            if (addr > 0x600000)
                throw new InvalidOperationException($"Unmapped pc address target ${addr:X}");
            return addr;
        }

        byte[] UintBytes(int value) => BitConverter.GetBytes((uint)value);

        byte[] UshortBytes(int value) => BitConverter.GetBytes((ushort)value);

        byte[] AsAscii(string text) => Encoding.ASCII.GetBytes(text);

    }

}
