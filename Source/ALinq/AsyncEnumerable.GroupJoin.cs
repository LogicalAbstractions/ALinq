using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer,IAsyncEnumerable<TInner> inner,
                                                                                    Func<TOuter, Task<TKey>> outerKeySelector,
                                                                                    Func<TInner, Task<TKey>> innerKeySelector,
                                                                                    Func<TOuter,IAsyncEnumerable<TInner>, Task<TResult>> resultSelector)
        {
            return GroupJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, EqualityComparer<TKey>.Default);
        }

        public static IAsyncEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer,IAsyncEnumerable<TInner> inner,
            Func<TOuter, Task<TKey>> outerKeySelector,
            Func<TInner, Task<TKey>> innerKeySelector,
            Func<TOuter, IAsyncEnumerable<TInner>, Task<TResult>> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            if (outer == null) throw new ArgumentNullException("outer");
            if (inner == null) throw new ArgumentNullException("inner");
            if (outerKeySelector == null) throw new ArgumentNullException("outerKeySelector");
            if (innerKeySelector == null) throw new ArgumentNullException("innerKeySelector");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");
            if (comparer == null) throw new ArgumentNullException("comparer");

            var innerDictionary = new Dictionary<TKey, List<TInner>>(comparer);

            return Create<TResult>(async producer =>
            {
                // Fill the inner set:
                await inner.ForEach(async state =>
                {
                    var innerKey = await innerKeySelector(state.Item).ConfigureAwait(false);
                    if ( !innerDictionary.ContainsKey(innerKey))
                    {
                        List<TInner> innerList = null;
                        if ( innerDictionary.TryGetValue(innerKey,out innerList))
                        {
                            innerList.Add(state.Item);
                        }
                        else
                        {
                            innerList = new List<TInner>() {state.Item};
                            innerDictionary.Add(innerKey,innerList);
                        }
                    }
                }).ConfigureAwait(false);

                await outer.ForEach(async state =>
                {
                    var outerKey = await outerKeySelector(state.Item).ConfigureAwait(false);
                    List<TInner> innerList;
                    if (innerDictionary.TryGetValue(outerKey, out innerList))
                    {
                        var result = await resultSelector(state.Item, innerList.ToAsync()).ConfigureAwait(false);
                        await producer.Yield(result).ConfigureAwait(false);
                    }
                }).ConfigureAwait(false);
            });
        }
    }
}