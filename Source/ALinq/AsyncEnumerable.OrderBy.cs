using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IOrderedAsyncEnumerable<TValue> OrderByDescending<TKey,TValue>(this IAsyncEnumerable<TValue> enumerable,Func<TValue,Task<TKey>> keySelector)
        {
            return OrderByCore(enumerable, keySelector, Comparer<TKey>.Default,true);
        }

        public static IOrderedAsyncEnumerable<TValue> OrderByDescending<TKey,TValue>(this IAsyncEnumerable<TValue> enumerable,Func<TValue,Task<TKey>> keySelector,IComparer<TKey> comparer)
        {
            return OrderByCore(enumerable, keySelector, comparer,true);
        }

        public static IOrderedAsyncEnumerable<TValue> OrderBy<TKey,TValue>(this IAsyncEnumerable<TValue> enumerable,Func<TValue,Task<TKey>> keySelector)
        {
            return OrderByCore(enumerable, keySelector, Comparer<TKey>.Default,false);
        }

        public static IOrderedAsyncEnumerable<TValue> OrderBy<TKey, TValue>(this IAsyncEnumerable<TValue> enumerable, Func<TValue, Task<TKey>> keySelector,IComparer<TKey> comparer)
        {
            return OrderByCore(enumerable, keySelector,comparer,false);
        }

        public static IOrderedAsyncEnumerable<TValue> ThenByDescending<TKey, TValue>(this IOrderedAsyncEnumerable<TValue> enumerable, Func<TValue, Task<TKey>> keySelector)
        {
            return ThenByCore(enumerable, keySelector, Comparer<TKey>.Default, true);
        }

        public static IOrderedAsyncEnumerable<TValue> ThenByDescending<TKey, TValue>(this IOrderedAsyncEnumerable<TValue> enumerable, Func<TValue, Task<TKey>> keySelector, IComparer<TKey> comparer)
        {
            return ThenByCore(enumerable, keySelector, comparer, true);
        }

        public static IOrderedAsyncEnumerable<TValue> ThenBy<TKey, TValue>(this IOrderedAsyncEnumerable<TValue> enumerable, Func<TValue, Task<TKey>> keySelector)
        {
            return ThenByCore(enumerable, keySelector, Comparer<TKey>.Default, false);
        }

        public static IOrderedAsyncEnumerable<TValue> ThenBy<TKey, TValue>(this IOrderedAsyncEnumerable<TValue> enumerable, Func<TValue, Task<TKey>> keySelector, IComparer<TKey> comparer)
        {
            return ThenByCore(enumerable, keySelector, comparer, false);
        }

        private static IOrderedAsyncEnumerable<TValue> OrderByCore<TKey,TValue>(IAsyncEnumerable<TValue> enumerable,Func<TValue,Task<TKey>> keySelector,IComparer<TKey> comparer,bool descending)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (comparer == null) throw new ArgumentNullException("comparer");

            return new OrderedAsyncSequence<TKey, TValue>(enumerable, keySelector, comparer, descending);
        }

        private static IOrderedAsyncEnumerable<TValue> ThenByCore<TKey,TValue>(IOrderedAsyncEnumerable<TValue> enumerable,Func<TValue,Task<TKey>> keySelector,IComparer<TKey> comparer,bool descending )
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (comparer == null) throw new ArgumentNullException("comparer");

            return enumerable.CreateOrderedEnumerable(keySelector, comparer, descending);
        }
    }
}