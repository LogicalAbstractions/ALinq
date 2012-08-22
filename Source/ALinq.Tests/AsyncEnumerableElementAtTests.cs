using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableElementAtTests
    {
        [TestMethod]
        public async Task ElementAtShouldReturnElement()
        {
            var element = await AsyncEnumerable.Range(0, 100).ElementAt(50);

            Assert.AreEqual(50,element);

            try
            {
                element = await AsyncEnumerable.Range(0, 10).ElementAt(50);
                Assert.Fail("No exception thrown");
            }
            catch (InvalidOperationException)
            {
               
            }
        }

        [TestMethod]
        public async Task ElementAtOrDefaultShouldReturnElement()
        {
            var element = await AsyncEnumerable.Range(0, 100).ElementAtOrDefault(50);

            Assert.AreEqual(50, element);
            element = await AsyncEnumerable.Range(0, 10).ElementAtOrDefault(50);
            Assert.AreEqual(0,element);
        }
    }
}