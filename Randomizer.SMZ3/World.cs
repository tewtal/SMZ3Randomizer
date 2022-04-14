using System;
using System.Collections.Generic;
using System.Linq;
using static Randomizer.SMZ3.RewardType;
using static Randomizer.SMZ3.WorldState;

namespace Randomizer.SMZ3 {

    class World {

        public List<Location> Locations { get; set; }
        public List<Region> Regions { get; set; }
        public Config Config { get; set; }
        public string Player { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public WorldState WorldState { get; set; }

        public int TowerCrystals => WorldState?.TowerCrystals ?? 7;
        public int GanonCrystals => WorldState?.GanonCrystals ?? 7;
        public int TourianBossTokens => WorldState?.TourianBossTokens ?? 4;

        public IEnumerable<Item> Items {
            get { return Locations.Select(l => l.Item).Where(i => i != null); }
        }

        public bool ForwardSearch { get; set; } = false;

        private Dictionary<int, IReward[]> rewardLookup { get; set; }
        private Dictionary<string, Location> locationLookup { get; set; }
        private Dictionary<string, Region> regionLookup { get; set; }

        public Location GetLocation(string name) => locationLookup[name];
        public Region GetRegion(string name) => regionLookup[name];

        public World(Config config, string player, int id, string guid) {
            Config = config;
            Player = player;
            Id = id;
            Guid = guid;

            Regions = new List<Region> {
                new Regions.Zelda.CastleTower(this, Config),
                new Regions.Zelda.EasternPalace(this, Config),
                new Regions.Zelda.DesertPalace(this, Config),
                new Regions.Zelda.TowerOfHera(this, Config),
                new Regions.Zelda.PalaceOfDarkness(this, Config),
                new Regions.Zelda.SwampPalace(this, Config),
                new Regions.Zelda.SkullWoods(this, Config),
                new Regions.Zelda.ThievesTown(this, Config),
                new Regions.Zelda.IcePalace(this, Config),
                new Regions.Zelda.MiseryMire(this, Config),
                new Regions.Zelda.TurtleRock(this, Config),
                new Regions.Zelda.GanonsTower(this, Config),
                new Regions.Zelda.LightWorld.DeathMountain.West(this, Config),
                new Regions.Zelda.LightWorld.DeathMountain.East(this, Config),
                new Regions.Zelda.LightWorld.NorthWest(this, Config),
                new Regions.Zelda.LightWorld.NorthEast(this, Config),
                new Regions.Zelda.LightWorld.South(this, Config),
                new Regions.Zelda.HyruleCastle(this, Config),
                new Regions.Zelda.DarkWorld.DeathMountain.West(this, Config),
                new Regions.Zelda.DarkWorld.DeathMountain.East(this, Config),
                new Regions.Zelda.DarkWorld.NorthWest(this, Config),
                new Regions.Zelda.DarkWorld.NorthEast(this, Config),
                new Regions.Zelda.DarkWorld.South(this, Config),
                new Regions.Zelda.DarkWorld.Mire(this, Config),
                new Regions.SuperMetroid.Crateria.West(this, Config),
                new Regions.SuperMetroid.Crateria.Central(this, Config),
                new Regions.SuperMetroid.Crateria.East(this, Config),
                new Regions.SuperMetroid.Brinstar.Blue(this, Config),
                new Regions.SuperMetroid.Brinstar.Green(this, Config),
                new Regions.SuperMetroid.Brinstar.Pink(this, Config),
                new Regions.SuperMetroid.Brinstar.Red(this, Config),
                new Regions.SuperMetroid.Brinstar.Kraid(this, Config),
                new Regions.SuperMetroid.WreckedShip(this, Config),
                new Regions.SuperMetroid.Maridia.Outer(this, Config),
                new Regions.SuperMetroid.Maridia.Inner(this, Config),
                new Regions.SuperMetroid.NorfairUpper.West(this, Config),
                new Regions.SuperMetroid.NorfairUpper.East(this, Config),
                new Regions.SuperMetroid.NorfairUpper.Crocomire(this, Config),
                new Regions.SuperMetroid.NorfairLower.West(this, Config),
                new Regions.SuperMetroid.NorfairLower.East(this, Config),
            };

            Locations = Regions.SelectMany(x => x.Locations).ToList();

            regionLookup = Regions.ToDictionary(r => r.Name, r => r);
            locationLookup = Locations.ToDictionary(l => l.Name, l => l);
            
            foreach(var region in Regions) {
                region.GenerateLocationLookup();
            }
        }

        public bool CanEnter(string regionName, Progression items) {
            var region = regionLookup[regionName];
            if (region == null)
                throw new ArgumentException($"World.CanEnter: Invalid region name {regionName}", nameof(regionName));
            return region.CanEnter(items);
        }

        public bool CanAcquire(Progression items, RewardType reward) {
            // For the purpose of logic unit tests, if no region has the reward then CanAcquire is satisfied
            return Regions.OfType<IReward>().FirstOrDefault(x => reward == x.Reward)?.CanComplete(items) ?? true;
        }

        public bool CanAcquireAll(Progression items, RewardType rewardsMask) {
            return rewardLookup[(int)rewardsMask].All(x => x.CanComplete(items));
        }

        public bool CanAcquireAtLeast(int amount, Progression items, RewardType rewardsMask) {
            return rewardLookup[(int)rewardsMask].Where(x => x.CanComplete(items)).Count() >= amount;
        }

        public void Setup(WorldState state) {
            WorldState = state;
            SetRewards(state.Rewards);
            SetMedallions(state.Medallions);
            SetRewardLookup();
        }

        void SetRewards(IEnumerable<RewardType> rewards) {
            var regions = Regions.OfType<IReward>().Where(x => x.Reward == None);
            foreach (var (region, reward) in regions.Zip(rewards)) {
                region.Reward = reward;
            }
        }

        void SetMedallions(IEnumerable<Medallion> medallions) {
            var (mm, tr, _) = medallions;
            (GetRegion("Misery Mire") as IMedallionAccess).Medallion = mm;
            (GetRegion("Turtle Rock") as IMedallionAccess).Medallion = tr;
        }

        void SetRewardLookup() {
            /* Generate a lookup of all possible regions for any given reward combination for faster lookup later */
            rewardLookup = new Dictionary<int, IReward[]>();
            for (var i = 0; i < 512; i += 1) {
                rewardLookup.Add(i, Regions.OfType<IReward>().Where(x => (((int)x.Reward) & i) != 0).ToArray());
            }
        }

    }

