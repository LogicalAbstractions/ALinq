using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<bool> Any<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return await Any(enumerable, async item => true).ConfigureAwait(false);
#pragma warning restore 1998
        }

        public static async Task<bool> Any<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var result = false;

            await enumerable.ForEach(async state =>
            {
                if (await predicate(state.Item).ConfigureAwait(false))
                {
                    result = true;
                    state.Break();
                }
            }).ConfigureAwait(false);

            return result;
        }
    }
}