using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static Task<int> Sum(this IAsyncEnumerable<int> enumerable)
        {
            return SumCore(enumerable, (current, next) => current + next);
        }

        public static async Task<int> Sum(this IAsyncEnumerable<int?> enumerable)
        {
            return (await SumCore(enumerable, 0, (current, next) => next.HasValue ? current + next.Value : current).ConfigureAwait(false)).Value;
        }

        public static Task<long> Sum(this IAsyncEnumerable<long> enumerable)
        {
            return SumCore(enumerable, (current, next) => current + next);
        }

        public static async Task<long> Sum(this IAsyncEnumerable<long?> enumerable)
        {
            return (await SumCore(enumerable, 0L, (current, next) => next.HasValue ? current + next.Value : current).ConfigureAwait(false)).Value;
        }

        public static Task<float> Sum(this IAsyncEnumerable<float> enumerable)
        {
            return SumCore(enumerable, (current, next) => current + next);
        }

        public static async Task<float> Sum(this IAsyncEnumerable<float?> enumerable)
        {
            return (await SumCore(enumerable, 0.0f, (current, next) => next.HasValue ? current + next.Value : current).ConfigureAwait(false)).Value;
        }

        public static Task<double> Sum(this IAsyncEnumerable<double> enumerable)
        {
            return SumCore(enumerable, (current, next) => current + next);
        }

        public static async Task<double> Sum(this IAsyncEnumerable<double?> enumerable)
        {
            return (await SumCore(enumerable, 0.0, (current, next) => next.HasValue ? current + next.Value : current).ConfigureAwait(false)).Value;
        }

        public static Task<decimal> Sum(this IAsyncEnumerable<decimal> enumerable)
        {
            return SumCore(enumerable, (current, next) => current + next);
        }

        public static async Task<decimal> Sum(this IAsyncEnumerable<decimal?> enumerable)
        {
            return (await SumCore(enumerable, 0.0m, (current, next) => next.HasValue ? current + next.Value : current).ConfigureAwait(false)).Value;
        }

        public static Task<int> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<int>> selector)
        {
            return SumCore(enumerable.Select(selector), (current, next) => current + next);
        }

        public static async Task<int> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<int?>> selector)
        {
            return (await SumCore(enumerable.Select(selector), 0, (current, next) => next.HasValue ? current + next.Value : current).ConfigureAwait(false)).Value;
        }

        public static Task<long> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<long>> selector)
        {
            return SumCore(enumerable.Select(selector), (current, next) => current + next);
        }

        public static async Task<long> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<long?>> selector)
        {
            return (await SumCore(enumerable.Select(selector), 0L, (current, next) => next.HasValue ? current + next.Value : current).ConfigureAwait(false)).Value;
        }

        public static Task<float> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<float>> selector)
        {
            return SumCore(enumerable.Select(selector), (current, next) => current + next);
        }

        public static async Task<float> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<float?>> selector)
        {
            return (await SumCore(enumerable.Select(selector), 0.0f, (current, next) => next.HasValue ? current + next.Value : current).ConfigureAwait(false)).Value;
        }

        public static Task<double> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<double>> selector)
        {
            return SumCore(enumerable.Select(selector), (current, next) => current + next);
        }

        public static async Task<double> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<double?>> selector)
        {
            return (await SumCore(enumerable.Select(selector), 0.0, (current, next) => next.HasValue ? current + next.Value : current).ConfigureAwait(false)).Value;
        }

        public static Task<decimal> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<decimal>> selector)
        {
            return SumCore(enumerable.Select(selector), (current, next) => current + next);
        }

        public static async Task<decimal> Sum<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<decimal?>> selector)
        {
            return (await SumCore(enumerable.Select(selector), 0.0m, (current, next) => next.HasValue ? current + next.Value : current).ConfigureAwait(false)).Value;
        }

        private static Task<T> SumCore<T>(this IAsyncEnumerable<T> enumerable,Func<T, T, T> sumFunction)
        {
            return SumCore<T>(enumerable, default(T), sumFunction);
        }

        private static Task<T> SumCore<T>(this IAsyncEnumerable<T> enumerable, T seed,Func<T, T, T> sumFunction)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (sumFunction == null) throw new ArgumentNullException("sumFunction");

#pragma warning disable 1998
            return Aggregate(enumerable,seed,async (a, b) => sumFunction(a, b));
#pragma warning restore 1998
        }
    }
}