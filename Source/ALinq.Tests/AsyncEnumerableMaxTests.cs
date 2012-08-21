using System;
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
            Assert.AreEqual(99,await AsyncEnumerable.Range(0,100).Max());
        }
    }
}
