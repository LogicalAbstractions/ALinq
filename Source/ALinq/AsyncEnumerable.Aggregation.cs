using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        #region Sum 
        public static Task<int> Sum(this IAsyncEnumerable<int> enumerable)
        {
            return SumCore(enumerable, (current, next) => current + next);
        }

        public static async Task<int> Sum(this IAsyncEnumerable<int?> enumerable)
        {
            return (await SumCore(enumerable, (current, next) => next.HasValue ? current + next.Value : current)).Value;
        }

        public static Task<long> Sum(this IAsyncEnumerable<long> enumerable)
        {
            return SumCore(enumerable, (current, next) => current + next);
        }

        public static async Task<long> Sum(this IAsyncEnumerable<long?> enumerable)
        {
            return (await SumCore(enumerable, (current, next) => next.HasValue ? current + next.Value : current)).Value;
        }

        public static Task<float> Sum(this IAsyncEnumerable<float> enumerable)
        {
            return SumCore(enumerable, (current, next) => current + next);
        }

        public static async Task<float> Sum(this IAsyncEnumerable<float?> enumerable)
        {
            return (await SumCore(enumerable, (current, next) => next.HasValue ? current + next.Value : current)).Value;
        }

        public static Task<double> Sum(this IAsyncEnumerable<double> enumerable)
        {
            return SumCore(enumerable, (current, next) => current + next);
        }

        public static async Task<double> Sum(this IAsyncEnumerable<double?> enumerable)
        {
            return (await SumCore(enumerable, (current, next) => next.HasValue ? current + next.Value : current)).Value;
        }

        public static Task<decimal> Sum(this IAsyncEnumerable<decimal> enumerable)
        {
            return SumCore(enumerable, (current, next) => current + next);
        }

        public static async Task<decimal> Sum(this IAsyncEnumerable<decimal?> enumerable)
        {
            return (await SumCore(enumerable, (current, next) => next.HasValue ? current + next.Value : current)).Value;
        }

        public static Task<int> Sum<T>(this IAsyncEnumerable<T> enumerable,Func<T,Task<int>> selector )
        {
            return SumCore(enumerable.Select(selector), (current, next) => current + next);
        }

        public static async Task<int> Sum<T>(this IAsyncEnumerable<T> enumerable,Func<T,Task<int?>> selector)
        {
            return (await SumCore(enumerable.Select(selector), (current, next) => next.HasValue ? current + next.Value : current)).Value;
        }

        public static Task<long> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<long>> selector)
        {
            return SumCore(enumerable.Select(selector), (current, next) => current + next);
        }

        public static async Task<long> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<long?>> selector)
        {
            return (await SumCore(enumerable.Select(selector), (current, next) => next.HasValue ? current + next.Value : current)).Value;
        }

        public static Task<float> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<float>> selector)
        {
            return SumCore(enumerable.Select(selector), (current, next) => current + next);
        }

        public static async Task<float> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<float?>> selector)
        {
            return (await SumCore(enumerable.Select(selector), (current, next) => next.HasValue ? current + next.Value : current)).Value;
        }

        public static Task<double> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<double>> selector)
        {
            return SumCore(enumerable.Select(selector), (current, next) => current + next);
        }

        public static async Task<double> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<double?>> selector)
        {
            return (await SumCore(enumerable.Select(selector), (current, next) => next.HasValue ? current + next.Value : current)).Value;
        }

        public static Task<decimal> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<decimal>> selector)
        {
            return SumCore(enumerable.Select(selector), (current, next) => current + next);
        }

        public static async Task<decimal> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<decimal?>> selector)
        {
            return (await SumCore(enumerable.Select(selector), (current, next) => next.HasValue ? current + next.Value : current)).Value;
        }

        private static Task<T> SumCore<T>(this IAsyncEnumerable<T> enumerable,Func<T,T,T> sumFunction)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (sumFunction == null) throw new ArgumentNullException("sumFunction");

#pragma warning disable 1998
            return Aggregate(enumerable, async (a, b) => sumFunction(a, b));
#pragma warning restore 1998
        }

        #endregion

        public static Task<TSource> Aggregate<TSource>(this IAsyncEnumerable<TSource> enumerable,Func<TSource, TSource, Task<TSource>> aggregationFunc)
        {
#pragma warning disable 1998
            return Aggregate(enumerable, default(TSource), aggregationFunc, async result => result);
#pragma warning restore 1998
        }

        public static Task<TAccumulate> Aggregate<TSource, TAccumulate>(this IAsyncEnumerable<TSource> enumerable,TAccumulate seed,Func<TAccumulate, TSource, Task<TAccumulate>> aggregationFunc)
        {
#pragma warning disable 1998
            return Aggregate(enumerable, seed, aggregationFunc, async result => result);
#pragma warning restore 1998
        }

        public static async Task<TResult> Aggregate<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> enumerable,TAccumulate seed,Func<TAccumulate, TSource, Task<TAccumulate>> aggregationFunc,Func<TAccumulate, Task<TResult>> resultSelector)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (aggregationFunc == null) throw new ArgumentNullException("aggregationFunc");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            var accumulator = seed;

            await enumerable.ForEach(async item =>
            {
                accumulator = await aggregationFunc(accumulator, item);
            });

            return await resultSelector(accumulator);
        }
    }
}