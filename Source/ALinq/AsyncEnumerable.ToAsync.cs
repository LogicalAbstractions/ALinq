using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> ToAsync<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            return Create<T>(async producer =>
            {
                foreach (var item in enumerable)
                {
                    await producer.Yield(item);
                }
            });
        }

        public static IAsyncEnumerable<T> ToAsync<T>(this IEnumerator<Task<T>> enumerator)
        {
            if (enumerator == null) throw new ArgumentNullException("enumerator");

            var wasRetrieved = false;

            return ToAsync(() =>
            {
                if (wasRetrieved)
                {
                    throw new InvalidOperationException("Sequence can only be evaluated once when created from raw enumerator");
                }

                wasRetrieved = true;
                return enumerator;
            });
        }

        public static IAsyncEnumerable<T> ToAsync<T>(this IEnumerable<Task<T>> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            return ToAsync(enumerable.GetEnumerator);
        }

        public static IAsyncEnumerable<T> ToAsync<T>(Func<IEnumerator<Task<T>>> enumeratorFactory)
        {
            if (enumeratorFactory == null) throw new ArgumentNullException("enumeratorFactory");

            return new AsyncEnumerableConverter<T>(enumeratorFactory);
        }

        public static IAsyncEnumerable<T> ToAsync<T>(this IObservable<T> observable)
        {
            if (observable == null) throw new ArgumentNullException("observable");

            return Create<T>(async producer => {
            
                using( var converter = new ConversionObserver<T>(producer))
                {
                    using (observable.Subscribe(converter))
                    {
                        await converter.WaitForCompletion();
                    }
                }
            });
        }
    }
}