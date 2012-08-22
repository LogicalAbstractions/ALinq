using System.Globalization;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableSelectTests
    {
        [TestMethod]
        public async Task SelectShouldWork()
        {
#pragma warning disable 1998
            var sequence = AsyncEnumerable.Range(0, 10).Select(async i => i.ToString(CultureInfo.InvariantCulture)).Select(async s => int.Parse(s));
#pragma warning restore 1998

            Assert.AreEqual(45,await sequence.Sum());
        }
    }
}