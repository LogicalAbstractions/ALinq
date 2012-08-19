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