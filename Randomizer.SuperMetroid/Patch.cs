using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Randomizer.SuperMetroid {

    class Patch {

        readonly List<World> allWorlds;
        readonly World myWorld;
        readonly string seedGuid;
        readonly int seed;
        Dictionary<int, byte[]> patches;

        public Patch(World myWorld, List<World> allWorlds, string seedGuid, int seed) {
            this.myWorld = myWorld;
            this.allWorlds = allWorlds;
            this.seedGuid = seedGuid;
            this.seed = seed;
        }

        public Dictionary<int, byte[]> Create() {
            patches = new Dictionary<int, byte[]>();

            WriteLocations();

            WritePlayerNames();
            WriteSeedData();

            return patches;
        }

        void WriteLocations() {
            foreach (var location in myWorld.Locations) {
                /* Write new "randomizer" PLM to original PLM locations */
                patches.Add(location.Address, GetLocationTypeData(location.Type));

                /* Write item information to new randomizer item table */
                var type = location.Item.World == location.Region.World ? 0 : 1;
                var itemId = (byte)location.Item.Type;
                var owner = location.Item.World.Id;

                patches.Add(0x1C6000 + (location.Id * 8), new[] { type, itemId, owner, 0 }.SelectMany(UshortBytes).ToArray());
            }
        }

        byte[] GetLocationTypeData(LocationType type) {
            return type switch {
                LocationType.Chozo => UshortBytes(0xEFE4),
                LocationType.Hidden => UshortBytes(0xEFE8),
                _ => UshortBytes(0xEFE0),
            };
        }

        void WritePlayerNames() {
            foreach (var world in allWorlds) {
                patches.Add(0x1C5000 + (world.Id * 16), PlayerNameBytes(world.Player));
            }
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
                /* Gap of 2 bits, taken by Z3 logic in combo */
                ((int)myWorld.Config.Logic << 8) |
                (Randomizer.version.Major << 4) |
                (Randomizer.version.Minor << 0);

            patches.Add(0x1C4F00, UshortBytes(myWorld.Id));
            patches.Add(0x1C4F02, UshortBytes(configField));
            patches.Add(0x1C4F04, UintBytes(seed));
            /* Reserve the rest of the space for future use */
            patches.Add(0x1C4F08, Enumerable.Repeat<byte>(0x00, 8).ToArray());
            patches.Add(0x1C4F10, AsAscii(seedGuid));
            patches.Add(0x1C4F30, AsAscii(myWorld.Guid));
        }

        byte[] UintBytes(int value) => BitConverter.GetBytes((uint)value);

        byte[] UshortBytes(int value) => BitConverter.GetBytes((ushort)value);

        byte[] AsAscii(string text) => Encoding.ASCII.GetBytes(text);

    }

}
