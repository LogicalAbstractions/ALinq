using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class OrderedAsyncSequence<TKey,TElement> : OrderedAsyncEnumerable<TElement>
    {
        private readonly OrderedAsyncEnumerable<TElement>   parent;
		private readonly Func<TElement, Task<TKey>>         keySelector;
		private readonly IComparer<TKey>                    comparer;
        private readonly bool                               descending;

        internal override AsyncSortContext<TElement> CreateContext(AsyncSortContext<TElement> current)
        {
 	        AsyncSortContext<TElement> context = new AsyncSortSequenceContext<TElement, TKey> (current,keySelector, comparer, descending);
			return parent != null ? parent.CreateContext(context) : context;
        }

        protected override IAsyncEnumerable<TElement> Sort(IAsyncEnumerable<TElement> source)
        {
            return AsyncEnumerable.Create<TElement>(async producer =>
            {
                var result = await QuickSort<TElement>.Sort(await source.ToList().ConfigureAwait(false), CreateContext(null)).ConfigureAwait(false);

                foreach( var element in result )
                {
                    await producer.Yield(element).ConfigureAwait(false);
                }
            });
        }

        internal OrderedAsyncSequence (OrderedAsyncEnumerable<TElement> parent, IAsyncEnumerable<TElement> source, Func<TElement, Task<TKey>> keySelector, IComparer<TKey> comparer, bool descending)
			: this (source, keySelector, comparer, descending)
		{
			this.parent = parent;
		}

        internal OrderedAsyncSequence (IAsyncEnumerable<TElement> source, Func<TElement, Task<TKey>> keySelector, IComparer<TKey> comparer, bool descending)
            : base (source)
        {
            this.keySelector    = keySelector;
            this.comparer       = comparer ?? Comparer<TKey>.Default;
            this.descending     = descending;
        }
    }
}