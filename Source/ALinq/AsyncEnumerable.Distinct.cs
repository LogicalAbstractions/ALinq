using System;
using System.Collections.Generic;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> Distinct<T>(this IAsyncEnumerable<T> enumerable,IEqualityComparer<T> comparer)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (comparer == null) throw new ArgumentNullException("comparer");

            return null;
        }
    }
}