using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class ASyncEnumerableDefaultIfEmptyTests
    {
        [TestMethod]
        public async Task DefaultIfEmptyShouldReturnDefaultValueOnEmptySequence()
        {
            var sequence1 = await AsyncEnumerable.Empty<int>().DefaultIfEmpty().ToList();

            CollectionAssert.AreEqual(new [] { 0 },sequence1.ToList());

            var sequence2 = await AsyncEnumerable.Range(0, 2).DefaultIfEmpty().ToList();

            CollectionAssert.AreEqual(new [] { 0,1},sequence2.ToList());
        }
    }
}