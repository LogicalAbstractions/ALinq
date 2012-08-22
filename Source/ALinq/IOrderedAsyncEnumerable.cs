using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALinq
{
    public interface IOrderedAsyncEnumerable<out T> : IAsyncEnumerable<T>
    {
        IOrderedAsyncEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, Task<TKey>> keySelector, IComparer<TKey> comparer, bool descending);
    }
}
