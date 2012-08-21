using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableTakeTests
    {
        [TestMethod]
        public async Task TakeShouldReturnElements()
        {
            var sequence = AsyncEnumerable.Range(0, 100).Take(10);
            Assert.AreEqual(45,await sequence.Sum());
        }
    }
}