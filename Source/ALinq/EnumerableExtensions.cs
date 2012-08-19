﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ALinq;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        public static IAsyncEnumerable<T> ToAsync<T>(this IEnumerator<Task<T>> enumerator)
        {
            if (enumerator == null) throw new ArgumentNullException("enumerator");

            var wasRetrieved = false;

            return ToAsync(() =>
            {
                if ( wasRetrieved )
                {
                    throw new InvalidOperationException("Sequence can only be evaluated once when created from raw enumerator");
                }

                wasRetrieved = true;
                return enumerator;
            });
        }

        public static IAsyncEnumerable<T> ToAsync<T>(this IEnumerable<Task<T>> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            return ToAsync(enumerable.GetEnumerator);
        }

        public static IAsyncEnumerable<T> ToAsync<T>(Func<IEnumerator<Task<T>>> enumeratorFactory)
        {
            if (enumeratorFactory == null) throw new ArgumentNullException("enumeratorFactory");

            return new AsyncEnumerableConverter<T>(enumeratorFactory);
        }
    }
}