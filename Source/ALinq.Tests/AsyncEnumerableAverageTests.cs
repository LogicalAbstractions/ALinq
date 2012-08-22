using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableAverageTests
    {
        [TestMethod]
        public async Task AverageShouldWork()
        {
            Assert.AreEqual(2,await AsyncEnumerable.Range(0,5).Average());
        }
    }
}