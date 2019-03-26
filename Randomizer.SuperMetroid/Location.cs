using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;

namespace Randomizer.SuperMetroid {

    enum LocationType {
        Visible,
        Chozo,
        Hidden
    }

    delegate bool Requirement(List<Item> items);

    class Location {

        public int Id { get; set; }
        public string Name { get; set; }
        public LocationType Type { get; set; }
        public int Address { get; set; }
        public Region Region { get; set; }
        public Item Item { get; set; }

        private readonly Requirement canAccess;

        public Location(Region region, int id, string name, LocationType type, int address) {
            Region = region;
            Id = id;
            Name = name;
            Type = type;
            Address = address;
            canAccess = items => true;
        }

        public Location(Region region, int id, string name, LocationType type, int address, Requirement access) {
            Region = region;
            Id = id;
            Name = name;
            Type = type;
            Address = address;
            canAccess = access;
        }

        public bool Available(List<Item> items) {
            return Region.CanEnter(items) && canAccess(items);
        }

    }

    public static class LocationListExtensions {

        internal static List<Location> Available(this List<Location> locations, List<Item> items, bool restrictWorld = false) {
            if (restrictWorld) {
                return locations.Where(l => l.Available(items.Where(i => i.World == l.Region.World).ToList())).ToList();
            }
            else {
                return locations.Where(l => l.Available(items)).ToList();
            }
        }

        internal static List<Location> Empty(this List<Location> locations) {
            return locations.Where(l => l.Item == null).ToList();
        }

        public static List<T> Shuffle<T>(this IList<T> list, Random rnd) {
            var shuffledList = new List<T>(list);
            int n = shuffledList.Count;
            while (n > 1) {
                n--;
                int k = rnd.Next(n + 1);
                T value = shuffledList[k];
                shuffledList[k] = shuffledList[n];
                shuffledList[n] = value;
            }
            return shuffledList;
        }
    }
}
