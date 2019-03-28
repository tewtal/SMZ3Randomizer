using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.Contracts;

namespace Randomizer.SuperMetroid {

    public class Randomizer : IRandomizer {

        public ISeedData GenerateSeed(IDictionary<string, string> options, string seed) {
            if (seed == "") {
                seed = new Random().Next(0, int.MaxValue).ToString();
            }

            var rnd = new Random(int.Parse(seed));

            var logic = Logic.Tournament;
            if(options.ContainsKey("logic")) {
                logic = options["logic"] switch
                {
                    "casual" => Logic.Casual,
                    "touranment" => Logic.Tournament,
                    _ => Logic.Tournament
                };
            }

            int players = options.ContainsKey("worlds") ? int.Parse(options["worlds"]) : 1;
            var worlds = new List<World>();

            for (int p = 0;  p< players; p++) {
                worlds.Add(new World(logic, options[$"player-{p}"]));
            }

            var guid = Guid.NewGuid().ToString();

            var filler = new Filler(worlds, rnd);
            filler.Fill();

            var playthrough = new Playthrough(worlds);
            var spheres = playthrough.Generate();

            var seedData = new SeedData {
                Guid = guid.Replace("-", ""),
                Seed = seed,
                Game = "Super Metroid Item Randomizer",
                Logic = Logic.Tournament.ToString(),
                Playthrough = spheres,
                Worlds = new List<IWorldData>()
            };

            foreach(var world in worlds) {
                var worldData = new WorldData {
                    Guid = world.Guid.Replace("-",""),
                    Player = world.Player,
                    Patches = new Dictionary<int, byte[]>()
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
        public string Guid { get; set; }
        public string Player { get; set; }
        public Dictionary<int, byte[]> Patches { get; set; }
    }
}
