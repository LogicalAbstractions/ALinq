using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALinq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<int> Range(int start,int end)
        {
            return Range(start, end, 1);
        }

        public static IAsyncEnumerable<int> Range(int start,int end,int step)
        {
            if ( start >= end )
            {
                throw new ArgumentOutOfRangeException("start", "start must be smaller than end");
            }

            if ( step < 1 )
            {
                throw new ArgumentOutOfRangeException("step", "step must be greater than zero");
            }

            return RangeCore(start, end, step).ToAsync();
        }

        private static IEnumerable<Task<int>> RangeCore(int start,int end,int step)
        {
            for( var i = start; i < end; i+= step)
            {
                yield return Task.FromResult(i);
            }
        }
    }
}