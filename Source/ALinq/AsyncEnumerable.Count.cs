using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static Task<int> Count<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return Count<T>(enumerable, async item => true);
#pragma warning restore 1998
        }

        public static async Task<int> Count<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> selector)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (selector == null) throw new ArgumentNullException("selector");

            var counter = 0;
#pragma warning disable 1998
            await enumerable.Where(selector).ForEach(async (T item) =>
#pragma warning restore 1998
            {
                counter++;
            }).ConfigureAwait(false);

            return counter;
        }

        public static Task<long> LongCount<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return LongCount<T>(enumerable, async item => true);
#pragma warning restore 1998
        }

        public static async Task<long> LongCount<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> selector)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (selector == null) throw new ArgumentNullException("selector");

            var counter = 0L;
#pragma warning disable 1998
            await enumerable.Where(selector).ForEach(async (T item) =>
#pragma warning restore 1998
            {
                counter++;
            }).ConfigureAwait(false);

            return counter;
        }
    }
}