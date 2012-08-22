using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableSingleTests
    {
        [TestMethod]
        public async Task SingleShouldReturnElement()
        {
            var sequence = new[] {10}.ToAsync();
            Assert.AreEqual(10,await sequence.Single());

            var sequence2 = new int[] {}.ToAsync();
            try
            {
                await sequence2.Single();
                Assert.Fail("No exception thrown");
            }
            catch (InvalidOperationException)
            {
            }

            var sequence3 = new int[] {10,11 }.ToAsync();
            try
            {
                await sequence3.Single();
                Assert.Fail("No exception thrown");
            }
            catch (InvalidOperationException)
            {  
            }
        }

        [TestMethod]
        public async Task SingleOrDefaultShouldReturnElement()
        {
            var sequence = new[] { 10 }.ToAsync();
            Assert.AreEqual(10, await sequence.SingleOrDefault());

            var sequence2 = new int[] { }.ToAsync();
            Assert.AreEqual(0,await sequence2.SingleOrDefault());
            
            var sequence3 = new int[] { 10, 11 }.ToAsync();
            Assert.AreEqual(0,await sequence3.SingleOrDefault());
        }
    }
}