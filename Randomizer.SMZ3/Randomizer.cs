using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.Contracts;

namespace Randomizer.SMZ3 {

    public class Randomizer : IRandomizer {

        public ISeedData GenerateSeed(IDictionary<string, string> options, string seed) {
            if (seed == "") {
                seed = new Random().Next(0, int.MaxValue).ToString();
            }

            var rnd = new Random(int.Parse(seed));

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
                worlds.Add(new World(config, options[$"player-{p}"], p));
            }

            var guid = Guid.NewGuid().ToString();

            var filler = new Filler(worlds, config, rnd);
            filler.Fill();

            var playthrough = new Playthrough(worlds);
            var spheres = playthrough.Generate();

            var seedData = new SeedData {
                Guid = guid.Replace("-", ""),
                Seed = seed,
                Game = "SMAlttP Combo Randomizer",
                Logic = logic.ToString(),
                Playthrough = spheres,
                Worlds = new List<IWorldData>()
            };

            /* Make sure RNG is the same when applying patches to the ROM to have consistent RNG for seed identifer etc */
            int patchSeed = rnd.Next();
            foreach (var world in worlds) {
                var patchRnd = new Random(patchSeed);
                var patch = new Patch(world, worlds, seedData.Guid, patchRnd);
                var worldData = new WorldData {
                    Id = world.Id,
                    Guid = world.Guid.Replace("-", ""),
                    Player = world.Player,
                    Patches = patch.Create()
                };

                seedData.Worlds.Add(worldData);
            }

            return seedData;
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
