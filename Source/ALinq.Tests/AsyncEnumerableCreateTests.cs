using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableCreateTests
    {
        [TestMethod]
        public async Task SingleValueCreateShouldWork()
        {
            var evaluated = await AsyncEnumerable.Create(0).ToList();

            CollectionAssert.AreEqual(new[] { 0},evaluated.ToList());
        }
    }
}