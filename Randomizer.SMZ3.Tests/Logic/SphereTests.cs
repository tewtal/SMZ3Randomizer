using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace Randomizer.SMZ3.Tests.Logic {

    public class SphereTests {

        static (List<World> worlds, Dictionary<Location, int> spheres) Generate(int seed, params (string, string)[] options) {
            var config = RandomizerOptions.Parse(options.ToDictionary(o => o.Item1, o => o.Item2));
            var worlds = new List<World> { new World(config, "Player", 0, "") };
            new Filler(worlds, config, new Random(seed), CancellationToken.None).Fill();
            return (worlds, Playthrough.LocationSpheres(worlds));
        }

        static List<int> SpheresOf(List<World> worlds, Dictionary<Location, int> spheres, ItemType type) =>
            worlds.SelectMany(w => w.Locations).Where(l => l.Item.Type == type).Select(l => spheres[l]).OrderBy(s => s).ToList();

        [Test]
        public void EveryCoreItemHasAnOptionAndEveryProgressiveCopyIsListed() {
            var keys = SphereConstraints.CreateOptions().Select(o => o.Key).ToList();
            Assert.That(keys, Is.Unique);
            Assert.That(keys.Count(k => k.StartsWith("sphere-progressivesword-")), Is.EqualTo(4));
            Assert.That(keys.Count(k => k.StartsWith("sphere-progressiveshield-")), Is.EqualTo(3));
            Assert.That(keys.Count(k => k.StartsWith("sphere-progressiveglove-")), Is.EqualTo(2));
            Assert.That(SphereConstraints.CreateOptions().All(o => o.Default == "random"), Is.True);
            Assert.That(RandomizerOptions.List.Select(o => o.Key), Is.SupersetOf(keys));
        }

        [Test]
        public void RandomIsTheDefault() {
            Assert.That(RandomizerOptions.Parse(new Dictionary<string, string>()).ItemSpheres, Is.Empty);
            Assert.That(RandomizerOptions.Parse(new Dictionary<string, string> { ["sphere-spacejump"] = "random" }).ItemSpheres, Is.Empty);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void ItemAppearsByRequestedSphere(int seed) {
            var (worlds, spheres) = Generate(seed, ("sphere-spacejump", "2"), ("sphere-moonpearl", "3"), ("sphere-hookshot", "1"));
            Assert.That(SpheresOf(worlds, spheres, ItemType.SpaceJump)[0], Is.LessThanOrEqualTo(2));
            Assert.That(SpheresOf(worlds, spheres, ItemType.MoonPearl)[0], Is.LessThanOrEqualTo(3));
            Assert.That(SpheresOf(worlds, spheres, ItemType.Hookshot)[0], Is.EqualTo(1));
            Assert.That(spheres.Count, Is.EqualTo(worlds[0].Locations.Count), "all locations must stay reachable");
        }

        [TestCase(11, "false")]
        [TestCase(12, "false")]
        [TestCase(13, "true")]
        public void ManyTightSettingsAreAllMet(int seed, string keysanity) {
            var limits = new Dictionary<string, string> {
                ["sphere-spacejump"] = "2", ["sphere-speedbooster"] = "3", ["sphere-gravity"] = "3",
                ["sphere-varia"] = "3", ["sphere-hammer"] = "2", ["sphere-boots"] = "1", ["sphere-flippers"] = "2",
                ["sphere-mirror"] = "2", ["sphere-bombs"] = "1", ["sphere-progressivesword-1"] = "1",
                ["sphere-progressiveglove-1"] = "2", ["sphere-progressiveglove-2"] = "3", ["sphere-bottle-4"] = "4",
                ["keyshuffle"] = keysanity == "true" ? "keysanity" : "none",
            };
            var config = RandomizerOptions.Parse(limits);
            var worlds = new List<World> { new World(config, "Player", 0, "") };
            new Filler(worlds, config, new Random(seed), System.Threading.CancellationToken.None).Fill();
            var spheres = Playthrough.LocationSpheres(worlds);

            foreach (var (type, sphereLimits) in config.ItemSpheres) {
                var actual = SpheresOf(worlds, spheres, type);
                for (var i = 0; i < sphereLimits.Count; i++) {
                    Assert.That(actual[i], Is.LessThanOrEqualTo(sphereLimits[i]), $"{type} copy {i + 1}");
                }
            }

            /* The same seed without settings; the filler itself can leave a location unreachable (e.g. the Hera key), but we must not add to that */
            var plainConfig = RandomizerOptions.Parse(new Dictionary<string, string> { ["keyshuffle"] = limits["keyshuffle"] });
            var plainWorlds = new List<World> { new World(plainConfig, "Player", 0, "") };
            new Filler(plainWorlds, plainConfig, new Random(seed), System.Threading.CancellationToken.None).Fill();
            Assert.That(spheres.Count, Is.GreaterThanOrEqualTo(Playthrough.LocationSpheres(plainWorlds).Count));
        }

        [Test]
        public void MultiworldSettingsApplyToEveryWorld() {
            var options = new Dictionary<string, string> {
                ["gamemode"] = "multiworld", ["sphere-moonpearl"] = "2", ["sphere-morph"] = "2", ["sphere-hookshot"] = "3",
            };
            var config = RandomizerOptions.Parse(options);
            var worlds = Enumerable.Range(0, 2).Select(p => new World(config, $"P{p}", p, "")).ToList();
            new Filler(worlds, config, new Random(21), System.Threading.CancellationToken.None).Fill();
            var spheres = Playthrough.LocationSpheres(worlds);

            foreach (var world in worlds) {
                foreach (var (type, limit) in new[] { (ItemType.MoonPearl, 2), (ItemType.Morph, 2), (ItemType.Hookshot, 3) }) {
                    var location = worlds.SelectMany(w => w.Locations).Single(l => l.Item.Is(type, world));
                    Assert.That(spheres[location], Is.LessThanOrEqualTo(limit), $"{type} of {world.Player}");
                }
            }
        }

        [Test]
        public void EachProgressiveCopyIsMatchedSeparately() {
            var (worlds, spheres) = Generate(7,
                ("sphere-progressivesword-1", "1"), ("sphere-progressivesword-2", "2"),
                ("sphere-progressiveglove-1", "2"), ("sphere-progressiveglove-2", "4"),
                ("sphere-progressiveshield-3", "5"));
            var swords = SpheresOf(worlds, spheres, ItemType.ProgressiveSword);
            Assert.That(swords.Count, Is.EqualTo(4));
            Assert.That(swords[0], Is.LessThanOrEqualTo(1));
            Assert.That(swords[1], Is.LessThanOrEqualTo(2));
            var gloves = SpheresOf(worlds, spheres, ItemType.ProgressiveGlove);
            Assert.That(gloves[0], Is.LessThanOrEqualTo(2));
            Assert.That(gloves[1], Is.LessThanOrEqualTo(4));
            Assert.That(SpheresOf(worlds, spheres, ItemType.ProgressiveShield).Count, Is.EqualTo(3));
        }

    }

}
