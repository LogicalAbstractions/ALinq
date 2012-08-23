using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableUnionTests
    {
        [TestMethod]
        public async Task UnionShouldWork()
        {
            var sequence1 = AsyncEnumerable.Range(0, 100);
            var sequence2 = AsyncEnumerable.Range(0, 200);

            var sequence3 = Enumerable.Range(0, 200).ToList();
            var sequence4 = await sequence1.Union(sequence2).ToList();

            CollectionAssert.AreEqual(sequence3,sequence4.ToList());
        }
    }
}