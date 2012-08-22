using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableCastTests
    {
        [TestMethod]
        public async Task CastShouldTransformElements()
        {
#pragma warning disable 1998
            var sequence = AsyncEnumerable.Range(0, 100).Cast<object>().Cast<int>();
#pragma warning restore 1998

            Assert.AreEqual(100,await sequence.Count());
        }

        [TestMethod]
        public async Task CastShouldThrowInvalidCastExceptionForOtherTypesContained()
        {
#pragma warning disable 1998
            var sequence = AsyncEnumerable.Range(0, 100).Cast<object>().Concat(AsyncEnumerable.Range(0, 100).Select(async i => i.ToString()));
#pragma warning restore 1998

            try
            {
                await sequence.Cast<string>().ToList();
                Assert.Fail("No exception thrown");
            }
            catch (AggregateException ex)
            {
               Assert.IsInstanceOfType(ex.InnerException,typeof(InvalidCastException));
            }
        }
    }
}