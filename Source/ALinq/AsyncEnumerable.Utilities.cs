using System;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        internal static void Dispose(this IAsyncEnumerator asyncEnumerator)
        {
            var disposable = asyncEnumerator as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}