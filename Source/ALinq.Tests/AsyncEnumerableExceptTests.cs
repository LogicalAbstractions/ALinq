using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableExceptTests
    {
        [TestMethod]
        public async Task ExceptShouldFilterOutItemsFromSecondSequence()
        {
            var sequence1 = AsyncEnumerable.Range(0, 10);
            var sequence2 = AsyncEnumerable.Range(5, 10);

            Assert.AreEqual(10,await sequence1.Except(sequence2).Sum());
        }
    }
}