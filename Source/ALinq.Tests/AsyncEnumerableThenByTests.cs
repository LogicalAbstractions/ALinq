using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableThenByTests
    {
        public class SortEntry : IEquatable<SortEntry>
        {
            public int Id1 { get; private set; }
            public int Id2 { get; private set; }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((SortEntry) obj);
            }

            public bool Equals(SortEntry other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Id1 == other.Id1 && Id2 == other.Id2;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (Id1*397) ^ Id2;
                }
            }

            public SortEntry(int id1,int id2)
            {
                this.Id1 = id1;
                this.Id2 = id2;
            }
        }

        [TestMethod]
        public async Task ThenByShouldWork()
        {
            var random      = new Random();
            var data        = Enumerable.Range(0,1000).Select(i => new SortEntry(random.Next(0,10),random.Next(0,10))).ToList();
            var orderedData = data.OrderBy(d => d.Id1).ThenBy(d => d.Id2).ToList();

#pragma warning disable 1998
            var result = await data.ToAsync().OrderBy(async i => i.Id1).ThenBy(async i => i.Id2).ToList();
#pragma warning restore 1998

            CollectionAssert.AreEqual((ICollection)orderedData.ToList(),(ICollection)result);

#pragma warning disable 1998
            var result2 = await data.ToAsync().OrderByDescending(async i => i.Id1).ThenByDescending(async i => i.Id2).ToList();
#pragma warning restore 1998

            var reverseOrderedData = ((IEnumerable<SortEntry>) orderedData).Reverse().ToList();

            CollectionAssert.AreEqual((ICollection)reverseOrderedData, (ICollection)result2);
        }

        [TestMethod]
        public async Task ThenByPerformanceComparison()
        {
            var random      = new Random();
            var data        = Enumerable.Range(0, 100000).Select(i => new SortEntry(random.Next(0, 100), random.Next(0, 100))).ToList();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var orderedData = data.OrderBy(d => d.Id1).ThenBy(d => d.Id2).ToList();
            Console.WriteLine("LINQ: {0}",stopWatch.Elapsed);


#pragma warning disable 1998
            stopWatch.Restart();
            var result = await data.ToAsync().OrderBy(async i => i.Id1).ThenBy(async i => i.Id2).ToList();
            Console.WriteLine("ALINQ: {0}",stopWatch.Elapsed);
#pragma warning restore 1998
        }
    }
}