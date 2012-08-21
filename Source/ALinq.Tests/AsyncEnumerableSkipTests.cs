using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableSkipTests
    {
        [TestMethod]
        public async Task SkipShouldSkipElements()
        {
            var sequence = AsyncEnumerable.Range(0, 100).Skip(99);
            Assert.AreEqual(99,await sequence.Sum());
        }
    }
}