using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<T> Create<T>(Func<ConcurrentAsyncProducer<T>,Task> producerFunc)
        {
            return new ConcurrentAsyncEnumerable<T>(producerFunc);
        }

        public static IAsyncEnumerable<T> Create<T>(T value)
        {
            return Create<T>(async producer =>
            {
                await producer.Yield(value);
            });
        }

        public static IAsyncEnumerable<T> Repeat<T>(T value,int count)
        {
            if ( count < 0 ) throw new ArgumentOutOfRangeException("count","count must be zero or greater");

            return Create<T>(async producer =>
            {
                for( var i = 0; i < count; ++i )
                {
                    await producer.Yield(value);
                }
            });
        }

        public static IAsyncEnumerable<T> Empty<T>()
        {
#pragma warning disable 1998
            return new ConcurrentAsyncEnumerable<T>(async producer => {});
#pragma warning restore 1998
        }

        public static IAsyncEnumerable<T> ToAsync<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            return AsyncEnumerable.Create<T>(async producer =>
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

        internal static void Dispose(this IAsyncEnumerator asyncEnumerator)
        {
            var disposable = asyncEnumerator as IDisposable;

            if ( disposable != null )
            {
                disposable.Dispose();
            }
        }
    }
}