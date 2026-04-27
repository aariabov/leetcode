namespace Tests.Heap;

// [Топ наиболее часто встречающихся элементов](https://leetcode.com/explore/learn/card/heap/646/practices/4015/)
public class FindKthLargestTests
{
    [Theory]
    [InlineData(new[] { 3, 2, 1, 5, 6, 4 }, 2, 5)]
    [InlineData(new[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 }, 4, 4)]
    public void Test(int[] nums, int k, int expected)
    {
        var res = FindKthLargest(nums, k);
        Assert.Equal(expected, res);
    }

    public int FindKthLargest(int[] nums, int k)
    {
        var minHeap = new PriorityQueue<int, int>();

        foreach (int num in nums)
        {
            // Добавляем число в кучу (значение и приоритет одинаковы)
            minHeap.Enqueue(num, num);

            // Если в куче больше k элементов, удаляем самый маленький
            if (minHeap.Count > k)
            {
                minHeap.Dequeue();
            }
        }

        // На вершине кучи останется k-й по величине элемент
        return minHeap.Peek();
    }

    // работает и быстро
    public int MyFindKthLargest(int[] nums, int k)
    {
        var heap = new PriorityQueue<int, int>();
        foreach (var num in nums)
        {
            heap.Enqueue(num, -num);
        }

        var res = 0;
        for (int i = 0; i < k; i++)
        {
            res = heap.Dequeue();
        }
        return res;
    }
}
