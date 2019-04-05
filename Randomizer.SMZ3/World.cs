using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SMZ3 {

    class World {

        public List<Location> Locations { get; set; }
        public List<Region> Regions { get; set; }
        public List<Item> Items { get; set; }
        public Logic Logic { get; set; }
        public string Player { get; set; }

        public World(Logic logic, string player) {
            Logic = logic;
            Player = player;

            Regions = new List<Region> {
                new Regions.Zelda.CastleTower(this, Logic),
                new Regions.Zelda.EasternPalace(this, Logic),
                new Regions.Zelda.DesertPalace(this, Logic),
                new Regions.Zelda.TowerOfHera(this, Logic),
                new Regions.Zelda.PalaceOfDarkness(this, Logic),
                new Regions.Zelda.SwampPalace(this, Logic),
                new Regions.Zelda.SkullWoods(this, Logic),
                new Regions.Zelda.ThievesTown(this, Logic),
                new Regions.Zelda.IcePalace(this, Logic),
                new Regions.Zelda.MiseryMire(this, Logic),
                new Regions.Zelda.TurtleRock(this, Logic),
                new Regions.Zelda.GanonTower(this, Logic),
                new Regions.Zelda.LightWorld.DeathMountain.West(this, Logic),
                new Regions.Zelda.LightWorld.DeathMountain.East(this, Logic),
                new Regions.Zelda.LightWorld.NorthWest(this, Logic),
                new Regions.Zelda.LightWorld.NorthEast(this, Logic),
                new Regions.Zelda.LightWorld.South(this, Logic),
                new Regions.Zelda.HyruleCastle(this, Logic),
                new Regions.Zelda.DarkWorld.DeathMountain.West(this, Logic),
                new Regions.Zelda.DarkWorld.DeathMountain.East(this, Logic),
                new Regions.Zelda.DarkWorld.NorthWest(this, Logic),
                new Regions.Zelda.DarkWorld.NorthEast(this, Logic),
                new Regions.Zelda.DarkWorld.South(this, Logic),
                new Regions.Zelda.DarkWorld.Mire(this, Logic),
                new Regions.SuperMetroid.Crateria.Central(this, Logic),
                new Regions.SuperMetroid.Crateria.West(this, Logic),
                new Regions.SuperMetroid.Crateria.East(this, Logic),
                new Regions.SuperMetroid.Brinstar.Blue(this, Logic),
                new Regions.SuperMetroid.Brinstar.Green(this, Logic),
                new Regions.SuperMetroid.Brinstar.Kraid(this, Logic),
                new Regions.SuperMetroid.Brinstar.Pink(this, Logic),
                new Regions.SuperMetroid.Brinstar.Red(this, Logic),
                new Regions.SuperMetroid.Maridia.Outer(this, Logic),
                new Regions.SuperMetroid.Maridia.Inner(this, Logic),
                new Regions.SuperMetroid.NorfairUpper.West(this, Logic),
                new Regions.SuperMetroid.NorfairUpper.East(this, Logic),
                new Regions.SuperMetroid.NorfairUpper.Crocomire(this, Logic),
                new Regions.SuperMetroid.NorfairLower.West(this, Logic),
                new Regions.SuperMetroid.NorfairLower.East(this, Logic),
                new Regions.SuperMetroid.WreckedShip(this, Logic)
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

        public void Setup(Random rnd) {
            SetMedallions(rnd);
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

    }

}
