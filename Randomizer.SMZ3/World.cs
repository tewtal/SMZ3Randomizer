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
        public WorldState WorldState { get; private set; }

        public IEnumerable<Item> Items {
            get { return Locations.Select(l => l.Item).Where(i => i != null); }
        }

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

        public void Setup(WorldState state) {
            WorldState = state;

            // Todo: Use the corresponding config setting, under whichever identifier we assign it,
            // when ready to shuffle in SM defeat statues with the crystals and pendants.
            var regions = /*Config.[RewardSetting]*/true ? Z3RewardRegions : SMZ3RewardRegions;
            foreach (var (region, reward) in regions.Zip(state.Rewards)) {
                region.Reward = reward;
            }

            var (mm, tr, _) = state.Medallions;
            (GetRegion("Misery Mire") as IMedallionAccess).Medallion = mm;
            (GetRegion("Turtle Rock") as IMedallionAccess).Medallion = tr;
        }

        IEnumerable<IReward> Z3RewardRegions => Regions.OfType<IReward>().Where(x => x.Reward == None);
        IEnumerable<IReward> SMZ3RewardRegions => Regions.OfType<IReward>().Where(x => x.Reward == None || x.Reward == GoldenFourBoss);

        public bool CanEnter(string regionName, Progression items) {
            var region = regionLookup[regionName];
            if (region == null)
                throw new ArgumentException($"World.CanEnter: Invalid region name {regionName}", nameof(regionName));
            return region.CanEnter(items);
        }

        public bool CanAquire(Progression items, RewardType reward) {
            // For the purpose of logic unit tests, if no region has the reward then CanAquire is satisfied
            return Regions.OfType<IReward>().FirstOrDefault(x => reward == x.Reward)?.CanComplete(items) ?? true;
        }

        public bool CanAquireAll(Progression items, params RewardType[] rewards) {
            return Regions.OfType<IReward>().Where(x => rewards.Contains(x.Reward)).All(x => x.CanComplete(items));
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

        static readonly IEnumerable<RewardType> BaseRewards = new[] {
            PendantGreen, PendantNonGreen, PendantNonGreen, CrystalRed, CrystalRed,
            CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue,
        };

        public static WorldState Generate(Random rnd) {
            var rewards = BaseRewards.Shuffle(rnd);
            var mm = (Medallion)rnd.Next(3);
            var tr = (Medallion)rnd.Next(3);
            return new() {
                Rewards = rewards,
                Medallions = new[] { mm, tr }
            };
        }

    }

}
