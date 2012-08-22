using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableOfTypeTests
    {
        [TestMethod]
        public async Task OfTypeShouldReturnElementsOfType()
        {
#pragma warning disable 1998
            var sequence = AsyncEnumerable.Range(0, 100).Cast<object>().Concat(AsyncEnumerable.Range(0, 100).Select(async i => i.ToString()));
#pragma warning restore 1998

            Assert.AreEqual(100,await sequence.OfType<string>().Count());
        }
    }
}