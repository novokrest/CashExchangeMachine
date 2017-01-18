using System;
using System.Collections.Generic;

namespace CashExchangeMachine.Core.Extensions
{
    public static class EnumerableExtension
    {
        public static ISet<T> ToSet<T>(this IEnumerable<T> enumerable)
        {
            var set = new HashSet<T>();
            enumerable.ForEach(e => set.Add(e));

            return set;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T element in enumerable)
            {
                action(element);
            }
        }

        public static IEnumerable<T> InDescreaseOrder<T>(this IEnumerable<T> enumerable)
        {
            return new SortedSet<T>(enumerable).Reverse();
        }
    }
}
