using System;
using System.Threading;
using System.Threading.Tasks;

namespace ALinq
{
    public sealed class ConcurrentAsyncProducer<T>
    {
        private readonly Func<T,Task>   notificationFunc;
        private readonly SemaphoreSlim  producerSemaphore = new SemaphoreSlim(0,1);
   
        public async Task Yield(T item)
        {
            await notificationFunc(item);
            producerSemaphore.Release();
        }

        internal void Dispose()
        {
            producerSemaphore.Dispose();
        }

        internal Task WaitAsync()
        {
            return producerSemaphore.WaitAsync();
        }

        internal bool TryRelease()
        {
            try
            {
                producerSemaphore.Release();
                return true;
            }
            catch( SemaphoreFullException )
            {
                return false;
            }
        }

        internal ConcurrentAsyncProducer(Func<T,Task> notificationFunc)
        {
            if (notificationFunc == null) throw new ArgumentNullException("notificationFunc");

            this.notificationFunc = notificationFunc;
        }
    }
}