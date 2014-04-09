using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source,
                                                                                                        Func<TSource, Task<TKey>> keySelector,
                                                                                                        Func<TSource, Task<TElement>> elementSelector)
        {
            return GroupBy(source, keySelector, elementSelector, EqualityComparer<TKey>.Default);
        }

        public static IAsyncEnumerable<IAsyncGrouping<TKey,TSource>> GroupBy<TSource, TKey>(this IAsyncEnumerable<TSource> source,
                                                                                            Func<TSource, Task<TKey>> keySelector)
        {
            return GroupBy(source, keySelector, EqualityComparer<TKey>.Default);
        }

        public static IAsyncEnumerable<IAsyncGrouping<TKey,TSource>> GroupBy<TSource, TKey>(this IAsyncEnumerable<TSource> source,
                                                                                            Func<TSource, Task<TKey>> keySelector,
                                                                                            IEqualityComparer<TKey> comparer)
        {
#pragma warning disable 1998
            return GroupBy(source, keySelector, async element => element, comparer);
#pragma warning restore 1998
        }

        public static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source,
	            Func<TSource, Task<TKey>> keySelector,
	            Func<TSource, Task<TElement>> elementSelector,
	            IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (elementSelector == null) throw new ArgumentNullException("elementSelector");
            if (comparer == null) throw new ArgumentNullException("comparer");

            return Create<IAsyncGrouping<TKey,TElement>>(async producer =>
            {
                var dictionary = new Dictionary<TKey, List<TElement>>(comparer);

                await source.ForEach(async item =>
                {
                    var key     = await keySelector(item).ConfigureAwait(false);
                    var element = await elementSelector(item).ConfigureAwait(false);

                    List<TElement> groupList = null;

                    if ( !dictionary.TryGetValue(key,out groupList))
                    {
                        groupList = new List<TElement>();
                        dictionary.Add(key,groupList);
                    }

                    groupList.Add(element);
                }).ConfigureAwait(false);

                foreach( var group in dictionary)
                {
                    var localGroup = group;
                    var grouping = new ConcurrentAsyncGrouping<TKey, TElement>(group.Key, async groupProducer =>
                    {
                        foreach( var element in localGroup.Value)
                        {
                            await groupProducer.Yield(element).ConfigureAwait(false);
                        }
                    });

                    await producer.Yield(grouping).ConfigureAwait(false);
                }
            });
        }

        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TElement,TResult>(this IAsyncEnumerable<TSource> source,
                                                                                         Func<TSource, Task<TKey>> keySelector,
                                                                                         Func<TSource, Task<TElement>> elementSelector,
                                                                                         Func<TKey,IAsyncEnumerable<TElement>,Task<TResult>> resultSelector)
        {
            return GroupBy(source, keySelector, elementSelector, resultSelector, EqualityComparer<TKey>.Default);
        }

        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey,TResult>(this IAsyncEnumerable<TSource> source,
                                                                               Func<TSource, Task<TKey>> keySelector,
                                                                               Func<TKey,IAsyncEnumerable<TSource>,Task<TResult>> resultSelector)
        {
            return GroupBy(source, keySelector, resultSelector, EqualityComparer<TKey>.Default);
        }

        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey,TResult>(this IAsyncEnumerable<TSource> source,
                                                                                         Func<TSource, Task<TKey>> keySelector,
                                                                                         Func<TKey,IAsyncEnumerable<TSource>,Task<TResult>> resultSelector,
                                                                                         IEqualityComparer<TKey> comparer)
        {
#pragma warning disable 1998
            return GroupBy(source, keySelector, async element => element, resultSelector, comparer);
#pragma warning restore 1998
        }

        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TElement,TResult>(this IAsyncEnumerable<TSource> source,
            Func<TSource, Task<TKey>> keySelector,
            Func<TSource, Task<TElement>> elementSelector,
            Func<TKey,IAsyncEnumerable<TElement>,Task<TResult>> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (elementSelector == null) throw new ArgumentNullException("elementSelector");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");
            if (comparer == null) throw new ArgumentNullException("comparer");

            return Create<TResult>(async producer =>
            {
                var dictionary = new Dictionary<TKey, List<TElement>>(comparer);

                await source.ForEach(async item =>
                {
                    var key = await keySelector(item).ConfigureAwait(false);
                    var element = await elementSelector(item).ConfigureAwait(false);

                    List<TElement> groupList = null;

                    if (!dictionary.TryGetValue(key, out groupList))
                    {
                        groupList = new List<TElement>();
                        dictionary.Add(key, groupList);
                    }

                    groupList.Add(element);
                }).ConfigureAwait(false);

                foreach (var group in dictionary)
                {
                    var localGroup = group;
                    var result = await resultSelector(localGroup.Key, localGroup.Value.ToAsync()).ConfigureAwait(false);
                    await producer.Yield(result).ConfigureAwait(false);
                }
            });
        }
    }
}