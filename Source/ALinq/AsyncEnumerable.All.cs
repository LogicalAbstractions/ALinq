using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<bool> All<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var result = true;

            await enumerable.ForEach(async state =>
            {
                if (!await predicate(state.Item).ConfigureAwait(false))
                {
                    result = false;
                    state.Break();
                }
            }).ConfigureAwait(false);

            return result;
        }
    }
}