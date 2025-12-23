namespace Tests.QueueStack.QueueAndBFS;

/// <summary>
/// [Подсчет островов](https://leetcode.com/explore/learn/card/queue-stack/231/practical-application-queue/1374/)
/// </summary>
public class NumIslandsTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new char[][] { 
                    ['1','1','1','1','0'],
                    ['1','1','0','1','0'],
                    ['1','1','0','0','0'],
                    ['0','0','0','0','0'] 
                },
                1
            },
            new object[]
            {
                new char[][] { 
                    ['1','1','0','0','0'],
                    ['1','1','0','0','0'],
                    ['0','0','1','0','0'],
                    ['0','0','0','1','1'] 
                },
                3
            },
            new object[]
            {
                new char[][] { ['0'] },
                0
            },
            new object[]
            {
                new char[][] { 
                    ['1','0','0'],
                    ['0','0','0'],
                    ['0','0','1']
                },
                2
            }
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(char[][] mat, int expected)
    {
        var result = NumIslands(mat);
        Assert.Equal(expected, result);
    }
    
    public int NumIslands(char[][] grid)
    {
        if (grid == null || grid.Length == 0)
            return 0;

        int rows = grid.Length;
        int cols = grid[0].Length;
        int islands = 0;

        int[][] directions = new int[][]
        {
            new int[] { 1, 0 },
            new int[] { -1, 0 },
            new int[] { 0, 1 },
            new int[] { 0, -1 }
        };

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j] == '1')
                {
                    islands++;
                    BFS(grid, i, j, directions);
                }
            }
        }

        return islands;
    }

    private void BFS(char[][] grid, int startRow, int startCol, int[][] directions)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        Queue<(int, int)> queue = new Queue<(int, int)>();
        queue.Enqueue((startRow, startCol));
        grid[startRow][startCol] = '0'; // помечаем как посещённую

        while (queue.Count > 0)
        {
            var (row, col) = queue.Dequeue();

            foreach (var dir in directions)
            {
                int newRow = row + dir[0];
                int newCol = col + dir[1];

                if (newRow >= 0 && newRow < rows &&
                    newCol >= 0 && newCol < cols &&
                    grid[newRow][newCol] == '1')
                {
                    queue.Enqueue((newRow, newCol));
                    grid[newRow][newCol] = '0';
                }
            }
        }
    }
    
    // вместо зануления посещенной единицы я складываю ее индексы в HashSet
    public int NumIslandsMy(char[][] grid)
    {
        var result = 0;
        var hashSet = new HashSet<(int i, int j)>();
        var queue = new Queue<(int i, int j)>();
        
        for (int row = 0; row < grid.Length; row++)
        {
            for (int col = 0; col < grid[0].Length; col++)
            {
                if (grid[row][col] == '1' && hashSet.Add((row, col)))
                {
                    queue.Enqueue((row, col));
                    ProcessIsland();
                }
            }
        }
        return result;

        void ProcessIsland()
        {
            while (queue.Count > 0)
            {
                (int row, int col) = queue.Dequeue();
                // если еще не обрабатывали
                if (row < grid.Length - 1 && hashSet.Add((row + 1, col)))
                {
                    if (grid[row+1][col] == '1')
                    {
                        queue.Enqueue((row+1, col));
                    }
                }

                // если еще не обрабатывали
                if (col < grid[0].Length - 1 && hashSet.Add((row, col + 1)))
                {
                    if (grid[row][col+1] == '1')
                    {
                        queue.Enqueue((row, col+1));
                    }
                }

                // если еще не обрабатывали
                if (col > 0 && hashSet.Add((row, col - 1)))
                {
                    if (grid[row][col-1] == '1')
                    {
                        queue.Enqueue((row, col-1));
                    }
                }
                
                // если еще не обрабатывали
                if (row > 0 && hashSet.Add((row - 1, col)))
                {
                    if (grid[row-1][col] == '1')
                    {
                        queue.Enqueue((row-1, col));
                    }
                }
            }

            result++;
        }
        
    }
}