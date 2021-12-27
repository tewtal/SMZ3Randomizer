using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SuperMetroid {

    static class Playthrough {

        public static List<Dictionary<string, string>> Generate(List<World> worlds, Config config) {
            var spheres = new List<Dictionary<string, string>>();
            var items = new List<Item>();

            var totalItemCount = worlds.SelectMany(w => w.Items).Count();
            while (items.Count < totalItemCount) {
                var sphere = new Dictionary<string, string>();

                var allLocations = worlds.SelectMany(w => w.Locations.Available(items.Where(i => i.World == w).ToList())).ToList();
                var allItems = allLocations.Select(l => l.Item).ToList();
                var addedItems = allItems.Except(items).ToList();
                if (!addedItems.Any()) {
                    /* No new items added, we got a problem */
                    var inaccessibleLocations = worlds.SelectMany(x => x.Locations).Where(l => !allLocations.Contains(l)).ToList();
                    var unplacedItems = inaccessibleLocations.Select(x => x.Item).ToList();
                    throw new Exception("Could not generate playthrough, all items are not accessible");
                }

                foreach (var item in addedItems.Where(i =>
                     i.Name.Contains("Progression") ||
                     (i.Type != ItemType.Missile &&
                      i.Type != ItemType.Super &&
                      i.Type != ItemType.PowerBomb &&
                      i.Type != ItemType.ETank &&
                      i.Type != ItemType.ReserveTank)
                )) {
                    var location = allLocations.First(l => l.Item == item);
                    AddLocation(sphere, location, item, config.MultiWorld);
                }

                spheres.Add(sphere);
                items = allItems;
            }

            return spheres;
        }

        static void AddLocation(Dictionary<string, string> sphere, Location location, Item item, bool multiWorld) {
            sphere.Add(
                multiWorld ? $"{location.Name} ({location.Region.World.Player})"
                           : $"{location.Name}",
                multiWorld ? $"{item.Name} ({item.World.Player})"
                           : $"{item.Name}"
            );
        }
    }

}
