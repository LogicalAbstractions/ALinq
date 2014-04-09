using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable 
    {
        public static Task<int> Max(this IAsyncEnumerable<int> enumerable)
        {
            return MaxCore(enumerable, (current, next) => next > current);
        }

        public static async Task<int> Max(this IAsyncEnumerable<int?> enumerable)
        {
            return (await MaxCore(enumerable, (current, next) => next.HasValue && next.Value > current).ConfigureAwait(false)).Value;
        }

        public static Task<long> Max(this IAsyncEnumerable<long> enumerable)
        {
            return MaxCore(enumerable, (current, next) => next > current);
        }

        public static async Task<long> Max(this IAsyncEnumerable<long?> enumerable)
        {
            return (await MaxCore(enumerable, (current, next) => next.HasValue && next.Value > current).ConfigureAwait(false)).Value;
        }

        public static Task<float> Max(this IAsyncEnumerable<float> enumerable)
        {
            return MaxCore(enumerable, (current, next) => next > current);
        }

        public static async Task<float> Max(this IAsyncEnumerable<float?> enumerable)
        {
            return (await MaxCore(enumerable, (current, next) => next.HasValue && next.Value > current).ConfigureAwait(false)).Value;
        }

        public static Task<double> Max(this IAsyncEnumerable<double> enumerable)
        {
            return MaxCore(enumerable, (current, next) => next > current);
        }

        public static async Task<double> Max(this IAsyncEnumerable<double?> enumerable)
        {
            return (await MaxCore(enumerable, (current, next) => next.HasValue && next.Value > current).ConfigureAwait(false)).Value;
        }

        public static Task<decimal> Max(this IAsyncEnumerable<decimal> enumerable)
        {
            return MaxCore(enumerable, (current, next) => next > current);
        }

        public static async Task<decimal> Max(this IAsyncEnumerable<decimal?> enumerable)
        {
            return (await MaxCore(enumerable, (current, next) => next.HasValue && next.Value > current).ConfigureAwait(false)).Value;
        }

        public static Task<int> Max<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<int>> selector)
        {
            return MaxCore(enumerable.Select(selector), (current, next) => next > current);
        }

        public static async Task<int> Max<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<int?>> selector)
        {
            return (await MaxCore(enumerable.Select(selector), (current, next) => next.HasValue && next.Value > current).ConfigureAwait(false)).Value;
        }

        public static Task<long> Max<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<long>> selector)
        {
            return MaxCore(enumerable.Select(selector), (current, next) => next > current);
        }

        public static async Task<long> Max<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<long?>> selector)
        {
            return (await MaxCore(enumerable.Select(selector), (current, next) => next.HasValue && next.Value > current).ConfigureAwait(false)).Value;
        }

        public static Task<float> Max<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<float>> selector)
        {
            return MaxCore(enumerable.Select(selector), (current, next) => next > current);
        }

        public static async Task<float> Max<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<float?>> selector)
        {
            return (await MaxCore(enumerable.Select(selector), (current, next) => next.HasValue && next.Value > current).ConfigureAwait(false)).Value;
        }

        public static Task<double> Max<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<double>> selector)
        {
            return MaxCore(enumerable.Select(selector), (current, next) => next > current);
        }

        public static async Task<double> Max<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<double?>> selector)
        {
            return (await MaxCore(enumerable.Select(selector), (current, next) => next.HasValue && next.Value > current).ConfigureAwait(false)).Value;
        }

        public static Task<decimal> Max<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<decimal>> selector)
        {
            return MaxCore(enumerable.Select(selector), (current, next) => next > current);
        }

        public static async Task<decimal> Max<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<decimal?>> selector)
        {
            return (await MaxCore(enumerable.Select(selector), (current, next) => next.HasValue && next.Value > current).ConfigureAwait(false)).Value;
        }

        private static async Task<T> MaxCore<T>(this IAsyncEnumerable<T> enumerable, Func<T, T, bool> comparerFunc)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (comparerFunc == null) throw new ArgumentNullException("comparerFunc");

            var setOnce = false;
            var accumulator = default(T);

#pragma warning disable 1998
            await enumerable.ForEach(async value =>
            {
                if (!setOnce)
                {
                    accumulator = value;
                    setOnce = true;
                }
                else
                {
                    if (comparerFunc(accumulator, value))
                    {
                        accumulator = value;
                    }
                }
            }).ConfigureAwait(false);
#pragma warning restore 1998

            return accumulator;
        }
    }
}