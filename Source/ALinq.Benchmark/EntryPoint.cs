using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ALinq.Benchmark
{
    public static class EntryPoint
    {
        public class Profiler : IDisposable
        {
            private readonly string     name;
            private readonly Stopwatch  stopWatch = new Stopwatch();

            public void Dispose()
            {
                stopWatch.Stop();
                Console.WriteLine("{0}: {1}",name,stopWatch.Elapsed);
            }

            public Profiler(string name)
            {
                this.name = name;
                stopWatch.Start();
            }
        }

        public class SortEntry : IEquatable<SortEntry>
        {
            public int Id1 { get; private set; }
            public int Id2 { get; private set; }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((SortEntry)obj);
            }

            public bool Equals(SortEntry other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Id1 == other.Id1 && Id2 == other.Id2;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (Id1 * 397) ^ Id2;
                }
            }

            public SortEntry(int id1, int id2)
            {
                this.Id1 = id1;
                this.Id2 = id2;
            }
        }

        public static int Main(string[] arguments)
        {
            Scenario03(100 * 1000 * 1000).Wait();
            Scenario04(100 * 1000 * 1000);

            
            Console.ReadKey();
            return 0;
        }

        private static async Task Scenario01(int size)
        {
            var random      = new Random();
            var data        = Enumerable.Range(0, size).Select(i => new SortEntry(random.Next(0, 100), random.Next(0, 100))).ToList();

#pragma warning disable 1998

            using (new Profiler("ALINQ Scenario01"))
            {
                var result =
                    await
                    data.ToAsync().OrderBy(i => Task.FromResult(i.Id1)).ThenBy(i => Task.FromResult(i.Id2)).ToList();
            }

#pragma warning restore 1998
        }

        private static void Scenario02(int size)
        {
            var random = new Random();
            var data = Enumerable.Range(0, size).Select(i => new SortEntry(random.Next(0, 100), random.Next(0, 100))).ToList();

            using (new Profiler("LINQ Scenario02"))
            {
                var orderedData = data.OrderBy(d => d.Id1).ThenBy(d => d.Id2).ToList();
            }
        }

        private static async Task Scenario03(int size)
        {
            using (new Profiler("ALINQ Scenario03"))
            {
                var data = await AsyncEnumerable.Range(0, size).ToList();
            }
        }

        private static void Scenario04(int size)
        {
            using (new Profiler("LINQ Scenario04"))
            {
                Enumerable.Range(0, size).ToList();
            }
        }
    }
}
