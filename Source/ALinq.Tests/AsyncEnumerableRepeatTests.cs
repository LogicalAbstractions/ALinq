using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableRepeatTests
    {
        [TestMethod]
        public async Task RepeatShouldWork()
        {
            var evaluated = await AsyncEnumerable.Repeat(0, 10).ToList();

            CollectionAssert.AreEqual(Enumerable.Repeat(0,10).ToList(),evaluated.ToList());
        }
    }
}