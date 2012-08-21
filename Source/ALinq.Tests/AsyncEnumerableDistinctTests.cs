using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableDistinctTests
    {
        [TestMethod]
        public async Task DistinctShouldFilterOutDuplicates()
        {
            var sequence = AsyncEnumerable.Repeat(10, 10);
            Assert.AreEqual(1,await sequence.Distinct().Count());
        }
    }
}
