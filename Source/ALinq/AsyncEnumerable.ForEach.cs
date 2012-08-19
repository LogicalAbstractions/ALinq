using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static Task ForEach<T>(this IAsyncEnumerable<T> enumerable, Func<T, AsyncLoopState<T>, Task> enumerationAction)
        {
            return ForEach(enumerable, loopState => enumerationAction(loopState.Item, loopState));
        }

        public static Task ForEach<T>(this IAsyncEnumerable<T> enumerable, Func<T, long, AsyncLoopState<T>, Task> enumerationAction)
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

        public static async Task ForEach<T>(this IAsyncEnumerable<T> enumerable, Func<AsyncLoopState<T>, Task> enumerationAction)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (enumerationAction == null) throw new ArgumentNullException("enumerationAction");

            var index      = 0L;
            var doContinue = true;
            var enumerator = enumerable.GetEnumerator();
            var loopState  = new AsyncLoopState<T>();

            try
            {
                while (doContinue)
                {
                    doContinue = await enumerator.MoveNext();

                    if (doContinue)
                    {
                        loopState.Item = enumerator.Current;
                        loopState.Index = index;

                        await enumerationAction(loopState);

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