using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.Contracts;
using static Randomizer.Contracts.RandomizerOptionType;

namespace Randomizer.SuperMetroid {

    public class Randomizer : IRandomizer {
        
        public static readonly Version version = new Version(3, 0);

        public string Id => "sm";

        public string Name => "Super Metroid Item Randomizer";

        public string Version => version.ToString();

        public List<IRandomizerOption> Options => new List<IRandomizerOption> {
            new RandomizerOption {
                Key = "players", Description = "Players", Type = Players
            },
            new RandomizerOption {
                Key = "seed", Description = "Seed", Type = Input
            },
            new RandomizerOption {
                Key = "logic", Description = "Logic", Type = Dropdown,
                Values = new Dictionary<string, string>() {
                    ["casual"] = "Normal",
                    ["tournament"] = "Hard"
                }
            }
        };
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
                    "tournament" => Logic.Tournament,
                    _ => Logic.Tournament
                };
            }

            int players = options.ContainsKey("worlds") ? int.Parse(options["worlds"]) : 1;
            var worlds = new List<World>();

            for (int p = 0; p < players; p++) {
                worlds.Add(new World(logic, options[$"player-{p}"], p));
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
                Logic = logic.ToString(),
                Playthrough = spheres,
                Worlds = new List<IWorldData>()
            };

            foreach(var world in worlds) {
                var patch = new Patch(world, worlds, seedData.Guid);
                var worldData = new WorldData {
                    Id = world.Id,
                    Guid = world.Guid.Replace("-",""),
                    Player = world.Player,
                    Patches = patch.Create()
                };

                seedData.Worlds.Add(worldData);
            }

            return seedData;
        }
    }

    public class RandomizerOption : IRandomizerOption {
        public string Key { get; set; }
        public string Description { get; set; }
        public RandomizerOptionType Type { get; set; }
        public Dictionary<string, string> Values { get; set; }
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
