namespace Tests.ArrayAndString._2_IntroductionTo2DArray;

/// <summary>
/// Треугольник Паскаля https://leetcode.com/explore/learn/card/array-and-string/202/introduction-to-2d-array/1170/
/// </summary>
public class PascalTriangleTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                5,
                new int[][] { [1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1] },
            },
            new object[]
            {
                1,
                new int[][] { [1] }
            }
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int numRows, int[][] expected)
    {
        var result = Generate(numRows);
        Assert.Equal(expected, result);
    }

    public IList<IList<int>> Generate(int numRows)
    {
        var result = new int[numRows][];
        for (int i = 0; i < numRows; i++)
        {
            var row = new int[i+1];
            for (int j = 0; j < row.Length; j++)
            {
                if (j == 0 || j == row.Length - 1)
                {
                    row[j] = 1;
                }
                else
                {
                    row[j] = result[i-1][j - 1]+result[i-1][j];
                }
            }
            result[i] = row;
        }
        return result;
    }
}