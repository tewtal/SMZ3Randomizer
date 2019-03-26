using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Randomizer.SuperMetroid {
    class Filler {
        List<World> Worlds { get; set; }
        List<Item> ProgressionItems { get; set; } = new List<Item>();
        List<Item> NiceItems { get; set; } = new List<Item>();
        List<Item> JunkItems { get; set; } = new List<Item>();
        Random Rnd { get; set; }

        public Filler(List<World> worlds, Random rnd) {
            Worlds = worlds;
            Rnd = rnd;

            /* Populate filler item pool with items for each world */
            foreach (var world in worlds) {
                ProgressionItems.AddRange(Item.CreateProgressionPool(world, Rnd));
                NiceItems.AddRange(Item.CreateNicePool(world, Rnd));
                JunkItems.AddRange(Item.CreateJunkPool(world, Rnd));
            }
        }

        public void Fill() {
            /* First fill the worlds with the very basic tools for progression from their own respective worlds */
            foreach (var world in Worlds) {
                InitialFill(world);
            }

            /* Next up we do assumed filling of progression items cross-world */
            AssumedFill(ProgressionItems, Worlds);
            FastFill(NiceItems, Worlds);
            FastFill(JunkItems, Worlds);

        }

        public void AssumedFill(List<Item> items, List<World> worlds) {
            var initialItems = worlds.SelectMany(w => w.Items).ToList();
            var assumedItems = new List<Item>(items);

            var locations = worlds.SelectMany(w => w.Locations).ToList().Empty();

            /* Place items until progression item pool is empty */
            while (assumedItems.Count > 0) {

                /* Get a candidate item from the pool */
                var itemToPlace = assumedItems.Shuffle(Rnd).First();

                /* Remove it from the pool */
                assumedItems.Remove(itemToPlace);

                /* Get a location */
                var locationToPlace = locations.Available(assumedItems.Concat(initialItems).ToList(), true).Shuffle(Rnd).First();

                /* Get the world from the location */
                var world = locationToPlace.Region.World;

                /* Place item at location */
                locationToPlace.Item = itemToPlace;
                world.Items.Add(itemToPlace);
                items.Remove(itemToPlace);
                locations.Remove(locationToPlace);
            }
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

        private void InitialFill(World world) {
            /* This fills the world with the initial items from its own world to make meaningful progress */

            /* Place Morph */
            FrontFillItemInWorld(world, ProgressionItems, ItemType.Morph, true);

            /* Place missile or super */
            FrontFillItemInWorld(world, ProgressionItems, Rnd.Next(2) == 0 ? ItemType.Missile : ItemType.Super, true);

            /* Place a way to break bomb blocks */
            FrontFillItemInWorld(world, ProgressionItems, Rnd.Next(8) switch
            {
                0 => ItemType.ScrewAttack,
                1 => ItemType.SpeedBooster,
                2 => ItemType.Bombs,
                _ => ItemType.PowerBomb
            }, true);

            /* Place a super if it's not already placed */
            if (!world.Items.Has(ItemType.Super))
                FrontFillItemInWorld(world, ProgressionItems, ItemType.Super, true);
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