    class WorldState {

        public enum Medallion {
            Bombos,
            Ether,
            Quake,
        }

        public IEnumerable<RewardType> Rewards { get; init; }
        public IEnumerable<Medallion> Medallions { get; init; }
        public int TowerCrystals { get; init; }
        public int GanonCrystals { get; init; }
        public int TourianBossTokens { get; init; }

        static readonly IEnumerable<RewardType> BaseRewards = new[] {
            PendantGreen, PendantNonGreen, PendantNonGreen, CrystalRed, CrystalRed,
            CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue,
            AnyBossToken, AnyBossToken, AnyBossToken, AnyBossToken,
        };

        static readonly IEnumerable<RewardType> BossTokens = new[] {
            BossTokenKraid, BossTokenPhantoon, BossTokenDraygon, BossTokenRidley,
        };

        public static WorldState Generate(Config config, Random rnd) {
            return new() {
                Rewards = DistributeRewards(rnd),
                Medallions = GenerateMedallions(rnd),
                TowerCrystals = config.OpenTower == OpenTower.Random ? rnd.Next(8) : (int)config.OpenTower,
                GanonCrystals = config.GanonVulnerable == GanonVulnerable.Random ? rnd.Next(8) : (int)config.GanonVulnerable,
                TourianBossTokens = config.OpenTourian == OpenTourian.Random ? rnd.Next(5) : (int)config.OpenTourian,
            };
        }

        static IEnumerable<RewardType> DistributeRewards(Random rnd) {
            // Assign four rewards for SM using a "loot table", randomized result
            var smRewards = new Distribution().Generate(dist => dist.Hit(rnd.Next(dist.Sum))).Take(4).ToList();

            // Exclude the SM rewards to get the Z3 lineup
            var z3Rewards = new List<RewardType>(BaseRewards);
            foreach (var reward in smRewards)
                z3Rewards.Remove(reward);

            // Replace "any token" with random specific tokens
            var rewards = z3Rewards.Shuffle(rnd).Concat(smRewards);
            var tokens = new Stack<RewardType>(BossTokens.Shuffle(rnd));
            rewards = rewards.Select(reward => reward == AnyBossToken ? tokens.Pop() : reward);

            return rewards.ToList();
        }

        record Distribution() {
            const int factor = 3;
            public int Boss { get; init; } = 4 * factor;
            public int Blue { get; init; } = 5 * factor;
            public int Red { get; init; } = 2 * factor;
            public int Pend { get; init; } = 2;
            public int Green { get; init; } = 1;
            public int Sum => Boss + Blue + Red + Pend + Green;
            public (RewardType, Distribution) Hit(int p) => p switch {
                _ when (p -= Boss) < 0 => (AnyBossToken, this with { Boss = Boss - factor }),
                _ when (p -= Blue) < 0 => (CrystalBlue, this with { Blue = Blue - factor }),
                _ when (p -= Red) < 0 => (CrystalRed, this with { Red = Red - factor }),
                _ when (p -= Pend) < 0 => (PendantNonGreen, this with { Pend = Pend - 1 }),
                _ => (PendantGreen, this with { Green = Green - 1 }),
            };
        }

        static Medallion[] GenerateMedallions(Random rnd) => new[] {
            (Medallion)rnd.Next(3),
            (Medallion)rnd.Next(3),
        };

    }

}
