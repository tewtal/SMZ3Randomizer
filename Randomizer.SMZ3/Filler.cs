using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SMZ3 {

    class Filler {

        List<World> Worlds { get; set; }
        Config Config { get; set; }
        List<Item> ProgressionItems { get; set; } = new List<Item>();
        List<Item> NiceItems { get; set; } = new List<Item>();
        List<Item> JunkItems { get; set; } = new List<Item>();
        Random Rnd { get; set; }

        public Filler(List<World> worlds, Config config, Random rnd) {
            Worlds = worlds;
            Config = config;
            Rnd = rnd;

            /* Populate item pools and setup each world. */
            foreach (var world in worlds) {
                NiceItems.AddRange(Item.CreateNicePool(world).Shuffle(Rnd));
                JunkItems.AddRange(Item.CreateJunkPool(world).Shuffle(Rnd));
                world.Setup(Rnd);
            }
        }

        public void Fill() {
            foreach (var world in Worlds) {
                /* The dungeon pool order is significant, don't shuffle */
                var dungeon = Item.CreateDungeonPool(world);
                var progression = Item.CreateProgressionPool(world);

                InitialFillInOwnWorld(dungeon, world);
                AssumedFill(dungeon, progression, new[] { world });

                /* We place a PB and Super in Sphere 1 to make sure the filler
                 * doesn't start locking items behind this when there are a
                 * high chance of the trash fill actually making them available */
                FrontFillItemInOwnWorld(progression, ItemType.Super, world);
                FrontFillItemInOwnWorld(progression, ItemType.PowerBomb, world);

                ProgressionItems.AddRange(progression);
            }

            ProgressionItems = ProgressionItems.Shuffle(Rnd);

            /* Place moonpearls and morphs in last 25%/50% of the pool so that
             * they will tend to place in earlier locations.
             * Prefer morphs being pushed too far up the list than moonpearls,
             * so start with morph, followed by moonpearls */
            ReorderItems(ProgressionItems, ItemType.Morph, n => n - Rnd.Next(n / 4));
            ReorderItems(ProgressionItems, ItemType.MoonPearl, n => n - Rnd.Next(n / 2));

            /* GT Trash fill */
            var gtLocations = Worlds.SelectMany(x => x.Locations).Where(x => x.Region is Regions.Zelda.GanonTower).Empty().Shuffle(Rnd);
            var gtTrashLocations = gtLocations.Take(gtLocations.Count() / 2).ToList();
            FastFillLocations(JunkItems, gtTrashLocations);

            /* Next up we do assumed filling of progression items cross-world */
            AssumedFill(ProgressionItems, new List<Item>(), Worlds);
            FastFill(NiceItems, Worlds);
            FastFill(JunkItems, Worlds);
        }

        void InitialFillInOwnWorld(List<Item> items, World world) {
            var swKey = items.Get(ItemType.KeySW);
            world.Locations.Get("Skull Woods - Pinball Room").Item = swKey;
            world.Items.Add(swKey);
            items.Remove(swKey);
        }

        void AssumedFill(List<Item> items, List<Item> baseItems, IEnumerable<World> worlds) {
            var assumedItems = new List<Item>(items);
            var locations = worlds.SelectMany(w => w.Locations).Empty().ToList();

            /* Place items until progression item pool is empty */
            while (assumedItems.Count > 0) {

                /* Get a candidate item from the pool */
                var itemToPlace = assumedItems.First();

                /* Remove it from the pool */
                assumedItems.Remove(itemToPlace);

                /* Get a location */
                var inventory = CollectItems(assumedItems.Concat(baseItems), worlds);
                var locationsToPlace = locations.CanFillWithinWorld(itemToPlace, inventory).ToList();

                if (locationsToPlace.Count == 0) {
                    assumedItems.Add(itemToPlace);
                    continue;
                }

                var locationToPlace = locationsToPlace.Shuffle(Rnd).First();

                /* Get the world from the location */
                var world = locationToPlace.Region.World;

                /* Place item at location */
                locationToPlace.Item = itemToPlace;
                world.Items.Add(itemToPlace);
                items.Remove(itemToPlace);
                locations.Remove(locationToPlace);
            }
        }

        IEnumerable<Item> CollectItems(IEnumerable<Item> items, IEnumerable<World> worlds) {
            var assumedItems = new List<Item>(items);
            var remainingLocations = worlds.SelectMany(l => l.Locations).Filled().ToList();
            while(true) {
                var availableLocations = remainingLocations.AvailableWithinWorld(assumedItems);
                remainingLocations = remainingLocations.Except(availableLocations).ToList();
                var foundItems = availableLocations.Select(x => x.Item).ToList();
                if (foundItems.Count == 0)
                    break;

                assumedItems.AddRange(foundItems);
            }

            return assumedItems;
        }

        void FrontFillItemInOwnWorld(List<Item> itemPool, ItemType itemType, World world) {
            var item = itemPool.Get(itemType);
            var location = world.Locations.Empty().Available(world.Items).Random(Rnd);
            if (location == null)
                throw new InvalidOperationException($"Tried to front fill {item.Name} in, but no location was available");
            location.Item = item;
            world.Items.Add(item);
            itemPool.Remove(item);
        }

        void ReorderItems(List<Item> itemPool, ItemType itemType, Func<int, int> index) {
            var items = itemPool.Where(x => x.Type == itemType).ToList();
            itemPool.RemoveAll(x => x.Type == itemType);
            foreach (var item in items) {
                itemPool.Insert(index(itemPool.Count), item);
            }
        }

        void FastFillLocations(List<Item> items, List<Location> locations) {
            while (locations.Empty().Count() > 0) {
                var item = items.Shuffle(Rnd).First();
                var location = locations.Empty().Shuffle(Rnd).First();
                if (location != null) {
                    location.Item = item;
                    location.Region.World.Items.Add(item);
                    items.Remove(item);
                }
                else {
                    throw new Exception($"Tried to fill item: {item.Name}, but no locations was available");
                }
            }
        }

        void FastFill(List<Item> items, List<World> worlds) {
            while (items.Count > 0) {
                var item = items.Shuffle(Rnd).First();
                var location = worlds.SelectMany(x => x.Locations.Empty()).Shuffle(Rnd).First();
                if (location != null) {
                    location.Item = item;
                    location.Region.World.Items.Add(item);
                    items.Remove(item);
                }
                else {
                    throw new Exception($"Tried to fill item: {item.Name}, but no locations was available");
                }
            }
        }

    }

}
