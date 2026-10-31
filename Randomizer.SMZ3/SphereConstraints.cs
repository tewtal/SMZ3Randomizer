using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Randomizer.Shared.Contracts;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3 {

    /// <summary>
    /// Lets the player require that core items show up by a given sphere.
    /// "By sphere N" means the item is obtainable at sphere N or earlier, never later.
    /// Items with several copies get one setting per copy, the earliest copy is matched
    /// against the tightest setting and so on.
    /// </summary>
    static class SphereConstraints {

        public const string RandomValue = "random";
        public const int MaxSphere = 10;
        const int Unreachable = 1000;
        const int MaxCandidatesPerMiss = 300;

        /// <summary>Every core item and how many copies of it exist in the pools</summary>
        static readonly (ItemType Type, int Copies)[] coreItems = {
            (ProgressiveSword, 4), (ProgressiveShield, 3), (ProgressiveGlove, 2), (ProgressiveTunic, 2),
            (Bow, 1), (SilverArrows, 1), (Hookshot, 1), (Mushroom, 1), (Powder, 1), (Firerod, 1), (Icerod, 1),
            (Bombos, 1), (Ether, 1), (Quake, 1), (Lamp, 1), (Hammer, 1), (Shovel, 1), (Flute, 1),
            (Bugnet, 1), (Book, 1), (Bottle, 4), (Somaria, 1), (Byrna, 1), (Cape, 1), (Mirror, 1),
            (Boots, 1), (Flippers, 1), (MoonPearl, 1), (HalfMagic, 1),

            (Morph, 1), (Bombs, 1), (Charge, 1), (Ice, 1), (Wave, 1), (Spazer, 1), (Plasma, 1),
            (Varia, 1), (Gravity, 1), (SpringBall, 1), (ScrewAttack, 1), (HiJump, 1), (SpaceJump, 1),
            (SpeedBooster, 1), (Grapple, 1), (XRay, 1), (Super, 1), (PowerBomb, 2),
        };

        static string Key(ItemType type, int copy, int copies) {
            var key = $"sphere-{type.ToString().ToLower()}";
            return copies > 1 ? $"{key}-{copy}" : key;
        }

        public static IEnumerable<RandomizerOption> CreateOptions() {
            var values = new Dictionary<string, string> { [RandomValue] = "Random" };
            for (var sphere = 1; sphere <= MaxSphere; sphere++) {
                values[sphere.ToString()] = $"By sphere {sphere}";
            }

            foreach (var (type, copies) in coreItems) {
                for (var copy = 1; copy <= copies; copy++) {
                    yield return new RandomizerOption {
                        Key = Key(type, copy, copies),
                        Description = copies > 1 ? $"{type.GetDescription()} #{copy}" : type.GetDescription(),
                        Type = RandomizerOptionType.Dropdown,
                        Default = RandomValue,
                        Values = values,
                    };
                }
            }
        }

        public static Dictionary<ItemType, List<int>> Parse(IDictionary<string, string> options) {
            var result = new Dictionary<ItemType, List<int>>();
            foreach (var (type, copies) in coreItems) {
                for (var copy = 1; copy <= copies; copy++) {
                    if (options.TryGetValue(Key(type, copy, copies), out var value) && int.TryParse(value, out var sphere) && sphere >= 1) {
                        if (!result.ContainsKey(type))
                            result[type] = new List<int>();
                        result[type].Add(sphere);
                    }
                }
            }
            foreach (var limits in result.Values) {
                limits.Sort();
            }
            return result;
        }

        /// <summary>
        /// Moves items earlier until every setting is satisfied. Each swap must keep the
        /// seed fully reachable and strictly improve how well the settings are met.
        /// </summary>
        public static void Enforce(List<World> worlds, Config config, Random rnd, CancellationToken cancellationToken) {
            if (config.ItemSpheres.Count == 0)
                return;

            var locations = worlds.SelectMany(w => w.Locations).ToList();
            /* Keycards and initial items are not placed in locations but still count as owned */
            var everything = worlds.ToDictionary(w => w.Id,
                w => new Progression(locations.Select(l => l.Item).Where(i => i.World == w)
                    .Concat(w.Config.Keysanity ? Enumerable.Empty<Item>() : Item.CreateKeycards(w))
                    .Concat(w.Config.InitialItems.SelectMany(ii => Enumerable.Repeat(new Item(ii.Key, w), ii.Value))),
                    w.Config.Skills));

            bool CanHold(Location location, Item item) => location.CanFill(item, everything[location.Region.World.Id]);

            bool IsPinned(Location location) =>
                location.Name == "Skull Woods - Pinball Room" ||
                config.SwordLocation == SwordLocation.Uncle && location.Name == "Link's Uncle" ||
                config.MorphLocation == MorphLocation.Original && location.Name == "Morphing Ball";

            var spheres = Playthrough.LocationSpheres(worlds);
            var misses = FindMisses(worlds, config, locations, spheres);

            while (misses.Count > 0) {
                var miss = misses[0];
                var score = misses.Sum(m => m.Excess);
                var candidates = locations
                    .Where(l => l != miss.Location && l.Item.Type != miss.Location.Item.Type &&
                        spheres.TryGetValue(l, out var s) && s <= miss.Limit && !IsPinned(l))
                    .Shuffle(rnd)
                    .Take(MaxCandidatesPerMiss)
                    .ToList();

                /* Applies the assignment if the seed stays reachable and the settings are better met, otherwise undoes it */
                bool TryApply(params (Location location, Item item)[] assignment) {
                    cancellationToken.ThrowIfCancellationRequested();

                    var previous = assignment.Select(a => a.location.Item).ToArray();
                    foreach (var (location, item) in assignment) {
                        location.Item = item;
                    }

                    var newSpheres = Playthrough.LocationSpheres(worlds);
                    if (newSpheres.Count >= spheres.Count) {
                        var newMisses = FindMisses(worlds, config, locations, newSpheres);
                        if (newMisses.Sum(m => m.Excess) < score) {
                            spheres = newSpheres;
                            misses = newMisses;
                            return true;
                        }
                    }

                    for (var i = 0; i < assignment.Length; i++) {
                        assignment[i].location.Item = previous[i];
                    }
                    return false;
                }

                var moving = miss.Location.Item;
                var fixedMiss = false;

                /* Swap with an item that is already early enough */
                foreach (var candidate in candidates) {
                    var displaced = candidate.Item;
                    if (CanHold(candidate, moving) && CanHold(miss.Location, displaced) &&
                        TryApply((miss.Location, displaced), (candidate, moving))) {
                        fixedMiss = true;
                        break;
                    }
                }

                /* Early locations tend to hold items the rest of the seed depends on, so also try
                 * sending the displaced item to where a non progression item is, and that one to
                 * the old location */
                if (!fixedMiss) {
                    var fillers = locations
                        .Where(l => l != miss.Location && !l.Item.Progression && !IsPinned(l))
                        .Shuffle(rnd);
                    foreach (var candidate in candidates) {
                        var displaced = candidate.Item;
                        if (!CanHold(candidate, moving))
                            continue;

                        foreach (var filler in fillers.Where(f => f != candidate).Take(MaxCandidatesPerMiss)) {
                            var junk = filler.Item;
                            if (CanHold(filler, displaced) && CanHold(miss.Location, junk) &&
                                TryApply((candidate, moving), (filler, displaced), (miss.Location, junk))) {
                                fixedMiss = true;
                                break;
                            }
                        }
                        if (fixedMiss)
                            break;
                    }
                }

                if (!fixedMiss) {
                    throw new InvalidOperationException(
                        $"Unable to place {miss.Location.Item.Name} by sphere {miss.Limit}, try a later sphere or fewer sphere settings");
                }
            }
        }

        record Miss(Location Location, int Limit, int Excess);

        /// <summary>Settings that are not met, tightest first</summary>
        static List<Miss> FindMisses(List<World> worlds, Config config, List<Location> locations, Dictionary<Location, int> spheres) {
            var misses = new List<Miss>();
            foreach (var world in worlds) {
                foreach (var (type, limits) in config.ItemSpheres) {
                    /* Initial items are available from the start, i.e. sphere 0 */
                    var initial = config.InitialItems.TryGetValue(type, out var n) ? n : 0;
                    var copies = Enumerable.Repeat((Location: (Location)null, Sphere: 0), initial)
                        .Concat(locations.Where(l => l.Item.Is(type, world))
                            .Select(l => (Location: l, Sphere: spheres.TryGetValue(l, out var s) ? s : Unreachable)))
                        .OrderBy(c => c.Sphere)
                        .ToList();

                    for (var i = 0; i < Math.Min(limits.Count, copies.Count); i++) {
                        if (copies[i].Sphere > limits[i]) {
                            misses.Add(new Miss(copies[i].Location, limits[i], copies[i].Sphere - limits[i]));
                        }
                    }
                }
            }
            return misses.OrderBy(m => m.Limit).ToList();
        }

    }

}
