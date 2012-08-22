using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableAnyTests
    {
        [TestMethod]
        public async Task AnyShouldWork()
        {
            var sequence = AsyncEnumerable.Range(0, 100);

#pragma warning disable 1998
            Assert.IsTrue(await sequence.Any(async i => i == 50));
#pragma warning restore 1998
#pragma warning disable 1998
            Assert.IsFalse(await sequence.Any(async i => i == 101));
#pragma warning restore 1998
        }
    }
}