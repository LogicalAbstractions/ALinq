using System.Collections.Generic;

namespace ALinq
{
    internal sealed class ReverseComparer<T> : IComparer<T>
    {
        private readonly IComparer<T> comparer;

        int IComparer<T>.Compare(T x, T y)
        {
            return comparer.Compare(x, y)*-1;
        }

        internal ReverseComparer(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }
    }
}