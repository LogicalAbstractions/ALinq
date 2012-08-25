using System.Threading.Tasks;

namespace ALinq
{
    internal abstract class AsyncSortContext<TElement>
    {
        protected readonly bool                         descending;
        private readonly AsyncSortContext<TElement>     childContext;

        protected AsyncSortContext<TElement> ChildContext
        {
            get { return childContext; }
        }

        internal abstract Task Initialize(TElement[] elements);
        internal abstract int  Compare(int firstIndex, int secondIndex);

        protected AsyncSortContext(AsyncSortContext<TElement> childContext,bool descending)
        {
            this.childContext   = childContext;
            this.descending     = descending;
        }
    }
}