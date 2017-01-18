using CashExchangeMachine.Core.Collections;
using System;
using System.Collections.Generic;

namespace CashExchangeMachine.Core.Extensions
{
    public static class CountableCollectionExtension
    {
        public static void Add<T>(this ICountableCollection<T> collection, ICountableCollection<T> added)
        {
            foreach (KeyValuePair<T, int> keyValuePair in added)
            {
                collection.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public static ICountableCollection<T> Select<T>(this ICountableCollection<T> collection, Func<T, T> transformer)
        {
            var result = new CountableCollection<T>();
            foreach (KeyValuePair<T, int> keyValuePair in collection)
            {
                result.Add(transformer(keyValuePair.Key), keyValuePair.Value);
            }
            return result;
        }
    }
}
