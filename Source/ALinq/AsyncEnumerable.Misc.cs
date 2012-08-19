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