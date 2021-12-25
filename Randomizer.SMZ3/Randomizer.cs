using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Randomizer.Shared.Contracts;
using static Randomizer.Shared.Contracts.RandomizerOptionType;

namespace Randomizer.SMZ3 {

    public class Randomizer : IRandomizer {

        public static readonly Version version = new Version(11, 1);

        public string Id => "smz3";
        public string Name => "Super Metroid & A Link to the Past Combo Randomizer";

        public string Version => version.ToString();

        static Regex alphaNumeric = new Regex(@"[A-Z\d]", RegexOptions.IgnoreCase);

        public List<IRandomizerOption> Options => new List<IRandomizerOption> {
            Config.GetRandomizerOption<SMLogic>("Super Metroid Logic"),
            Config.GetRandomizerOption<Goal>("Goal"),
            //Config.GetRandomizerOption<Z3Logic>("A Link to the Past Logic"),
            Config.GetRandomizerOption<SwordLocation>("First Sword"),
            Config.GetRandomizerOption<MorphLocation>("Morph Ball"),
            new RandomizerOption {
                Key = "seed", Description = "Seed", Type = Seed
            },
            Config.GetRandomizerOption("Race", "Race ROM (no spoilers)", false),
            Config.GetRandomizerOption<GameMode>("Game mode"),

            new RandomizerOption {
                Key = "players", Description = "Players", Type = Players, Default = "2"
            },
        };

        public ISeedData GenerateSeed(IDictionary<string, string> options, string seed) {
            int randoSeed;
            if (string.IsNullOrEmpty(seed)) {
                randoSeed = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, int.MaxValue);
                seed = randoSeed.ToString();
            } else {
                randoSeed = int.Parse(seed);
                /* The Random ctor takes the absolute value of a negative seed.
                 * This is an non-obvious behavior so we treat a negative value
                 * as out of range. */
                if (randoSeed < 0)
                    throw new ArgumentOutOfRangeException("Expected the seed option value to be an integer value in the range [0, 2147483647]");
            }

            var randoRnd = new Random(randoSeed);

            var config = new Config(options);
            var worlds = new List<World>();

            /* FIXME: Just here to semi-obfuscate race seeds until a better solution is in place */
            if (config.Race) {
                randoRnd = new Random(randoRnd.Next());
            }

            int players = options.ContainsKey("players") ? int.Parse(options["players"]) : 1;
            for (int p = 0; p < players; p++) {
                // Get the playername
                string playername = "Player";
                if (config.GameMode != GameMode.Normal) {
                    var found = options.TryGetValue($"player-{p}", out var player);
                    if (!found || !alphaNumeric.IsMatch(player))
                        throw new ArgumentException($"Name for player {p + 1} not provided, or contains no alphanumeric characters");
                    playername = player;
                }

                // Setup worlds while here
                var new_world = new World(config, playername, p, new HexGuid());
                new_world.Setup(randoRnd);
                worlds.Add(new_world);
            }

            var filler = new Filler(worlds, config, randoRnd);
            filler.Fill();

            var playthrough = new Playthrough(worlds, config);
            var spheres = playthrough.Generate();

            var seedData = new SeedData {
                Guid = new HexGuid(),
                Seed = seed,
                Game = Name,
                Mode = config.GameMode.ToLString(),
                Logic = $"{config.SMLogic.ToLString()}+{config.Z3Logic.ToLString()}",
                Playthrough = config.Race ? new List<Dictionary<string, string>>() : spheres,
                Worlds = new List<IWorldData>(),
            };

            /* Make sure RNG is the same when applying patches to the ROM to have consistent RNG for seed identifer etc */
            int patchSeed = randoRnd.Next();
            foreach (var world in worlds) {
                var patchRnd = new Random(patchSeed);
                var patch = new Patch(world, worlds, seedData.Guid, config.Race ? 0 : randoSeed, patchRnd);
                var worldData = new WorldData {
                    Id = world.Id,
                    Guid = world.Guid,
                    Player = world.Player,
                    Patches = patch.Create(config),
                    Locations = world.Locations.Select(l => new LocationData() { LocationId = l.Id, ItemId = (int)l.Item.Type, ItemWorldId = l.Item.World.Id }).ToList<ILocationData>()
                };

                seedData.Worlds.Add(worldData);
            }

            return seedData;
        }

        public Dictionary<int, ILocationTypeData> GetLocations() => 
            new World(new Config(new Dictionary<string, string>()), "", 0, "")
                .Locations.Select(location => new LocationTypeData {
                    Id = location.Id,
                    Name = location.Name,
                    Type = location.Type.ToString(),
                    Region = location.Region.Name,
                    Area = location.Region.Area
                }).Cast<ILocationTypeData>().ToDictionary(locationData => locationData.Id);

        public Dictionary<int, IItemTypeData> GetItems() =>
            Enum.GetValues(typeof(ItemType)).Cast<ItemType>().Select(i => new ItemTypeData {
                Id = (int)i,
                Name = i.GetDescription()
            }).Cast<IItemTypeData>().ToDictionary(itemTypeData => itemTypeData.Id);
    }

    public class RandomizerOption : IRandomizerOption {
        public string Key { get; set; }
        public string Description { get; set; }
        public RandomizerOptionType Type { get; set; }
        public Dictionary<string, string> Values { get; set; }
        public string Default { get; set; }
    }

    public class SeedData : ISeedData {

        public string Guid { get; set; }
        public string Seed { get; set; }
        public string Game { get; set; }
        public string Logic { get; set; }
        public string Mode { get; set; }
        public List<IWorldData> Worlds { get; set; }
        public List<Dictionary<string, string>> Playthrough { get; set; }

    }

    public class WorldData : IWorldData {

        public int Id { get; set; }
        public string Guid { get; set; }
        public string Player { get; set; }
        public Dictionary<int, byte[]> Patches { get; set; }
        public List<ILocationData> Locations { get; set; }
    }

    public class LocationData : ILocationData {
        public int LocationId { get; set; }
        public int ItemId { get; set; }
        public int ItemWorldId { get; set; }
    }

    public class ItemTypeData : IItemTypeData {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class LocationTypeData : ILocationTypeData {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Region { get; set; }

        public string Area { get; set; }
    }

}
