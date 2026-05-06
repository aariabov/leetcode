namespace Tests.Graph.BFSInGraph;

// [Гниющие апельсины](https://leetcode.com/explore/learn/card/graph/620/breadth-first-search-in-graph/3898/)
public class OrangesRottingTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { new int[][] { [2, 1, 1], [1, 1, 0], [0, 1, 1] }, 4 },
            new object[] { new int[][] { [2, 1, 1], [0, 1, 1], [1, 0, 1] }, -1 },
            new object[] { new int[][] { [0, 2] }, 0 },
            new object[] { new int[][] { [1], [2] }, 1 },
            new object[] { new int[][] { [0] }, 0 },
            new object[] { new int[][] { [1] }, -1 },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] nums, int expected)
    {
        var result = OrangesRotting(nums);
        Assert.Equal(expected, result);
    }

    public int OrangesRotting(int[][] grid)
    {
        var freshCount = 0;
        var queue = new Queue<(int row, int col)>();
        for (int row = 0; row < grid.Length; row++)
        {
            for (int col = 0; col < grid[0].Length; col++)
            {
                if (grid[row][col] == 2)
                {
                    queue.Enqueue((row, col));
                }
                else if (grid[row][col] == 1)
                {
                    freshCount++;
                }
            }
        }

        if (freshCount == 0)
        {
            return 0;
        }

        if (queue.Count == 0)
        {
            return -1;
        }

        var level = -1;
        var rottenCount = 0;
        while (queue.Count > 0)
        {
            var levelSize = queue.Count;
            for (int i = 0; i < levelSize; i++)
            {
                var (row, col) = queue.Dequeue();
                CheckAndEnqueue(row - 1, col);
                CheckAndEnqueue(row, col - 1);
                CheckAndEnqueue(row + 1, col);
                CheckAndEnqueue(row, col + 1);
            }
            level++;
        }

        void CheckAndEnqueue(int row, int col)
        {
            if (
                row >= 0
                && row < grid.Length
                && col >= 0
                && col < grid[0].Length
                && grid[row][col] == 1
            )
            {
                queue.Enqueue((row, col));
                grid[row][col] = 2;
                rottenCount++;
            }
        }

        return rottenCount == freshCount ? level : -1;
    }
}
