using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static Task<TSource> Aggregate<TSource>(this IAsyncEnumerable<TSource> enumerable,Func<TSource, TSource, Task<TSource>> aggregationFunc)
        {
#pragma warning disable 1998
            return Aggregate(enumerable, default(TSource), aggregationFunc, async result => result);
#pragma warning restore 1998
        }

        public static Task<TAccumulate> Aggregate<TSource, TAccumulate>(this IAsyncEnumerable<TSource> enumerable,TAccumulate seed,Func<TAccumulate, TSource, Task<TAccumulate>> aggregationFunc)
        {
#pragma warning disable 1998
            return Aggregate(enumerable, seed, aggregationFunc, async result => result);
#pragma warning restore 1998
        }

        public static async Task<TResult> Aggregate<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> enumerable,TAccumulate seed,Func<TAccumulate, TSource, Task<TAccumulate>> aggregationFunc,Func<TAccumulate, Task<TResult>> resultSelector)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (aggregationFunc == null) throw new ArgumentNullException("aggregationFunc");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            var accumulator = seed;

            await enumerable.ForEach(async item =>
            {
                accumulator = await aggregationFunc(accumulator, item).ConfigureAwait(false);
            }).ConfigureAwait(false);

            return await resultSelector(accumulator).ConfigureAwait(false);
        }
    }
}