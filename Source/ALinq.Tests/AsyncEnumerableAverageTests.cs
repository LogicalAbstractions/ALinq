using System;
using System.Linq;
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
            Assert.AreEqual(2, await GetRange(0, 5, i => i).Average());
            Assert.AreEqual(2L, await GetRange(0, 5, i => (long)i).Average());
            Assert.AreEqual(2.0f, await GetRange(0, 5, i => (float)i).Average());
            Assert.AreEqual(2.0, await GetRange(0, 5, i => (double)i).Average());
            Assert.AreEqual(2.0m, await GetRange(0, 5, i => (decimal)i).Average());

            Assert.AreEqual(2, await GetRange(0, 5, i => (int?)i).Average());
            Assert.AreEqual(2L, await GetRange(0, 5, i => (long?)i).Average());
            Assert.AreEqual(2.0f, await GetRange(0, 5, i => (float?)i).Average());
            Assert.AreEqual(2.0, await GetRange(0, 5, i => (double?)i).Average());
            Assert.AreEqual(2.0m, await GetRange(0, 5, i => (decimal?)i).Average());
        }

        private static IAsyncEnumerable<T> GetRange<T>(int start, int count, Func<int, T> converter)
        {
            return Enumerable.Range(start, count).Select(converter).ToList().ToAsync();
        }
    }
}