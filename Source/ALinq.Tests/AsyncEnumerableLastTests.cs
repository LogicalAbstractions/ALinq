using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableLastTests
    {
        [TestMethod]
        public async Task LastShouldReturnLastElement()
        {
            Assert.AreEqual(29,await AsyncEnumerable.Range(10,20).Last());
#pragma warning disable 1998
            Assert.AreEqual(19,await AsyncEnumerable.Range(0,20).Last(async i => i > 10 && i % 2 > 0));
#pragma warning restore 1998
            
            try
            {
#pragma warning disable 1998
                await AsyncEnumerable.Range(0, 1).Last(async i => i > 3);
#pragma warning restore 1998
                Assert.Fail("No exception thrown");
            }
            catch( InvalidOperationException)
            {
                
            }
        }

        [TestMethod]
        public async Task LastOrDefaultShouldReturnLastElement()
        {
            Assert.AreEqual(29, await AsyncEnumerable.Range(10, 20).LastOrDefault());
#pragma warning disable 1998
            Assert.AreEqual(19, await AsyncEnumerable.Range(0, 20).LastOrDefault(async i => i > 10 && i % 2 > 0));
#pragma warning restore 1998
          
#pragma warning disable 1998
            Assert.AreEqual(0,await AsyncEnumerable.Range(0, 1).LastOrDefault(async i => i > 3));
#pragma warning restore 1998  
        }
    }
}