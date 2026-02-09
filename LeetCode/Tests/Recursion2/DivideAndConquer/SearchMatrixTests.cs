namespace Tests.Recursion2.DivideAndConquer;

/// <summary>
/// [Поиск в отсортированной матрице](https://leetcode.com/explore/learn/card/recursion-ii/470/divide-and-conquer/2872/)
/// </summary>
public class SearchMatrixTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new int[][]
                {
                    [1, 4, 7, 11, 15],
                    [2, 5, 8, 12, 19],
                    [3, 6, 9, 16, 22],
                    [10, 13, 14, 17, 24],
                    [18, 21, 23, 26, 30],
                },
                5,
                true,
            },
            new object[]
            {
                new int[][]
                {
                    [1, 4, 7, 11, 15],
                    [2, 5, 8, 12, 19],
                    [3, 6, 9, 16, 22],
                    [10, 13, 14, 17, 24],
                    [18, 21, 23, 26, 30],
                },
                20,
                false,
            },
            new object[] { new int[][] { [1, 4], [2, 5] }, 5, true },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] mat, int target, bool expected)
    {
        var result = SearchMatrix(mat, target);
        Assert.Equal(expected, result);
    }

    public bool SearchMatrix(int[][] matrix, int target)
    {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
            return false;

        return Search(matrix, target, 0, matrix.Length - 1, 0, matrix[0].Length - 1);
    }

    private bool Search(int[][] matrix, int target, int top, int bottom, int left, int right)
    {
        // если границы неверные
        if (top > bottom || left > right)
            return false;

        // если target вне диапазона этой подматрицы
        if (target < matrix[top][left] || target > matrix[bottom][right])
            return false;

        int midRow = (top + bottom) / 2;
        int midCol = (left + right) / 2;
        int midValue = matrix[midRow][midCol];

        if (midValue == target)
            return true;

        if (midValue > target)
        {
            // ищем в 3 возможных областях
            return Search(matrix, target, top, midRow - 1, left, right)
                || Search(matrix, target, midRow, bottom, left, midCol - 1);
        }
        else
        {
            return Search(matrix, target, midRow + 1, bottom, left, right)
                || Search(matrix, target, top, midRow, midCol + 1, right);
        }
    }
}
