namespace Tests.QueueStack.Conclusion;

/// <summary>
/// [Залить картинку](https://leetcode.com/explore/learn/card/queue-stack/239/conclusion/1393/)
/// </summary>
public class FloodFillTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new int[][] { 
                    [1,1,1],
                    [1,1,0],
                    [1,0,1] },
                1,1,2,
                new int[][]
                {
                    [2,2,2],
                    [2,2,0],
                    [2,0,1]
                },
            },
            new object[]
            {
                new int[][] { [0,0,0],[0,0,0] },
                0,0,0,
                new int[][] { [0,0,0],[0,0,0] },
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] image, int sr, int sc, int color, int[][] expected)
    {
        var result = FloodFill(image,sr, sc, color);
        Assert.Equal(expected, result);
    }
    
    public int[][] FloodFill(int[][] image, int sr, int sc, int color)
    {
        int originalColor = image[sr][sc];

        // Если цвет уже совпадает — ничего делать не нужно
        if (originalColor == color)
            return image;

        DFS(image, sr, sc, originalColor, color);
        return image;
    }

    // рекурсивное решение, поиск в глубину
    private void DFS(int[][] image, int r, int c, int originalColor, int newColor)
    {
        // Проверка границ
        if (r < 0 || c < 0 || r >= image.Length || c >= image[0].Length)
            return;

        // Проверка цвета
        if (image[r][c] != originalColor)
            return;

        // Перекрашиваем текущий пиксель
        image[r][c] = newColor;

        // Обходим соседей (вверх, вниз, влево, вправо)
        DFS(image, r + 1, c, originalColor, newColor);
        DFS(image, r - 1, c, originalColor, newColor);
        DFS(image, r, c + 1, originalColor, newColor);
        DFS(image, r, c - 1, originalColor, newColor);
    }
    
    public int[][] FloodFillBFS(int[][] image, int sr, int sc, int color) 
    {
        var directions = new int[][]
        {
            new int[] { 1, 0 },
            new int[] { -1, 0 },
            new int[] { 0, 1 },
            new int[] { 0, -1 }
        };

        BFS(image, sr, sc, directions, color);

        return image;
    }

    private void BFS(int[][] grid, int startRow, int startCol, int[][] directions, int color)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        var initColor = grid[startRow][startCol];
        if (color == initColor)
        {
            return;
        }

        Queue<(int, int)> queue = new Queue<(int, int)>();
        queue.Enqueue((startRow, startCol));
        grid[startRow][startCol] = color;

        while (queue.Count > 0)
        {
            var (row, col) = queue.Dequeue();

            foreach (var dir in directions)
            {
                int newRow = row + dir[0];
                int newCol = col + dir[1];

                if (newRow >= 0 && newRow < rows &&
                    newCol >= 0 && newCol < cols &&
                    grid[newRow][newCol] == initColor)
                {
                    queue.Enqueue((newRow, newCol));
                    grid[newRow][newCol] = color;
                }
            }
        }
    }
}