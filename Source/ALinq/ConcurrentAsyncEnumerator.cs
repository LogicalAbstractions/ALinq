using System;
using System.Threading;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class ConcurrentAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly ConcurrentAsyncProducer<T> producer;
        private readonly AsyncAutoResetEvent        consumerEvent = new AsyncAutoResetEvent();
        private readonly AsyncAutoResetEvent        producerEvent = new AsyncAutoResetEvent();
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

            consumerEvent.Set();
            await producerEvent.WaitAsync();
            return true;
        }

        internal ConcurrentAsyncEnumerator(Func<ConcurrentAsyncProducer<T>,Task> producerFunc)
        {
            producer = new ConcurrentAsyncProducer<T>(async item =>
            {
                await consumerEvent.WaitAsync();
                current = item;
                producerEvent.Set();
            });

            producerFunc(producer).ContinueWith(t =>
            {
                if ( t.Exception != null )
                {
                    exception = t.Exception;
                }

                hasFinished = true;
                producerEvent.Set();
            },TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}