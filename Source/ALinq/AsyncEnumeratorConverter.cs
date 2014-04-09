using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class AsyncEnumeratorConverter<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<Task<T>>   enumerator;
        private T                               current;

        object IAsyncEnumerator.Current
        {
            get { return current; }
        }

        T IAsyncEnumerator<T>.Current
        {
            get { return current; }
        }

        async Task<bool> IAsyncEnumerator.MoveNext()
        {
            if (enumerator.MoveNext())
            {
                using (var task = enumerator.Current)
                {
                    if (task != null)
                    {
                        current = await task.ConfigureAwait(false);
                        return true;
                    }
                }
            }

            return false;
        }

        internal AsyncEnumeratorConverter(IEnumerator<Task<T>> enumerator)
        {
            if (enumerator == null) throw new ArgumentNullException("enumerator");

            this.enumerator = enumerator;
        }
    }
}