namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> DefaultIfEmpty<T>(this IAsyncEnumerable<T> enumerable)
        {
            return DefaultIfEmpty<T>(enumerable, default(T));
        }

        public static IAsyncEnumerable<T> DefaultIfEmpty<T>(this IAsyncEnumerable<T> enumerable, T defaultValue)
        {
            return Create<T>(async producer =>
            {
                var atLeastOne = false;

                await enumerable.ForEach(async item =>
                {
                    atLeastOne = true;
                    await producer.Yield(item);
                });

                if (!atLeastOne)
                {
                    await producer.Yield(defaultValue);
                }
            });
        }
    }
}