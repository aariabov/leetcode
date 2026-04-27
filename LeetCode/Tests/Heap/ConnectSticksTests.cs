namespace Tests.Heap;

// [Минимальная стоимость соединения палочек](https://leetcode.com/explore/learn/card/heap/646/practices/4090/)
public class ConnectSticksTests
{
    [Theory]
    [InlineData(new int[] { 2, 4, 3 }, 14)]
    [InlineData(new int[] { 1, 8, 3, 5 }, 30)]
    public void Test(int[] nums, int expected)
    {
        var result = ConnectSticks(nums);
        Assert.Equal(expected, result);
    }

    public int ConnectSticks(int[] sticks)
    {
        var minHeap = new PriorityQueue<int, int>();
        foreach (var stick in sticks)
        {
            minHeap.Enqueue(stick, stick);
        }

        var result = 0;
        while (minHeap.Count > 1)
        {
            var stick1 = minHeap.Dequeue();
            var stick2 = minHeap.Dequeue();
            var sum = stick1 + stick2;
            minHeap.Enqueue(sum, sum);
            result += sum;
        }

        return result;
    }
}
