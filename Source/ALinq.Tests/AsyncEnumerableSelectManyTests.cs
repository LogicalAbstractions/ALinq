using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableSelectManyTests
    {
        public class Entry
        {
            private readonly int start;

            public IAsyncEnumerable<int> Member
            {
                get { return AsyncEnumerable.Range(start, start + 10); }
            }

            public Entry(int start)
            {
                this.start = start;
            }
        }

        [TestMethod]
        public async Task SelectManyShouldWork()
        {
#pragma warning disable 1998
            var sequence = await AsyncEnumerable.Range(0, 10).Select(async i => new Entry(i * 10)).SelectMany(async i => i.Member).ToList();
#pragma warning restore 1998
            
            CollectionAssert.AreEqual(Enumerable.Range(0,100).ToList(),sequence.ToList());
        }
    }
}