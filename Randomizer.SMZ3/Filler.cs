using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Randomizer.SMZ3 {

    class Filler {

        List<World> Worlds { get; set; }
        Config Config { get; set; }
        Random Rnd { get; set; }

        private CancellationToken CancellationToken { get; set; }

        public Filler(List<World> worlds, Config config, Random rnd, CancellationToken cancellationToken) {
            Worlds = worlds;
            Config = config;
            Rnd = rnd;
            CancellationToken = cancellationToken;

            foreach (var world in worlds) {
                world.Setup(Rnd);
            }
        }

        public void Fill() {
            var progressionItems = new List<Item>();
            var baseItems = new List<Item>();

            foreach (var world in Worlds) {
                /* The dungeon pool order is significant, don't shuffle */
                var dungeon = Item.CreateDungeonPool(world);
                var progression = Item.CreateProgressionPool(world);

                InitialFillInOwnWorld(dungeon, progression, world);

                if (Config.Keysanity == false) {
                    var worldLocations = world.Locations.Empty().Shuffle(Rnd);
                    var keyCards = Item.CreateKeycards(world);
                    AssumedFill(dungeon, progression.Concat(keyCards).ToList(), worldLocations, new[] { world });
                    baseItems = baseItems.Concat(keyCards).ToList();
                } else {
                    progressionItems.AddRange(Item.CreateKeycards(world));
                }

                progressionItems.AddRange(dungeon);
                progressionItems.AddRange(progression);
            }

            progressionItems = progressionItems.Shuffle(Rnd);
            var niceItems = Worlds.SelectMany(world => Item.CreateNicePool(world)).Shuffle(Rnd);
            var junkItems = Worlds.SelectMany(world => Item.CreateJunkPool(world)).Shuffle(Rnd);

            var locations = Worlds.SelectMany(x => x.Locations).Empty().Shuffle(Rnd);
            if (Config.SingleWorld)
                locations = ApplyLocationWeighting(locations).ToList();

            if (Config.MultiWorld) {
                /* Place moonpearls and morphs in last 40%/20% of the pool so that
                 * they will tend to place in earlier locations.
                 */
                ApplyItemBias(progressionItems, new[] {
                    (ItemType.MoonPearl, .40),
                    (ItemType.Morph, .20),
                });
            }

            GanonTowerFill(junkItems, 2);
            AssumedFill(progressionItems, baseItems, locations, Worlds);
            FastFill(niceItems, locations);
            FastFill(junkItems, locations);
        }

        void ApplyItemBias(List<Item> itemPool, IEnumerable<(ItemType type, double weight)> reorder) {
            var n = itemPool.Count;

            /* Gather all items that are being biased */
            var items = reorder.ToDictionary(x => x.type, x => itemPool.FindAll(item => item.Type == x.type));
            itemPool.RemoveAll(item => reorder.Any(x => x.type == item.Type));

            /* Insert items from each biased type such that their lowest index 
             * is based on their weight on the original pool size
             */
            foreach (var (type, weight) in reorder.OrderByDescending(x => x.weight)) {
                var i = (int)(n * (1 - weight));
                if (i >= itemPool.Count)
                    throw new InvalidOperationException($"Too many items are being biased which makes the tail portion for {type} too big");
                foreach (var item in items[type]) {
                    var k = Rnd.Next(i, itemPool.Count);
                    itemPool.Insert(k, item);
                }
            }
        }

        IEnumerable<Location> ApplyLocationWeighting(IEnumerable<Location> locations) {
            return from location in locations.Select((x, i) => (x, i: i - x.Weight))
                   orderby location.i select location.x;
        }

        void InitialFillInOwnWorld(List<Item> dungeonItems, List<Item> progressionItems, World world) {
            FillItemAtLocation(dungeonItems, ItemType.KeySW, world.GetLocation("Skull Woods - Pinball Room"));

            /* Check Swords option and place as needed */
            switch (Config.SwordLocation) {
                case SwordLocation.Uncle: FillItemAtLocation(progressionItems, ItemType.ProgressiveSword, world.GetLocation("Link's Uncle")); break;
                case SwordLocation.Early: FrontFillItemInOwnWorld(progressionItems, ItemType.ProgressiveSword, world); break;
            }

            /* Check Morph option and place as needed */
            switch (Config.MorphLocation) {
                case MorphLocation.Original: FillItemAtLocation(progressionItems, ItemType.Morph, world.GetLocation("Morphing Ball")); break;
                case MorphLocation.Early: FrontFillItemInOwnWorld(progressionItems, ItemType.Morph, world); break;
            }

            /* We place a PB and Super in Sphere 1 to make sure the filler
             * doesn't start locking items behind this when there are a
             * high chance of the trash fill actually making them available */
            FrontFillItemInOwnWorld(progressionItems, ItemType.Super, world);
            FrontFillItemInOwnWorld(progressionItems, ItemType.PowerBomb, world);
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
                itemPool.Remove(item);

                if(CancellationToken.IsCancellationRequested) {
                    throw new OperationCanceledException("The operation has been cancelled.");
                }
            }
        }

        IEnumerable<Item> CollectItems(IEnumerable<Item> items, IEnumerable<World> worlds) {
            var assumedItems = new List<Item>(items);
            var remainingLocations = worlds.SelectMany(l => l.Locations).Filled().ToList();

            while (true) {
                var availableLocations = remainingLocations.AvailableWithinWorld(assumedItems).ToList();
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
            if (location == null) {
                throw new InvalidOperationException($"Tried to front fill {item.Name} in, but no location was available");
            }
            
            location.Item = item;
            itemPool.Remove(item);
        }

        void GanonTowerFill(List<Item> itemPool, double factor) {
            var locations = Worlds
                .SelectMany(x => x.Locations)
                .Where(x => x.Region is Regions.Zelda.GanonsTower)
                .Empty().Shuffle(Rnd);
            FastFill(itemPool, locations.Take((int)(locations.Count / factor)));
        }

        void FastFill(List<Item> itemPool, IEnumerable<Location> locations) {
            foreach (var (location, item) in locations.Empty().Zip(itemPool, (l, i) => (l, i)).ToList()) {
                location.Item = item;
                itemPool.Remove(item);
            }
        }

        void FillItemAtLocation(List<Item> itemPool, ItemType itemType, Location location) {
            var itemToPlace = itemPool.Get(itemType);
            location.Item = itemToPlace ?? throw new InvalidOperationException($"Tried to place item {itemType} at {location.Name}, but there is no such item in the item pool");
            itemPool.Remove(itemToPlace);
        }

    }

}
