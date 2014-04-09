using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class AsyncSortSequenceContext<TElement, TKey> : AsyncSortContext<TElement>
    {
        private readonly Func<TElement, Task<TKey>> keySelector;
        private readonly IComparer<TKey>            comparer;
        private TKey[]                              keys;

        internal override async Task Initialize(TElement[] elements)
        {
            if (ChildContext != null)
            {
                await ChildContext.Initialize(elements).ConfigureAwait(false);
            }

            keys = new TKey[elements.Length];

            for (var i = 0; i < keys.Length; i++)
            {
                keys[i] = await keySelector(elements[i]).ConfigureAwait(false);
            }
        }

        internal override int Compare(int firstIndex, int secondIndex)
        {
            int comparison = comparer.Compare(keys[firstIndex], keys[secondIndex]);

            if (comparison == 0)
            {
                if (ChildContext != null)
                {
                    return ChildContext.Compare(firstIndex, secondIndex);
                }

                comparison = descending ? secondIndex - firstIndex : firstIndex - secondIndex;
            }

            return descending ? -comparison : comparison;
        }

        public AsyncSortSequenceContext(AsyncSortContext<TElement> childContext,Func<TElement, Task<TKey>> keySelector, IComparer<TKey> comparer, bool descending)
            : base(childContext,@descending)
        {
            this.keySelector    = keySelector;
            this.comparer       = comparer;
        }
    }
}