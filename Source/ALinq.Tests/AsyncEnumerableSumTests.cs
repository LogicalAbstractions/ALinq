using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableSumTests
    {
        [TestMethod]
        public async Task SumShouldWork()
        {
            Assert.AreEqual(45,await AsyncEnumerable.Range(0,10).Sum());
        }
    }
}
