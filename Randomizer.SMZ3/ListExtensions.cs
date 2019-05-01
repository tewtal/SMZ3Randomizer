using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SMZ3 {

    public static class ListExtensions {

        public static List<T> Shuffle<T>(this IList<T> list, Random random) {
            var shuffledList = new List<T>(list);
            var n = shuffledList.Count;
            while (n > 1) {
                n -= 1;
                var k = random.Next(n + 1);
                var value = shuffledList[k];
                shuffledList[k] = shuffledList[n];
                shuffledList[n] = value;
            }
            return shuffledList;
        }

    }

    public static class EnumerableExtensions {

        public static (IEnumerable<T>, IEnumerable<T>) SplitOff<T>(this IEnumerable<T> list, int count) {
            var head = list.Take(count);
            var tail = list.Skip(count);
            return (head, tail);
        }

        public static void Deconstruct<T>(this IEnumerable<T> source, out T first, out IEnumerable<T> rest) {
            first = source.FirstOrDefault();
            rest = source.Skip(1);
        }

        public static void Deconstruct<T>(this IEnumerable<T> source, out T first, out T second, out IEnumerable<T> rest)
            => (first, (second, rest)) = source;

    }

}
