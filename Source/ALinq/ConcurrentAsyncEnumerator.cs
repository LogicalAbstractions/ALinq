using System;
using System.Threading;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class ConcurrentAsyncEnumerator<T> : IAsyncEnumerator<T>,IDisposable
    {
        private readonly ConcurrentAsyncProducer<T> producer;
        private readonly SemaphoreSlim              consumerSemaphore = new SemaphoreSlim(0,1);
        private T                                   current;
        private volatile bool                       hasFinished;
        private Exception                           exception;
       
        T IAsyncEnumerator<T>.Current
        {
            get { return current; }
        }

        object IAsyncEnumerator.Current
        {
            get { return current; }
        }

        async Task<bool> IAsyncEnumerator.MoveNext()
        {
            if ( hasFinished )
            {
                if ( exception != null )
                {
                    throw exception;
                }

                return false;
            }

            consumerSemaphore.Release();
            await producer.WaitAsync();

            if (hasFinished)
            {
                if (exception != null)
                {
                    throw exception;
                }

                return false;
            }

            return true;
        }

        void IDisposable.Dispose()
        {
            producer.Dispose();
            consumerSemaphore.Dispose();
        }

        internal ConcurrentAsyncEnumerator(Func<ConcurrentAsyncProducer<T>,Task> producerFunc)
        {
            producer = new ConcurrentAsyncProducer<T>(async item =>
            {
                await consumerSemaphore.WaitAsync();
                current = item;
            });

            producerFunc(producer).ContinueWith(t =>
            {
                if ( t.Exception != null )
                {
                    exception = t.Exception;
                }

                hasFinished = true;
                producer.TryRelease();
            },TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}