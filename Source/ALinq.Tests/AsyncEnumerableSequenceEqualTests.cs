using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableSequenceEqualTests
    {
        [TestMethod]
        public async Task SequenceEqualShouldWork()
        {
            Assert.IsTrue(await AsyncEnumerable.Range(0,10).SequenceEqual(AsyncEnumerable.Range(0,10)));
            Assert.IsFalse(await AsyncEnumerable.Range(0, 10).SequenceEqual(AsyncEnumerable.Range(0, 11)));
        }
    }
}