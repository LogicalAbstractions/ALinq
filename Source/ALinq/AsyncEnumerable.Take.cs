using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> Take<T>(this IAsyncEnumerable<T> enumerable, int count)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (count < 0) throw new ArgumentOutOfRangeException("count", "count must be zero or greater");

            var counter = 0;

            return Create<T>(async producer =>
            {
                await enumerable.ForEach(async state =>
                {
                    if (counter < count)
                    {
                        await producer.Yield(state.Item).ConfigureAwait(false);
                    }

                    counter++;
                }).ConfigureAwait(false);
            });
        }

        public static IAsyncEnumerable<T> TakeWhile<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            return TakeWhile(enumerable, (item, index) => predicate(item));
        }

        public static IAsyncEnumerable<T> TakeWhile<T>(this IAsyncEnumerable<T> enumerable, Func<T, long, Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var doYield = true;

            return Create<T>(async producer =>
            {
                await enumerable.ForEach(async state =>
                {
                    if (doYield && !await predicate(state.Item, state.Index).ConfigureAwait(false))
                    {
                        doYield = false;
                        state.Break();
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