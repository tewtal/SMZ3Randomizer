using System;
using System.Collections.Generic;
using System.Linq;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3 {

    class World {

        public List<Location> Locations { get; set; }
        public List<Region> Regions { get; set; }
        public Config Config { get; set; }
        public string Player { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public int OpenTower { get; set; } = 0;
        public int OpenTourian { get; set; } = 0;
        public int GanonVulnerable { get; set; } = 0;

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
                new Regions.SuperMetroid.Crateria.Central(this, Config),
                new Regions.SuperMetroid.Crateria.West(this, Config),
                new Regions.SuperMetroid.Crateria.East(this, Config),
                new Regions.SuperMetroid.Brinstar.Blue(this, Config),
                new Regions.SuperMetroid.Brinstar.Green(this, Config),
                new Regions.SuperMetroid.Brinstar.Kraid(this, Config),
                new Regions.SuperMetroid.Brinstar.Pink(this, Config),
                new Regions.SuperMetroid.Brinstar.Red(this, Config),
                new Regions.SuperMetroid.Maridia.Outer(this, Config),
                new Regions.SuperMetroid.Maridia.Inner(this, Config),
                new Regions.SuperMetroid.NorfairUpper.West(this, Config),
                new Regions.SuperMetroid.NorfairUpper.East(this, Config),
                new Regions.SuperMetroid.NorfairUpper.Crocomire(this, Config),
                new Regions.SuperMetroid.NorfairLower.West(this, Config),
                new Regions.SuperMetroid.NorfairLower.East(this, Config),
                new Regions.SuperMetroid.WreckedShip(this, Config)
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

        public void Setup(Random rnd) {
            SetMedallions(rnd);
            SetRewards(rnd);
            SetRewardLookup();
            SetRequirements(rnd);
        }

        void SetMedallions(Random rnd) {
            foreach (var region in Regions.OfType<IMedallionAccess>()) {
                region.Medallion = rnd.Next(3) switch {
                    0 => ItemType.Bombos,
                    1 => ItemType.Ether,
                    _ => ItemType.Quake,
                };
            }
        }

        void SetRewards(Random rnd) {
            var rewards = new[] {
                PendantGreen, PendantNonGreen, PendantNonGreen, CrystalRed, CrystalRed,
                CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue,
                BossTokenKraid, BossTokenPhantoon, BossTokenDraygon, BossTokenRidley}.Shuffle(rnd);
            foreach (var region in Regions.OfType<IReward>().Where(x => x.Reward == None)) {
                region.Reward = rewards.First();
                rewards.Remove(region.Reward);
            }
        }

        // internal for logic unit tests
        internal void SetRewardLookup() {
            // Generate a lookup of all possible regions for any given reward combination for faster lookup later
            rewardLookup = new Dictionary<int, IReward[]>();
            for (var i = 0; i < 512; i += 1) {
                rewardLookup.Add(i, Regions.OfType<IReward>().Where(x => (((int)x.Reward) & i) != 0).ToArray());
            }
        }

        void SetRequirements(Random rnd) {
            OpenTower = Config.OpenTower == SMZ3.OpenTower.Random ? rnd.Next(8) : (int)Config.OpenTower;
            GanonVulnerable = Config.GanonVulnerable == SMZ3.GanonVulnerable.Random ? rnd.Next(8) : (int)Config.GanonVulnerable;
            OpenTourian = Config.OpenTourian == SMZ3.OpenTourian.Random ? rnd.Next(5) : (int)Config.OpenTourian;
        }

    }

}
