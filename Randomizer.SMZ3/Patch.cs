using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Randomizer.SMZ3.Regions.Zelda;

namespace Randomizer.SMZ3 {

    class Patch {

        private readonly World myWorld;
        private readonly List<World> allWorlds;
        private readonly string seedGuid;
        private readonly Random rnd;

        public Patch(World myWorld, List<World> allWorlds, string seedGuid, Random rnd) {
            this.myWorld = myWorld;
            this.allWorlds = allWorlds;
            this.seedGuid = seedGuid;
            this.rnd = rnd;
        }

        public Dictionary<int, byte[]> Create() {
            var patches = new Dictionary<int, byte[]>();

            /* Write Medallions to patch list */
            WriteMedallions(ref patches);

            /* Write Rewards to patch list */
            WriteRewards(ref patches);

            /* Write new chests room data */
            WriteWishingWellChests(ref patches);
            WritePyramidFairyChets(ref patches);

            /* Write digging game RNG */
            WriteDiggingGameRng(ref patches);

            /* Write open mode flags */
            WriteOpenMode(ref patches);

            /* setLockAgahnimDoorInEscape */
            patches.Add(0x180169, new byte[] { 0x01 });

            /* setWishingWellUpgrade = false */
            patches.Add(0x348DB, new byte[] { 0x2A });
            patches.Add(0x348EB, new byte[] { 0x05 });

            /* setRestrictFairyPonds */
            patches.Add(0x18017E, new byte[] { 0x01 });

            /* setGanonInvicible = Crystals */
            patches.Add(0x18003E, new byte[] { 0x03 });

            /* Write RNG Block */
            patches.Add(0x178000, Enumerable.Range(0, 1024).Select(x => (byte)rnd.Next(0x100 + 1)).ToArray());

            /* setSmithyQuickItemGive */
            patches.Add(0x180029, new byte[] { 0x01 });

            /* setSaveAndQuitFromBossRoom */
            patches.Add(0x180042, new byte[] { 0x01 });

            /* setWorldOnAgahnimDeath */
            patches.Add(0x1800A3, new byte[] { 0x01 });

            /* Remap patches to combo offsets */
            patches = Remap(patches);

            /* Write items to locations*/
            foreach (var location in myWorld.Locations) {
                switch (location.Region.Area) {
                    case "Crateria":
                    case "Brinstar":
                    case "Maridia":
                    case "Norfair Upper":
                    case "Norfair Lower":
                    case "Wrecked Ship":
                        WriteSMLocation(ref patches, location);
                        break;
                    default:
                        WriteZ3Location(ref patches, location);
                        break;
                }
            }

            /* Write player names into ROM */
            foreach (var world in allWorlds) {
                /* Pad name if needed */
                var name = world.Player.Length > 12 ? world.Player[..12] : world.Player;
                int padding = (12 - name.Length);
                if (padding > 0) {
                    double pad = padding / 2.0;
                    name = name.PadLeft(name.Length + (int)Math.Ceiling(pad));
                    name = name.PadRight(name.Length + (int)Math.Floor(pad));
                }

                var playerName = Encoding.ASCII.GetBytes(name.ToUpper());

                patches.Add(0x385000 + (world.Id * 16), playerName.Concat(new byte[] { 0x00, 0x00, 0x00, 0x00 }).ToArray());
            }

            /* Write seed data into ROM */

            /* My world player id */
            patches.Add(0x00FF50, BitConverter.GetBytes((uint)myWorld.Id));

            /* Seed configuration bitfield */
            patches.Add(0x00FF52, BitConverter.GetBytes((uint)0));

            /* Write seed guid */
            patches.Add(0x00FF60, Encoding.ASCII.GetBytes(seedGuid.Replace("-", "")));

            /* Write world guid */
            patches.Add(0x00FF80, Encoding.ASCII.GetBytes(myWorld.Guid.Replace("-", "")));

            return patches;
        }

        private Dictionary<int, byte[]> Remap(Dictionary<int, byte[]> patches) {
            var newPatches = new Dictionary<int, byte[]>();
            foreach(var patch in patches) {
                newPatches.Add(ComboOffset(patch.Key), patch.Value);
            }

            return newPatches;
        }

        private int ComboOffset(int offset) {
            var orig_offset = offset;

            if (offset < 0x170000) {
                /* This converts LoROM to HiROM mapping and then applies the ExHiROM offset */
                offset = 0x400000 + (offset + (0x8000 * ((int)Math.Floor((decimal)offset / 0x8000) + 1)));
            }

            else if ((offset & 0xff0000) == 0x180000) {
                /* Change 0x180000 access into ExHiROM bank 40 */
                offset = 0x400000 + (offset & 0x00ffff);
            }

            else if (offset == 0x178000) {
                /* Repoint RNG Block */
                offset = 0x420000;
            }

            else if ((offset & 0xff0000) == 0xff0000) {
                /* SM ExHiROM Header */
                offset = (offset & 0x00ffff);
            }

            else {
                throw new Exception("Unmapped combo offset");
            }

            if (offset > 0x600000) {
                throw new Exception("Unmapped combo offset");
            }

            return offset;
        }

