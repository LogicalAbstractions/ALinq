using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class AsyncEnumerableConverter<T> : IAsyncEnumerable<T>
    {
        private readonly Func<IEnumerator<Task<T>>> enumeratorFactory;

        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetEnumerator()
        {
            return new AsyncEnumeratorConverter<T>(enumeratorFactory());
        }

        IAsyncEnumerator IAsyncEnumerable.GetEnumerator()
        {
            return new AsyncEnumeratorConverter<T>(enumeratorFactory());
        }

        internal AsyncEnumerableConverter(Func<IEnumerator<Task<T>>> enumeratorFactory)
        {
            if (enumeratorFactory == null) throw new ArgumentNullException("enumeratorFactory");

            this.enumeratorFactory = enumeratorFactory;
        }
    }
}