namespace Tests.DP.Intro;

// [Найти максимальный квадрат](https://leetcode.com/explore/learn/card/dynamic-programming/631/strategy-for-solving-dp-problems/4046/)
public class MaximalSquareTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new char[][]
                {
                    ['1', '0', '1', '0', '0'],
                    ['1', '0', '1', '1', '1'],
                    ['1', '1', '1', '1', '1'],
                    ['1', '0', '0', '1', '0'],
                },
                4,
            },
            new object[] { new char[][] { ['0', '1'], ['1', '0'] }, 1 },
            new object[] { new char[][] { ['0'] }, 0 },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(char[][] mat, int expected)
    {
        var result = MaximalSquare(mat);
        Assert.Equal(expected, result);
    }

    public int MaximalSquare(char[][] matrix)
    {
        var result = 0;
        var rows = matrix.Length;
        var cols = matrix[0].Length;
        var mat = new int[rows + 1, cols + 1];
        for (int i = rows - 1; i >= 0; i--)
        {
            for (int j = cols - 1; j >= 0; j--)
            {
                if (matrix[i][j] == '0')
                {
                    mat[i, j] = 0;
                }
                else
                {
                    var bottom = mat[i + 1, j];
                    var right = mat[i, j + 1];
                    var diag = mat[i + 1, j + 1];
                    mat[i, j] = Math.Min(bottom, Math.Min(right, diag)) + 1;
                    if (mat[i, j] > result)
                    {
                        result = mat[i, j];
                    }
                }
            }
        }

        return result * result;
    }
}
