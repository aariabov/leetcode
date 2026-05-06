namespace Tests.Graph.BFSInGraph;

// [Найти кратчайший путь в матрице](https://leetcode.com/explore/learn/card/graph/620/breadth-first-search-in-graph/3896/)
public class ShortestPathBinaryMatrixTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { new int[][] { [0, 1], [1, 0] }, 2 },
            new object[] { new int[][] { [0, 0, 0], [1, 1, 0], [1, 1, 0] }, 4 },
            new object[] { new int[][] { [1, 0, 0], [1, 1, 0], [1, 1, 0] }, -1 },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] nums, int expected)
    {
        var result = ShortestPathBinaryMatrix(nums);
        Assert.Equal(expected, result);
    }

    public int ShortestPathBinaryMatrix(int[][] grid)
    {
        int n = grid.Length;

        // Если старт или финиш заблокированы, пути нет
        if (grid[0][0] == 1 || grid[n - 1][n - 1] == 1)
            return -1;
        if (n == 1)
            return 1;

        // Очередь для BFS: хранит координаты (row, col, distance)
        Queue<(int r, int c, int d)> queue = new Queue<(int, int, int)>();
        queue.Enqueue((0, 0, 1));
        grid[0][0] = 1; // Помечаем как посещенную, чтобы не возвращаться

        // 8 направлений движения
        int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };

        while (queue.Count > 0)
        {
            var (r, c, dist) = queue.Dequeue();

            if (r == n - 1 && c == n - 1)
                return dist;

            for (int i = 0; i < 8; i++)
            {
                int nr = r + dr[i];
                int nc = c + dc[i];

                // Проверка границ и того, что ячейка пуста (0)
                if (nr >= 0 && nr < n && nc >= 0 && nc < n && grid[nr][nc] == 0)
                {
                    grid[nr][nc] = 1; // Помечаем посещенной
                    queue.Enqueue((nr, nc, dist + 1));
                }
            }
        }

        return -1;
    }

    // работает и быстро
    // идея: не использовать visited, а сразу занулять в матрице. Также нам нужна только дистанция, поэтому необязательно хранить полный путь
    public int ShortestPathBinaryMatrixMy(int[][] grid)
    {
        if (grid[0][0] != 0 || grid[^1][^1] != 0)
        {
            return -1;
        }

        var queue = new Queue<(int row, int col, int dist)>();
        queue.Enqueue((0, 0, 1));
        grid[0][0] = 1;

        while (queue.Count > 0)
        {
            var last = queue.Dequeue();
            if (last.row == grid.Length - 1 && last.col == grid.Length - 1)
            {
                return last.dist;
            }

            // верхняя строка
            CheckAndEnqueue(last.row - 1, last.col - 1, last.dist);
            CheckAndEnqueue(last.row - 1, last.col, last.dist);
            CheckAndEnqueue(last.row - 1, last.col + 1, last.dist);

            // нижняя строка
            CheckAndEnqueue(last.row + 1, last.col - 1, last.dist);
            CheckAndEnqueue(last.row + 1, last.col, last.dist);
            CheckAndEnqueue(last.row + 1, last.col + 1, last.dist);

            // клетка слева
            CheckAndEnqueue(last.row, last.col - 1, last.dist);
            // клетка справа
            CheckAndEnqueue(last.row, last.col + 1, last.dist);
        }

        return -1;

        void CheckAndEnqueue(int row, int col, int dist)
        {
            if (
                row >= 0
                && row < grid.Length
                && col >= 0
                && col < grid.Length
                && grid[row][col] == 0
            )
            {
                queue.Enqueue((row, col, dist + 1));
                grid[row][col] = 1;
            }
        }
    }

    // работает, но медленно
    public int ShortestPathBinaryMatrixMyNotOptimize(int[][] grid)
    {
        if (grid[0][0] != 0 || grid[^1][^1] != 0)
        {
            return -1;
        }

        var visited = new HashSet<(int row, int col)>();
        visited.Add((0, 0));
        var result = new List<List<(int row, int col)>>();
        var queue = new Queue<List<(int row, int col)>>();
        queue.Enqueue([(0, 0)]);

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            var last = path.Last();
            if (last == (grid.Length - 1, grid.Length - 1))
            {
                result.Add([.. path]);
                continue;
            }

            // верхняя строка
            CheckAndEnqueue(last.row - 1, last.col - 1, path);
            CheckAndEnqueue(last.row - 1, last.col, path);
            CheckAndEnqueue(last.row - 1, last.col + 1, path);

            // нижняя строка
            CheckAndEnqueue(last.row + 1, last.col - 1, path);
            CheckAndEnqueue(last.row + 1, last.col, path);
            CheckAndEnqueue(last.row + 1, last.col + 1, path);

            // клетка слева
            CheckAndEnqueue(last.row, last.col - 1, path);
            // клетка справа
            CheckAndEnqueue(last.row, last.col + 1, path);
        }

        if (result.Count == 0)
        {
            return -1;
        }

        var count = result.Select(x => x.Count).OrderByDescending(x => x).First();
        return count;

        void CheckAndEnqueue(int row, int col, List<(int row, int col)> path)
        {
            if (
                row >= 0
                && row < grid.Length
                && col >= 0
                && col < grid.Length
                && grid[row][col] == 0
                && visited.Add((row, col))
            )
            {
                var newPath = new List<(int row, int col)>(path);
                newPath.Add((row, col));
                queue.Enqueue(newPath);
            }
        }
    }
}
