using System;
using System.Collections.Generic;
using System.Linq;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3 {

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
