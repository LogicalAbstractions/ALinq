using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<T> ElementAtOrDefault<T>(this IAsyncEnumerable<T> enumerable, int index)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (index < 0) throw new ArgumentOutOfRangeException("index", "index must be zero or greater");

            var result = default(T);
            var found = false;

#pragma warning disable 1998
            await enumerable.ForEach(async state =>
#pragma warning restore 1998
            {
                if (state.Index == index)
                {
                    result = state.Item;
                    found = true;
                }
            }).ConfigureAwait(false);

            if (found)
            {
                return result;
            }

            return default(T);
        }

        public static async Task<T> ElementAt<T>(this IAsyncEnumerable<T> enumerable, int index)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (index < 0) throw new ArgumentOutOfRangeException("index", "index must be zero or greater");

            var result = default(T);
            var found = false;

#pragma warning disable 1998
            await enumerable.ForEach(async state =>
#pragma warning restore 1998
            {
                if (state.Index == index)
                {
                    result = state.Item;
                    found = true;
                }
            }).ConfigureAwait(false);

            if (found)
            {
                return result;
            }

            throw new InvalidOperationException(string.Format("Sequence is smaller than the given index: {0}", index));
        }
    }
}