using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SMZ3 {

    class Playthrough {

        readonly List<World> worlds;

        public Playthrough(List<World> worlds) {
            this.worlds = worlds;
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
                    /* With no new items added we might have a problem, so list inaccessable items */
                    var inaccessibleLocations = worlds.SelectMany(x => x.Locations).Where(l => !newLocations.Contains(l)).ToList();
                    var inaccessableItems = inaccessibleLocations.Select(x => x.Item).ToList();

                    if (inaccessableItems.Count >= (5 * worlds.Count)) {
                        throw new Exception("Too many inaccessible items, seed likely impossible.");
                    }

                    var n = 0;
                    foreach (var item in inaccessableItems) {
                        var itemLocation = inaccessibleLocations.Find(x => x.Item == item);
                        sphere.Add($"Inaccessable Item - #{n += 1} ({itemLocation.Name} - {itemLocation.Region.World.Player})", $"{item.Name} ({item.World.Player})");
                    }
                    spheres.Add(sphere);
                    return spheres;
                }

                prevCount = newItems.Count;
                foreach (var addedItem in addedItems.Where(i => i.Progression)) {
                    var itemLocation = newLocations.Where(l => l.Item == addedItem).First();
                    sphere.Add($"{itemLocation.Name} ({itemLocation.Region.World.Player})", $"{addedItem.Name} ({addedItem.World.Player})");
                }

                spheres.Add(sphere);
                items = newItems;

                if(spheres.Count > 100) {
                    throw new Exception("Too many spheres, seed likely impossible.");
                }
            }

            return spheres;
        }

    }

}
