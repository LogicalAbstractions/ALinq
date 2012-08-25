using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableGroupJoinTests
    {
        [TestMethod]
        public async Task GroupJoinShouldReduceValues()
        {
#pragma warning disable 1998
            var sequence1 = AsyncEnumerable.Range(0, 10).Select(async i => Tuple.Create(i, i));
#pragma warning restore 1998
#pragma warning disable 1998
            var sequence2 = AsyncEnumerable.Range(0, 10).Select(async i => Tuple.Create(i,AsyncEnumerable.Range(i * 10,10)));
#pragma warning restore 1998

#pragma warning disable 1998
            var results = await sequence1.GroupJoin(sequence2, async i => i.Item1, async i => i.Item1, async (outer, group) =>
#pragma warning restore 1998
            {
#pragma warning disable 1998
                return Tuple.Create(outer.Item1, await group.SelectMany(async i => i.Item2).ToList());
#pragma warning restore 1998
            }).ToList();

            Assert.AreEqual(10,results.Count);

            foreach( var group in results )
            {
                CollectionAssert.AreEqual(Enumerable.Range(group.Item1 * 10,10).ToList(),group.Item2.ToList());
            }
        }
    }
}