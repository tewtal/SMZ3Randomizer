using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static System.Linq.Enumerable;
using static Randomizer.SuperMetroid.Logic;
using static Randomizer.SuperMetroid.Tests.Logic.LogicCases;

namespace Randomizer.SuperMetroid.Tests.Logic {

    public class LogicTests {

        World world;

        internal void Setup(SuperMetroid.Logic logic) {
            var config = new Config { Logic = logic };
            world = new World(config, "", 0, "");
        }

        [TestFixture]
        public class SMCasualTests : LogicTests {

            [SetUp]
            public void Setup() => Setup(Casual);

            [TestCaseSource(nameof(Regions))]
            public void CanEnterRegion(string name, Case list)
                => AssertThatRegionLogicIsSound(name, list);

            [TestCaseSource(nameof(Locations))]
            public void CanAccessLocation(string name, Case list)
                => AssertThatLocationLogicIsSound(name, list);

            public static IEnumerable<TestCaseData> Regions() => LogicCaseData(ForRegionsUsing<CasualCases>());
            public static IEnumerable<TestCaseData> Locations() => LogicCaseData(ForLocationsUsing<CasualCases>());

        }

        [TestFixture]
        public class SMTournamentTests : LogicTests {

            [SetUp]
            public void Setup() => Setup(Tournament);

            [TestCaseSource(nameof(Regions))]
            public void CanEnterRegion(string name, Case list)
                => AssertThatRegionLogicIsSound(name, list);

            [TestCaseSource(nameof(Locations))]
            public void CanAccessLocation(string name, Case list)
                => AssertThatLocationLogicIsSound(name, list);

            public static IEnumerable<TestCaseData> Regions() => LogicCaseData(ForRegionsUsing<TournamentCases>());
            public static IEnumerable<TestCaseData> Locations() => LogicCaseData(ForLocationsUsing<TournamentCases>());

        }

        void AssertThatRegionLogicIsSound(string name, Case list) {
            var region = world.Regions.Single(x => x.Name == name);

            AssertThatLogicIsSound(region.CanEnter, list);
        }

        void AssertThatLocationLogicIsSound(string name, Case list) {
            var location = world.Locations.Single(x => x.Name == name);

            AssertThatLogicIsSound(location.CanAccess, list);
        }

        void AssertThatLogicIsSound(Func<List<Item>, bool> accessWith, Case @case) {
            var inventory = Arrange();
            Assert.That(accessWith(inventory), Is.True);

            foreach (var index in @case.IndicesToSkip) {
                inventory = Arrange(index);
                var message = @case.ElementAt(index).DescribeSkipped();
                Assert.That(accessWith(inventory), Is.False, message);
            }

            List<Item> Arrange(int? index = null) {
                foreach (var location in world.Locations) {
                    location.Item = null;
                }
                return @case.Prepare(world, index);
            }
        }

        static IEnumerable<TestCaseData> LogicCaseData(IEnumerable<Locality> localities) {
            return from locality in localities
                   from @case in locality.Cases
                   select new TestCaseData(locality.Name, @case)
                       .SetName($"{{m}}({{0}},\"{string.Join(" ", @case)}\")");
        }

    }

}
