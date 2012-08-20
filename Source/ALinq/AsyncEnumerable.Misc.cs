using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> Create<T>(Func<ConcurrentAsyncProducer<T>,Task> producerFunc)
        {
            return new ConcurrentAsyncEnumerable<T>(producerFunc);
        }

        public static IAsyncEnumerable<T> Create<T>(T value)
        {
            return Create<T>(async producer =>
            {
                await producer.Yield(value);
            });
        }

        public static IAsyncEnumerable<T> Repeat<T>(T value,int count)
        {
            if ( count < 0 ) throw new ArgumentOutOfRangeException("count","count must be zero or greater");

            return Create<T>(async producer =>
            {
                for( var i = 0; i < count; ++i )
                {
                    await producer.Yield(value);
                }
            });
        }

        public static IAsyncEnumerable<T> Empty<T>()
        {
#pragma warning disable 1998
            return new ConcurrentAsyncEnumerable<T>(async producer => {});
#pragma warning restore 1998
        }

        internal static void Dispose(this IAsyncEnumerator asyncEnumerator)
        {
            var disposable = asyncEnumerator as IDisposable;

            if ( disposable != null )
            {
                disposable.Dispose();
            }
        }
    }
}