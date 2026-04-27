namespace Tests.Heap;

// [k ближайших точек](https://leetcode.com/explore/learn/card/heap/646/practices/4088/)
public class KClosestTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { new int[][] { [1, 3], [-2, 2] }, 1, new int[][] { [-2, 2] } },
            new object[]
            {
                new int[][] { [3, 3], [5, -1], [-2, 4] },
                2,
                new int[][] { [3, 3], [-2, 4] },
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] points, int k, int[][] expected)
    {
        var result = KClosest(points, k);
        Assert.Equal(expected, result);
    }

    public int[][] KClosest(int[][] points, int k)
    {
        var minHeap = new PriorityQueue<int[], double>();
        foreach (var point in points)
        {
            minHeap.Enqueue(point, Math.Sqrt(point[0] * point[0] + point[1] * point[1]));
        }

        var result = new int[k][];
        for (int i = 0; i < k; i++)
        {
            var point = minHeap.Dequeue();
            result[i] = point;
        }
        return result;
    }
}
