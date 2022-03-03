using System;
using System.Collections.Generic;

namespace Randomizer.SMZ3 {

    static class Extensions {

        // The opposite of Aggregate, instead of combining elements into a
        // value, `Generate` provides elements from a seed argument.
        public static IEnumerable<TResult> Generate<TSeed, TResult>(
            this TSeed seed, Func<TSeed, (TResult, TSeed)> func
        ) {
            TResult result;
            while (true) {
                (result, seed) = func(seed);
                yield return result;
            }
        }

    }

}
