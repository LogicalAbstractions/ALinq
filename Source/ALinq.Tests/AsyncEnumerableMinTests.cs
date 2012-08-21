using System;
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
            Assert.AreEqual(0,await AsyncEnumerable.Range(0,99).Min());
        }
    }
}
