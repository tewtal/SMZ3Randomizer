using System;
using System.Collections.Generic;
using System.Linq;
using static Randomizer.SMZ3.WorldState;

namespace Randomizer.SMZ3 {

    static class Playthrough {

        public static List<Dictionary<string, string>> Generate(List<World> worlds, Config config) {
            var spheres = new List<Dictionary<string, string>>();
            var locations = new List<Location>();
            var items = new List<Item>();

            foreach (var world in worlds) {
                world.ForwardSearch = true;
                if (!world.Config.Keysanity) {
                    items.AddRange(Item.CreateKeycards(world));
                }
            }

            var totalItemCount = worlds.SelectMany(w => w.Items).Count();
            while (items.Count < totalItemCount) {
                var sphere = new Dictionary<string, string>();

                var allLocations = worlds.SelectMany(w => w.Locations.Available(items.Where(i => i.World == w)));
                var addedLocations = allLocations.Except(locations).ToList();
                var addedItems = addedLocations.Select(l => l.Item).ToList();
                locations.AddRange(addedLocations);
                items.AddRange(addedItems);

                if (!addedItems.Any()) {
                    /* With no new items added we might have a problem, so list inaccessable items */
                    var inaccessibleLocations = worlds.SelectMany(w => w.Locations).Except(locations).ToList();
                    if (inaccessibleLocations.Count >= (15 * worlds.Count))
                        throw new Exception("Too many inaccessible items, seed likely impossible.");

                    var n = 0;
                    foreach (var location in inaccessibleLocations) {
                        AddInaccessible(sphere, location, n += 1, config.MultiWorld);
                    }
                    spheres.Add(sphere);
                    break;
                }

                foreach (var location in addedLocations) {
                    if (location.Item.Progression || config.Keysanity && (location.Item.IsDungeonItem || location.Item.IsKeycard || location.Item.IsSmMap)) {
                        AddLocation(sphere, location, config.MultiWorld);
                    }
                }
                spheres.Add(sphere);

                if (spheres.Count > 100)
                    throw new Exception("Too many spheres, seed likely impossible.");
            }

            foreach (var world in worlds) {
                world.ForwardSearch = false;
            }

            var rewardSphere = new Dictionary<string, string>();
            /* Add Crystal/Pendant/Boss Token Prizes to playthrough */
            foreach (var region in worlds.SelectMany(w => w.Regions.OfType<IReward>().Where(r => r.Reward != RewardType.Agahnim))) {
                AddPrize(rewardSphere, region as Region, region.Reward, config.MultiWorld);
            }

            /* Add Medallion requirements to playthrough */
            foreach (var region in worlds.SelectMany(w => w.Regions.OfType<IMedallionAccess>())) {
                AddMedallion(rewardSphere, region as Region, region.Medallion, config.MultiWorld);
            }

            spheres.Add(rewardSphere);
            return spheres;
        }

        static void AddLocation(Dictionary<string, string> sphere, Location location, bool multiWorld) {
            sphere.Add(
                multiWorld ? $"{location.Name} ({location.Region.World.Player})"
                           : $"{location.Name}",
                multiWorld ? $"{location.Item.Name} ({location.Item.World.Player})"
                           : $"{location.Item.Name}"
            );
        }

        static void AddPrize(Dictionary<string, string> sphere, Region region, RewardType reward, bool multiWorld) {
            sphere.Add(
                multiWorld ? $"Prize - {region.Area} - {region.World.Player}"
                           : $"Prize - {region.Area}",
                reward.GetDescription()
            );
        }

        static void AddMedallion(Dictionary<string, string> sphere, Region region, Medallion medallion, bool multiWorld) {
            sphere.Add(
                multiWorld ? $"Medallion Required - {region.Area} - {region.World.Player}"
                           : $"Medallion Required - {region.Area}",
                medallion.GetDescription()
            );
        }

        static void AddInaccessible(Dictionary<string, string> sphere, Location location, int n, bool multiWorld) {
            sphere.Add(
                multiWorld ? $"Inaccessible Item #{n}: {location.Name} ({location.Region.World.Player})"
                           : $"Inaccessible Item #{n}: {location.Name}",
                multiWorld ? $"{location.Item.Name} ({location.Item.World.Player})"
                           : $"{location.Item.Name}"
            );
        }
    }

}
