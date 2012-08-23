using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableIntersectTests
    {
        [TestMethod]
        public async Task IntersectShouldWork()
        {
            var sequence1 = AsyncEnumerable.Range(0, 100);
            var sequence2 = AsyncEnumerable.Range(0, 50);

            var sequence3 = await sequence1.Intersect(sequence2).ToList();

            CollectionAssert.AreEqual(Enumerable.Range(0,50).ToList(),sequence3.ToList());
        }
    }
}