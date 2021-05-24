using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Randomizer.SMZ3.Regions.Zelda;
using static System.Linq.Enumerable;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.RewardType;
using static Randomizer.SMZ3.Tests.Logic.LogicCases;

namespace Randomizer.SMZ3.Tests.Logic {

    public class LogicTests {

        World world;

        internal void Setup(SMLogic smLogic, bool keysanity = false) {
            var config = new Config {
                SMLogic = smLogic,
                KeyShuffle = keysanity ? KeyShuffle.Keysanity : KeyShuffle.None,
            };
            world = new World(config, "", 0, "");
            (world.Regions.Single(x => x.Name == "Misery Mire") as MiseryMire).Medallion = Ether;
            (world.Regions.Single(x => x.Name == "Turtle Rock") as TurtleRock).Medallion = Quake;
            /* Here we use the assumptions that single/multiple reward checks yield true if the rewards are missing */
            foreach (var region in world.Regions.OfType<IReward>()) {
                region.Reward = region.Reward == Agahnim ? Agahnim : None;
            }
        }

        [TestFixture]
        public class SMNormalTests : LogicTests {

            [SetUp]
            public void Setup() => Setup(SMLogic.Normal);

            [TestCaseSource(nameof(Regions))]
            public void CanEnterRegion(string name, Case list)
                => AssertThatRegionLogicIsSound(name, list);

            [TestCaseSource(nameof(Locations))]
            public void CanAccessLocation(string name, Case list)
                => AssertThatLocationLogicIsSound(name, list);

            public static IEnumerable<TestCaseData> Regions() => LogicCaseData(ForRegionsUsing<SMNormal>());
            public static IEnumerable<TestCaseData> Locations() => LogicCaseData(ForLocationsUsing<SMNormal>());

        }

        [TestFixture]
        public class SMHardTests : LogicTests {

            [SetUp]
            public void Setup() => Setup(SMLogic.Hard);

            [TestCaseSource(nameof(Regions))]
            public void CanEnterRegion(string name, Case list)
                => AssertThatRegionLogicIsSound(name, list);

            [TestCaseSource(nameof(Locations))]
            public void CanAccessLocation(string name, Case list)
                => AssertThatLocationLogicIsSound(name, list);

            public static IEnumerable<TestCaseData> Regions() => LogicCaseData(ForRegionsUsing<SMHard>());
            public static IEnumerable<TestCaseData> Locations() => LogicCaseData(ForLocationsUsing<SMHard>());

        }

        [TestFixture]
        public class SMNormalKeysanityTests : LogicTests {

            [SetUp]
            public void Setup() => Setup(SMLogic.Normal, keysanity: true);

            [TestCaseSource(nameof(Regions))]
            public void CanEnterRegion(string name, Case list)
                => AssertThatRegionLogicIsSound(name, list, keysanity: true);

            [TestCaseSource(nameof(Locations))]
            public void CanAccessLocation(string name, Case list)
                => AssertThatLocationLogicIsSound(name, list, keysanity: true);

            public static IEnumerable<TestCaseData> Regions() => LogicCaseData(ForRegionsUsing<SMNormalKeysanity>());
            public static IEnumerable<TestCaseData> Locations() => LogicCaseData(ForLocationsUsing<SMNormalKeysanity>());

        }

        [TestFixture]
        public class SMHardKeysanityTests : LogicTests {

            [SetUp]
            public void Setup() => Setup(SMLogic.Hard, keysanity: true);

            [TestCaseSource(nameof(Regions))]
            public void CanEnterRegion(string name, Case list)
                => AssertThatRegionLogicIsSound(name, list, keysanity: true);

            [TestCaseSource(nameof(Locations))]
            public void CanAccessLocation(string name, Case list)
                => AssertThatLocationLogicIsSound(name, list, keysanity: true);

            public static IEnumerable<TestCaseData> Regions() => LogicCaseData(ForRegionsUsing<SMHardKeysanity>());
            public static IEnumerable<TestCaseData> Locations() => LogicCaseData(ForLocationsUsing<SMHardKeysanity>());

        }

        void AssertThatRegionLogicIsSound(string name, Case list, bool keysanity = false) {
            var region = world.GetRegion(name);

            AssertThatLogicIsSound(region.CanEnter, name, list, keysanity);
        }

        void AssertThatLocationLogicIsSound(string name, Case list, bool keysanity = false) {
            var location = world.Locations.Get(name);

            AssertThatLogicIsSound(location.CanAccess, name, list, keysanity);
        }

        void AssertThatLogicIsSound(Func<Progression, bool> accessWith, string name, Case @case, bool keysanity) {
            var inventory = Arrange();
            Assert.That(accessWith(inventory), Is.True);

            foreach (var index in @case.IndicesToSkip) {
                inventory = Arrange(index);
                var message = @case.ElementAt(index).DescribeSkipped();
                Assert.That(accessWith(inventory), Is.False, message);
            }

            Progression Arrange(int? index = null) {
                foreach (var location in world.Locations) {
                    location.Item = null;
                }
                var inventory = @case.Prepare(world, name, index);
                if (!keysanity) {
                    inventory.Add(AllKeycards(world));
                }
                return inventory;
            }
        }

        static IEnumerable<TestCaseData> LogicCaseData(IEnumerable<Locality> localities) {
            return from locality in localities
                   from @case in locality.Cases
                   select new TestCaseData(locality.Name, @case)
                       .SetName($"{{m}}({{0}},\"{string.Join(" ", @case)}\")");
        }

        static IEnumerable<Item> AllKeycards(World world) {
            yield return new Item(CardCrateriaL1, world);
            yield return new Item(CardCrateriaL2, world);
            yield return new Item(CardCrateriaBoss, world);
            yield return new Item(CardBrinstarL1, world);
            yield return new Item(CardBrinstarL2, world);
            yield return new Item(CardBrinstarBoss, world);
            yield return new Item(CardMaridiaL1, world);
            yield return new Item(CardMaridiaL2, world);
            yield return new Item(CardMaridiaBoss, world);
            yield return new Item(CardNorfairL1, world);
            yield return new Item(CardNorfairL2, world);
            yield return new Item(CardNorfairBoss, world);
            yield return new Item(CardLowerNorfairL1, world);
            yield return new Item(CardLowerNorfairBoss, world);
            yield return new Item(CardWreckedShipL1, world);
            yield return new Item(CardWreckedShipBoss, world);
        }

    }

}
