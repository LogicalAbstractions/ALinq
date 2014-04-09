using System;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<TOut> Cast<TOut>(this IAsyncEnumerable enumerable)
        {
            return Create<TOut>(async producer =>
            {
                await enumerable.ForEach(async (object item) =>
                {
                    await producer.Yield((TOut)item).ConfigureAwait(false);
                }).ConfigureAwait(false);
            });
        }

        public static IAsyncEnumerable<TOut> Cast<TIn, TOut>(this IAsyncEnumerable<TIn> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            return Create<TOut>(async producer =>
            {
                await enumerable.ForEach(async (TIn item) =>
                {
                    await producer.Yield((TOut)(object)item).ConfigureAwait(false);
                }).ConfigureAwait(false);
            });
        }
    }
}