        private void WriteSMLocation(ref Dictionary<int, byte[]> patches, Location location) {
            var locationValue = location.Type switch
            {
                LocationType.Visible => BitConverter.GetBytes((ushort)0xefe0),
                LocationType.Chozo => BitConverter.GetBytes((ushort)0xefe4),
                LocationType.Hidden => BitConverter.GetBytes((ushort)0xefe8),
                _ => new byte[] { 0xe0, 0xef }
            };

            patches.Add(0x80000 + location.Address, locationValue);
            //Console.WriteLine($"Writing SM Location: {location.Name} to {0x80000 + location.Address}");

            /* Write item information to new randomizer item table */
            var type = (location.Item.World == location.Region.World) ? BitConverter.GetBytes((ushort)0) : BitConverter.GetBytes((ushort)1);
            var itemId = BitConverter.GetBytes((ushort)location.Item.Type);
            var owner = BitConverter.GetBytes((ushort)location.Item.World.Id);
            var extra = BitConverter.GetBytes((ushort)0);

            patches.Add(0x386000 + (location.Id * 8), type.Concat(itemId).Concat(owner).Concat(extra).ToArray());
        }

        private void WriteZ3Location(ref Dictionary<int, byte[]> patches, Location location) {
            patches.Add(ComboOffset(location.Address), new byte[] { (byte)(location.Id - 256) });

            if(location.Type == LocationType.HeraStandingKey) {
                patches.Add(ComboOffset(0x4E3BB), location.Item.Type == ItemType.KeyTH ? new byte[] { 0xE4 } : new byte[] { 0xEB });
            }

            /* Write item information to new randomizer item table */
            var type = (location.Item.World == location.Region.World) ? BitConverter.GetBytes((ushort)0) : BitConverter.GetBytes((ushort)1);
            var itemId = BitConverter.GetBytes((ushort)GetZ3ItemId(location.Item.Type));
            var owner = BitConverter.GetBytes((ushort)location.Item.World.Id);
            var extra = BitConverter.GetBytes((ushort)0);

            patches.Add(0x386000 + (location.Id * 8), type.Concat(itemId).Concat(owner).Concat(extra).ToArray());
        }

        private byte GetZ3ItemId(ItemType item) {
            int id = (int)item;
            if(id >= 0x72 && id <= 0x7F) {
                return 0x33;
            } else if(id >= 0x82 && id <= 0x8D) {
                return 0x25;
            } else if(id >= 0x92 && id <= 0x9D) {
                return 0x32;
            } else if(id >= 0xA0 && id <= 0xAD) {
                return 0x24;
            } else {
                return (byte)id;
            }
        }

        private void WriteDiggingGameRng(ref Dictionary<int, byte[]> patches) {
            byte digs = (byte)rnd.Next(1, 30 + 1);
            patches.Add(0x180020, new byte[] { digs });
            patches.Add(0xEFD95, new byte[] { digs });
        }

        private void WriteWishingWellChests(ref Dictionary<int, byte[]> patches) {
            patches.Add(0xE9AE, new byte[] { 0x14, 0x01 });
            patches.Add(0xE9CF, new byte[] { 0x14, 0x01 });
            patches.Add(0x1F714, Convert.FromBase64String("4QAQrA0pmgFYmA8RsWH8TYEg2gIs4WH8voFhsWJU2gL9jYNE4WL9HoMxpckxpGkxwCJNpGkxxvlJxvkQmaBcmaILmGAN6MBV6MALkgBzmGD+aQCYo2H+a4H+q4WpyGH+roH/aQLYo2L/a4P/K4fJyGL/LoP+oQCqIWH+poH/IQLKIWL/JoO7I/rDI/q7K/rDK/q7U/rDU/qwoD2YE8CYUsCIAGCQAGDoAGDwAGCYysDYysDYE8DYUsD8vYX9HYf/////8P+ALmEOgQ7//w=="));
        }

        private void WritePyramidFairyChets(ref Dictionary<int, byte[]> patches) {
            patches.Add(0x1FC16, new byte[] { 0xB1, 0xC6, 0xF9, 0xC9, 0xC6, 0xF9 });
        }

        private void WriteOpenMode(ref Dictionary<int, byte[]> patches) {
            patches.Add(0x180032, new byte[] { 0x01 });
            patches.Add(0x180038, new byte[] { 0x00 });
            patches.Add(0x180039, new byte[] { 0x00 });
            patches.Add(0x18003A, new byte[] { 0x00 });
        }

