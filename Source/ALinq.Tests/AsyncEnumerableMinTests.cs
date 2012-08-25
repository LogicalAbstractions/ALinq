using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableMinTests
    {
        [TestMethod]
        public async Task MinShouldWork()
        {
            Assert.AreEqual(5, await GetRange(5, 10, i => i).Min());
            Assert.AreEqual(5L, await GetRange(5, 10, i => (long)i).Min());
            Assert.AreEqual(5.0f, await GetRange(5, 10, i => (float)i).Min());
            Assert.AreEqual(5.0, await GetRange(5, 10, i => (double)i).Min());
            Assert.AreEqual(5.0m, await GetRange(5, 10, i => (decimal)i).Min());

            Assert.AreEqual(5, await GetRange(5, 10, i => (int?)i).Min());
            Assert.AreEqual(5L, await GetRange(5, 10, i => (long?)i).Min());
            Assert.AreEqual(5.0f, await GetRange(5, 10, i => (float?)i).Min());
            Assert.AreEqual(5.0, await GetRange(5, 10, i => (double?)i).Min());
            Assert.AreEqual(5.0m, await GetRange(5, 10, i => (decimal?)i).Min());

            Assert.AreEqual(5, await GetRange(5, 10, i => i).Select(async i => Tuple.Create(i)).Min(async i => i.Item1));
            Assert.AreEqual(5L, await GetRange(5, 10, i => (long)i).Select(async i => Tuple.Create(i)).Min(async i => i.Item1));
            Assert.AreEqual(5.0f, await GetRange(5, 10, i => (float)i).Select(async i => Tuple.Create(i)).Min(async i => i.Item1));
            Assert.AreEqual(5.0, await GetRange(5, 10, i => (double)i).Select(async i => Tuple.Create(i)).Min(async i => i.Item1));
            Assert.AreEqual(5.0m, await GetRange(5, 10, i => (decimal)i).Select(async i => Tuple.Create(i)).Min(async i => i.Item1));

            Assert.AreEqual(5, await GetRange(5, 10, i => (int?)i).Select(async i => Tuple.Create(i)).Min(async i => i.Item1));
            Assert.AreEqual(5L, await GetRange(5, 10, i => (long?)i).Select(async i => Tuple.Create(i)).Min(async i => i.Item1));
            Assert.AreEqual(5.0f, await GetRange(5, 10, i => (float?)i).Select(async i => Tuple.Create(i)).Min(async i => i.Item1));
            Assert.AreEqual(5.0, await GetRange(5, 10, i => (double?)i).Select(async i => Tuple.Create(i)).Min(async i => i.Item1));
            Assert.AreEqual(5.0m, await GetRange(5, 10, i => (decimal?)i).Select(async i => Tuple.Create(i)).Min(async i => i.Item1));
        }

        private static IAsyncEnumerable<T> GetRange<T>(int start, int count, Func<int, T> converter)
        {
            return Enumerable.Range(start, count).Select(converter).ToList().ToAsync();
        }
    }
}
