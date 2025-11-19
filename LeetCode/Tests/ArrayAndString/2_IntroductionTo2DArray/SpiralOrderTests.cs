namespace Tests.ArrayAndString._2_IntroductionTo2DArray;

/// <summary>
/// Обойти матрицу по спирали https://leetcode.com/explore/learn/card/array-and-string/202/introduction-to-2d-array/1168/
/// </summary>
public class SpiralOrderTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new int[][] { [1,2,3],[4,5,6],[7,8,9] },
                new[] { 1,2,3,6,9,8,7,4,5 }
            },
            new object[]
            {
                new int[][] { [1,2,3,4],[5,6,7,8],[9,10,11,12] },
                new[] { 1,2,3,4,8,12,11,10,9,5,6,7 }
            }
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] mat, int[] expected)
    {
        var result = SpiralOrder(mat);
        Assert.Equal(expected, result);
    }

    public IList<int> SpiralOrder(int[][] matrix)
    {
        var m = matrix.Length;
        var n = matrix[0].Length;
        var rightCount = 0;
        var downCount = 0;
        var leftCount = 0;
        var upCount = 0;
        var direction = 0; // 0 - вправо, 1 - вниз, 2 - влево, 3 - вверх 
        var i = 0;
        var j = 0;
        var result = new int[m * n];

        for (int k = 0; k < result.Length; k++)
        {
            result[k] = matrix[i][j];
            if (direction == 0)
            {
                if (j + 1 < n - rightCount)
                {
                    j++;
                }
                else
                {
                    rightCount++;
                    direction = 1;
                    i++;
                }
            }else if (direction == 1)
            {
                if (i + 1 < m - downCount)
                {
                    i++;
                }
                else
                {
                    downCount++;
                    direction = 2;
                    j--;
                }
            }else if (direction == 2)
            {
                if (j - 1 > leftCount - 1)
                {
                    j--;
                }
                else
                {
                    leftCount++;
                    direction = 3;
                    i--;
                }
            }
            else
            {
                if (i - 1 > upCount)
                {
                    i--;
                }
                else
                {
                    upCount++;
                    direction = 0;
                    j++;
                }
            }
            
        }

        return result;
    }
}