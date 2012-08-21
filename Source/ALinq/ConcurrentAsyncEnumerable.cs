using System;
using System.Threading.Tasks;

namespace ALinq
{
    internal class ConcurrentAsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        private readonly Func<ConcurrentAsyncProducer<T>, Task> producerFunc;

        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetEnumerator()
        {
            return new ConcurrentAsyncEnumerator<T>(producerFunc);
        }

        IAsyncEnumerator IAsyncEnumerable.GetEnumerator()
        {
            return new ConcurrentAsyncEnumerator<T>(producerFunc);
        }

        internal ConcurrentAsyncEnumerable(Func<ConcurrentAsyncProducer<T>, Task> producerFunc)
        {
            if (producerFunc == null) throw new ArgumentNullException("producerFunc");
            this.producerFunc = producerFunc;
        }
    }
}