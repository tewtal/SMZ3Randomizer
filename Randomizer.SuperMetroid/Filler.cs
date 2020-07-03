using System;
using System.Collections.Generic;
using System.Linq;
using static Randomizer.SuperMetroid.ItemType;
using System.Threading;

namespace Randomizer.SuperMetroid {

    class Filler {

        List<World> Worlds { get; set; }
        List<Item> ProgressionItems { get; set; } = new List<Item>();
        List<Item> NiceItems { get; set; } = new List<Item>();
        List<Item> JunkItems { get; set; } = new List<Item>();
        Random Rnd { get; set; }
        Config Config { get; set; }

        private CancellationToken CancellationToken { get; set; }

        public Filler(List<World> worlds, Config config, Random rnd, CancellationToken cancellationToken) {
            Worlds = worlds;
            Rnd = rnd;
            Config = config;
            CancellationToken = cancellationToken;

            /* Populate filler item pool with items for each world */
            foreach (var world in worlds) {
                ProgressionItems.AddRange(Item.CreateProgressionPool(world, Rnd));
                NiceItems.AddRange(Item.CreateNicePool(world, Rnd));
                JunkItems.AddRange(Item.CreateJunkPool(world, Rnd));
            }
        }

        public void Fill() {
            /* First fill the worlds with the very basic tools for progression from their own respective worlds */
            InitialFill(ProgressionItems, Worlds);

            /* Priority fill items that needs to be placed first (will be placed in random order out of all items matching the types) */
            PriorityFill(new[] { Varia, Gravity }, ProgressionItems, new List<Item>(), Worlds);

            /* Next up we do assumed filling of progression items cross-world */
            AssumedFill(ProgressionItems, new List<Item>(), Worlds);

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
                var inventory = CollectItems(assumedItems.Concat(initialItems).ToList(), worlds).ToList();
                var locationsToPlace = Config.Placement switch
                {
                    Placement.Split => locations.Where(x => x.Class == itemToPlace.Class).ToList().CanFillWithinWorld(itemToPlace, inventory),
                    _ => locations.CanFillWithinWorld(itemToPlace, inventory)
                };

                if (locationsToPlace.Count == 0) {
                    priorityItems.Add(itemToPlace);
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
                var inventory = CollectItems(assumedItems.Concat(initialItems).ToList(), worlds).ToList();
                var locationsToPlace = Config.Placement switch
                {
                    Placement.Split => locations.Where(x => x.Class == itemToPlace.Class && CanPlaceAtLocation(x, itemToPlace.Type)).ToList().CanFillWithinWorld(itemToPlace, inventory),
                    _ => locations.Where(x => CanPlaceAtLocation(x, itemToPlace.Type)).ToList().CanFillWithinWorld(itemToPlace, inventory)
                };

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

                if (CancellationToken.IsCancellationRequested) {
                    throw new OperationCanceledException("The operation has been cancelled.");
                }
            }
        }

        public List<Item> CollectItems(List<Item> items, IEnumerable<World> worlds) {
            var myItems = new List<Item>(items);
            var availableLocations = worlds.SelectMany(l => l.Locations).Where(x => x.Item != null).ToList();
            while (true) {
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
                var location = Config.Placement switch
                {
                    Placement.Split => worlds.SelectMany(x => x.Locations.Where(x => x.Class == item.Class).ToList().Empty()).ToList().Shuffle(Rnd).FirstOrDefault(),
                    _ => worlds.SelectMany(x => x.Locations.Empty()).ToList().Shuffle(Rnd).FirstOrDefault()
                };

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
            foreach (var world in worlds) {
                /* Place Morph */
                FrontFillItemInWorld(world, items, Morph, true);

                /* Place missile or super */
                FrontFillItemInWorld(world, items, Rnd.Next(2) == 0 ? Missile : Super, true);

                /* Place a way to break bomb blocks.
                 * If split placement is used with casual logic we must place a powerbomb since there are no more major item locations available */
                if (Config.Placement == Placement.Split && Config.Logic == Logic.Casual) {
                    FrontFillItemInWorld(world, items, PowerBomb);
                } else {
                    FrontFillItemInWorld(world, items, Rnd.Next(8) switch
                    {
                        0 => ScrewAttack,
                        1 => SpeedBooster,
                        2 => Bombs,
                        _ => PowerBomb
                    }, true);
                }

                /* With split placement, we'll run into problem with placement if progression minors aren't available from the start */
                if (Config.Placement == Placement.Split) {
                    /* If missile was placed, also place a super */
                    if (!world.Items.Exists(x => x.Type == Super)) {
                        FrontFillItemInWorld(world, items, Super, true);
                    }

                    /* If no power bomb was placed, place one */
                    if (!world.Items.Exists(x => x.Type == PowerBomb)) {
                        FrontFillItemInWorld(world, items, PowerBomb, true);
                    }
                }

            }

            return worlds.SelectMany(w => w.Items).ToList();
        }

        private void FrontFillItemInWorld(World world, List<Item> itemPool, ItemType itemType, bool restrictWorld = false) {
            /* Get a shuffled list of available locations to place this item in */
            Item item = restrictWorld ? itemPool.Get(itemType, world) : itemPool.Get(itemType);
            var availableLocations = Config.Placement switch
            {
                Placement.Split => world.Locations.Empty().Where(x => x.Class == item.Class && CanPlaceAtLocation(x, itemType)).ToList().Available(world.Items).Shuffle(Rnd),
                _ => world.Locations.Empty().Where(x => CanPlaceAtLocation(x, itemType)).ToList().Available(world.Items).Shuffle(Rnd)
            };

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
            if (Config.SingleWorld) {
                return itemType switch
                {
                    Gravity => (!(location.Region.Area == "Crateria" || location.Region.Area == "Brinstar")) || location.Name == "X-Ray Scope" || location.Name == "Energy Tank, Waterway",
                    Varia => !(location.Region.Area == "LowerNorfair" || location.Region.Area == "Crateria" || location.Name == "Morphing Ball" || location.Name == "Missile (blue Brinstar middle)" || location.Name == "Energy Tank, Brinstar Ceiling"),
                    SpeedBooster => !(location.Name == "Morphing Ball" || location.Name == "Missile (blue Brinstar middle)" || location.Name == "Energy Tank, Brinstar Ceiling"),
                    ScrewAttack => !(location.Name == "Morphing Ball" || location.Name == "Missile (blue Brinstar middle)" || location.Name == "Energy Tank, Brinstar Ceiling"),
                    _ => true
                };
            } else {
                return true;
            }
        }

    }

}
