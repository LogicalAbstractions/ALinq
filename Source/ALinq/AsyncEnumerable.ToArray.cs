using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<T[]> ToArray<T>(this IAsyncEnumerable<T> enumerable )
        {
            var list = await ToList(enumerable).ConfigureAwait(false);
            return list.ToArray();
        }
    }
}