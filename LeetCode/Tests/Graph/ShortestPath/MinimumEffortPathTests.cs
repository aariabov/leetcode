namespace Tests.Graph.ShortestPath;

// [Путь с минимальным усилием](https://leetcode.com/explore/learn/card/graph/622/single-source-shortest-path-algorithm/3952/)
public class MinimumEffortPathTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { new int[][] { [1, 2, 2], [3, 8, 2], [5, 3, 5] }, 2 },
            new object[] { new int[][] { [1, 2, 3], [3, 8, 4], [5, 3, 5] }, 1 },
            new object[]
            {
                new int[][]
                {
                    [1, 2, 1, 1, 1],
                    [1, 2, 1, 2, 1],
                    [1, 2, 1, 2, 1],
                    [1, 2, 1, 2, 1],
                    [1, 1, 1, 2, 1],
                },
                0,
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] nums, int expected)
    {
        var result = MinimumEffortPath(nums);
        Assert.Equal(expected, result);
    }

    public int MinimumEffortPath(int[][] heights)
    {
        int rows = heights.Length;
        int cols = heights[0].Length;

        // Массив для хранения минимального усилия для каждой клетки
        int[][] costs = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            costs[i] = new int[cols];
            Array.Fill(costs[i], int.MaxValue);
        }
        costs[0][0] = 0;

        var pq = new PriorityQueue<(int r, int c), int>();
        pq.Enqueue((0, 0), 0);

        // Направления движения: вправо, вниз, влево, вверх
        int[][] directions = new int[][]
        {
            new int[] { 0, 1 },
            new int[] { 1, 0 },
            new int[] { 0, -1 },
            new int[] { -1, 0 },
        };

        while (pq.Count > 0)
        {
            pq.TryDequeue(out (int r, int c) curr, out int currentEffort);

            // Если мы дошли до нижней правой клетки, это и есть ответ
            if (curr.r == rows - 1 && curr.c == cols - 1)
                return currentEffort;

            // Если текущее извлеченное усилие больше уже найденного — пропускаем
            if (currentEffort > costs[curr.r][curr.c])
                continue;

            foreach (var dir in directions)
            {
                int nr = curr.r + dir[0];
                int nc = curr.c + dir[1];

                // Проверка границ массива
                if (nr >= 0 && nr < rows && nc >= 0 && nc < cols)
                {
                    // Усилие на данном шаге — это максимум между предыдущим усилием
                    // и разницей высот текущего перехода
                    int nextEffort = Math.Max(
                        currentEffort,
                        Math.Abs(heights[curr.r][curr.c] - heights[nr][nc])
                    );

                    // Если нашли путь с меньшим усилием, обновляем и добавляем в очередь
                    if (nextEffort < costs[nr][nc])
                    {
                        costs[nr][nc] = nextEffort;
                        pq.Enqueue((nr, nc), nextEffort);
                    }
                }
            }
        }

        return 0;
    }

    // работает, но медленно
    public int MyMinimumEffortPath(int[][] heights)
    {
        var rows = heights.Length;
        var cols = heights[0].Length;

        var costs = new Dictionary<(int row, int col), int>();
        var inQueue = new Dictionary<(int row, int col), bool>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                costs.Add((i, j), int.MaxValue);
            }
        }
        costs[(0, 0)] = 0;

        var queue = new Queue<(int row, int col)>();

        queue.Enqueue((0, 0));
        inQueue[(0, 0)] = true;

        while (queue.Count > 0)
        {
            var curr = queue.Dequeue();
            inQueue[curr] = false;

            ProcessCell(curr.row - 1, curr.col, curr);
            ProcessCell(curr.row + 1, curr.col, curr);
            ProcessCell(curr.row, curr.col + 1, curr);
            ProcessCell(curr.row, curr.col - 1, curr);
        }

        return costs[(rows - 1, cols - 1)];

        void ProcessCell(int row, int col, (int row, int col) curr)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols)
            {
                return;
            }

            // Релаксация ребра
            var curCost = costs[curr];
            var nextCost = costs[(row, col)];
            var weight = Math.Abs(
                Math.Abs(heights[row][col]) - Math.Abs(heights[curr.row][curr.col])
            );
            var max = Math.Max(curCost, weight);
            if (max < nextCost)
            {
                costs[(row, col)] = max;

                if (!inQueue.ContainsKey((row, col)) || !inQueue[(row, col)])
                {
                    queue.Enqueue((row, col));
                    inQueue[(row, col)] = true;
                }
            }
        }
    }
}
