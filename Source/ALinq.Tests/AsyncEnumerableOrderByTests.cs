using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableOrderByTests
    {
        [TestMethod]
        public async Task OrderShouldWork()
        {
            var data        = new[] {4, 2, 9, 22, 54, 11, 7, 16};
            var orderedData = data.OrderBy(i => i);

#pragma warning disable 1998
            var result = await data.ToAsync().OrderBy(async i => i).ToList();
#pragma warning restore 1998

            CollectionAssert.AreEqual((ICollection)orderedData.ToList(),(ICollection)result);

#pragma warning disable 1998
            var result2 = await data.ToAsync().OrderByDescending(async i => i).ToList();
#pragma warning restore 1998

            CollectionAssert.AreEqual((ICollection)orderedData.Reverse().ToList(), (ICollection)result2);
        }
    }
}