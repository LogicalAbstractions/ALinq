using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static Task<int> Min(this IAsyncEnumerable<int> enumerable)
        {
            return MinCore(enumerable, (current, next) => next < current);
        }

        public static async Task<int> Min(this IAsyncEnumerable<int?> enumerable)
        {
            return (await MinCore(enumerable, (current, next) => next.HasValue && next.Value < current).ConfigureAwait(false)).Value;
        }

        public static Task<long> Min(this IAsyncEnumerable<long> enumerable)
        {
            return MinCore(enumerable, (current, next) => next < current);
        }

        public static async Task<long> Min(this IAsyncEnumerable<long?> enumerable)
        {
            return (await MinCore(enumerable, (current, next) => next.HasValue && next.Value < current).ConfigureAwait(false)).Value;
        }

        public static Task<float> Min(this IAsyncEnumerable<float> enumerable)
        {
            return MinCore(enumerable, (current, next) => next < current);
        }

        public static async Task<float> Min(this IAsyncEnumerable<float?> enumerable)
        {
            return (await MinCore(enumerable, (current, next) => next.HasValue && next.Value < current).ConfigureAwait(false)).Value;
        }

        public static Task<double> Min(this IAsyncEnumerable<double> enumerable)
        {
            return MinCore(enumerable, (current, next) => next < current);
        }

        public static async Task<double> Min(this IAsyncEnumerable<double?> enumerable)
        {
            return (await MinCore(enumerable, (current, next) => next.HasValue && next.Value < current).ConfigureAwait(false)).Value;
        }

        public static Task<decimal> Min(this IAsyncEnumerable<decimal> enumerable)
        {
            return MinCore(enumerable, (current, next) => next < current);
        }

        public static async Task<decimal> Min(this IAsyncEnumerable<decimal?> enumerable)
        {
            return (await MinCore(enumerable, (current, next) => next.HasValue && next.Value < current).ConfigureAwait(false)).Value;
        }

        public static Task<int> Min<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<int>> selector)
        {
            return MinCore(enumerable.Select(selector), (current, next) => next < current);
        }

        public static async Task<int> Min<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<int?>> selector)
        {
            return (await MinCore(enumerable.Select(selector), (current, next) => next.HasValue && next.Value < current).ConfigureAwait(false)).Value;
        }

        public static Task<long> Min<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<long>> selector)
        {
            return MinCore(enumerable.Select(selector), (current, next) => next < current);
        }

        public static async Task<long> Min<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<long?>> selector)
        {
            return (await MinCore(enumerable.Select(selector), (current, next) => next.HasValue && next.Value < current).ConfigureAwait(false)).Value;
        }

        public static Task<float> Min<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<float>> selector)
        {
            return MinCore(enumerable.Select(selector), (current, next) => next < current);
        }

        public static async Task<float> Min<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<float?>> selector)
        {
            return (await MinCore(enumerable.Select(selector), (current, next) => next.HasValue && next.Value < current).ConfigureAwait(false)).Value;
        }

        public static Task<double> Min<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<double>> selector)
        {
            return MinCore(enumerable.Select(selector), (current, next) => next < current);
        }

        public static async Task<double> Min<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<double?>> selector)
        {
            return (await MinCore(enumerable.Select(selector), (current, next) => next.HasValue && next.Value < current).ConfigureAwait(false)).Value;
        }

        public static Task<decimal> Min<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<decimal>> selector)
        {
            return MinCore(enumerable.Select(selector), (current, next) => next < current);
        }

        public static async Task<decimal> Min<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task<decimal?>> selector)
        {
            return (await MinCore(enumerable.Select(selector), (current, next) => next.HasValue && next.Value < current).ConfigureAwait(false)).Value;
        }

        private static async Task<T> MinCore<T>(this IAsyncEnumerable<T> enumerable, Func<T, T, bool> comparerFunc)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (comparerFunc == null) throw new ArgumentNullException("comparerFunc");

            var setOnce     = false;
            var accumulator = default(T);

#pragma warning disable 1998
            await enumerable.ForEach(async value =>
            {
                if ( !setOnce )
                {
                    accumulator = value;
                    setOnce = true;
                }
                else
                {
                    if ( comparerFunc(accumulator,value))
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