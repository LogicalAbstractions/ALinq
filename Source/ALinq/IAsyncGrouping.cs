namespace ALinq
{
    public interface IAsyncGrouping<out TKey,out TElement> : IAsyncEnumerable<TElement>
    {
        TKey Key { get; }
    }
}