using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<int> Range(int start,int count)
        {
            return Range(start, count, 1);
        }

        public static IAsyncEnumerable<int> Range(int start,int count,int step)
        {
            if ( start < 0 )
            {
                throw new ArgumentOutOfRangeException("start","start must be greater or equal to zero");
            }

            if ( count < 0 )
            {
                throw new ArgumentOutOfRangeException("count","count must be greater or equal to zero");
            }

            if ( step < 1 )
            {
                throw new ArgumentOutOfRangeException("step", "step must be greater than zero");
            }

            return RangeCore(start, count, step).ToAsync();
        }

        private static IEnumerable<Task<int>> RangeCore(int start,int count,int step)
        {
            for( var i = start; i < start + count; i+= step)
            {
                yield return Task.FromResult(i);
            }
        }
    }
}