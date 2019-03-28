using System;
using System.Collections.Generic;

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

}
