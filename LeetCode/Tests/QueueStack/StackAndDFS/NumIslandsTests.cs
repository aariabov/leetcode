namespace Tests.QueueStack.StackAndDFS;

/// <summary>
/// [Подсчет островов Stack и DFS](https://leetcode.com/explore/learn/card/queue-stack/232/practical-application-stack/1380/)
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

        var directions = new (int rowIdx, int colIdx)[]
        {
            (1, 0), (-1, 0), (0, 1), (0, -1)
        };

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j] == '1')
                {
                    islands++;
                    DFSStack(grid, i, j, directions);
                }
            }
        }

        return islands;
    }
    
    // явно используем стек
    private void DFSStack(char[][] grid, int startRow1, int startCol1, (int rowIdx, int colIdx)[] directions)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        var stack = new Stack<(int rowIdx, int colIdx)>();
        stack.Push((startRow1, startCol1));

        while (stack.Count>0)
        {
            (int startRow, int startCol) = stack.Pop();
            foreach (var dir in directions)
            {
                var rowIdx = startRow + dir.rowIdx;
                var colIdx = startCol + dir.colIdx;
                if (rowIdx > -1 && rowIdx < rows && colIdx > -1 && colIdx < cols && grid[rowIdx][colIdx] == '1')
                {
                    grid[rowIdx][colIdx] = '0';
                    stack.Push((rowIdx, colIdx));
                }
            }
        }
    }
    
    
    
    
    
    public int NumIslands1(char[][] grid)
    {
        if (grid == null || grid.Length == 0)
            return 0;

        int rows = grid.Length;
        int cols = grid[0].Length;
        int islands = 0;

        var directions = new (int rowIdx, int colIdx)[]
        {
            (1, 0), (-1, 0), (0, 1), (0, -1)
        };

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j] == '1')
                {
                    islands++;
                    DFS1(grid, i, j, directions);
                }
            }
        }

        return islands;
    }
    
    // используем рекурсию и неявный стек (CallStack)
    private void DFS1(char[][] grid, int startRow, int startCol, (int rowIdx, int colIdx)[] directions)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        foreach (var dir in directions)
        {
            var rowIdx = startRow + dir.rowIdx;
            var colIdx = startCol + dir.colIdx;
            if (rowIdx > -1 && rowIdx < rows && colIdx > -1 && colIdx < cols && grid[rowIdx][colIdx] == '1')
            {
                grid[rowIdx][colIdx] = '0';
                DFS1(grid, rowIdx, colIdx, directions);
            }
        }
    }
}