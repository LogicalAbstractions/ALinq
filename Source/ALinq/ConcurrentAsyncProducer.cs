using System;
using System.Threading;
using System.Threading.Tasks;

namespace ALinq
{
    public sealed class ConcurrentAsyncProducer<T>
    {
        private readonly Func<T,Task> notificationFunc;
     
        public async Task Yield(T item)
        {
            await notificationFunc(item).ConfigureAwait(false);
        }

        internal ConcurrentAsyncProducer(Func<T,Task> notificationFunc)
        {
            if (notificationFunc == null) throw new ArgumentNullException("notificationFunc");

            this.notificationFunc = notificationFunc;
        }
    }
}