using System.Collections.Generic;
using System.Linq;

namespace Randomizer.CLI {

    static class ListExtensions {

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int size) {
            using var e = source.GetEnumerator();
            IEnumerable<T> chunk(int size) {
                do {
                    yield return e.Current;
                } while ((size -= 1) > 0 && e.MoveNext());
            }
            while (e.MoveNext()) {
                yield return chunk(size).ToList();
            }
        }

        public static IEnumerable<T> IntersectAll<T>(this IEnumerable<T> first, IEnumerable<T> second) {
            var lookup = second.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            foreach (var item in first) {
                if (lookup.TryGetValue(item, out var value) && value > 0){
                    yield return item;
                    lookup[item]--;
                }
            }
        }

    }

}
