using System.Threading.Tasks;

namespace ALinq
{
    internal abstract class AsyncSortContext<TElement>
    {
        private readonly bool                        descending;
        private readonly AsyncSortContext<TElement>  childContext;

        protected bool Descending
        {
            get { return descending; }
        }

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