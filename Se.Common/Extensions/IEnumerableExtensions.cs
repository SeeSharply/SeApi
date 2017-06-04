using System;
using System.Collections.Generic;
using System.Linq;

namespace SeApi.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T elem in enumerable)
            {
                action(elem);
            }
        }

        public static string Join<T>(this IEnumerable<T> target, string separator)
        {
            return target.IsNull()
                ? string.Empty
                : string.Join(separator, target.Select(i => i.ToString()).ToArray());
        }
    }
}
