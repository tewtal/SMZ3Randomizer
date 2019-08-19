using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SMZ3 {

    static class EnumerableExtensions {

        public static T Random<T>(this IEnumerable<T> source, Random rnd) {
            var list = source.ToList();
            return list.ElementAt(rnd.Next(list.Count));
        }

        public static List<T> Shuffle<T>(this IEnumerable<T> source, Random rnd) {
            var copy = new List<T>(source);
            var n = copy.Count;
            while ((n -= 1) > 0) {
                var k = rnd.Next(n + 1);
                (copy[n], copy[k]) = (copy[k], copy[n]);
            }
            return copy;
        }

        public static (IEnumerable<T>, IEnumerable<T>) SplitOff<T>(this IEnumerable<T> source, int count) {
            var head = source.Take(count);
            var tail = source.Skip(count);
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
