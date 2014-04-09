using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<TResult> Zip<TFirst,TSecond,TResult>(this IAsyncEnumerable<TFirst> enumerable1,IAsyncEnumerable<TSecond> enumerable2,Func<TFirst,TSecond,Task<TResult>> merger)
        {
            if (enumerable1 == null) throw new ArgumentNullException("enumerable1");
            if (enumerable2 == null) throw new ArgumentNullException("enumerable2");
            if (merger == null) throw new ArgumentNullException("merger");

            return Create<TResult>(async producer =>
            {
                var enumerator1 = enumerable1.GetEnumerator();
                var enumerator2 = enumerable2.GetEnumerator();
                var doContinue  = true;

                try
                {
                    while( doContinue )
                    {
                        if (await enumerator1.MoveNext().ConfigureAwait(false) && await enumerator2.MoveNext().ConfigureAwait(false))
                        {
                            await producer.Yield(await merger(enumerator1.Current, enumerator2.Current).ConfigureAwait(false)).ConfigureAwait(false);
                        }
                        else
                        {
                            doContinue = false;
                        }
                    }
                }
                finally
                {
                    enumerator1.Dispose();
                    enumerator2.Dispose();
                }
            });
        }
    }
}