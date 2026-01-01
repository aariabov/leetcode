namespace Tests.QueueStack.Conclusion;

/// <summary>
/// [Матрица с кратчайшими путями от 0 до 1](https://leetcode.com/explore/learn/card/queue-stack/239/conclusion/1388/)
/// </summary>
public class UpdateMatrixTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new int[][] { [0, 0, 0], [0, 1, 0], [0, 0, 0] },
                new int[][] { [0, 0, 0], [0, 1, 0], [0, 0, 0] },
            },
            new object[]
            {
                new int[][] { [0, 0, 0], [0, 1, 0], [1, 1, 1] },
                new int[][] { [0, 0, 0], [0, 1, 0], [1, 2, 1] },
            },
            new object[]
            {
                new int[][]
                {
                    [0, 1, 0, 1, 1],
                    [1, 1, 0, 0, 1],
                    [0, 0, 0, 1, 0],
                    [1, 0, 1, 1, 1],
                    [1, 0, 0, 0, 1],
                },
                new int[][]
                {
                    [0, 1, 0, 1, 2],
                    [1, 1, 0, 0, 1],
                    [0, 0, 0, 1, 0],
                    [1, 0, 1, 1, 1],
                    [1, 0, 0, 0, 1],
                },
            },
            new object[]
            {
                new int[][]
                {
                    [1, 0, 1, 1, 0, 0, 1, 0, 0, 1],
                    [0, 1, 1, 0, 1, 0, 1, 0, 1, 1],
                    [0, 0, 1, 0, 1, 0, 0, 1, 0, 0],
                    [1, 0, 1, 0, 1, 1, 1, 1, 1, 1],
                    [0, 1, 0, 1, 1, 0, 0, 0, 0, 1],
                    [0, 0, 1, 0, 1, 1, 1, 0, 1, 0],
                    [0, 1, 0, 1, 0, 1, 0, 0, 1, 1],
                    [1, 0, 0, 0, 1, 1, 1, 1, 0, 1],
                    [1, 1, 1, 1, 1, 1, 1, 0, 1, 0],
                    [1, 1, 1, 1, 0, 1, 0, 0, 1, 1],
                },
                new int[][]
                {
                    [1, 0, 1, 1, 0, 0, 1, 0, 0, 1],
                    [0, 1, 1, 0, 1, 0, 1, 0, 1, 1],
                    [0, 0, 1, 0, 1, 0, 0, 1, 0, 0],
                    [1, 0, 1, 0, 1, 1, 1, 1, 1, 1],
                    [0, 1, 0, 1, 1, 0, 0, 0, 0, 1],
                    [0, 0, 1, 0, 1, 1, 1, 0, 1, 0],
                    [0, 1, 0, 1, 0, 1, 0, 0, 1, 1],
                    [1, 0, 0, 0, 1, 2, 1, 1, 0, 1],
                    [2, 1, 1, 1, 1, 2, 1, 0, 1, 0],
                    [3, 2, 2, 1, 0, 1, 0, 0, 1, 1],
                },
            },
            new object[]
            {
                new int[][]
                {
                    [1, 1, 0, 0, 1, 0, 0, 1, 1, 0],
                    [1, 0, 0, 1, 0, 1, 1, 1, 1, 1],
                    [1, 1, 1, 0, 0, 1, 1, 1, 1, 0],
                    [0, 1, 1, 1, 0, 1, 1, 1, 1, 1],
                    [0, 0, 1, 1, 1, 1, 1, 1, 1, 0],
                    [1, 1, 1, 1, 1, 1, 0, 1, 1, 1],
                    [0, 1, 1, 1, 1, 1, 1, 0, 0, 1],
                    [1, 1, 1, 1, 1, 0, 0, 1, 1, 1],
                    [0, 1, 0, 1, 1, 0, 1, 1, 1, 1],
                    [1, 1, 1, 0, 1, 0, 1, 1, 1, 1],
                },
                new int[][]
                {
                    [2, 1, 0, 0, 1, 0, 0, 1, 1, 0],
                    [1, 0, 0, 1, 0, 1, 1, 2, 2, 1],
                    [1, 1, 1, 0, 0, 1, 2, 2, 1, 0],
                    [0, 1, 2, 1, 0, 1, 2, 3, 2, 1],
                    [0, 0, 1, 2, 1, 2, 1, 2, 1, 0],
                    [1, 1, 2, 3, 2, 1, 0, 1, 1, 1],
                    [0, 1, 2, 3, 2, 1, 1, 0, 0, 1],
                    [1, 2, 1, 2, 1, 0, 0, 1, 1, 2],
                    [0, 1, 0, 1, 1, 0, 1, 2, 2, 3],
                    [1, 2, 1, 0, 1, 0, 1, 2, 3, 4],
                },
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] image, int[][] expected)
    {
        var result = UpdateMatrix(image);
        Assert.Equal(expected, result);
    }

    // идея: идем не от 1 до 0, а от 0 до 1, при этом обновляя расстояния
    public int[][] UpdateMatrix(int[][] mat)
    {
        int m = mat.Length;
        int n = mat[0].Length;

        Queue<(int, int)> queue = new Queue<(int, int)>();

        // Инициализация
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (mat[i][j] == 0)
                {
                    queue.Enqueue((i, j));
                }
                else
                {
                    mat[i][j] = -1; // помечаем как непосещённую
                }
            }
        }

        int[][] directions = new int[][]
        {
            new int[] { 1, 0 },
            new int[] { -1, 0 },
            new int[] { 0, 1 },
            new int[] { 0, -1 },
        };

        // BFS
        while (queue.Count > 0)
        {
            var (row, col) = queue.Dequeue();

            foreach (var d in directions)
            {
                int nrow = row + d[0];
                int ncol = col + d[1];

                if (nrow >= 0 && ncol >= 0 && nrow < m && ncol < n && mat[nrow][ncol] == -1)
                {
                    mat[nrow][ncol] = mat[row][col] + 1;
                    queue.Enqueue((nrow, ncol));
                }
            }
        }

        return mat;
    }

    // работает, но один тест не проходит по времени
    // запрогал прямолинейно, как я бы решал руками
    public int[][] UpdateMatrixMy(int[][] mat)
    {
        int[][] res = new int[mat.Length][];

        for (int i = 0; i < mat.Length; i++)
        {
            res[i] = new int[mat[i].Length];
        }

        for (int row = 0; row < mat.Length; row++)
        {
            for (int col = 0; col < mat[0].Length; col++)
            {
                var k = GetStepCount(row, col);
                res[row][col] = k;
            }
        }
        return res;

        int GetStepCount(int row1, int col1)
        {
            if (mat[row1][col1] == 0)
            {
                return 0;
            }

            var hashSet = new HashSet<(int i, int j)>();
            var steps = 0;
            var min = int.MaxValue;
            var queue = new Queue<(int i, int j)>();
            queue.Enqueue((row1, col1));
            hashSet.Add((row1, col1));

            // поиск в ширину (BFS) гарантирует минимальное количество шагов
            while (queue.Count > 0)
            {
                if (steps >= min)
                {
                    return min;
                }

                var initQueueSize = queue.Count;
                // обработка ближайших вариантов, на конкретном шаге
                for (int j = 0; j < initQueueSize; j++)
                {
                    var cur = queue.Dequeue();
                    var row = cur.i;
                    var col = cur.j;
                    if (mat[cur.i][cur.j] == 0)
                    {
                        return steps < min ? steps : min;
                    }

                    if (row < mat.Length - 1 && hashSet.Add((row + 1, col)))
                    {
                        queue.Enqueue((row + 1, col));
                    }

                    if (col < mat[0].Length - 1 && hashSet.Add((row, col + 1)))
                    {
                        queue.Enqueue((row, col + 1));
                    }

                    if (col > 0 && hashSet.Add((row, col - 1)))
                    {
                        if (row <= row1 && col - 1 < col1)
                        {
                            if (res[row][col - 1] + 1 < min)
                            {
                                min = res[row][col - 1] + 1;
                            }
                        }
                        else
                        {
                            queue.Enqueue((row, col - 1));
                        }
                    }

                    if (row > 0 && hashSet.Add((row - 1, col)))
                    {
                        if (row <= row1 && col == col1)
                        {
                            if (res[row - 1][col] + 1 < min)
                            {
                                min = res[row - 1][col] + 1;
                            }
                        }
                        else
                        {
                            queue.Enqueue((row - 1, col));
                        }
                    }
                }

                steps++;
            }

            return min;
        }
    }
}
