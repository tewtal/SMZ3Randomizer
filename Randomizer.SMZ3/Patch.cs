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
        readonly Random rnd;
        StringTable stringTable;
        List<(int, byte[])> patches;

        #region Whishing Well room data

        static readonly byte[] wishingWellRoomData = Convert.FromBase64String("4QAQrA0pmgFYmA8RsWH8TYEg2gIs4WH8voFhsWJU2gL9jYNE4WL9HoMxpckxpGkxwCJNpGkxxvlJxvkQmaBcmaILmGAN6MBV6MALkgBzmGD+aQCYo2H+a4H+q4WpyGH+roH/aQLYo2L/a4P/K4fJyGL/LoP+oQCqIWH+poH/IQLKIWL/JoO7I/rDI/q7K/rDK/q7U/rDU/qwoD2YE8CYUsCIAGCQAGDoAGDwAGCYysDYysDYE8DYUsD8vYX9HYf/////8P+ALmEOgQ7//w==");

        #endregion

        public Patch(World myWorld, List<World> allWorlds, string seedGuid, Random rnd) {
            this.myWorld = myWorld;
            this.allWorlds = allWorlds;
            this.seedGuid = seedGuid;
            this.rnd = rnd;
        }

        public Dictionary<int, byte[]> Create(Config config) {
            stringTable = new StringTable();
            patches = new List<(int, byte[])>();

            WriteMedallions();
            WriteRewards();
            WriteDungeonMusic(config.Keysanity);

            WriteWishingWellRoomData();
            WriteWishingWellChests();
            WritePyramidFairyChests();

            WriteDiggingGameRng();

            WritePrizeShuffle(config.Difficulty);

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

            WriteSMLocations(myWorld.Regions.OfType<SMRegion>().SelectMany(x => x.Locations));
            WriteZ3Locations(myWorld.Regions.OfType<Z3Region>().SelectMany(x => x.Locations));

            WriteStringTable();

            WritePlayerNames();
            WriteSeedData();

            return patches.ToDictionary(x => x.Item1, x => x.Item2);
        }

        void WriteMedallions() {
            var turtleRock = myWorld.Regions.OfType<TurtleRock>().First();
            var miseryMire = myWorld.Regions.OfType<MiseryMire>().First();

            var turtleRockAddresses = new int[] { 0x180023, 0x5020, 0x50FF, 0x51DE };
            var miseryMireAddresses = new int[] { 0x180022, 0x4FF2, 0x50D1, 0x51B0 };

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

            patches.AddRange(turtleRockAddresses.Zip(turtleRockValues, (i, b) => (Z3Snes(i), new byte[] { b })));
            patches.AddRange(miseryMireAddresses.Zip(miseryMireValues, (i, b) => (Z3Snes(i), new byte[] { b })));
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
            return associations.SelectMany(x => x.a.Zip(x.v, (i, b) => (Z3Snes(i), new byte[] { b })));
        }

        int[] RewardAddresses(IReward region) {
            return region switch {
                EasternPalace _ => new[] { 0x1209D, 0x53EF8, 0x53EF9, 0x180052, 0x18007C, 0xC6FE },
                DesertPalace _ => new[] { 0x1209E, 0x53F1C, 0x53F1D, 0x180053, 0x180078, 0xC6FF },
                TowerOfHera _ => new[] { 0x120A5, 0x53F0A, 0x53F0B, 0x18005A, 0x18007A, 0xC706 },
                PalaceOfDarkness _ => new[] { 0x120A1, 0x53F00, 0x53F01, 0x180056, 0x18007D, 0xC702 },
                SwampPalace _ => new[] { 0x120A0, 0x53F6C, 0x53F6D, 0x180055, 0x180071, 0xC701 },
                SkullWoods _ => new[] { 0x120A3, 0x53F12, 0x53F13, 0x180058, 0x18007B, 0xC704 },
                ThievesTown _ => new[] { 0x120A6, 0x53F36, 0x53F37, 0x18005B, 0x180077, 0xC707 },
                IcePalace _ => new[] { 0x120A4, 0x53F5A, 0x53F5B, 0x180059, 0x180073, 0xC705 },
                MiseryMire _ => new[] { 0x120A2, 0x53F48, 0x53F49, 0x180057, 0x180075, 0xC703 },
                TurtleRock _ => new[] { 0x120A7, 0x53F24, 0x53F25, 0x18005C, 0x180079, 0xC708 },
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
                var locationValue = location.Type switch {
                    LocationType.Visible => UshortBytes(0xEFE0),
                    LocationType.Chozo => UshortBytes(0xEFE4),
                    LocationType.Hidden => UshortBytes(0xEFE8),
                    var x => throw new InvalidOperationException($"Location {location.Name} should not have the type {x}")
                };

                patches.Add((SMSnes(location.Address), locationValue));
                patches.Add(ItemTablePatch(location, (byte)location.Item.Type));
            }
        }

        void WriteZ3Locations(IEnumerable<Location> locations) {
            foreach (var location in locations) {
                if (location.Type == LocationType.HeraStandingKey) {
                    patches.Add((Z3Snes(0x4E3BB), location.Item.Type == KeyTH ? new byte[] { 0xE4 } : new byte[] { 0xEB }));
                } else if (new[] { LocationType.Pedestal, LocationType.Ether, LocationType.Bombos }.Contains(location.Type)) {
                    var text = Texts.ItemTextbox(location.Item);
                    var dialog = Dialog.Simple(text);
                    if (location.Type == LocationType.Pedestal) {
                        stringTable.SetPedestalText(text);
                        patches.Add((Z3Snes(0x180300), dialog));
                    }
                    else if (location.Type == LocationType.Ether) {
                        stringTable.SetEtherText(text);
                        patches.Add((Z3Snes(0x180F00), dialog));
                    }
                    else if (location.Type == LocationType.Bombos) {
                        stringTable.SetBombosText(text);
                        patches.Add((Z3Snes(0x181000), dialog));
                    }
                }
                patches.Add((Z3Snes(location.Address), new byte[] { (byte)(location.Id - 256) }));
                patches.Add(ItemTablePatch(location, GetZ3ItemId(location.Item.Type)));
            }
        }

        byte GetZ3ItemId(ItemType item) {
            var value = (int)item switch {
                var id when id >= 0x72 && id <= 0x7F => 0x33,
                var id when id >= 0x82 && id <= 0x8D => 0x25,
                var id when id >= 0x92 && id <= 0x9D => 0x32,
                var id when id >= 0xA0 && id <= 0xAD => 0x24,
                var id => id,
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
            return associations.SelectMany(x => x.a.Select(i => (Z3Snes(i), new byte[] { x.b })));
        }

        int[] MusicAddresses(IReward region) {
            return region switch {
                EasternPalace _ => new[] { 0x1559A },
                DesertPalace _ => new[] { 0x1559B, 0x1559C, 0x1559D, 0x1559E },
                TowerOfHera _ => new[] { 0x155C5, 0x1107A, 0x10B8C },
                PalaceOfDarkness _ => new[] { 0x155B8 },
                SwampPalace _ => new[] { 0x155B7 },
                SkullWoods _ => new[] { 0x155BA, 0x155BB, 0x155BC, 0x155BD, 0x15608, 0x15609, 0x1560A, 0x1560B },
                ThievesTown _ => new[] { 0x155C6 },
                IcePalace _ => new[] { 0x155BF },
                MiseryMire _ => new[] { 0x155B9 },
                TurtleRock _ => new[] { 0x155C7, 0x155A7, 0x155AA, 0x155AB },
                var x => throw new InvalidOperationException($"Region {x} should not be a dungeon music region")
            };
        }

        void WritePrizeShuffle(Difficulty difficulty) {
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

            /* Hard+ does not allow fairy or full magic */
            if (difficulty >= Difficulty.Hard) {
                pool = pool.Select(prize =>
                    prize == FullMagic ? Magic :
                    prize == Fairy ? Heart : prize);
            }

            var prizes = pool.ToList().Shuffle(rnd).Cast<byte>();

            /* prize pack drop order */
            (bytes, prizes) = prizes.SplitOff(prizePackItems);
            patches.Add((Z3Snes(0x37A78), bytes.ToArray()));

            /* tree pull prizes */
            (bytes, prizes) = prizes.SplitOff(treePullItems);
            patches.Add((Z3Snes(0xEFBD4), bytes.ToArray()));

            /* crab prizes */
            (drop, final, prizes) = prizes;
            patches.Add((Z3Snes(0x329C8), new[] { drop }));
            patches.Add((Z3Snes(0x329C4), new[] { final }));

            /* stun prize */
            (drop, prizes) = prizes;
            patches.Add((Z3Snes(0x37993), new[] { drop }));

            /* fish prize */
            (drop, _) = prizes;
            patches.Add((Z3Snes(0xE82CC), new[] { drop }));

            patches.AddRange(EnemyPrizePackDistribution());

            /* Pack drop chance */
            /* 0 => 100%, 1 => 50%, 3 => 25% */
            const int nrPacks = 7;
            var p = difficulty switch {
                Difficulty.Easy => (byte)0,
                Difficulty.Normal => (byte)1,
                Difficulty.Hard => (byte)1,
                _ => (byte)3,
            };
            patches.Add((Z3Snes(0x37A62), Repeat(p, nrPacks).ToArray()));
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

            return patches.Select(x => (Z3Snes(x.offset), x.bytes));
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
            const int offset = 0x6B632;
            var patches = new[] {
                /* sprite_prep */
                (0x3088D, new byte[] { 0x00 }), // Keese DW
                (0x308A8, new byte[] { 0x00 }), // Rope
                (0x30967, new byte[] { 0x00, 0x00 }), // Crow/Dacto
                (0x31125, new byte[] { 0x00, 0x00 }), // Red/Blue Hardhat Bettle
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

        void WriteStringTable() {
            patches.Add((Z3Snes(0xE0000), stringTable.GetPaddedBytes()));
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
            patches.Add((SMSnes(0xC07F50), UshortBytes(myWorld.Id)));
            /* Seed configuration bitfield */
            patches.Add((SMSnes(0xC07F52), UintBytes(0)));
            patches.Add((SMSnes(0xC07F60), AsAscii(seedGuid)));
            patches.Add((SMSnes(0xC07F80), AsAscii(myWorld.Guid)));
        }

        void WriteWishingWellRoomData() {
            patches.Add((Z3Snes(0x1F714), wishingWellRoomData));
        }

        void WriteWishingWellChests() {
            patches.Add((Z3Snes(0xE9AE), new byte[] { 0x14, 0x01 }));
            patches.Add((Z3Snes(0xE9CF), new byte[] { 0x14, 0x01 }));
        }

        void WritePyramidFairyChests() {
            patches.Add((Z3Snes(0x1FC16), new byte[] { 0xB1, 0xC6, 0xF9, 0xC9, 0xC6, 0xF9 }));
        }

        void WriteDiggingGameRng() {
            byte digs = (byte)(rnd.Next(30) + 1);
            patches.Add((Z3Snes(0x180020), new byte[] { digs }));
            patches.Add((Z3Snes(0xEFD95), new byte[] { digs }));
        }

        void WriteOpenModeFlags() {
            patches.AddRange(new[] {
                (Z3Snes(0x180032), new byte[] { 0x01 }),
                (Z3Snes(0x180038), new byte[] { 0x00 }),
                (Z3Snes(0x180039), new byte[] { 0x00 }),
                (Z3Snes(0x18003A), new byte[] { 0x00 }),
            });
        }

        // Removes Sword/Shield from Uncle by moving the tiles for
        // sword/shield to his head and replaces them with his head.
        void WriteRemoveEquipmentFromUncle(Item item) {
            if (item.Type != ProgressiveSword) {
                patches.AddRange(new[] {
                    (Z3Snes(0x6D263), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (Z3Snes(0x6D26B), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (Z3Snes(0x6D293), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (Z3Snes(0x6D29B), new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x00, 0x0E }),
                    (Z3Snes(0x6D2B3), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x02, 0x0E }),
                    (Z3Snes(0x6D2BB), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x02, 0x0E }),
                    (Z3Snes(0x6D2E3), new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x02, 0x0E }),
                    (Z3Snes(0x6D2EB), new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x02, 0x0E }),
                    (Z3Snes(0x6D31B), new byte[] { 0x00, 0x00, 0xE4, 0xFF, 0x08, 0x0E }),
                    (Z3Snes(0x6D323), new byte[] { 0x00, 0x00, 0xE4, 0xFF, 0x08, 0x0E }),
                });
            }
            if (item.Type != ProgressiveShield) {
                patches.AddRange(new[] {
                    (Z3Snes(0x6D253), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (Z3Snes(0x6D25B), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (Z3Snes(0x6D283), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (Z3Snes(0x6D28B), new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x00, 0x0E }),
                    (Z3Snes(0x6D2CB), new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x02, 0x0E }),
                    (Z3Snes(0x6D2FB), new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x02, 0x0E }),
                    (Z3Snes(0x6D313), new byte[] { 0x00, 0x00, 0xE4, 0xFF, 0x08, 0x0E }),
                });
            }
        }

        void WriteLockAgahnimDoorInEscape() {
            patches.Add((Z3Snes(0x180169), new byte[] { 0x01 }));
        }

        void WriteWishingWellUpgradeFalse() {
            patches.Add((Z3Snes(0x348DB), new byte[] { 0x2A }));
            patches.Add((Z3Snes(0x348EB), new byte[] { 0x05 }));
        }

        void WriteRestrictFairyPonds() {
            patches.Add((Z3Snes(0x18017E), new byte[] { 0x01 }));
        }

        void WriteGanonInvicible(GanonInvincible invincible) {
            var value = invincible switch {
                GanonInvincible.Never => 0x00,
                GanonInvincible.Always => 0x01,
                GanonInvincible.BeforeAllDungeons => 0x02,
                GanonInvincible.BeforeCrystals => 0x03,
                var x => throw new ArgumentException($"Unknown Ganon invincible value {x}", nameof(invincible))
            };
            patches.Add((Z3Snes(0x18003E), new byte[] { (byte)value }));
        }

        void WriteRngBlock() {
            patches.Add((Z3Snes(0x178000), Range(0, 1024).Select(x => (byte)rnd.Next(0x100)).ToArray()));
        }

        void WriteSmithyQuickItemGive() {
            patches.Add((Z3Snes(0x180029), new byte[] { 0x01 }));
        }

        void WriteSaveAndQuitFromBossRoom() {
            patches.Add((Z3Snes(0x180042), new byte[] { 0x01 }));
        }

        void WriteWorldOnAgahnimDeath() {
            patches.Add((Z3Snes(0x1800A3), new byte[] { 0x01 }));
        }

        int Z3Snes(int addr) {
            addr = addr switch {
                _ when addr < 0x170000 => LoSnesAsExHiPc(addr),
                /* Place $180000 access into ExHiROM bank 40 */
                _ when (addr & 0xFF0000) == 0x180000 => 0x400000 | (addr & 0xFFFF),
                /* Repoint RNG Block */
                _ when addr == 0x178000 => 0x420000,
                _ => throw new InvalidOperationException($"Unmapped Z3 snes address source ${addr:X}"),
            };
            if (addr > 0x600000)
                throw new InvalidOperationException($"Unmapped pc address target ${addr:X}");
            return addr;
        }

        int SMSnes(int addr) {
            addr = addr switch {
                _ when addr >= 0x800000 => LoSnesAsExHiPc(addr),
                _ => throw new InvalidOperationException($"Unmapped SM snes address source ${addr:X}"),
            };
            if (addr > 0x600000)
                throw new InvalidOperationException($"Unmapped pc address target ${addr:X}");
            return addr;
        }

        int LoSnesAsExHiPc(int addr) {
            var ex = (addr & 0x800000) == 0 ? 0x400000 : 0;
            var pc = ((addr << 1) & 0x3F0000) | 0x8000 | (addr & 0x7FFF);
            return ex | pc;
        }

        byte[] UintBytes(int value) => BitConverter.GetBytes((uint)value);

        byte[] UshortBytes(int value) => BitConverter.GetBytes((ushort)value);

        byte[] AsAscii(string text) => Encoding.ASCII.GetBytes(text);

    }

}
