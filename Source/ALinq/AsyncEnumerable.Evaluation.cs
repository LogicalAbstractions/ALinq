using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<IList<T>> ToList<T>(this IAsyncEnumerable<T> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            var result = new List<T>();

#pragma warning disable 1998
            await enumerable.ForEach(async item => result.Add(item));
#pragma warning restore 1998

            return result;
        }

        public static async Task<T[]> ToArray<T>(this IAsyncEnumerable<T> enumerable )
        {
            var list = await ToList(enumerable);
            return list.ToArray();
        }

        public static async Task<T> First<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return await First<T>(enumerable, async item => true);
#pragma warning restore 1998
        }

        public static async Task<T> First<T>(this IAsyncEnumerable<T> enumerable,Func<T,Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var result  = default(T);
            var found   = false;

            await enumerable.ForEach(async item =>
            {
                if ( await predicate(item))
                {
                    found = true;
                    result = item;
                }
            });

            if ( found )
            {
                return result;
            }

            throw new InvalidOperationException("Sequence contains no matching element");
        }

        public static async Task<T> Last<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return await First<T>(enumerable.Reverse(), async item => true);
#pragma warning restore 1998
        }

        public static async Task<T> Last<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            return await First<T>(enumerable.Reverse(), predicate);
        }

        public static async Task<T> Single<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return await First<T>(enumerable, async item => true);
#pragma warning restore 1998
        }

        public static async Task<T> Single<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var result  = default(T);
            var counter = 0;

            await enumerable.ForEach(async state =>
            {
                if (await predicate(state.Item))
                {
                    counter++;
                    result = state.Item;

                    if (counter > 1)
                    {
                        state.Break();
                    }
                }
            });

            if (counter == 0)
            {
                throw new InvalidOperationException("Sequence contains no matching element");
            }

            if (counter == 1)
            {
                return result;
            }

            throw new InvalidOperationException("Sequence contains more than one element");
        }

        public static async Task<T> SingleOrDefault<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return await First<T>(enumerable, async item => true);
#pragma warning restore 1998
        }

        public static async Task<T> SingleOrDefault<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var result = default(T);
            var counter = 0;

            await enumerable.ForEach(async state =>
            {
                if (await predicate(state.Item))
                {
                    counter++;
                    result = state.Item;

                    if (counter > 1)
                    {
                        state.Break();
                    }
                }
            });


            if (counter == 1)
            {
                return result;
            }

            return default(T);
        }

        public static Task<T> LastOrDefault<T>(this IAsyncEnumerable<T> enumerable)
        {
            return FirstOrDefault(enumerable.Reverse());
        }

        public static Task<T> LastOrDefault<T>(this IAsyncEnumerable<T> enumerable,Func<T,Task<bool>> predicate)
        {
            return FirstOrDefault(enumerable.Reverse(), predicate);
        }

        public static async Task<T> FirstOrDefault<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return await FirstOrDefault<T>(enumerable, async item => true);
#pragma warning restore 1998
        }

        public static async Task<T> FirstOrDefault<T>(this IAsyncEnumerable<T> enumerable,Func<T,Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var result = default(T);
            var found = false;

            await enumerable.ForEach(async item =>
            {
                if (await predicate(item))
                {
                    found = true;
                    result = item;
                }
            });

            if (found)
            {
                return result;
            }

            return default(T);
        }

        public static IAsyncEnumerable<T> DefaultIfEmpty<T>(this IAsyncEnumerable<T> enumerable)
        {
            return DefaultIfEmpty<T>(enumerable, default(T));
        }

        public static IAsyncEnumerable<T> DefaultIfEmpty<T>(this IAsyncEnumerable<T> enumerable,T defaultValue)
        {
            return Create<T>(async producer =>
            {
                var atLeastOne = false;

                await enumerable.ForEach(async item =>
                {
                    atLeastOne = true;
                    await producer.Yield(item);
                });

                if ( !atLeastOne )
                {
                    await producer.Yield(defaultValue);
                }
            });
        }

        public static Task<int> Count<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return Count<T>(enumerable, async item => true);
