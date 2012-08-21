using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALinq.Tests
{
    [TestClass]
    public class AsyncEnumerableAggregateTests
    {
        [TestMethod]
        public async Task AggregateWithoutSeedShouldWork()
        {
#pragma warning disable 1998
            var aggregate = await AsyncEnumerable.Range(0, 10).Aggregate(async (current, next) => current + next);
#pragma warning restore 1998

            Assert.AreEqual(45,aggregate);
        }

        [TestMethod]
        public async Task AggregateWithSeedShouldWork()
        {
#pragma warning disable 1998
            var aggregate = await AsyncEnumerable.Range(0, 10).Aggregate(10,async (current, next) => current + next);
#pragma warning restore 1998

            Assert.AreEqual(55, aggregate);
        }

        [TestMethod]
        public async Task AggregateWithResultSelectorShouldWork()
        {
#pragma warning disable 1998
            var aggregate = await AsyncEnumerable.Range(0, 10).Aggregate(10, async (current, next) => current + next,async result => result * 2);
#pragma warning restore 1998

            Assert.AreEqual(110, aggregate);
        }
    }
}
