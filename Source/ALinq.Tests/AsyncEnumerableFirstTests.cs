using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableFirstTests
    {
        [TestMethod]
        public async Task FirstShouldReturnFirstElement()
        {
            Assert.AreEqual(10,await AsyncEnumerable.Range(10,20).First());
#pragma warning disable 1998
            Assert.AreEqual(11,await AsyncEnumerable.Range(0,20).First(async i => i > 10 && i % 2 > 0));
#pragma warning restore 1998
            
            try
            {
#pragma warning disable 1998
                await AsyncEnumerable.Range(0, 1).First(async i => i > 3);
#pragma warning restore 1998
                Assert.Fail("No exception thrown");
            }
            catch( InvalidOperationException )
            {
                
            }
        }

        [TestMethod]
        public async Task FirstOrDefaultShouldReturnFirstElement()
        {
            Assert.AreEqual(10, await AsyncEnumerable.Range(10, 20).FirstOrDefault());
#pragma warning disable 1998
            Assert.AreEqual(11, await AsyncEnumerable.Range(0, 20).FirstOrDefault(async i => i > 10 && i % 2 > 0));
#pragma warning restore 1998
          
#pragma warning disable 1998
            Assert.AreEqual(0,await AsyncEnumerable.Range(0, 1).FirstOrDefault(async i => i > 3));
#pragma warning restore 1998  
        }
    }
}