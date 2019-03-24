using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.Contracts;

namespace Randomizer.SuperMetroid {

    public class Randomizer : IRandomizer {

        public IPatchData GenerateSeed(IDictionary<string, string> options, string seed) {
            var world = new World(Logic.Tournament);
            var patchData = new PatchData {
                Seed = new Random().Next(1000000, 9999999).ToString(),
                Name = "Super Metroid Randomizer",
                Patches = new Dictionary<int, byte[]> {
                    [0x71234] = new byte[] { 0x10, 0x20, 0x30 }
                }
            };
            return patchData;
        }
    }

    public class PatchData : IPatchData {

        private Dictionary<int, byte[]> patches;
        public IDictionary<int, byte[]> Patches {
            get { return patches; }
            set { patches = value as Dictionary<int, byte[]> ?? value.ToDictionary(x => x.Key, x => x.Value); }
        }

        public string Seed { get; set; }
        public string Name { get; set; }

    }

}
