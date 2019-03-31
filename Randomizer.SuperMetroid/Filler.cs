using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static Randomizer.SuperMetroid.ItemType;

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
            var initialItems = InitialFill(ProgressionItems, Worlds);

            /* Priority fill items that needs to be placed first (will be placed in random order out of all items matching the types) */
            PriorityFill(new[] { Varia, Gravity }, ProgressionItems, initialItems, Worlds);

            /* Next up we do assumed filling of progression items cross-world */
            AssumedFill(ProgressionItems, initialItems, Worlds);

            /* Fast fill (no logic) the rest of the world */
            FastFill(NiceItems, Worlds);
            FastFill(JunkItems, Worlds);
        }

        public void PriorityFill(IList<ItemType> itemTypes, List<Item> items, List<Item> initialItems, List<World> worlds) {
            var assumedItems = new List<Item>(items.Where(x => !itemTypes.Contains(x.Type)));
            var priorityItems = new List<Item>(items.Where(x => itemTypes.Contains(x.Type)));
            var locations = worlds.SelectMany(w => w.Locations).ToList().Empty();
            
            /* Place items until priority item pool is empty */
            while (priorityItems.Count > 0) {

                /* Get a candidate item from the pool */
                var itemToPlace = priorityItems.Shuffle(Rnd).First();

                /* Remove it from the pool */
                priorityItems.Remove(itemToPlace);

                /* Get a location */
                var locationToPlace = locations.Available(priorityItems.Concat(assumedItems.Concat(initialItems)).ToList(), true).Shuffle(Rnd).First();

                /* If we can't place this item, put the item back and try again */
                if (!CanPlaceAtLocation(locationToPlace, itemToPlace.Type)) {
                    priorityItems.Add(itemToPlace);
                    continue;
                }

                /* Get the world from the location */
                var world = locationToPlace.Region.World;

                /* Place item at location */
                locationToPlace.Item = itemToPlace;
                world.Items.Add(itemToPlace);

                /* Remove item and location from their respective pools */
                items.Remove(itemToPlace);
                locations.Remove(locationToPlace);
            }
        }

        public void AssumedFill(List<Item> items, List<Item> initialItems, List<World> worlds) {
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

                /* If we can't place this item, put the item back and try again */
                if (!CanPlaceAtLocation(locationToPlace, itemToPlace.Type)) {
                    assumedItems.Add(itemToPlace);
                    continue;
                }

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

        private List<Item> InitialFill(List<Item> items, List<World> worlds) {
            foreach(var world in worlds) {
                /* Place Morph */
                FrontFillItemInWorld(world, items, Morph, true);

                /* Place missile or super */
                FrontFillItemInWorld(world, items, Rnd.Next(2) == 0 ? Missile : Super, true);

                /* Place a way to break bomb blocks */
                FrontFillItemInWorld(world, items, Rnd.Next(8) switch {
                    0 => ScrewAttack,
                    1 => SpeedBooster,
                    2 => Bombs,
                    _ => PowerBomb
                }, true);
            }

            return worlds.SelectMany(w => w.Items).ToList();
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

        private bool CanPlaceAtLocation(Location location, ItemType itemType) {
            return itemType switch
            {
                Gravity => (!(location.Region.Area == "Crateria" || location.Region.Area == "Brinstar")) || location.Name == "X-Ray Scope" || location.Name == "Energy Tank, Waterway",
                Varia => (!(location.Region.Area == "LowerNorfair" || location.Region.Area == "Crateria" || location.Name == "Morphing Ball" || location.Name == "Missile (blue Brinstar middle)" || location.Name == "Energy Tank, Brinstar Ceiling")),
                SpeedBooster => !(location.Name == "Morphing Ball" || location.Name == "Missile (blue Brinstar middle)" || location.Name == "Energy Tank, Brinstar Ceiling"),
                ScrewAttack => !(location.Name == "Morphing Ball" || location.Name == "Missile (blue Brinstar middle)" || location.Name == "Energy Tank, Brinstar Ceiling"),
                _ => true
            };
        }

    }

}
