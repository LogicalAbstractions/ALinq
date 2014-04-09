using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<IList<T>> ToList<T>(this IAsyncEnumerable<T> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            var result = new List<T>();

#pragma warning disable 1998
            await enumerable.ForEach(async item => 
            {
                result.Add(item);
            }).ConfigureAwait(false);
#pragma warning restore 1998

            return result;
        }
    }
}