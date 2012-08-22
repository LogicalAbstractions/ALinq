using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableGroupByTests
    {
        [TestMethod]
        public async Task GroupByShouldWork()
        {
            var random  = new Random();
            var data    = Enumerable.Range(0, 1000).Select(i => random.Next(0, 20)).ToList();
            var groups  = data.GroupBy(i => i);

#pragma warning disable 1998
            var asyncGroups = await data.ToAsync().GroupBy(async i => i).ToList();
#pragma warning restore 1998

            Assert.AreEqual(groups.Count(),asyncGroups.Count());

            for( var i = 0; i < groups.Count(); ++i )
            {
                var group       = groups.ElementAt(i);
                var asyncGroup  = await asyncGroups.First(g => g.Key == group.Key).ToList();

                CollectionAssert.AreEqual((ICollection)group.ToList(),(ICollection)asyncGroup);
            }
        }
    }
}