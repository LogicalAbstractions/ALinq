using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class ConversionObserver<T> : IObserver<T> 
    {
        private readonly ConcurrentAsyncProducer<T> producer;
        private readonly AsyncAutoResetEvent        completedEvent = new AsyncAutoResetEvent();
        private Exception                           exception;

        internal async Task WaitForCompletion()
        {
            await completedEvent.WaitAsync();

            if (exception != null)
            {
                throw exception;
            }
        }

        void IObserver<T>.OnCompleted()
        {
            completedEvent.Set();
        }

        void IObserver<T>.OnError(Exception error)
        {
            exception = error;
            completedEvent.Set();
        }

        void IObserver<T>.OnNext(T value)
        {
            producer.Yield(value).Wait();
        }

        internal ConversionObserver(ConcurrentAsyncProducer<T> producer)
        {
            this.producer = producer;
        }
    }
}
