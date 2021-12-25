﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Randomizer.Shared.Contracts;

namespace Randomizer.SMZ3 {

    public class Randomizer : IRandomizer {

        public static readonly Version version = new Version(11, 2, 1);

        public string Id => "smz3";
        public string Name => "Super Metroid & A Link to the Past Combo Randomizer";
        public string Version => version.ToString();
        public List<IRandomizerOption> Options => RandomizerOptions.List;

        static readonly Regex legalCharacters = new Regex(@"[A-Z0-9]", RegexOptions.IgnoreCase);
        static readonly Regex illegalCharacters = new Regex(@"[^A-Z0-9]", RegexOptions.IgnoreCase);
        static readonly Regex continousSpace = new Regex(@" +");

        public ISeedData GenerateSeed(IDictionary<string, string> options, string seed, CancellationToken cancellationToken) {
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
            var config = RandomizerOptions.Parse(options);

            /* FIXME: Just here to semi-obfuscate race seeds until a better solution is in place */
            if (config.Race) {
                randoRnd = new Random(randoRnd.Next());
            }

            var worlds = new List<World>();
            var players = options.ContainsKey("players") ? int.Parse(options["players"]) : 1;
            for (var p = 0; p < players; p++) {
                string playername = "Player";
                if (config.MultiWorld) {
                    var found = options.TryGetValue($"player-{p}", out var player);
                    if (!found)
                        throw new ArgumentException($"No name provided for player {p + 1}");
                    if (!legalCharacters.IsMatch(player))
                        throw new ArgumentException($"No alphanumeric characters found in name for player {p + 1}");
                    playername = CleanPlayerName(player);
                }

                // Setup the new world while here
                var new_world = new World(config, playername, p, new HexGuid());
                new_world.Setup(randoRnd);
                worlds.Add(new_world);
            }

            var filler = new Filler(worlds, config, randoRnd, cancellationToken);
            filler.Fill();

            var playthrough = new Playthrough(worlds, config);
            var spheres = playthrough.Generate();

            var seedData = new SeedData {
                Guid = new HexGuid(),
                Seed = seed,
                Game = Name,
                Mode = config.GameMode.ToLowerString(),
                Logic = $"{config.SMLogic.ToLowerString()}+{config.Z3Logic.ToLowerString()}",
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
                    Locations = world.Locations
                        .Select(l => new LocationData() { LocationId = l.Id, ItemId = (int)l.Item.Type, ItemWorldId = l.Item.World.Id })
                        .ToList<ILocationData>(),
                };

                seedData.Worlds.Add(worldData);
            }

            return seedData;
        }

        public Dictionary<int, ILocationTypeData> GetLocations() => 
            new World(new Config(), "", 0, "")
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

        static string CleanPlayerName(string name) {
            name = illegalCharacters.Replace(name, " ");
            name = continousSpace.Replace(name, " ");
            return name.Trim();
        }

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
