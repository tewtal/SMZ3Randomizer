using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SMZ3 {

    class Playthrough {

        readonly List<World> worlds;
        readonly Config config;

        public Playthrough(List<World> worlds, Config config) {
            this.worlds = worlds;
            this.config = config;
        }

        public List<Dictionary<string, string>> Generate() {
            var spheres = new List<Dictionary<string, string>>();
            var locations = new List<Location>();
            var items = new List<Item>();

            var totalItemCount = worlds.SelectMany(w => w.Items).Count();
            while (items.Count < totalItemCount) {
                var sphere = new Dictionary<string, string>();

                var allLocations = worlds.SelectMany(w => w.Locations.Available(items.Where(i => i.World == w)));
                var newLocations = allLocations.Except(locations).ToList();
                var newItems = newLocations.Select(l => l.Item).ToList();
                locations.AddRange(newLocations);
                items.AddRange(newItems);

                if (!newItems.Any()) {
                    /* With no new items added we might have a problem, so list inaccessable items */
                    var inaccessibleLocations = worlds.SelectMany(w => w.Locations).Where(l => !locations.Contains(l)).ToList();
                    if (inaccessibleLocations.Select(l => l.Item).Count() >= (5 * worlds.Count))
                        throw new Exception("Too many inaccessible items, seed likely impossible.");

                    var n = 0;
                    foreach (var location in inaccessibleLocations) {
                        if (config.GameMode == GameMode.Multiworld) {
                            sphere.Add($"Inaccessable Item #{n += 1}: {location.Name} ({location.Region.World.Player})", $"{location.Item.Name} ({location.Item.World.Player})");
                        }
                        else {
                            sphere.Add($"Inaccessable Item #{n += 1}: {location.Name}", $"{location.Item.Name}");
                        }
                    }
                    spheres.Add(sphere);
                    return spheres;
                }

                foreach (var location in newLocations) {
                    if (!location.Item.Progression)
                        continue;

                    if (config.GameMode == GameMode.Multiworld) {
                        sphere.Add($"{location.Name} ({location.Region.World.Player})", $"{location.Item.Name} ({location.Item.World.Player})");
                    }
                    else {
                        sphere.Add($"{location.Name}", $"{location.Item.Name}");
                    }
                }
                spheres.Add(sphere);

                if (spheres.Count > 100)
                    throw new Exception("Too many spheres, seed likely impossible.");
            }

            return spheres;
        }

    }

}
