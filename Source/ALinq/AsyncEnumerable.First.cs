using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<T> First<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return await First(enumerable, async item => true).ConfigureAwait(false);
#pragma warning restore 1998
        }

        public static async Task<T> First<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var result = default(T);
            var found = false;

            await enumerable.ForEach(async state =>
            {
                if (await predicate(state.Item).ConfigureAwait(false))
                {
                    found = true;
                    result = state.Item;
                    state.Break();
                }
            }).ConfigureAwait(false);

            if (found)
            {
                return result;
            }

            throw new InvalidOperationException("Sequence contains no matching element");
        }

        public static async Task<T> FirstOrDefault<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return await FirstOrDefault<T>(enumerable, async item => true).ConfigureAwait(false);
#pragma warning restore 1998
        }

        public static async Task<T> FirstOrDefault<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var result = default(T);
            var found = false;

            await enumerable.ForEach(async state =>
            {
                if (await predicate(state.Item).ConfigureAwait(false))
                {
                    found = true;
                    result = state.Item;
                    state.Break();
                }
            }).ConfigureAwait(false);

            return found ? result : default(T);
        }
    }
}