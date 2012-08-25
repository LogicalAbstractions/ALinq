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
        private readonly TaskCompletionSource<int>  taskCompletionSource = new TaskCompletionSource<int>();
     
        internal Task WaitForCompletion()
        {
            return taskCompletionSource.Task;
        }

        void IObserver<T>.OnCompleted()
        {
            taskCompletionSource.SetResult(0);
        }

        void IObserver<T>.OnError(Exception error)
        {
            taskCompletionSource.SetException(error);
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
