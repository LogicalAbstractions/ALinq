using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableCountTests
    {
        [TestMethod]
        public async Task CountShouldBeAccurate()
        {
            Assert.AreEqual(100,await AsyncEnumerable.Range(0,100).Count());
            Assert.AreEqual(100, await AsyncEnumerable.Range(0, 100).LongCount());
#pragma warning disable 1998
            Assert.AreEqual(50, await AsyncEnumerable.Range(0, 100).Count(async a => a % 2 == 0));
#pragma warning restore 1998
#pragma warning disable 1998
            Assert.AreEqual(50, await AsyncEnumerable.Range(0, 100).Count(async a => a % 2 == 0));
#pragma warning restore 1998
        }
    }
}
