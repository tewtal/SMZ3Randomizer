using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Randomizer.Contracts;

namespace Randomizer.SMZ3 {

    public class Randomizer : IRandomizer {

        static readonly RNGCryptoServiceProvider cryptoRng = new RNGCryptoServiceProvider();

        public ISeedData GenerateSeed(IDictionary<string, string> options, string seed) {
            int randoSeed;
            if (string.IsNullOrEmpty(seed)) {
                randoSeed = StrongRng();
                seed = randoSeed.ToString();
            } else {
                randoSeed = int.Parse(seed);
            }

            var randoRnd = new Random(randoSeed);

            var logic = Logic.Tournament;
            if (options.ContainsKey("logic")) {
                logic = options["logic"] switch {
                    "casual" => Logic.Casual,
                    "tournament" => Logic.Tournament,
                    _ => Logic.Tournament
                };
            }

            var config = new Config { Logic = logic };
            var worlds = new List<World>();

            int players = options.ContainsKey("worlds") ? int.Parse(options["worlds"]) : 1;
            for (int p = 0; p < players; p++) {
                worlds.Add(new World(config, options[$"player-{p}"], p, new HexGuid()));
            }

            var filler = new Filler(worlds, config, randoRnd);
            filler.Fill();

            var playthrough = new Playthrough(worlds);
            var spheres = playthrough.Generate();

            var seedData = new SeedData {
                Guid = new HexGuid(),
                Seed = seed,
                Game = "SMAlttP Combo Randomizer",
                Logic = logic.ToString(),
                Playthrough = spheres,
                Worlds = new List<IWorldData>()
            };

            /* Make sure RNG is the same when applying patches to the ROM to have consistent RNG for seed identifer etc */
            int patchSeed = randoRnd.Next();
            foreach (var world in worlds) {
                var patchRnd = new Random(patchSeed);
                var patch = new Patch(world, worlds, seedData.Guid, patchRnd);
                var worldData = new WorldData {
                    Id = world.Id,
                    Guid = world.Guid,
                    Player = world.Player,
                    Patches = patch.Create(config)
                };

                seedData.Worlds.Add(worldData);
            }

            return seedData;
        }

        int StrongRng() {
            var bytes = new byte[4];
            cryptoRng.GetBytes(bytes);
            // non-negative number by dropping sign bit
            return BitConverter.ToInt32(bytes) & 0x7FFFFFFF;
        }

    }

    public class SeedData : ISeedData {

        public string Guid { get; set; }
        public string Seed { get; set; }
        public string Game { get; set; }
        public string Logic { get; set; }
        public List<IWorldData> Worlds { get; set; }
        public List<Dictionary<string, string>> Playthrough { get; set; }

    }

    public class WorldData : IWorldData {

        public int Id { get; set; }
        public string Guid { get; set; }
        public string Player { get; set; }
        public Dictionary<int, byte[]> Patches { get; set; }

    }

}
