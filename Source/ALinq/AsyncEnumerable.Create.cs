using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> Create<T>(Func<ConcurrentAsyncProducer<T>, Task> producerFunc)
        {
            return new ConcurrentAsyncEnumerable<T>(producerFunc);
        }

        public static IAsyncEnumerable<T> Create<T>(T value)
        {
            return Create<T>(async producer =>
            {
                await producer.Yield(value).ConfigureAwait(false);
            });
        }
    }
}