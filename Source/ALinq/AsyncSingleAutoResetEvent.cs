using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class AsyncSingleAutoResetEvent
    {
        private readonly static Task        completed   = Task.FromResult(true);
        private TaskCompletionSource<bool>  waiter;
        private bool                        isSignaled;

        internal Task WaitAsync()
        {
            if (isSignaled)
            {
                isSignaled = false;
                return completed;
            }

            if ( waiter != null )
            {
                throw new InvalidOperationException("Only one waiter allowed");
            }

            waiter = new TaskCompletionSource<bool>();
            return waiter.Task;
        }

        internal void Set()
        {
            if (waiter != null)
            {
                var localWaiter = waiter;
                waiter = null;
                localWaiter.SetResult(true);
            }
            else if (!isSignaled)
            {
                isSignaled = true;
            }
        }
    }
}