using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableAllTests
    {
        [TestMethod]
        public async Task AllShouldWork()
        {
#pragma warning disable 1998
            Assert.IsFalse(await AsyncEnumerable.Range(0, 100).All(async i => i % 2 == 0));
#pragma warning restore 1998

#pragma warning disable 1998
            Assert.IsTrue(await AsyncEnumerable.Range(0,10).All(async i => i < 10));
#pragma warning restore 1998
        }
    }
}
