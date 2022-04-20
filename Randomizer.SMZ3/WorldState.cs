using System;
using System.Collections.Generic;
using System.Linq;
using static Randomizer.SMZ3.DropPrize;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3 {

    class WorldState {

        public enum Medallion {
            Bombos,
            Ether,
            Quake,
        }

        public record DropPrizeRecord(
            IList<DropPrize> Packs,
            IList<DropPrize> TreePulls,
            DropPrize CrabContinous,
            DropPrize CrabFinal,
            DropPrize Stun,
            DropPrize Fish
        );

        public IEnumerable<RewardType> Rewards { get; init; }
        public IEnumerable<Medallion> Medallions { get; init; }

        public int TowerCrystals { get; init; }
        public int GanonCrystals { get; init; }
        public int TourianBossTokens { get; init; }

        public DropPrizeRecord DropPrizes { get; init; }

        public static WorldState Generate(Config config, Random rnd) {
            return new() {
                Rewards = DistributeRewards(rnd),
                Medallions = GenerateMedallions(rnd),
                TowerCrystals = config.OpenTower == OpenTower.Random ? rnd.Next(8) : (int)config.OpenTower,
                GanonCrystals = config.GanonVulnerable == GanonVulnerable.Random ? rnd.Next(8) : (int)config.GanonVulnerable,
                TourianBossTokens = config.OpenTourian == OpenTourian.Random ? rnd.Next(5) : (int)config.OpenTourian,
                DropPrizes = ShuffleDropPrizes(rnd),
            };
        }

        #region Rewards

        static readonly IEnumerable<RewardType> BaseRewards = new[] {
            PendantGreen, PendantNonGreen, PendantNonGreen, CrystalRed, CrystalRed,
            CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue,
            AnyBossToken, AnyBossToken, AnyBossToken, AnyBossToken,
        };

        static readonly IEnumerable<RewardType> BossTokens = new[] {
            BossTokenKraid, BossTokenPhantoon, BossTokenDraygon, BossTokenRidley,
        };

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

        #endregion

        #region Medallions

        static Medallion[] GenerateMedallions(Random rnd) => new[] {
            (Medallion)rnd.Next(3),
            (Medallion)rnd.Next(3),
        };

        #endregion

        #region Drop prizes

        static readonly IEnumerable<DropPrize> BaseDropPrizes = new[] {
            Heart, Heart, Heart, Heart, Green, Heart, Heart, Green,         // pack 1
            Blue, Green, Blue, Red, Blue, Green, Blue, Blue,                // pack 2
            FullMagic, Magic, Magic, Blue, FullMagic, Magic, Heart, Magic,  // pack 3
            Bomb1, Bomb1, Bomb1, Bomb4, Bomb1, Bomb1, Bomb8, Bomb1,         // pack 4
            Arrow5, Heart, Arrow5, Arrow10, Arrow5, Heart, Arrow5, Arrow10, // pack 5
            Magic, Green, Heart, Arrow5, Magic, Bomb1, Green, Heart,        // pack 6
            Heart, Fairy, FullMagic, Red, Bomb8, Heart, Red, Arrow10,       // pack 7
            Green, Blue, Red, // from pull trees
            Green, Red, // from prize crab
            Green, // stunned prize
            Red, // saved fish prize
        };

        static DropPrizeRecord ShuffleDropPrizes(Random rnd) {
            const int nrPackDrops = 8 * 7;
            const int nrTreePullDrops = 3;

            IEnumerable<DropPrize> prizes = BaseDropPrizes.Shuffle(rnd);

            (var packs, prizes) = prizes.SplitOff(nrPackDrops);
            (var treePulls, prizes) = prizes.SplitOff(nrTreePullDrops);
            (var crabContinous, var crabFinalDrop, prizes) = prizes;
            (var stun, prizes) = prizes;
            (var fish, _) = prizes;
            return new(packs.ToList(), treePulls.ToList(),
                crabContinous, crabFinalDrop, stun, fish
            );
        }

        #endregion

    }

}
