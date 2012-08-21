using System;
using System.Collections.Generic;
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

        public static IAsyncEnumerable<TCollection> SelectMany<TSource, TCollection>(this IAsyncEnumerable<TSource> enumerable, Func<TSource,Task<IAsyncEnumerable<TCollection>>> collectionSelector)
        {
#pragma warning disable 1998
            return SelectMany(enumerable, collectionSelector, async (source, item) => item);
#pragma warning restore 1998
        }

        public static IAsyncEnumerable<TCollection> SelectMany<TSource, TCollection>(this IAsyncEnumerable<TSource> enumerable, Func<TSource, int, Task<IAsyncEnumerable<TCollection>>> collectionSelector)
        {
#pragma warning disable 1998
            return SelectMany(enumerable, collectionSelector, async (source, item) => item);
#pragma warning restore 1998
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> enumerable, Func<TSource, Task<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, Task<TResult>> resultSelector)
        {
            return SelectMany(enumerable, (source, index) => collectionSelector(source), resultSelector);
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource,TCollection,TResult>( this IAsyncEnumerable<TSource> enumerable,Func<TSource,int,Task<IAsyncEnumerable<TCollection>>> collectionSelector,Func<TSource,TCollection,Task<TResult>> resultSelector)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (collectionSelector == null) throw new ArgumentNullException("collectionSelector");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            return Create<TResult>(async producer =>
            {
                await enumerable.ForEach(async state =>
                {
                    var collection = await collectionSelector(state.Item, (int)state.Index);
                    await collection.ForEach(async innerState =>
                    {
                        var result = await resultSelector(state.Item, innerState.Item);
                        await producer.Yield(result);
                    });
                });
            });
        }
    }
}