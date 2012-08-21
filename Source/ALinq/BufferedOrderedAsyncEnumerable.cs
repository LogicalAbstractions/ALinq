using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class BufferedOrderedAsyncEnumerable<T> : IOrderedAsyncEnumerable<T>
    {
        private readonly IOrderedEnumerable<T> enumerable;

        IOrderedAsyncEnumerable<T> IOrderedAsyncEnumerable<T>.CreateOrderedEnumerable<TKey>(Func<T, Task<TKey>> keySelector, IComparer<TKey> comparer, bool descendind)
        {
            throw new NotImplementedException();
        }

        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetEnumerator()
        {
            return enumerable.ToAsync().GetEnumerator();
        }

        IAsyncEnumerator IAsyncEnumerable.GetEnumerator()
        {
            return enumerable.ToAsync().GetEnumerator();
        }

        internal BufferedOrderedAsyncEnumerable(IOrderedEnumerable<T> enumerable)
        {
            this.enumerable = enumerable;
        }
    }
}