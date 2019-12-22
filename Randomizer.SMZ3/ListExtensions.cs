using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

    public static class EnumExtensions {
        public static string GetDescription(this Enum GenericEnum) {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0)) {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0)) {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }
    }
}
