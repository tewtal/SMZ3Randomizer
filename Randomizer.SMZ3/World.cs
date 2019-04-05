using System;
using System.Collections.Generic;
using System.Linq;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3 {

    class World {

        public List<Location> Locations { get; set; }
        public List<Region> Regions { get; set; }
        public List<Item> Items { get; set; }
        public Config Config { get; set; }
        public string Player { get; set; }

        public World(Config config, string player) {
            Config = config;
            Player = player;

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
                new Regions.Zelda.GanonTower(this, Config),
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
            Items = new List<Item>();
        }

        public bool CanEnter(string regionName, List<Item> items) {
            var region = Regions.Find(r => r.Name == regionName);
            if (region == null)
                throw new ArgumentException($"World.CanEnter: Invalid region name {regionName}", nameof(regionName));
            return region.CanEnter(items);
        }

        public bool CanAquire(List<Item> items, RewardType reward) {
            return Regions.OfType<Reward>().First(x => reward == x.Reward).CanComplete(items);
        }

        public bool CanAquireAll(List<Item> items, params RewardType[] rewards) {
            return Regions.OfType<Reward>().Where(x => rewards.Contains(x.Reward)).All(x => x.CanComplete(items));
        }

        public void Setup(Random rnd) {
            SetMedallions(rnd);
            SetRewards(rnd);
        }

        private void SetMedallions(Random rnd) {
            foreach (var region in Regions.OfType<MedallionAccess>()) {
                region.Medallion = rnd.Next(0, 2) switch {
                    0 => ItemType.Bombos,
                    1 => ItemType.Quake,
                    _ => ItemType.Ether
                };
            }
        }

        private void SetRewards(Random rnd) {
            var rewards = new[] {
                PendantGreen, PendantNonGreen, PendantNonGreen, CrystalRed, CrystalRed,
                CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue }.Shuffle(rnd);
            foreach (var region in Regions.OfType<Reward>().Where(x => x.Reward == None)) {
                region.Reward = rewards.First();
                rewards.Remove(region.Reward);
            }
        }

    }

}
