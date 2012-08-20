using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<bool> Any<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return await Any<T>(enumerable, async item => true);
#pragma warning restore 1998
        }

        public static async Task<bool> Any<T>(this IAsyncEnumerable<T> enumerable,Func<T,Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var result = false;

            await enumerable.ForEach(async state =>
            {
                if ( await predicate(state.Item))
                {
                    result = true;
                    state.Break();
                }
            });

            return result;
        }

        public static async Task<bool> All<T>(this IAsyncEnumerable<T> enumerable,Func<T,Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var result = true;

            await enumerable.ForEach(async state =>
            {
                if ( !await predicate(state.Item))
                {
                    result = false;
                    state.Break();
                }
            });

            return result;
        }

        public static async Task<bool> Contains<T>(this IAsyncEnumerable<T> enumerable,T item )
        {
            return await Contains<T>(enumerable, item, EqualityComparer<T>.Default);
        }

        public static async Task<bool> Contains<T>(this IAsyncEnumerable<T> enumerable,T item,IEqualityComparer<T> comparer )
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (comparer == null) throw new ArgumentNullException("comparer");

            var found = false;

#pragma warning disable 1998
            await enumerable.ForEach(async state =>
#pragma warning restore 1998
            {
                if ( comparer.Equals(state.Item,item))
                {
                    found = true;
                    state.Break();
                }
            });

            return found;
        }

        public static IAsyncEnumerable<T> Where<T>(this IAsyncEnumerable<T> enumerable,Func<T,Task<bool>> predicate)
        {
            return Where(enumerable, (item, index) => predicate(item));
        }

        public static IAsyncEnumerable<T> Where<T>(this IAsyncEnumerable<T> enumerable,Func<T,int,Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            return Create<T>(async producer =>
            {
                await enumerable.ForEach(async state =>
                {
                    if ( await predicate(state.Item,(int)state.Index))
                    {
                        await producer.Yield(state.Item);
                    }
                });
            });
        }
    }
}