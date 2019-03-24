using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Randomizer.SuperMetroid {

    enum LocationType {
        Visible,
        Chozo,
        Hidden
    }

    delegate bool Requirement(List<Item> items);

    class Location {

        public string Name { get; set; }
        public LocationType Type { get; set; }
        public int Address { get; set; }
        public Region Region {get; set;}
        public Item Item { get; set; }

        private readonly Requirement canAccess;

        public Location(Region region, string name, LocationType type, int address) {
            Region = region;
            Name = name;
            Type = type;
            Address = address;
            canAccess = items => true;
        }

        public Location(Region region, string name, LocationType type, int address, Requirement access) {
            Region = region;
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

        public static List<T> Shuffle<T>(this IList<T> list) {
            if (list.Count > byte.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(list), $"List Count can not exceed #{byte.MaxValue}");

            var shuffledList = new List<T>(list);
            var provider = new RNGCryptoServiceProvider();
            int n = shuffledList.Count;
            while (n > 1) {
                int k = GetRandomByte(provider, n) % n;
                n -= 1;

                var value = shuffledList[k];
                shuffledList[k] = shuffledList[n];
                shuffledList[n] = value;
            }

            return shuffledList;
        }

        private static byte GetRandomByte(RandomNumberGenerator provider, int n) {
            var numbers = new byte[1];
            do provider.GetBytes(numbers);
            while (numbers[0] >= n * (byte.MaxValue / n));
            return numbers[0];
        }

    }

}
