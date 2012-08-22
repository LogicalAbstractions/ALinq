using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class AsyncAutoResetEvent
    {
        private readonly static Task                        completed   = Task.FromResult(true);
        private readonly Queue<TaskCompletionSource<bool>>  waiters     = new Queue<TaskCompletionSource<bool>>();
        private bool                                        isSignaled;

        internal Task WaitAsync()
        {
            lock (waiters)
            {
                if (isSignaled)
                {
                    isSignaled = false;
                    return completed;
                }

                var completionSource = new TaskCompletionSource<bool>();
                waiters.Enqueue(completionSource);
                return completionSource.Task;
            }
        }

        internal void Set()
        {
            TaskCompletionSource<bool> toRelease = null;

            lock (waiters)
            {
                if (waiters.Count > 0)
                {
                    toRelease = waiters.Dequeue();
                }
                else if (!isSignaled)
                {
                    isSignaled = true;
                }
            }

            if (toRelease != null)
            {
                toRelease.SetResult(true);
            }
        }
    }
}