using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Randomizer.SuperMetroid {

    class Patch {

        private readonly World myWorld;
        private readonly List<World> allWorlds;
        private readonly string seedGuid;

        public Patch(World myWorld, List<World> allWorlds, string seedGuid) {
            this.myWorld = myWorld;
            this.allWorlds = allWorlds;
            this.seedGuid = seedGuid;
        }

        private byte[] GetLocationTypeData(LocationType type) {
            return type switch {
                LocationType.Visible => BitConverter.GetBytes((ushort)0xefe0),
                LocationType.Chozo => BitConverter.GetBytes((ushort)0xefe4),
                LocationType.Hidden => BitConverter.GetBytes((ushort)0xefe8),
                _ => new byte[] { 0xe0, 0xef }
            };
        }

        public Dictionary<int, byte[]> Create() {
            var patches = new Dictionary<int, byte[]>();

            foreach(var location in myWorld.Locations) {
                /* Write new "randomizer" PLM to original PLM locations */
                patches.Add(location.Address, GetLocationTypeData(location.Type));

                /* Write item information to new randomizer item table */
                var type = (location.Item.World == location.Region.World) ? BitConverter.GetBytes((ushort)0) : BitConverter.GetBytes((ushort)1);
                var itemId = BitConverter.GetBytes((ushort)location.Item.Type);
                var owner = BitConverter.GetBytes((ushort)location.Item.World.Id);
                var extra = BitConverter.GetBytes((ushort)0);

                patches.Add(0x1C6000 + (location.Id * 8), type.Concat(itemId).Concat(owner).Concat(extra).ToArray());
            }

            /* Write player names into ROM */
            foreach(var world in allWorlds) {
                /* Pad name if needed */
                var name = world.Player.Length > 12 ? world.Player[..12] : world.Player;
                int padding = (12 - name.Length);
                if(padding > 0) {
                    double pad = padding / 2.0;
                    name = name.PadLeft(name.Length + (int)Math.Ceiling(pad));
                    name = name.PadRight(name.Length + (int)Math.Floor(pad));
                }

                var playerName = Encoding.ASCII.GetBytes(name.ToUpper());

                patches.Add(0x1C5000 + (world.Id * 16), playerName.Concat(new byte[] { 0x00, 0x00, 0x00, 0x00 }).ToArray());                
            }

            /* Write seed data into ROM */

            /* My world player id */
            patches.Add(0x1C4F00, BitConverter.GetBytes((uint)myWorld.Id));

            /* Seed configuration bitfield */
            patches.Add(0x1C4F02, BitConverter.GetBytes((uint)0));

            /* Write seed guid */
            patches.Add(0x1C4F10, Encoding.ASCII.GetBytes(seedGuid.Replace("-", "")));

            /* Write world guid */
            patches.Add(0x1C4F30, Encoding.ASCII.GetBytes(myWorld.Guid.Replace("-", "")));

            return patches;
        }

    }

}