#pragma warning restore 1998
        }

        public static async Task<int> Count<T>(this IAsyncEnumerable<T> enumerable,Func<T,Task<bool>> selector)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (selector == null) throw new ArgumentNullException("selector");

            var counter = 0;
#pragma warning disable 1998
            await enumerable.Where(selector).ForEach(async (T item) =>
#pragma warning restore 1998
            {
                counter++;
            });

            return counter;
        }

        public static Task<long> LongCount<T>(this IAsyncEnumerable<T> enumerable)
        {
#pragma warning disable 1998
            return LongCount<T>(enumerable, async item => true);
#pragma warning restore 1998
        }

        public static async Task<long> LongCount<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> selector)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (selector == null) throw new ArgumentNullException("selector");

            var counter = 0L;
#pragma warning disable 1998
            await enumerable.Where(selector).ForEach(async (T item) =>
#pragma warning restore 1998
            {
                counter++;
            });

            return counter;
        }

        public static async Task<T> ElementAtOrDefault<T>(this IAsyncEnumerable<T> enumerable,int index)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (index < 0) throw new ArgumentOutOfRangeException("index", "index must be zero or greater");

            var result = default(T);
            var found = false;

#pragma warning disable 1998
            await enumerable.ForEach(async state =>
#pragma warning restore 1998
            {
                if (state.Index == index)
                {
                    result = state.Item;
                    found = true;
                }
            });

            if (found)
            {
                return result;
            }

            return default(T);
        }

        public static async Task<T> ElementAt<T>(this IAsyncEnumerable<T> enumerable,int index)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (index < 0 ) throw new ArgumentOutOfRangeException("index","index must be zero or greater");

            var result = default(T);
            var found  = false;

#pragma warning disable 1998
            await enumerable.ForEach(async state =>
#pragma warning restore 1998
            {
                if ( state.Index == index)
                {
                    result = state.Item;
                    found = true;
                }
            });

            if ( found )
            {
                return result;
            }

            throw new InvalidOperationException(string.Format("Sequence is smaller than the given index: {0}",index));
        }

        public static IAsyncEnumerable<T> Skip<T>(this IAsyncEnumerable<T> enumerable,int count)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if ( count < 0 ) throw new ArgumentOutOfRangeException("count","count must be zero or greater");

            return Create<T>(async producer =>
            {
                await enumerable.ForEach(async state =>
                {
                    if ( state.Index >= count )
                    {
                        await producer.Yield(state.Item);
                    }
                });
            });
        }

        public static IAsyncEnumerable<T> SkipWhile<T>(this IAsyncEnumerable<T> enumerable,Func<T,Task<bool>> predicate)
        {
            return SkipWhile(enumerable, (item, index) => predicate(item));
        }

        public static IAsyncEnumerable<T> SkipWhile<T>(this IAsyncEnumerable<T> enumerable,Func<T,long,Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var doYield = false;

            return Create<T>(async producer =>
            {
                await enumerable.ForEach(async state =>
                {
                    if ( !doYield && !await predicate(state.Item,state.Index))
                    {
                        doYield = true;
                    }

                    if ( doYield )
                    {
                        await producer.Yield(state.Item);
                    }
                });
            });
        }

        public static IAsyncEnumerable<T> Take<T>(this IAsyncEnumerable<T> enumerable,int count)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (count < 0) throw new ArgumentOutOfRangeException("count", "count must be zero or greater");

            var counter = 0;

            return Create<T>(async producer =>
            {
                await enumerable.ForEach(async state =>
                {
                    if (counter < count)
                    {
                        await producer.Yield(state.Item);
                    }

                    counter++;
                });
            });
        }

        public static IAsyncEnumerable<T> TakeWhile<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<bool>> predicate)
        {
            return TakeWhile(enumerable, (item, index) => predicate(item));
        }

        public static IAsyncEnumerable<T> TakeWhile<T>(this IAsyncEnumerable<T> enumerable, Func<T, long, Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var doYield = true;

            return Create<T>(async producer =>
            {
                await enumerable.ForEach(async state =>
                {
                    if (doYield && !await predicate(state.Item, state.Index))
                    {
                        doYield = false;
                        state.Break();
                    }

                    if (doYield)
                    {
                        await producer.Yield(state.Item);
                    }
                });
            });
        }
    }
}