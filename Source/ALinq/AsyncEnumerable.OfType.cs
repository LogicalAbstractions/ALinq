using System;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<TOut> OfType<TOut>(this IAsyncEnumerable enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            return Create<TOut>(async producer =>
            {
                await enumerable.ForEach(async (object item) =>
                {
                    if (item is TOut)
                    {
                        await producer.Yield((TOut)item).ConfigureAwait(false);
                    }
                }).ConfigureAwait(false);
            });
        }

        public static IAsyncEnumerable<TOut> OfType<TIn, TOut>(this IAsyncEnumerable<TIn> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            return Create<TOut>(async producer =>
            {
                await enumerable.ForEach(async (TIn item) =>
                {
                    if ( item is TOut)
                    {
                        await producer.Yield((TOut)(object)item).ConfigureAwait(false);
                    }
                }).ConfigureAwait(false);
            });
        }
    }
}