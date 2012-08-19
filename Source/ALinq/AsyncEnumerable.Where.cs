using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> Where<T>(this IAsyncEnumerable<T> enumerable,Func<T,Task<bool>> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (predicate == null) throw new ArgumentNullException("predicate");

            return Create<T>(async producer =>
            {
                await enumerable.ForEach(async item =>
                {
                    if ( await predicate(item))
                    {
                        await producer.Yield(item);
                    }
                });
            });
        }
    }
}