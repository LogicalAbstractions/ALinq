using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static Task<int> Average(this IAsyncEnumerable<int> enumerable)
        {
            return AverageCore(enumerable, (current, next) => current + next, (sum, counter) => sum / (int)counter);
        }

        public static async Task<int> Average(this IAsyncEnumerable<int?> enumerable)
        {
            return (await AverageCore(enumerable,0, (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / (int?)counter).ConfigureAwait(false)).Value;
        }

        public static Task<long> Average(this IAsyncEnumerable<long> enumerable)
        {
            return AverageCore(enumerable, (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<long> Average(this IAsyncEnumerable<long?> enumerable)
        {
            return (await AverageCore(enumerable,0L, (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / counter).ConfigureAwait(false)).Value;
        }

        public static Task<float> Average(this IAsyncEnumerable<float> enumerable)
        {
            return AverageCore(enumerable, (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<float> Average(this IAsyncEnumerable<float?> enumerable)
        {
            return (await AverageCore(enumerable,0.0f, (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / counter).ConfigureAwait(false)).Value;
        }

        public static Task<double> Average(this IAsyncEnumerable<double> enumerable)
        {
            return AverageCore(enumerable, (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<double> Average(this IAsyncEnumerable<double?> enumerable)
        {
            return (await AverageCore(enumerable,0.0, (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / counter).ConfigureAwait(false)).Value;
        }

        public static Task<decimal> Average(this IAsyncEnumerable<decimal> enumerable)
        {
            return AverageCore(enumerable, (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<decimal> Average(this IAsyncEnumerable<decimal?> enumerable)
        {
            return (await AverageCore(enumerable,0.0m, (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / counter).ConfigureAwait(false)).Value;
        }

        public static Task<int> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<int>> selector)
        {
            return AverageCore(enumerable.Select(selector), (current, next) => current + next, (sum, counter) => sum / (int)counter);
        }

        public static async Task<int> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<int?>> selector)
        {
            return (await AverageCore(enumerable.Select(selector),0,(current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / (int?)counter).ConfigureAwait(false)).Value;
        }

        public static Task<long> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<long>> selector)
        {
            return AverageCore(enumerable.Select(selector), (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<long> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<long?>> selector)
        {
            return (await AverageCore(enumerable.Select(selector),0L, (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / counter).ConfigureAwait(false)).Value;
        }

        public static Task<float> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<float>> selector)
        {
            return AverageCore(enumerable.Select(selector), (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<float> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<float?>> selector)
        {
            return (await AverageCore(enumerable.Select(selector),0.0f, (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / counter).ConfigureAwait(false)).Value;
        }

        public static Task<double> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<double>> selector)
        {
            return AverageCore(enumerable.Select(selector), (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<double> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<double?>> selector)
        {
            return (await AverageCore(enumerable.Select(selector),0.0, (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / counter).ConfigureAwait(false)).Value;
        }

        public static Task<decimal> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<decimal>> selector)
        {
            return AverageCore(enumerable.Select(selector), (current, next) => current + next, (sum, counter) => sum / counter);
        }

        public static async Task<decimal> Average<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<decimal?>> selector)
        {
            return (await AverageCore(enumerable.Select(selector),0.0m, (current, next) => next.HasValue ? current + next.Value : current, (sum, counter) => sum / counter).ConfigureAwait(false)).Value;
        }

        private static async Task<T> AverageCore<T>(this IAsyncEnumerable<T> enumerable,Func<T, T, T> sumFunction, Func<T, long, T> averageFunction)
        {
            return await AverageCore<T>(enumerable, default(T), sumFunction, averageFunction).ConfigureAwait(false);
        }

        private static async Task<T> AverageCore<T>(this IAsyncEnumerable<T> enumerable,T seed,Func<T, T, T> sumFunction, Func<T, long, T> averageFunction)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (sumFunction == null) throw new ArgumentNullException("sumFunction");
            if (averageFunction == null) throw new ArgumentNullException("averageFunction");

            var counter = 0L;
            var accumulator = seed;

#pragma warning disable 1998
            await enumerable.ForEach(async value =>
            {
                accumulator = sumFunction(accumulator, value);
                counter++;
            }).ConfigureAwait(false);
#pragma warning restore 1998

            return averageFunction(accumulator, counter);
        }
    }
}