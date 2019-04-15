using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SMZ3 {

    enum LocationType {
        Regular,
        HeraStandingKey,
        Pedestal,
        Ether,
        Bombos,

        Visible,
        Chozo,
        Hidden
    }

    delegate bool Requirement(Progression items);
    delegate bool Verification(Item item, Progression items);

    class Location {

        public int Id { get; set; }
        public string Name { get; set; }
        public LocationType Type { get; set; }
        public int Address { get; set; }
        public Region Region { get; set; }
        public Item Item { get; set; }

        readonly Requirement canAccess;
        Verification alwaysAllow;
        Verification allow;

        public ItemType ItemType {
            get { return Item?.Type ?? ItemType.Nothing; }
        }

        public Location(Region region, int id, int address, LocationType type, string name)
            : this(region, id, address, type, name, items => true) {
        }

        public Location(Region region, int id, int address, LocationType type, string name, Requirement access) {
            Region = region;
            Id = id;
            Name = name;
            Type = type;
            Address = address;
            canAccess = access;
            alwaysAllow = (item, items) => false;
            allow = (item, items) => true;
        }

        public Location AlwaysAllow(Verification allow) {
            alwaysAllow = allow;
            return this;
        }

        public Location Allow(Verification allow) {
            this.allow = allow;
            return this;
        }

        public bool Available(Progression items) {
            return Region.CanEnter(items) && canAccess(items);
        }

        public bool CanFill(Item item, Progression items) {
            var oldItem = Item;
            Item = item;
            bool fillable = alwaysAllow(item, items) || (Region.CanFill(item) && allow(item, items) && Available(items));
            Item = oldItem;
            return fillable;
        }
    }

    public static class LocationListExtensions {

        internal static Location Get(this List<Location> locations, string name) {
            var location = locations.Find(l => l.Name == name);
            if (location == null)
                throw new ArgumentException($"Could not find location name {name}", nameof(name));
            return location;
        }

        internal static List<Location> Empty(this List<Location> locations) {
            return locations.Where(l => l.Item == null).ToList();
        }

        internal static List<Location> AvailableWithinWorld(this List<Location> locations, List<Item> items) {
            var availableLocations = new List<Location>();
            foreach (var world in locations.Select(x => x.Region.World).Distinct()) {
                var progression = new Progression(items.Where(i => i.World == world));
                availableLocations.AddRange(locations.Where(l => l.Region.World == world && l.Available(progression)).ToList());
            }
            return availableLocations;
        }

        internal static List<Location> Available(this List<Location> locations, List<Item> items) {
            var progression = new Progression(items);
            return locations.Where(l => l.Available(progression)).ToList();
        }

        internal static List<Location> CanFillWithinWorld(this List<Location> locations, Item item, List<Item> items) {
            var itemWorldProgression = new Progression(items.Where(i => i.World == item.World).Append(item));
            var availableLocations = new List<Location>();
            foreach (var world in locations.Select(x => x.Region.World).Distinct()) {
                var progression = new Progression(items.Where(i => i.World == world));
                availableLocations.AddRange(locations.Where(l => l.Region.World == world && l.CanFill(item, progression) && item.World.Locations.Find(ll => ll.Id == l.Id).Available(itemWorldProgression)).ToList());
            }
            return availableLocations;
        }

    }

}
