using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableContainsTests
    {
        [TestMethod]
        public async Task ContainsShouldFindMatch()
        {
            var sequence = AsyncEnumerable.Range(0, 100);
            Assert.IsTrue(await sequence.Contains(50));
            Assert.IsFalse(await sequence.Contains(101));
        }
    }
}
