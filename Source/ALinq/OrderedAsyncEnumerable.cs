using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALinq
{
    internal abstract class OrderedAsyncEnumerable<TValue> : IOrderedAsyncEnumerable<TValue> 
    {
        private readonly IAsyncEnumerable<TValue> source;

        internal abstract AsyncSortContext<TValue>  CreateContext(AsyncSortContext<TValue> current);
        protected abstract IAsyncEnumerable<TValue> Sort(IAsyncEnumerable<TValue> source);

        IOrderedAsyncEnumerable<TValue> IOrderedAsyncEnumerable<TValue>.CreateOrderedEnumerable<TKey>(Func<TValue, Task<TKey>> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return new OrderedAsyncSequence<TKey,TValue>(this, source, keySelector, comparer,descending);
        }

        IAsyncEnumerator<TValue> IAsyncEnumerable<TValue>.GetEnumerator()
        {
            return Sort(source).GetEnumerator();
        }

        IAsyncEnumerator IAsyncEnumerable.GetEnumerator()
        {
            return Sort(source).GetEnumerator();
        }

        protected OrderedAsyncEnumerable (IAsyncEnumerable<TValue> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            this.source = source;
        }
    }
}