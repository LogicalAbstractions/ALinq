using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<IDictionary<TKey, TElement>> ToDictionary<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source,
                                                                                                    Func<TSource, Task<TKey>> keySelector,
                                                                                                    Func<TSource, Task<TElement>> elementSelector)
        {
            return await ToDictionary(source, keySelector, elementSelector, EqualityComparer<TKey>.Default).ConfigureAwait(false);
        }

        public static async Task<IDictionary<TKey, TSource>> ToDictionary<TSource, TKey>(this IAsyncEnumerable<TSource> source,
                                                                                         Func<TSource, Task<TKey>> keySelector)
        {
            return await ToDictionary(source, keySelector, EqualityComparer<TKey>.Default).ConfigureAwait(false);
        }

        public static async Task<IDictionary<TKey, TSource>> ToDictionary<TSource, TKey>(this IAsyncEnumerable<TSource> source,
                                                                                                    Func<TSource, Task<TKey>> keySelector,
                                                                                                    IEqualityComparer<TKey> comparer)
        {
#pragma warning disable 1998
            return await ToDictionary(source, keySelector, async e => e, comparer).ConfigureAwait(false);
#pragma warning restore 1998
        }

        public static async Task<IDictionary<TKey, TElement>> ToDictionary<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source,
                Func<TSource, Task<TKey>> keySelector,
	            Func<TSource, Task<TElement>> elementSelector,
	            IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (elementSelector == null) throw new ArgumentNullException("elementSelector");
            if (comparer == null) throw new ArgumentNullException("comparer");

            var result = new Dictionary<TKey, TElement>(comparer);

            await source.ForEach(async item =>
            {
                var key     = await keySelector(item).ConfigureAwait(false);
                var value   = await elementSelector(item).ConfigureAwait(false);

                result.Add(key,value);
            }).ConfigureAwait(false);

            return result;
        }
    }
}