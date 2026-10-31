using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static System.Linq.Enumerable;
using static Randomizer.SMZ3.WorldState;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Tests.Logic {

    public class SkillTests {

        static World CreateWorld(Config config) {
            var world = new World(config, "", 0, "");
            world.Setup(new WorldState {
                Rewards = Repeat(RewardType.None, 7 + 4),
                Medallions = new[] { Medallion.Ether, Medallion.Quake },
                GanonCrystals = 0,
                TowerCrystals = 0,
                TourianBossTokens = 0,
            });
            return world;
        }

        static Progression Items(World world, params ItemType[] types) =>
            new(types.Select(t => new Item(t, world)), world.Config.Skills);

        static Config Parse(params (string, string)[] options) =>
            RandomizerOptions.Parse(options.ToDictionary(o => o.Item1, o => o.Item2));

        [Test]
        public void LogicIsOnlyAPresetForTheSkills() {
            Assert.That(Parse(("smlogic", "normal")).Skills.HardSkills, Is.False);
            Assert.That(Parse(("smlogic", "hard")).Skills.HardSkills, Is.True);
        }

        [Test]
        public void ExplicitSkillOverridesThePreset() {
            var config = Parse(("smlogic", "hard"), ("hardskills", "disabled"), ("mockball", "enabled"));
            Assert.That(config.Skills.HardSkills, Is.False);
            Assert.That(config.Skills.MockBall, Is.True);
            Assert.That(config.Skills.InfiniteBombJump, Is.True);
        }

        [Test]
        public void HardSkillsOnNormalLogicUsesHardRoutes() {
            // Hard drops the energy requirement on the Crateria gauntlet energy tank
            var normal = CreateWorld(Parse(("smlogic", "normal")));
            var hard = CreateWorld(Parse(("smlogic", "normal"), ("hardskills", "enabled")));
            var gauntlet = new[] { CardCrateriaL1, Morph, Bombs };
            Assert.That(normal.Locations.Get("Energy Tank, Gauntlet").Available(Items(normal, gauntlet)), Is.False);
            Assert.That(hard.Locations.Get("Energy Tank, Gauntlet").Available(Items(hard, gauntlet)), Is.True);
        }

        [Test]
        public void HealthIndifferentIgnoresEnergyRequirements() {
            var gauntlet = new[] { CardCrateriaL1, Morph, Bombs };
            var world = CreateWorld(Parse(("healthindifferent", "enabled")));
            Assert.That(world.Locations.Get("Energy Tank, Gauntlet").Available(Items(world, gauntlet)), Is.True);
        }

        [Test]
        public void InfiniteBombJumpCanBeDisabled() {
            var items = new[] { Morph, Bombs };
            var on = CreateWorld(Parse());
            var off = CreateWorld(Parse(("infinitebombjump", "disabled")));
            Assert.That(Items(on, items).CanFly(), Is.True);
            Assert.That(Items(off, items).CanFly(), Is.False);
        }

        [Test]
        public void DarkRoomsDoNotNeedTheLamp() {
            var normal = CreateWorld(Parse());
            var skilled = CreateWorld(Parse(("darkrooms", "enabled")));
            Assert.That(Items(normal).CanNavigateDarkRooms(), Is.False);
            Assert.That(Items(normal, Lamp).CanNavigateDarkRooms(), Is.True);
            Assert.That(Items(skilled).CanNavigateDarkRooms(), Is.True);
        }

    }

}
