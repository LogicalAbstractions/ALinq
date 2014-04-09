using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static async Task<bool> SequenceEqual<T>(this IAsyncEnumerable<T> enumerable1, IAsyncEnumerable<T> enumerable2)
        {
            return await SequenceEqual<T>(enumerable1, enumerable2, EqualityComparer<T>.Default).ConfigureAwait(false);
        }

        public static async Task<bool> SequenceEqual<T>(this IAsyncEnumerable<T> enumerable1, IAsyncEnumerable<T> enumerable2, IEqualityComparer<T> comparer)
        {
            if (enumerable1 == null) throw new ArgumentNullException("enumerable1");
            if (enumerable2 == null) throw new ArgumentNullException("enumerable2");

            if (comparer == null) throw new ArgumentNullException("comparer");

            var enumerator1 = enumerable1.GetEnumerator();
            var enumerator2 = enumerable2.GetEnumerator();

            try
            {
                while (true)
                {
                    var move1 = await enumerator1.MoveNext().ConfigureAwait(false);
                    var move2 = await enumerator2.MoveNext().ConfigureAwait(false);

                    if (move1 && move2)
                    {
                        if (!comparer.Equals(enumerator1.Current, enumerator2.Current))
                        {
                            return false;
                        }
                    }
                    else if (move1 || move2)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            finally
            {
                enumerator1.Dispose();
                enumerator2.Dispose();
            }
        }
    }
}