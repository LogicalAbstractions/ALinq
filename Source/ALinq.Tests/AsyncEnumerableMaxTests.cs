using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableMaxTests
    {
        [TestMethod]
        public async Task MaxShouldWork()
        {
            Assert.AreEqual(9, await GetRange(5, 5, i => i).Max());
            Assert.AreEqual(9L, await GetRange(5, 5, i => (long)i).Max());
            Assert.AreEqual(9.0f, await GetRange(5, 5, i => (float)i).Max());
            Assert.AreEqual(9.0, await GetRange(5, 5, i => (double)i).Max());
            Assert.AreEqual(9.0m, await GetRange(5, 5, i => (decimal)i).Max());

            Assert.AreEqual(9, await GetRange(5, 5, i => (int?)i).Max());
            Assert.AreEqual(9L, await GetRange(5, 5, i => (long?)i).Max());
            Assert.AreEqual(9.0f, await GetRange(5, 5, i => (float?)i).Max());
            Assert.AreEqual(9.0, await GetRange(5, 5, i => (double?)i).Max());
            Assert.AreEqual(9.0m, await GetRange(5, 5, i => (decimal?)i).Max());
        }

        private static IAsyncEnumerable<T> GetRange<T>(int start, int count, Func<int, T> converter)
        {
            return Enumerable.Range(start, count).Select(converter).ToList().ToAsync();
        }
    }
}
