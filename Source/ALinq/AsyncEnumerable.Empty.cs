namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> Empty<T>()
        {
#pragma warning disable 1998
            return new ConcurrentAsyncEnumerable<T>(async producer => { });
#pragma warning restore 1998
        }
    }
}