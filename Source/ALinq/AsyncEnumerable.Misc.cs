using System;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
         public static IAsyncEnumerable<T> Create<T>(Func<ConcurrentAsyncProducer<T>,Task> producerFunc)
         {
             return new ConcurrentAsyncEnumerable<T>(producerFunc);
         }
    }
}