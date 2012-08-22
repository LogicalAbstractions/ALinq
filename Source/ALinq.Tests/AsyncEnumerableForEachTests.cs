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

#pragma warning disable 1998
            await sequence.ForEach(async (object value) =>
#pragma warning restore 1998
            {
                counter += (int) value;
            });

            Assert.AreEqual(45,counter);
        }
    }
}