using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> Skip<T>(this IAsyncEnumerable<T> enumerable, int count)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (count < 0) throw new ArgumentOutOfRangeException("count", "count must be zero or greater");

            return Create<T>(async producer =>
            {
                await enumerable.ForEach(async state =>
                {
                    if (state.Index >= count)
                    {
                        await producer.Yield(state.Item).ConfigureAwait(false);
                    }
                }).ConfigureAwait(false);
            });
        }

        public static IAsyncEnumerable<T> SkipWhile<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            return SkipWhile(enumerable, (item, index) => predicate(item));
        }

        public static IAsyncEnumerable<T> SkipWhile<T>(this IAsyncEnumerable<T> enumerable, Func<T, long, Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var doYield = false;

            return Create<T>(async producer =>
            {
                await enumerable.ForEach(async state =>
                {
                    if (!doYield && !await predicate(state.Item, state.Index).ConfigureAwait(false))
                    {
                        doYield = true;
                    }

                    if (doYield)
                    {
                        await producer.Yield(state.Item).ConfigureAwait(false);
                    }
                }).ConfigureAwait(false);
            });
        }
    }
}