using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableJoinTests
    {
        [TestMethod]
        public async Task JoinShouldWork()
        {
            var range1 = AsyncEnumerable.Range(0, 100);
            var range2 = AsyncEnumerable.Range(0, 200);

#pragma warning disable 1998
            var joinedSequence = await range1.Join(range2, async i => i, async i => i, async (a, b) => Tuple.Create(a, b)).ToList();
#pragma warning restore 1998

            foreach( var item in joinedSequence )
            {
                Assert.AreEqual(item.Item1,item.Item2);
            }

            Assert.AreEqual(100,joinedSequence.Count);
        }
    }
}