using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SuperMetroid {

    class Playthrough {

        readonly List<World> worlds;
        readonly Config config;
        
        public Playthrough(List<World> worlds, Config config) {
            this.worlds = worlds;
            this.config = config;
        }

        public List<Dictionary<string, string>> Generate() {
            var spheres = new List<Dictionary<string, string>>();

            var items = new List<Item>();
            var prevCount = 0;
            while (items.Count < worlds.SelectMany(w => w.Items).Count()) {
                var sphere = new Dictionary<string, string>();
                var newLocations = worlds.SelectMany(w => w.Locations.Available(items.Where(i => i.World == w).ToList())).ToList();
                var newItems = newLocations.Select(l => l.Item).ToList();
                var addedItems = newItems.Where(i => !items.Contains(i)).ToList();
                if (prevCount == newItems.Count) {
                    /* No new items added, we got a problem */
                    var inaccessibleLocations = worlds.SelectMany(x => x.Locations).Where(l => !newLocations.Contains(l)).ToList();
                    var unplacedItems = inaccessibleLocations.Select(x => x.Item).ToList();
                    throw new Exception("Could not generate playthrough, all items are not accessible");
                }

                prevCount = newItems.Count;
                foreach (var addedItem in addedItems.Where(i =>
                     i.Name.Contains("Progression") ||
                     (i.Type != ItemType.Missile &&
                      i.Type != ItemType.Super &&
                      i.Type != ItemType.PowerBomb &&
                      i.Type != ItemType.ETank &&
                      i.Type != ItemType.ReserveTank)
                )) {
                    var itemLocation = newLocations.Where(l => l.Item == addedItem).First();
                    if (config.MultiWorld) {
                        sphere.Add($"{itemLocation.Name} ({itemLocation.Region.World.Player})", $"{addedItem.Name} ({addedItem.World.Player})");
                    } else {
                        sphere.Add($"{itemLocation.Name}", $"{addedItem.Name}");
                    }
                }

                spheres.Add(sphere);
                items = newItems;
            }

            return spheres;
        }

    }

}
