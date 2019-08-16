using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SMZ3 {

    static class EnumerableExtensions {

        public static List<T> Shuffle<T>(this IEnumerable<T> list, Random rnd) {
            var copy = new List<T>(list);
            var n = copy.Count;
            while ((n -= 1) > 0) {
                var k = rnd.Next(n + 1);
                (copy[n], copy[k]) = (copy[k], copy[n]);
            }
            return copy;
        }

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
