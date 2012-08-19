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
            await enumerable.ForEach(async item => result.Add(item));
#pragma warning restore 1998

            return result;
        }

        public static async Task<T[]> ToArray<T>(this IAsyncEnumerable<T> enumerable )
        {
            var list = await ToList(enumerable);
            return list.ToArray();
        }

        public static async Task<T> First<T>(this IAsyncEnumerable<T> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            var enumerator = enumerable.GetEnumerator();

            try
            {
                if ( await enumerator.MoveNext())
                {
                    return enumerator.Current;
                }
            }
            finally 
            {
                enumerator.Dispose();  
            }

            throw new InvalidOperationException("Sequence contains no elements");
        }

        public static async Task<T> FirstOrDefault<T>(this IAsyncEnumerable<T> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            var enumerator = enumerable.GetEnumerator();

            try
            {
                if (await enumerator.MoveNext())
                {
                    return enumerator.Current;
                }
            }
            finally
            {
                enumerator.Dispose();
            }

            return default(T);
        }
    }
}