using System;
using System.Collections.Generic;

namespace Utilities.Extensions
{
    public static class ArrayExtensions
    {
        private static readonly Random rand = new Random();

        public static T RandomElement<T>(this T[] items)
        {
            return items[rand.Next(0, items.Length)];
        }

        public static T RandomElement<T>(this List<T> items)
        {
            return items[rand.Next(0, items.Count)];
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rand.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static TV GetValue<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV defaultValue = default(TV))
        {
            return dict.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }
}