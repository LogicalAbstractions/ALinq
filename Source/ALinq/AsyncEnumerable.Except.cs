using System;
using System.Collections.Generic;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> Except<T>(this IAsyncEnumerable<T> first, IAsyncEnumerable<T> second)
        {
            return Except(first, second, EqualityComparer<T>.Default);
        }

        public static IAsyncEnumerable<T> Except<T>(this IAsyncEnumerable<T> first,IAsyncEnumerable<T> second,IEqualityComparer<T> comparer)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");
            if (comparer == null) throw new ArgumentNullException("comparer");

            return Create<T>(async producer =>
            {
                var set = new HashSet<T>();
#pragma warning disable 1998
                await second.ForEach(async item => set.Add(item)).ConfigureAwait(false);
#pragma warning restore 1998

                await first.ForEach(async item =>
                {
                    if ( !set.Contains(item))
                    {
                        await producer.Yield(item).ConfigureAwait(false);
                    }
                }).ConfigureAwait(false);
            });
        }
    }
}