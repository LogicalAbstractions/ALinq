using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableConcatTests
    {
        [TestMethod]
        public async Task ConcatShouldAppendSequences()
        {
            var sequence1 = AsyncEnumerable.Range(0, 9);
            var sequence2 = AsyncEnumerable.Range(9, 20);

            var sequence3 = sequence1.Concat(sequence2);
            var result = await sequence3.ToList();

            CollectionAssert.AreEqual((ICollection)Enumerable.Range(0,20).ToList(),(ICollection)result);
        }
    }
}
