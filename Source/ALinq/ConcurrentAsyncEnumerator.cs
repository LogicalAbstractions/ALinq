using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ALinq
{
    internal sealed class ConcurrentAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly BufferBlock<T> valueBufferBlock = new BufferBlock<T>(new DataflowBlockOptions() { BoundedCapacity = 1});
        private T                       current;
        private Exception               exception;
       
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
            try
            {
                var newValue = await valueBufferBlock.ReceiveAsync().ConfigureAwait(false);
                current = newValue;
                return true;
            }
            catch ( InvalidOperationException )
            {
                if ( exception != null )
                {
                    throw exception;
                }

                return false;
            }
        }

        internal ConcurrentAsyncEnumerator(Func<ConcurrentAsyncProducer<T>,Task> producerFunc)
        {
            var producer = new ConcurrentAsyncProducer<T>(async item =>
            {
                await valueBufferBlock.SendAsync(item).ConfigureAwait(false);
            });

            producerFunc(producer).ContinueWith(t =>
            {
                if ( t.Exception != null )
                {
                    exception = t.Exception;
                }

                valueBufferBlock.Complete();
            },TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}