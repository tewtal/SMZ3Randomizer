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

        void SetRewards(Dictionary<string, RewardType> rewards) {
            foreach(var reward in rewards) {
                ((IReward)regionLookup[reward.Key]).Reward = reward.Value;
            }
        }

        void SetMedallions(Dictionary<string, Medallion> medallions) {
            foreach (var medallion in medallions) {
                ((IMedallionAccess)regionLookup[medallion.Key]).Medallion = medallion.Value;
            }
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

        public Dictionary<string, RewardType> Rewards { get; init; }
        public Dictionary<string, Medallion> Medallions { get; init; }
        public int TowerCrystals { get; init; }
        public int GanonCrystals { get; init; }
        public int TourianBossTokens { get; init; }

        static readonly IEnumerable<RewardType> BaseRewards = new[] {
            PendantGreen, PendantNonGreen, PendantNonGreen, CrystalRed, CrystalRed,
            CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue,
            BossTokenKraid, BossTokenPhantoon, BossTokenDraygon, BossTokenRidley,
        };

        public static WorldState Generate(World world, Random rnd) {
            var config = world.Config;
            var rewards = BaseRewards.Shuffle(rnd);

            // Assign medallions to areas
            foreach (var medallionRegion in world.Regions.OfType<IMedallionAccess>()) {
                medallionRegion.Medallion = (Medallion)rnd.Next(3);
            }

            // Assign pendants weighted towards ALTTP since pendants in SM have a higher gameplay impact
            var pendantRegions = world.Regions
                .Where(r => r is SMRegion && r is IReward)
                .Concat(Enumerable.Repeat(world.Regions.Where(r => r is Z3Region && r is IReward), 3).SelectMany(r => r))
                .OfType<IReward>().Where(x => x.Reward == None);

            var pendantRewards = rewards.Where(r => (r == PendantGreen || r == PendantNonGreen)).ToList();
            foreach (var pendant in pendantRewards) {
                var region = pendantRegions.Where(r => r.Reward == None).Shuffle(rnd).First();
                region.Reward = pendant;
            }

            // Assign the non-pendant rewards
            var regions = world.Regions.OfType<IReward>().Where(x => x.Reward == None).Shuffle(rnd);
            foreach (var (region, reward) in regions.Zip(rewards.Where(r => (r & AnyPendant) == 0))) {
                region.Reward = reward;
            }

            var allRewards = world.Regions.OfType<IReward>().ToList();
            if (allRewards.Any(r => r.Reward == None)) {
                throw new Exception("All rewards are not assigned");
            }

            return new() {
                Rewards = world.Regions.OfType<IReward>().ToDictionary(r => ((Region)r).Name, r => r.Reward),
                Medallions = world.Regions.OfType<IMedallionAccess>().ToDictionary(r => ((Region)r).Name, r => r.Medallion),
                TowerCrystals = config.OpenTower == OpenTower.Random ? rnd.Next(8) : (int)config.OpenTower,
                GanonCrystals = config.GanonVulnerable == GanonVulnerable.Random ? rnd.Next(8) : (int)config.GanonVulnerable,
                TourianBossTokens = config.OpenTourian == OpenTourian.Random ? rnd.Next(5) : (int)config.OpenTourian,
            };
        }

    }

}
