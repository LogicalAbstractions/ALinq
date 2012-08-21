using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    public class OrderedAsyncEnumerable<TKey,TValue> : IOrderedAsyncEnumerable<TValue>
    {
        private readonly IAsyncEnumerable<TValue>   enumerable;
        private readonly Func<TValue, Task<TKey>>   keySelector;
        private readonly IComparer<TKey>            comparer; 

        public OrderedAsyncEnumerable(IAsyncEnumerable<TValue> enumerable,Func<TValue,Task<TKey>> keySelector,IComparer<TKey> comparer)
        {
            
        }

        IOrderedAsyncEnumerable<TValue> IOrderedAsyncEnumerable<TValue>.CreateOrderedEnumerable<TThenKey>(Func<TValue, Task<TThenKey>> keySelector, IComparer<TThenKey> comparer, bool descendind)
        {
            throw new NotImplementedException();
        }

        IAsyncEnumerator<TValue> IAsyncEnumerable<TValue>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IAsyncEnumerator IAsyncEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}