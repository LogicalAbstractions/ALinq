using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableEmptyTests
    {
        [TestMethod]
        public async Task EmptyShouldBeEmpty()
        {
            Assert.AreEqual(0,await AsyncEnumerable.Empty<int>().Count());
        }
    }
}