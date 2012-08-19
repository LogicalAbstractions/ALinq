using System.Threading.Tasks;

namespace ALinq
{
    public interface IAsyncEnumerator
    {
        object      Current { get; }
        Task<bool>  MoveNext();
    }

    public interface IAsyncEnumerator<out T> : IAsyncEnumerator
    {
        new T Current { get; }
    }
}