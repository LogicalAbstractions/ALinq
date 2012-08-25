using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableZipTests
    {
        [TestMethod]
        public async Task ZipShouldMergeElementsByIndex()
        {
            var sequence1 = AsyncEnumerable.Range(0, 10);
            var sequence2 = AsyncEnumerable.Range(0, 20);

#pragma warning disable 1998
            var zipped = sequence1.Zip(sequence2, async (a, b) => Tuple.Create(a, b));
#pragma warning restore 1998

            var evaluated = await zipped.ToList();

            Assert.AreEqual(10,evaluated.Count);

            var index = 0;
            foreach( var item in evaluated )
            {
                Assert.AreEqual(index,item.Item1);
                Assert.AreEqual(item.Item1,item.Item2);
                ++index;
            }
        }
    }
}