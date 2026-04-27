namespace Tests.Heap;

// [K-ый элемент с потоке элементов](https://leetcode.com/explore/learn/card/introduction-to-data-structure-binary-search-tree/142/conclusion/1018/)
public class KthLargestTests
{
    [Fact]
    public void Test()
    {
        var kthLargest = new KthLargest(3, [4, 5, 8, 2]);
        Assert.Equal(4, kthLargest.Add(3)); // return 4
        Assert.Equal(5, kthLargest.Add(5)); // return 5
        Assert.Equal(5, kthLargest.Add(10)); // return 5
        Assert.Equal(8, kthLargest.Add(9)); // return 8
        Assert.Equal(8, kthLargest.Add(4)); // return 8
    }

    [Fact]
    public void Test1()
    {
        var kthLargest = new KthLargest(4, [7, 7, 7, 7, 8, 3]);
        Assert.Equal(7, kthLargest.Add(2));
        Assert.Equal(7, kthLargest.Add(10));
        Assert.Equal(7, kthLargest.Add(9));
        Assert.Equal(8, kthLargest.Add(9));
    }

    [Fact]
    public void Test2()
    {
        var kthLargest = new KthLargest(1, []);
        Assert.Equal(-3, kthLargest.Add(-3));
        Assert.Equal(-2, kthLargest.Add(-2));
        Assert.Equal(-2, kthLargest.Add(-4));
        Assert.Equal(0, kthLargest.Add(0));
        Assert.Equal(4, kthLargest.Add(4));
    }

    public class KthLargest
    {
        private readonly int k;
        private PriorityQueue<int, int> minHeap;

        public KthLargest(int k, int[] nums)
        {
            this.k = k;
            minHeap = new PriorityQueue<int, int>();

            foreach (var num in nums)
            {
                Add(num);
            }
        }

        public int Add(int val)
        {
            minHeap.Enqueue(val, val);

            if (minHeap.Count > k)
            {
                minHeap.Dequeue();
            }

            return minHeap.Peek();
        }
    }
}
