namespace Tests.ArrayAndString._2_IntroductionTo2DArray;

/// <summary>
/// Пройти матрицу по диагоналям https://leetcode.com/explore/learn/card/array-and-string/202/introduction-to-2d-array/1167/
/// </summary>
public class FindDiagonalOrderTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new int[][] { [1, 2, 3], [4, 5, 6], [7, 8, 9] },
                new[] { 1, 2, 4, 7, 5, 3, 6, 8, 9 }
            },
            new object[]
            {
                new int[][] { [1, 2], [3, 4] },
                new[] { 1,2,3,4 }
            },
            new object[]
            {
                new int[][] { [1] },
                new[] { 1 }
            },
            new object[]
            {
                new int[][] { [3],[2] },
                new[] { 3,2 }
            }
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] mat, int[] expected)
    {
        var result = FindDiagonalOrder(mat);
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test1(int[][] mat, int[] expected)
    {
        var result = FindDiagonalOrder1(mat);
        Assert.Equal(expected, result);
    }

    public int[] FindDiagonalOrder1(int[][] mat)
    {
        int m = mat.Length;
        int n = mat[0].Length;
        int[] result = new int[m * n];
    
        int row = 0, col = 0, idx = 0;
        bool goingUp = true;

        while (idx < m * n)
        {
            result[idx++] = mat[row][col];

            if (goingUp)
            {
                if (col == n - 1)       // дошли до правого края
                {
                    row++;
                    goingUp = false;
                }
                else if (row == 0)      // дошли до верхнего края
                {
                    col++;
                    goingUp = false;
                }
                else
                {
                    row--;
                    col++;
                }
            }
            else
            {
                if (row == m - 1)       // дошли до нижнего края
                {
                    col++;
                    goingUp = true;
                }
                else if (col == 0)      // дошли до левого края
                {
                    row++;
                    goingUp = true;
                }
                else
                {
                    row++;
                    col--;
                }
            }
        }

        return result;
    }

    public int[] FindDiagonalOrder(int[][] mat)
    {
        int total = mat.Sum(row => row.Length);
        if (total == 0)
        {
            return [];
        }
        
        var result = new int[total];
        
        var i = 0;
        var j = 0;
        var k = 0;
        var down = false;

        do
        {
            result[k] = mat[i][j];
            k++;
            if (down)
            {
                // можем влево и вниз
                if (j - 1 > -1 && i + 1 < mat.Length)
                {
                    j--;
                    i++;
                }
                else
                {
                    // можем вниз
                    if (i + 1 < mat.Length)
                    {
                        i++;
                    }
                    else
                    {
                        j++;
                    }
                    down = false;
                }
            }
            else
            {
                // можем вправо и вверх
                if (j + 1 < mat[0].Length && i - 1 > -1)
                {
                    j++;
                    i--;
                }
                else
                {
                    // можем вправо
                    if (j + 1 < mat[0].Length)
                    {
                        j++;
                    }
                    else
                    {
                        i++;
                    }
                    down = true;
                }
            }
        } while (i != mat.Length && j != mat[0].Length);
        
        return result;
    }
}
