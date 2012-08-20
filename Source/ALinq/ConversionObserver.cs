using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class ConversionObserver<T> : IObserver<T>,IDisposable 
    {
        private readonly ConcurrentAsyncProducer<T> producer;
        private readonly SemaphoreSlim              completedSemaphore = new SemaphoreSlim(0, 1);
        private Exception                           exception;

        internal async Task WaitForCompletion()
        {
            await completedSemaphore.WaitAsync();

            if (exception != null)
            {
                throw exception;
            }
        }

        void IObserver<T>.OnCompleted()
        {
            try
            {
                completedSemaphore.Release();
            }
            catch(SemaphoreFullException)
            {
            
            }
        }

        void IObserver<T>.OnError(Exception error)
        {
            try
            {
                exception = error;
                completedSemaphore.Release();
            }
            catch (SemaphoreFullException)
            {

            }
        }

        void IObserver<T>.OnNext(T value)
        {
            producer.Yield(value).Wait();
        }

        void IDisposable.Dispose()
        {
            completedSemaphore.Dispose();
        }

        internal ConversionObserver(ConcurrentAsyncProducer<T> producer)
        {
            this.producer = producer;
        }
    }
}
