using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableForEachTests
    {
        [TestMethod]
        public async Task UntypedForEachShouldWork()
        {
            var sequence = AsyncEnumerable.Range(0, 10) as IAsyncEnumerable;

            var counter = 0;

            await sequence.ForEach(async (object value) =>
            {
                counter += (int) value;
            });

            Assert.AreEqual(45,counter);
        }
    }
}