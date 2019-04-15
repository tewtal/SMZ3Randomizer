using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SMZ3 {

    class Filler {

        List<World> Worlds { get; set; }
        Config Config { get; set; }
        List<Item> DungeonItems { get; set; } = new List<Item>();
        List<Item> ProgressionItems { get; set; } = new List<Item>();
        List<Item> NiceItems { get; set; } = new List<Item>();
        List<Item> JunkItems { get; set; } = new List<Item>();
        Random Rnd { get; set; }

        public Filler(List<World> worlds, Config config, Random rnd) {
            Worlds = worlds;
            Config = config;
            Rnd = rnd;

            /* Populate item pools and setup each world */
            /* The order of the dungeon pool is significant */
            foreach (var world in worlds) {
                DungeonItems.AddRange(Item.CreateDungeonPool(world));
                ProgressionItems.AddRange(Item.CreateProgressionPool(world).Shuffle(Rnd));
                NiceItems.AddRange(Item.CreateNicePool(world).Shuffle(Rnd));
                JunkItems.AddRange(Item.CreateJunkPool(world).Shuffle(Rnd));
                world.Setup(Rnd);
            }
        }

        public void Fill() {

            var initialFillItems = InitialFill(DungeonItems, Worlds);

            foreach (var world in Worlds) {
                var dungeon = DungeonItems.Where(x => x.World == world).ToList();
                var progression = ProgressionItems.Where(x => x.World == world).ToList();
                AssumedFill(dungeon, progression, new[] { world });

                /* We place a PB and Super in Sphere 1 to make sure the filler doesn't start locking items behind this when there are a high chance of the trash fill actually making them available */
                FrontFillItemInWorld(world, ProgressionItems, ItemType.Super, true);
                FrontFillItemInWorld(world, ProgressionItems, ItemType.PowerBomb, true);
            }

            /* Place moon pearls randomly in the last 50% of items to be placed to move it to a early-mid game item*/
            /* Temporary test hack */
            var pearls = ProgressionItems.Where(x => x.Type == ItemType.MoonPearl).ToList();
            ProgressionItems.RemoveAll(x => x.Type == ItemType.MoonPearl);
            foreach (var pearl in pearls) {
                ProgressionItems.Insert(ProgressionItems.Count - Rnd.Next(ProgressionItems.Count >> 1), pearl);
            }

            /* Place morph balls randomly in the last 25% of items to be placed to move it to an early game item */
            /* Temporary test hack */
            var morphs = ProgressionItems.Where(x => x.Type == ItemType.Morph).ToList();
            ProgressionItems.RemoveAll(x => x.Type == ItemType.Morph);
            foreach(var morph in morphs) {
                ProgressionItems.Insert(ProgressionItems.Count - Rnd.Next(ProgressionItems.Count >> 2), morph);
            }

            /* GT Trash fill */
            var gtLocations = Worlds.SelectMany(x => x.Locations).Where(x => x.Region is Regions.Zelda.GanonTower).ToList().Empty().Shuffle(Rnd);
            var gtTrashLocations = gtLocations.Take((int)(gtLocations.Count() * 0.5)).ToList();
            FastFillLocations(JunkItems, gtTrashLocations);

            /* Next up we do assumed filling of progression items cross-world */
            AssumedFill(ProgressionItems, new List<Item>(), Worlds);
            FastFill(NiceItems, Worlds);
            FastFill(JunkItems, Worlds);

        }

        public void AssumedFill(List<Item> items, List<Item> baseItems, IEnumerable<World> worlds) {
            var assumedItems = new List<Item>(items);
            var locations = worlds.SelectMany(w => w.Locations).ToList().Empty();

            /* Place items until progression item pool is empty */
            while (assumedItems.Count > 0) {

                /* Get a candidate item from the pool */
                var itemToPlace = assumedItems.First();

                /* Remove it from the pool */
                assumedItems.Remove(itemToPlace);

                /* Get a location */
                var inventory = CollectItems(assumedItems.Concat(baseItems).ToList(), worlds).ToList();
                var locationsToPlace = locations.CanFillWithinWorld(itemToPlace, inventory);

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

        public List<Item> CollectItems(List<Item> items, IEnumerable<World> worlds) {
            var myItems = new List<Item>(items);
            var availableLocations = worlds.SelectMany(l => l.Locations).Where(x => x.Item != null).ToList();
            while(true) {
                var searchLocations = availableLocations.AvailableWithinWorld(myItems);
                availableLocations = availableLocations.Except(searchLocations).ToList();
                var foundItems = searchLocations.Select(x => x.Item).ToList();
                if (foundItems.Count == 0)
                    break;

                myItems = myItems.Concat(foundItems).ToList();
            }

            return myItems;
        }

        public void FastFill(List<Item> items, List<World> worlds) {
            while (items.Count > 0) {
                var item = items.Shuffle(Rnd).First();
                var location = worlds.SelectMany(x => x.Locations.Empty()).ToList().Shuffle(Rnd).First();
                if (location != null) {
                    location.Item = item;
                    location.Region.World.Items.Add(item);
                    items.Remove(item);
                }
                else {
                    throw new Exception("Tried to fill item: " + item.Name + ", but no locations was available");
                }
            }
        }

        public void FastFillLocations(List<Item> items, List<Location> locations) {
            while (locations.Empty().Count > 0) {
                var item = items.Shuffle(Rnd).First();
                var location = locations.Empty().ToList().Shuffle(Rnd).First();
                if (location != null) {
                    location.Item = item;
                    location.Region.World.Items.Add(item);
                    items.Remove(item);
                }
                else {
                    throw new Exception("Tried to fill item: " + item.Name + ", but no locations was available");
                }
            }
        }

        public List<Item> InitialFill(List<Item> itemPool, List<World> worlds) {
            foreach (var world in worlds) {

                /* Place Swamp Palace Key */
                if (!Config.Keysanity) {
                    var spKey = itemPool.Find(x => x.World == world && x.Type == ItemType.KeySP);
                    world.Locations.Get("Swamp Palace - Entrance").Item = spKey;
                    world.Items.Add(spKey);
                    itemPool.Remove(spKey);
                }

                /* Place Skull Woods Pinball Key */
                var swKey = itemPool.Find(x => x.World == world && x.Type == ItemType.KeySW);
                world.Locations.Get("Skull Woods - Pinball Room").Item = swKey;
                world.Items.Add(swKey);
                itemPool.Remove(swKey);
            }

            return worlds.SelectMany(x => x.Items).ToList();
        }

        private void FrontFillItemInWorld(World world, List<Item> itemPool, ItemType itemType, bool restrictWorld = false) {
            /* Get a shuffled list of available locations to place this item in */
            Item item = restrictWorld ? itemPool.Get(itemType, world) : itemPool.Get(itemType);
            var availableLocations = world.Locations.Empty().Available(world.Items).Shuffle(Rnd);
            if (availableLocations.Count > 0) {
                var locationToFill = availableLocations.First();
                locationToFill.Item = item;
                itemPool.Remove(item);
                world.Items.Add(item);
            }
            else {
                throw new Exception("No location to place item:" + item.Name);
            }
        }

    }

}
