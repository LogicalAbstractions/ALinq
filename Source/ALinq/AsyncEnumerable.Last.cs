using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<T> Last<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return await First(enumerable.Reverse(), async item => true).ConfigureAwait(false);
#pragma warning restore 1998
        }

        public static async Task<T> Last<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            return await First(enumerable.Reverse(), predicate).ConfigureAwait(false);
        }

        public static Task<T> LastOrDefault<T>(this IAsyncEnumerable<T> enumerable)
        {
            return FirstOrDefault(enumerable.Reverse());
        }

        public static Task<T> LastOrDefault<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            return FirstOrDefault(enumerable.Reverse(), predicate);
        }
    }
}