        private void WriteMedallions(ref Dictionary<int, byte[]> patches) {
            var turtleRock = myWorld.Regions.Find(x => x is TurtleRock) as TurtleRock;
            var miseryMire = myWorld.Regions.Find(x => x is MiseryMire) as MiseryMire;

            var turtleRockAddresses = new int[] { 0x180023, 0x5020, 0x50FF, 0x51DE };
            var miseryMireAddresses = new int[] { 0x180022, 0x4FF2, 0x50D1, 0x51B0 };

            var turtleRockData = turtleRock.Medallion switch
            {
                ItemType.Bombos => new byte[] { 0x00, 0x51, 0x10, 0x00 },
                ItemType.Ether => new byte[] { 0x01, 0x51, 0x18, 0x00 },
                ItemType.Quake => new byte[] { 0x02, 0x14, 0xEF, 0xC4 },
                _ => new byte[] { 0x02, 0x14, 0xEF, 0xC4 }
            };

            var miseryMireData = miseryMire.Medallion switch
            {
                ItemType.Bombos => new byte[] { 0x00, 0x51, 0x00, 0x00 },
                ItemType.Ether => new byte[] { 0x01, 0x13, 0x9F, 0xF1 },
                ItemType.Quake => new byte[] { 0x02, 0x51, 0x08, 0x00 },
                _ => new byte[] { 0x01, 0x13, 0x9F, 0xF1 }
            };

            for (int i = 0; i < 4; i++) {
                patches.Add(turtleRockAddresses[i], new[] { turtleRockData[i] });
                patches.Add(miseryMireAddresses[i], new[] { miseryMireData[i] });
            }
        }

        private void WriteRewards(ref Dictionary<int, byte[]> patches) {
            var crystalsBlue = new List<int> { 1, 2, 3, 4, 7 }.Shuffle(rnd);
            var crystalsRed = new List<int> { 5, 6 }.Shuffle(rnd);
            var pendantsBlueRed = new List<int> { 2, 3 }.Shuffle(rnd);

            foreach (var region in myWorld.Regions.Where(x => x is Reward && (((Reward)x).Reward == RewardType.CrystalBlue || ((Reward)x).Reward == RewardType.CrystalRed))) {
                int crystal = 0;
                var reward = region as Reward;
                if (reward.Reward is RewardType.CrystalBlue) {
                    crystal = crystalsBlue.First();
                    crystalsBlue.Remove(crystal);
                }
                else {
                    crystal = crystalsRed.First();
                    crystalsRed.Remove(crystal);
                }

                var addresses = GetRewardAddresses(region);
                var values = GetCrystalValues(crystal);
                for (int i = 0; i < addresses.Length; i++) {
                    patches.Add(addresses[i], new[] { values[i] });
                }
            }

            foreach (var region in myWorld.Regions.Where(x => x is Reward && (((Reward)x).Reward == RewardType.PendantGreen || ((Reward)x).Reward == RewardType.PendantNonGreen))) {
                int pendant = 0;
                var reward = region as Reward;
                if (reward.Reward is RewardType.PendantNonGreen) {
                    pendant = pendantsBlueRed.First();
                    pendantsBlueRed.Remove(pendant);
                }
                else {
                    pendant = 1;
                }

                var addresses = GetRewardAddresses(region);
                var values = GetPendantValues(pendant);
                for (int i = 0; i < addresses.Length; i++) {
                    patches.Add(addresses[i], new[] { values[i] });
                }
            }
        }

        private int[] GetRewardAddresses(Region region) {
            return region switch
            {
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
                _ => new[] { 0x1209D, 0x53EF8, 0x53EF9, 0x180052, 0x18007C, 0xC6FE }
            };
        }

        private byte[] GetCrystalValues(int crystal) {
            return crystal switch
            {
                1 => new byte[] { 0x02, 0x34, 0x64, 0x40, 0x7F, 0x06 },
                2 => new byte[] { 0x10, 0x34, 0x64, 0x40, 0x79, 0x06 },
                3 => new byte[] { 0x40, 0x34, 0x64, 0x40, 0x6C, 0x06 },
                4 => new byte[] { 0x20, 0x34, 0x64, 0x40, 0x6D, 0x06 },
                5 => new byte[] { 0x04, 0x32, 0x64, 0x40, 0x6E, 0x06 },
                6 => new byte[] { 0x01, 0x32, 0x64, 0x40, 0x6F, 0x06 },
                7 => new byte[] { 0x08, 0x34, 0x64, 0x40, 0x7C, 0x06 },
                _ => new byte[] { 0x02, 0x34, 0x64, 0x40, 0x7F, 0x06 }
            };
        }

        private byte[] GetPendantValues(int pendant) {
            return pendant switch
            {
                1 => new byte[] { 0x04, 0x38, 0x62, 0x00, 0x69, 0x01 },
                2 => new byte[] { 0x01, 0x32, 0x60, 0x00, 0x69, 0x03 },
                3 => new byte[] { 0x02, 0x34, 0x60, 0x00, 0x69, 0x02 },
                _ => new byte[] { 0x04, 0x38, 0x62, 0x00, 0x69, 0x01 }
            };
        }

    }

}
