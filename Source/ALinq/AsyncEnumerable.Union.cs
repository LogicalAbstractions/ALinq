using System;
using System.Collections.Generic;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> Union<T>(this IAsyncEnumerable<T> first, IAsyncEnumerable<T> second)
        {
            return Union(first, second, EqualityComparer<T>.Default);
        }

        public static IAsyncEnumerable<T> Union<T>(this IAsyncEnumerable<T> first, IAsyncEnumerable<T> second, IEqualityComparer<T> comparer)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");
            if (comparer == null) throw new ArgumentNullException("comparer");

            return Create<T>(async producer =>
            {
                var set = new HashSet<T>(comparer);

                await first.ForEach(async item =>
                {
                    if (!set.Contains(item))
                    {
                        set.Add(item);
                        await producer.Yield(item).ConfigureAwait(false);
                    }
                }).ConfigureAwait(false);

                await second.ForEach(async item =>
                {
                    if (!set.Contains(item))
                    {
                        set.Add(item);
                        await producer.Yield(item).ConfigureAwait(false);
                    }
                }).ConfigureAwait(false);
            });
        }
    }
}