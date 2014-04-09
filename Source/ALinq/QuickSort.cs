using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALinq
{
    internal sealed class QuickSort<TElement>
    {
        private readonly TElement[]                 elements;
        private readonly int[]                      indexes;
        private readonly AsyncSortContext<TElement> context;

        internal static async Task<IEnumerable<TElement>> Sort(IEnumerable<TElement> source, AsyncSortContext<TElement> context)
        {
            var sorter = new QuickSort<TElement>(source, context);

            await sorter.PerformSort().ConfigureAwait(false);

            var result = new TElement[sorter.indexes.Length];

            for (var i = 0; i < sorter.indexes.Length; i++)
            {
                result[i] = sorter.elements[sorter.indexes[i]];
            }

            return result;
        }

        private async Task PerformSort()
        {
            if (elements.Length <= 1)
            {
                return;
            }

            await context.Initialize(elements).ConfigureAwait(false);

            Sort(0, indexes.Length - 1);
        }

        private int CompareItems(int firstIndex, int secondIndex)
        {
            return context.Compare(firstIndex, secondIndex);
        }

        private int MedianOfThree(int left, int right)
        {
            var center = (left + right) / 2;

            if (CompareItems(indexes[center], indexes[left]) < 0)
            {
                Swap(left, center);
            }

            if (CompareItems(indexes[right], indexes[left]) < 0)
            {
                Swap(left, right);
            }

            if (CompareItems(indexes[right], indexes[center]) < 0)
            {
                Swap(center, right);
            }

            Swap(center, right - 1);

            return indexes[right - 1];
        }

        private void Sort(int left, int right)
        {
            if (left + 3 <= right)
            {
                var l = left; 
                var r = right - 1; 
                var pivot = MedianOfThree(left, right);

                while (true)
                {
                    while (CompareItems(indexes[++l], pivot) < 0)
                    { }

                    while (CompareItems(indexes[--r], pivot) > 0)
                    { }

                    if (l < r)
                    {
                        Swap(l, r);
                    }
                    else
                    {
                        break;
                    }
                }

                Swap(l, right - 1);
                Sort(left, l - 1);
                Sort(l + 1, right);
            }
            else
            {
                InsertionSort(left, right);
            }
        }

        private void InsertionSort(int left, int right)
        {
            for (var i = left + 1; i <= right; i++)
            {
                int j, tmp = indexes[i];

                for (j = i; j > left && CompareItems(tmp, indexes[j - 1]) < 0; j--)
                {
                    indexes[j] = indexes[j - 1];
                }

                indexes[j] = tmp;
            }
        }

        private void Swap(int left, int right)
        {
            var temp = indexes[right];
            indexes[right] = indexes[left];
            indexes[left] = temp;
        }

        private static int[] CreateIndexes(int length)
        {
            var indexes = new int[length];

            for (var i = 0; i < length; i++)
            {
                indexes[i] = i;
            }

            return indexes;
        }

        private QuickSort(IEnumerable<TElement> source, AsyncSortContext<TElement> context)
        {
            this.elements = source.ToArray();
            this.indexes  = CreateIndexes(elements.Length);
            this.context  = context;
        }
    }
}