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

        #region Average

        public static Task<int> Average(this IAsyncEnumerable<int> enumerable)
        {
            return AverageCore(enumerable, (current, next) => current + next, (sum, counter) => sum / (int)counter);
        }

        public static async Task<int> Average(this IAsyncEnumerable<int?> enumerable)
        {
            return (await AverageCore(enumerable, (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / (int?)counter)).Value;
        }

        public static Task<long> Average(this IAsyncEnumerable<long> enumerable)
        {
            return AverageCore(enumerable, (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<long> Average(this IAsyncEnumerable<long?> enumerable)
        {
            return (await AverageCore(enumerable, (current, next) => next.HasValue ? current + next.Value : current,(sum,counter) => sum / counter)).Value;
        }

        public static Task<float> Average(this IAsyncEnumerable<float> enumerable)
        {
            return AverageCore(enumerable, (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<float> Average(this IAsyncEnumerable<float?> enumerable)
        {
            return (await AverageCore(enumerable, (current, next) => next.HasValue ? current + next.Value : current,(sum,counter) => sum / counter)).Value;
        }

        public static Task<double> Average(this IAsyncEnumerable<double> enumerable)
        {
            return AverageCore(enumerable, (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<double> Average(this IAsyncEnumerable<double?> enumerable)
        {
            return (await AverageCore(enumerable, (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / counter)).Value;
        }

        public static Task<decimal> Average(this IAsyncEnumerable<decimal> enumerable)
        {
            return AverageCore(enumerable, (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<decimal> Average(this IAsyncEnumerable<decimal?> enumerable)
        {
            return (await AverageCore(enumerable, (current, next) => next.HasValue ? current + next.Value : current,(sum,counter) => sum / counter)).Value;
        }

        public static Task<int> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<int>> selector)
        {
            return AverageCore(enumerable.Select(selector), (current, next) => current + next, (sum, counter) => sum / (int)counter);
        }

        public static async Task<int> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<int?>> selector)
        {
            return (await AverageCore(enumerable.Select(selector), (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / (int?)counter)).Value;
        }

        public static Task<long> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<long>> selector)
        {
            return AverageCore(enumerable.Select(selector), (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<long> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<long?>> selector)
        {
            return (await AverageCore(enumerable.Select(selector), (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / counter)).Value;
        }

        public static Task<float> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<float>> selector)
        {
            return AverageCore(enumerable.Select(selector), (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<float> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<float?>> selector)
        {
            return (await AverageCore(enumerable.Select(selector), (current, next) => next.HasValue ? current + next.Value : current,(sum,counter) => sum / counter)).Value;
        }

        public static Task<double> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<double>> selector)
        {
            return AverageCore(enumerable.Select(selector), (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<double> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<double?>> selector)
        {
            return (await AverageCore(enumerable.Select(selector), (current, next) => next.HasValue ? current + next.Value : current,(sum,counter) => sum / counter)).Value;
        }

        public static Task<decimal> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<decimal>> selector)
        {
            return AverageCore(enumerable.Select(selector), (current, next) => current + next,(sum,counter) => sum / counter);
        }

        public static async Task<decimal> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<decimal?>> selector)
        {
            return (await AverageCore(enumerable.Select(selector), (current, next) => next.HasValue ? current + next.Value : current,(sum,counter) => sum / counter)).Value;
        }

        private static async Task<T> AverageCore<T>(this IAsyncEnumerable<T> enumerable, Func<T, T, T> sumFunction,Func<T,long,T> averageFunction)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (sumFunction == null) throw new ArgumentNullException("sumFunction");
            if (averageFunction == null) throw new ArgumentNullException("averageFunction");

            var counter = 0L;
            var accumulator = default(T);

#pragma warning disable 1998
            await enumerable.ForEach(async value => {
                accumulator = sumFunction(accumulator, value);
                counter++;
            });
#pragma warning restore 1998

            return averageFunction(accumulator, counter);
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