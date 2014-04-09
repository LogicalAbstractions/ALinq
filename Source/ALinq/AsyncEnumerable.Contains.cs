using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<bool> Contains<T>(this IAsyncEnumerable<T> enumerable, T item)
        {
            return await Contains(enumerable, item, EqualityComparer<T>.Default).ConfigureAwait(false);
        }

        public static async Task<bool> Contains<T>(this IAsyncEnumerable<T> enumerable, T item, IEqualityComparer<T> comparer)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (comparer == null) throw new ArgumentNullException("comparer");

            var found = false;

#pragma warning disable 1998
            await enumerable.ForEach(async state =>
#pragma warning restore 1998
            {
                if (comparer.Equals(state.Item, item))
                {
                    found = true;
                    state.Break();
                }
            }).ConfigureAwait(false);

            return found;
        }
    }
}