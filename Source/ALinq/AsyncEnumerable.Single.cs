using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<T> Single<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return await Single(enumerable, async item => true).ConfigureAwait(false);
#pragma warning restore 1998
        }

        public static async Task<T> Single<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var result = default(T);
            var counter = 0;

            await enumerable.ForEach(async state =>
            {
                if (await predicate(state.Item).ConfigureAwait(false))
                {
                    counter++;
                    result = state.Item;

                    if (counter > 1)
                    {
                        state.Break();
                    }
                }
            }).ConfigureAwait(false);

            if (counter == 0)
            {
                throw new InvalidOperationException("Sequence contains no matching element");
            }

            if (counter == 1)
            {
                return result;
            }

            throw new InvalidOperationException("Sequence contains more than one element");
        }

        public static async Task<T> SingleOrDefault<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return await SingleOrDefault(enumerable, async item => true).ConfigureAwait(false);
#pragma warning restore 1998
        }

        public static async Task<T> SingleOrDefault<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var result = default(T);
            var counter = 0;

            await enumerable.ForEach(async state =>
            {
                if (await predicate(state.Item).ConfigureAwait(false))
                {
                    counter++;
                    result = state.Item;

                    if (counter > 1)
                    {
                        state.Break();
                    }
                }
            }).ConfigureAwait(false);


            if (counter == 1)
            {
                return result;
            }

            return default(T);
        }
    }
}