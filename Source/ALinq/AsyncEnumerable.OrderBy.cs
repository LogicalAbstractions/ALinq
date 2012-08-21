using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<TValue> OrderByDescending<TKey,TValue>(this IAsyncEnumerable<TValue> enumerable,Func<TValue,Task<TKey>> keySelector)
        {
            return OrderByDescending(enumerable, keySelector, Comparer<TKey>.Default);
        }

        public static IAsyncEnumerable<TValue> OrderByDescending<TKey,TValue>(this IAsyncEnumerable<TValue> enumerable,Func<TValue,Task<TKey>> keySelector,IComparer<TKey> comparer)
        {
            return OrderBy(enumerable, keySelector, new ReverseComparer<TKey>(comparer));
        }

        public static IAsyncEnumerable<TValue> OrderBy<TKey,TValue>(this IAsyncEnumerable<TValue> enumerable,Func<TValue,Task<TKey>> keySelector)
        {
            return OrderBy(enumerable, keySelector, Comparer<TKey>.Default);
        }

        public static IAsyncEnumerable<TValue> OrderBy<TKey,TValue>(this IAsyncEnumerable<TValue> enumerable,Func<TValue,Task<TKey>> keySelector,IComparer<TKey> comparer)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (comparer == null) throw new ArgumentNullException("comparer");

            var result = new List<KeyValuePair<TKey,TValue>>();

            return Create<TValue>(async producer =>
            {
                await enumerable.ForEach(async item => result.Add(new KeyValuePair<TKey,TValue>(await keySelector(item),item)));

                foreach( var item in result.OrderBy(pair => pair.Key,comparer))
                {
                    await producer.Yield(item.Value);
                }
            });
        }
    }
}