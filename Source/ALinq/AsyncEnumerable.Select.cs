using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<TOut> Select<TIn, TOut>(this IAsyncEnumerable<TIn> enumerable, Func<TIn,Task<TOut>> selector)
        {
            return Select(enumerable, (item, index) => selector(item));
        }

        public static IAsyncEnumerable<TOut> Select<TIn, TOut>(this IAsyncEnumerable<TIn> enumerable, Func<TIn,long,Task<TOut>> selector)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (selector == null) throw new ArgumentNullException("selector");

            return Create<TOut>(async producer =>
            {
                await enumerable.ForEach(async (item,index) =>
                {
                    await producer.Yield(await selector(item,index));
                });
            });
        }

        public static IAsyncEnumerable<TOut> SelectMany<TIn,TOut>(this IAsyncEnumerable<TIn> enumerable,Func<TIn,Task<IAsyncEnumerable<TOut>>> selector)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (selector == null) throw new ArgumentNullException("selector");

            return Create<TOut>(async producer =>
            {
                await enumerable.ForEach(async (TIn item, long index) =>
                {
                    var innerEnumerable = await selector(item);
                    if ( innerEnumerable != null )
                    {
                        await innerEnumerable.ForEach(async (TOut innerItem, long innerIndex) =>
                        {
                            await producer.Yield(innerItem);
                        });
                    }
                });
            });
        }
    }
}