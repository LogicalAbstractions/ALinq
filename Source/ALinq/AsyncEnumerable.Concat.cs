using System;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> Concat<T>(this IAsyncEnumerable<T> enumerable, IAsyncEnumerable<T> otherEnumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (otherEnumerable == null) throw new ArgumentNullException("otherEnumerable");

            return Create<T>(async producer =>
            {
                await enumerable.ForEach(async item => await producer.Yield(item)).ConfigureAwait(false);
                await otherEnumerable.ForEach(async item => await producer.Yield(item)).ConfigureAwait(false);
            });
        }
    }
}