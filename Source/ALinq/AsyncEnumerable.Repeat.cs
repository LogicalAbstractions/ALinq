using System;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> Repeat<T>(T value, int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException("count", "count must be zero or greater");

            return Create<T>(async producer =>
            {
                for (var i = 0; i < count; ++i)
                {
                    await producer.Yield(value).ConfigureAwait(false);
                }
            });
        }
    }
}