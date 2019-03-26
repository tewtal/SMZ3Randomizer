using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.Contracts;

namespace Randomizer.SuperMetroid {

    public class Randomizer : IRandomizer {

        public IPatchData GenerateSeed(IDictionary<string, string> options, string seed) {
            if (seed == "") {
                seed = new Random().Next(0, int.MaxValue).ToString();
            }

            var rnd = new Random(int.Parse(seed));
            var worlds = new List<World>(Enumerable.Range(1, 2).Select(x => new World(Logic.Tournament, $"Player {x}")));

            var filler = new Filler(worlds, rnd);
            filler.Fill();

            var playthrough = new Playthrough(worlds);
            var spheres = playthrough.Generate();

            var patchData = new PatchData {
                Seed = seed,
                Name = "Super Metroid Randomizer",
                Patches = new Dictionary<int, byte[]> {
                    [0x71234] = new byte[] { 0x10, 0x20, 0x30 }
                },
                Playthrough = spheres
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
        public List<Dictionary<string, string>> Playthrough { get; set; }
    }

}
