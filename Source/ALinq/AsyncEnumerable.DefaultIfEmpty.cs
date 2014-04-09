using System;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> DefaultIfEmpty<T>(this IAsyncEnumerable<T> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            return DefaultIfEmpty<T>(enumerable, default(T));
        }

        public static IAsyncEnumerable<T> DefaultIfEmpty<T>(this IAsyncEnumerable<T> enumerable, T defaultValue)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            return Create<T>(async producer =>
            {
                var atLeastOne = false;

                await enumerable.ForEach(async item =>
                {
                    atLeastOne = true;
                    await producer.Yield(item).ConfigureAwait(false);
                }).ConfigureAwait(false);

                if (!atLeastOne)
                {
                    await producer.Yield(defaultValue).ConfigureAwait(false);
                }
            });
        }
    }
}