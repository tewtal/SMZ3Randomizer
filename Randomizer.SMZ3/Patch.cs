using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Randomizer.SMZ3.Regions.Zelda;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.RewardType;
using Randomizer.SMZ3.Text;

namespace Randomizer.SMZ3 {

    class Patch {

        readonly List<World> allWorlds;
        readonly World myWorld;
        readonly string myWorldGuid;
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
            myWorldGuid = myWorld.Guid.Replace("-", "");
            this.seedGuid = seedGuid.Replace("-", "");
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

            patches = RemapComboOffsets(patches);

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

            patches.AddRange(turtleRockAddresses.Zip(turtleRockValues, (i, b) => (i, new byte[] { b })));
            patches.AddRange(miseryMireAddresses.Zip(miseryMireValues, (i, b) => (i, new byte[] { b })));
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
            return associations.SelectMany(x => x.a.Zip(x.v, (i, b) => (i, new byte[] { b })));
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

                patches.Add((0x80000 + location.Address, locationValue));
                patches.Add(ItemTablePatch(location, (byte)location.Item.Type));
            }
        }

        void WriteZ3Locations(IEnumerable<Location> locations) {
            foreach (var location in locations) {
                if (location.Type == LocationType.HeraStandingKey) {
                    patches.Add((ComboOffset(0x4E3BB), location.Item.Type == KeyTH ? new byte[] { 0xE4 } : new byte[] { 0xEB }));
                } else if (new[] { LocationType.Pedestal, LocationType.Ether, LocationType.Bombos }.Contains(location.Type)) {
                    var text = Texts.ItemTextbox(location.Item);
                    var dialog = Dialog.Simple(text);
                    if (location.Type == LocationType.Pedestal) {
                        stringTable.SetPedestalText(text);
                        patches.Add((ComboOffset(0x180300), dialog));
                    }
                    else if (location.Type == LocationType.Ether) {
                        stringTable.SetEtherText(text);
                        patches.Add((ComboOffset(0x180F00), dialog));
                    }
                    else if (location.Type == LocationType.Bombos) {
                        stringTable.SetBombosText(text);
                        patches.Add((ComboOffset(0x181000), dialog));
                    }
                }
                patches.Add((ComboOffset(location.Address), new byte[] { (byte)(location.Id - 256) }));
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
            var extra = 0;
            return (0x386000 + (location.Id * 8), new[] { type, itemId, owner, extra }.SelectMany(UshortBytes).ToArray());
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
            return associations.SelectMany(x => x.a.Select(i => (i, new byte[] { x.b })));
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

        void WriteStringTable() {
            patches.Add((ComboOffset(0xE0000), stringTable.GetPaddedBytes()));
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
            return AsciiBytes(name.ToUpper()).Concat(new byte[] { 0x00, 0x00, 0x00, 0x00 }).ToArray();
        }

        void WriteSeedData() {
            patches.Add((0x00FF50, UintBytes(myWorld.Id)));
            /* Seed configuration bitfield */
            patches.Add((0x00FF52, UintBytes(0)));
            patches.Add((0x00FF60, AsciiBytes(seedGuid)));
            patches.Add((0x00FF80, AsciiBytes(myWorldGuid)));
        }

        void WriteWishingWellRoomData() {
            patches.Add((0x1F714, wishingWellRoomData));
        }

        void WriteWishingWellChests() {
            patches.Add((0xE9AE, new byte[] { 0x14, 0x01 }));
            patches.Add((0xE9CF, new byte[] { 0x14, 0x01 }));
        }

        void WritePyramidFairyChests() {
            patches.Add((0x1FC16, new byte[] { 0xB1, 0xC6, 0xF9, 0xC9, 0xC6, 0xF9 }));
        }

        void WriteDiggingGameRng() {
            byte digs = (byte)(rnd.Next(30) + 1);
            patches.Add((0x180020, new byte[] { digs }));
            patches.Add((0xEFD95, new byte[] { digs }));
        }

        void WriteOpenModeFlags() {
            patches.AddRange(new[] {
                (0x180032, new byte[] { 0x01 }),
                (0x180038, new byte[] { 0x00 }),
                (0x180039, new byte[] { 0x00 }),
                (0x18003A, new byte[] { 0x00 }),
            });
        }

        // Removes Sword/Shield from Uncle by moving the tiles for
        // sword/shield to his head and replaces them with his head.
        void WriteRemoveEquipmentFromUncle(Item item) {
            if (item.Type != ProgressiveSword) {
                patches.AddRange(new[] {
                    (0x6D263, new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (0x6D26B, new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (0x6D293, new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (0x6D29B, new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x00, 0x0E }),
                    (0x6D2B3, new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x02, 0x0E }),
                    (0x6D2BB, new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x02, 0x0E }),
                    (0x6D2E3, new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x02, 0x0E }),
                    (0x6D2EB, new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x02, 0x0E }),
                    (0x6D31B, new byte[] { 0x00, 0x00, 0xE4, 0xFF, 0x08, 0x0E }),
                    (0x6D323, new byte[] { 0x00, 0x00, 0xE4, 0xFF, 0x08, 0x0E }),
                });
            }
            if (item.Type != ProgressiveShield) {
                patches.AddRange(new[] {
                    (0x6D253, new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (0x6D25B, new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (0x6D283, new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x00, 0x0E }),
                    (0x6D28B, new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x00, 0x0E }),
                    (0x6D2CB, new byte[] { 0x00, 0x00, 0xF6, 0xFF, 0x02, 0x0E }),
                    (0x6D2FB, new byte[] { 0x00, 0x00, 0xF7, 0xFF, 0x02, 0x0E }),
                    (0x6D313, new byte[] { 0x00, 0x00, 0xE4, 0xFF, 0x08, 0x0E }),
                });
            }
        }

        void WriteLockAgahnimDoorInEscape() {
            patches.Add((0x180169, new byte[] { 0x01 }));
        }

        void WriteWishingWellUpgradeFalse() {
            patches.Add((0x348DB, new byte[] { 0x2A }));
            patches.Add((0x348EB, new byte[] { 0x05 }));
        }

        void WriteRestrictFairyPonds() {
            patches.Add((0x18017E, new byte[] { 0x01 }));
        }

        void WriteGanonInvicible(GanonInvincible invincible) {
            var value = invincible switch {
                GanonInvincible.Never => 0x00,
                GanonInvincible.Always => 0x01,
                GanonInvincible.BeforeAllDungeons => 0x02,
                GanonInvincible.BeforeCrystals => 0x03,
                var x => throw new ArgumentException($"Unknown Ganon invincible value {x}", nameof(invincible))
            };
            patches.Add((0x18003E, new byte[] { (byte)value }));
        }

        void WriteRngBlock() {
            patches.Add((0x178000, Enumerable.Range(0, 1024).Select(x => (byte)rnd.Next(0x100)).ToArray()));
        }

        void WriteSmithyQuickItemGive() {
            patches.Add((0x180029, new byte[] { 0x01 }));
        }

        void WriteSaveAndQuitFromBossRoom() {
            patches.Add((0x180042, new byte[] { 0x01 }));
        }

        void WriteWorldOnAgahnimDeath() {
            patches.Add((0x1800A3, new byte[] { 0x01 }));
        }

        List<(int, byte[])> RemapComboOffsets(List<(int, byte[])> patches) {
            return patches.Select(x => (ComboOffset(x.Item1), x.Item2)).ToList();
        }

        int ComboOffset(int offset) {
            offset = offset switch {
                /* Convert LoROM to HiROM mapping and then apply the ExHiROM offset */
                _ when offset < 0x170000 =>
                    0x400000 + offset + (0x8000 * ((int)Math.Floor((decimal)offset / 0x8000) + 1)),
                /* Change 0x180000 access into ExHiROM bank 40 */
                _ when (offset & 0xff0000) == 0x180000 =>
                    0x400000 + (offset & 0x00ffff),
                /* Repoint RNG Block */
                _ when offset == 0x178000 => 0x420000,
                /* SM ExHiROM Header */
                _ when (offset & 0xff0000) == 0xff0000 => offset & 0x00ffff,
                _ => throw new InvalidOperationException($"Unmapped combo offset source {offset}"),
            };
            if (offset > 0x600000)
                throw new InvalidOperationException($"Unmapped combo offset target {offset}");
            return offset;
        }

        byte[] UintBytes(int value) {
            return BitConverter.GetBytes((uint)value);
        }

        byte[] UshortBytes(int value) {
            return BitConverter.GetBytes((ushort)value);
        }

        byte[] AsciiBytes(string s) {
            return Encoding.ASCII.GetBytes(s);
        }

    }

}
