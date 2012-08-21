using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class OrderedAsyncEnumerable<TKey,TValue> : IOrderedAsyncEnumerable<TValue> 
    {
        private readonly IAsyncEnumerable<TValue>   enumerable;
        private readonly Func<TValue, Task<TKey>>   keySelector;
        private readonly IComparer<TKey>            comparer; 

        internal OrderedAsyncEnumerable(IAsyncEnumerable<TValue> enumerable,Func<TValue,Task<TKey>> keySelector,IComparer<TKey> comparer,bool descending)
        {
            this.enumerable     = enumerable;
            this.keySelector    = keySelector;
            this.comparer       = descending ? new ReverseComparer<TKey>(comparer) : comparer;
        }

        IOrderedAsyncEnumerable<TValue> IOrderedAsyncEnumerable<TValue>.CreateOrderedEnumerable<TThenKey>(Func<TValue, Task<TThenKey>> keySelector, IComparer<TThenKey> comparer, bool descending)
        {
            throw new NotImplementedException();
        }

        IAsyncEnumerator<TValue> IAsyncEnumerable<TValue>.GetEnumerator()
        {
            return CreateEnumerator();
        }

        IAsyncEnumerator IAsyncEnumerable.GetEnumerator()
        {
            return CreateEnumerator();
        }

        private IAsyncEnumerator<TValue> CreateEnumerator()
        {
            return AsyncEnumerable.Create<TValue>(async producer =>
            {
                var items = await enumerable.ToList();
                var itemPairs = new List<KeyValuePair<TKey, TValue>>();

                foreach (var item in items)
                {
                    itemPairs.Add(new KeyValuePair<TKey, TValue>(await keySelector(item), item));
                }

                foreach (var item in itemPairs.OrderBy(pair => pair.Key).Select(pair => pair.Value))
                {
                    await producer.Yield(item);
                }
            }).GetEnumerator();
        }
    }
}