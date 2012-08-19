namespace ALinq
{
    public interface IAsyncEnumerable
    {
        IAsyncEnumerator GetEnumerator();
    }

    public interface IAsyncEnumerable<out T> : IAsyncEnumerable
    {
        new IAsyncEnumerator<T> GetEnumerator();
    }
}