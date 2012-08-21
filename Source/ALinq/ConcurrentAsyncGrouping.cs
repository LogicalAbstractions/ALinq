using System;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class ConcurrentAsyncGrouping<TKey,TElement> : ConcurrentAsyncEnumerable<TElement>,IAsyncGrouping<TKey,TElement>
    {
        private readonly TKey key;

        TKey IAsyncGrouping<TKey, TElement>.Key
        {
            get { return key; }
        }

        internal ConcurrentAsyncGrouping(TKey key,Func<ConcurrentAsyncProducer<TElement>, Task> producerFunc) 
            : base(producerFunc)
        {
            this.key = key;
        }
    }
}