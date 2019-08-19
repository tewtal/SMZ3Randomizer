using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SMZ3 {

    class Filler {

        List<World> Worlds { get; set; }
        Config Config { get; set; }
        Random Rnd { get; set; }

        public Filler(List<World> worlds, Config config, Random rnd) {
            Worlds = worlds;
            Config = config;
            Rnd = rnd;

            foreach (var world in worlds) {
                world.Setup(Rnd);
            }
        }

        public void Fill() {
            var progressionItems = new List<Item>();

            foreach (var world in Worlds) {
                /* The dungeon pool order is significant, don't shuffle */
                var dungeon = Item.CreateDungeonPool(world);
                var progression = Item.CreateProgressionPool(world);

                InitialFillInOwnWorld(dungeon, world);

                if (Config.Keysanity == false) {
                    var worldLocations = world.Locations.Empty().Shuffle(Rnd);
                    AssumedFill(dungeon, progression, worldLocations, new[] { world });
                }

                /* We place a PB and Super in Sphere 1 to make sure the filler
                 * doesn't start locking items behind this when there are a
                 * high chance of the trash fill actually making them available */
                FrontFillItemInOwnWorld(progression, ItemType.Super, world);
                FrontFillItemInOwnWorld(progression, ItemType.PowerBomb, world);

                progressionItems.AddRange(dungeon);
                progressionItems.AddRange(progression);
            }

            progressionItems = progressionItems.Shuffle(Rnd);
            var niceItems = Worlds.SelectMany(world => Item.CreateNicePool(world)).Shuffle(Rnd);
            var junkItems = Worlds.SelectMany(world => Item.CreateJunkPool(world)).Shuffle(Rnd);

            /* Place moonpearls and morphs in last 25%/50% of the pool so that
             * they will tend to place in earlier locations.
             * Prefer morphs being pushed too far up the list than moonpearls,
             * so start with morph, followed by moonpearls */
            ReorderItems(progressionItems, ItemType.Morph, n => n - Rnd.Next(n / 4));
            ReorderItems(progressionItems, ItemType.MoonPearl, n => n - Rnd.Next(n / 2));

            GanonTowerFill(junkItems);

            var locations = Worlds.SelectMany(x => x.Locations).Empty().Shuffle(Rnd);
            if (Worlds.Count == 1)
                locations = ApplyWeighting(locations).ToList();

            AssumedFill(progressionItems, new List<Item>(), locations, Worlds);
            FastFill(niceItems, locations);
            FastFill(junkItems, locations);
        }

        IEnumerable<Location> ApplyWeighting(IEnumerable<Location> locations) {
            return from location in locations.Select((x, i) => (x, i: i - x.Weight))
                   orderby location.i select location.x;
        }

        void InitialFillInOwnWorld(List<Item> items, World world) {
            var swKey = items.Get(ItemType.KeySW);
            world.Locations.Get("Skull Woods - Pinball Room").Item = swKey;
            world.Items.Add(swKey);
            items.Remove(swKey);
        }

        void AssumedFill(List<Item> itemPool, List<Item> baseItems, IEnumerable<Location> locations, IEnumerable<World> worlds) {
            var assumedItems = new List<Item>(itemPool);
            while (assumedItems.Count > 0) {
                /* Try placing next item */
                var item = assumedItems.First();
                assumedItems.Remove(item);

                var inventory = CollectItems(assumedItems.Concat(baseItems), worlds);
                var location = locations.Empty().CanFillWithinWorld(item, inventory).FirstOrDefault();
                if (location == null) {
                    assumedItems.Add(item);
                    continue;
                }

                location.Item = item;
                location.Region.World.Items.Add(item);
                itemPool.Remove(item);
            }
        }

        IEnumerable<Item> CollectItems(IEnumerable<Item> items, IEnumerable<World> worlds) {
            var assumedItems = new List<Item>(items);
            var remainingLocations = worlds.SelectMany(l => l.Locations).Filled().ToList();

            while (true) {
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

        void GanonTowerFill(List<Item> itemPool) {
            var locations = Worlds
                .SelectMany(x => x.Locations)
                .Where(x => x.Region is Regions.Zelda.GanonTower)
                .Empty().Shuffle(Rnd);
            FastFill(itemPool, locations.Take(locations.Count / 2));
        }

        void FastFill(List<Item> itemPool, IEnumerable<Location> locations) {
            foreach (var (location, item) in locations.Empty().Zip(itemPool, (l, i) => (l, i)).ToList()) {
                location.Item = item;
                location.Region.World.Items.Add(item);
                itemPool.Remove(item);
            }
        }

    }

}
