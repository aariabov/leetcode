namespace Tests.Heap;

// [Вес последнего камня](https://leetcode.com/explore/learn/card/heap/646/practices/4084/)
public class LastStoneWeightTests
{
    [Theory]
    [InlineData(new int[] { 2, 7, 4, 1, 8, 1 }, 1)]
    [InlineData(new int[] { 1 }, 1)]
    [InlineData(new int[] { 2, 2 }, 0)]
    public void Test(int[] nums, int expected)
    {
        var result = LastStoneWeight(nums);
        Assert.Equal(expected, result);
    }

    public int LastStoneWeight(int[] stones)
    {
        var maxHeap = new PriorityQueue<int, int>();
        foreach (var stone in stones)
        {
            maxHeap.Enqueue(stone, -stone);
        }

        while (maxHeap.Count > 1)
        {
            var stone1 = maxHeap.Dequeue();
            var stone2 = maxHeap.Dequeue();
            var diff = stone1 - stone2;
            if (diff > 0)
            {
                maxHeap.Enqueue(diff, -diff);
            }
        }

        return maxHeap.Count == 0 ? 0 : maxHeap.Dequeue();
    }
}
