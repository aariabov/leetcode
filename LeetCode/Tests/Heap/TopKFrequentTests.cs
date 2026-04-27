namespace Tests.Heap;

// [K-ый наибольший элемент в массиве](https://leetcode.com/explore/learn/card/heap/646/practices/4014/)
public class TopKFrequentTests
{
    [Theory]
    [InlineData(new[] { 1, 1, 1, 2, 2, 3 }, 2, new[] { 1, 2 })]
    [InlineData(new[] { 1 }, 1, new[] { 1 })]
    [InlineData(new[] { 1, 2, 1, 2, 1, 2, 3, 1, 3, 2 }, 2, new[] { 1, 2 })]
    [InlineData(
        new[] { 3, 2, 3, 1, 2, 4, 5, 5, 6, 7, 7, 8, 2, 3, 1, 1, 1, 10, 11, 5, 6, 2, 4, 7, 8, 5, 6 },
        10,
        new[] { 1, 2, 5, 3, 6, 7, 4, 8, 10, 11 }
    )]
    public void Test(int[] nums, int k, int[] expected)
    {
        var result = TopKFrequent(nums, k);
        Array.Sort(expected);
        Array.Sort(result);
        Assert.Equal(expected, result);
    }

    public int[] TopKFrequent(int[] nums, int k)
    {
        var dict = nums.GroupBy(n => n).ToDictionary(n => n.Key, n => n.Count());
        var minHeap = new PriorityQueue<int, int>();
        foreach (var pair in dict)
        {
            minHeap.Enqueue(pair.Key, pair.Value);

            if (minHeap.Count > k)
            {
                minHeap.Dequeue();
            }
        }

        var res = new int[k];
        for (int i = 0; i < k; i++)
        {
            res[i] = minHeap.Dequeue();
        }
        return res;
    }
}
