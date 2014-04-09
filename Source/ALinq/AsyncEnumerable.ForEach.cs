using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static Task ForEach(this IAsyncEnumerable enumerable, Func<object, AsyncLoopContext<object>, Task> enumerationAction)
        {
            return ForEach(enumerable, loopState => enumerationAction(loopState.Item, loopState));
        }

        public static Task ForEach(this IAsyncEnumerable enumerable, Func<object, long, AsyncLoopContext<object>, Task> enumerationAction)
        {
            return ForEach(enumerable, loopState => enumerationAction(loopState.Item, loopState.Index, loopState));
        }

        public static Task ForEach(this IAsyncEnumerable enumerable, Func<object, long, Task> enumerationAction)
        {
            return ForEach(enumerable, loopState => enumerationAction(loopState.Item, loopState.Index));
        }

        public static Task ForEach(this IAsyncEnumerable enumerable, Func<object, Task> enumerationAction)
        {
            return ForEach(enumerable, loopState => enumerationAction(loopState.Item));
        }

        public static async Task ForEach(this IAsyncEnumerable enumerable, Func<AsyncLoopContext<object>, Task> enumerationAction)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (enumerationAction == null) throw new ArgumentNullException("enumerationAction");

            var index = 0L;
            var doContinue = true;
            var enumerator = enumerable.GetEnumerator();
            var loopState = new AsyncLoopContext<object>();

            try
            {
                while (doContinue)
                {
                    doContinue = await enumerator.MoveNext().ConfigureAwait(false);

                    if (doContinue)
                    {
                        loopState.Item  = enumerator.Current;
                        loopState.Index = index;

                        await enumerationAction(loopState).ConfigureAwait(false);

                        if (loopState.WasBreakCalled)
                        {
                            break;
                        }

                        index++;
                    }
                }
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        public static Task ForEach<T>(this IAsyncEnumerable<T> enumerable, Func<T, AsyncLoopContext<T>, Task> enumerationAction)
        {
            return ForEach(enumerable, loopState => enumerationAction(loopState.Item, loopState));
        }

        public static Task ForEach<T>(this IAsyncEnumerable<T> enumerable, Func<T, long, AsyncLoopContext<T>, Task> enumerationAction)
        {
            return ForEach(enumerable, loopState => enumerationAction(loopState.Item, loopState.Index, loopState));
        }

        public static Task ForEach<T>(this IAsyncEnumerable<T> enumerable, Func<T, long, Task> enumerationAction)
        {
            return ForEach(enumerable, loopState => enumerationAction(loopState.Item, loopState.Index));
        }

        public static Task ForEach<T>(this IAsyncEnumerable<T> enumerable, Func<T, Task> enumerationAction)
        {
            return ForEach(enumerable, loopState => enumerationAction(loopState.Item));
        }

        public static async Task ForEach<T>(this IAsyncEnumerable<T> enumerable, Func<AsyncLoopContext<T>, Task> enumerationAction)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (enumerationAction == null) throw new ArgumentNullException("enumerationAction");

            var index      = 0L;
            var doContinue = true;
            var enumerator = enumerable.GetEnumerator();
            var loopState  = new AsyncLoopContext<T>();

            try
            {
                while (doContinue)
                {
                    doContinue = await enumerator.MoveNext().ConfigureAwait(false);

                    if (doContinue)
                    {
                        loopState.Item = enumerator.Current;
                        loopState.Index = index;

                        await enumerationAction(loopState).ConfigureAwait(false);

                        if (loopState.WasBreakCalled)
                        {
                            break;
                        }

                        index++;
                    }
                }
            }
            finally
            {
                enumerator.Dispose();
            }
        }
    }
}