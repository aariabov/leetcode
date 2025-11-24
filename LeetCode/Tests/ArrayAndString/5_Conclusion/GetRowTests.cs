namespace Tests.ArrayAndString._5_Conclusion;

public class GetRowTests
{
    [Theory]
    [InlineData(3, new[] { 1,3,3,1 })]
    [InlineData(0, new[] { 1 })]
    [InlineData(1, new[] { 1, 1 })]
    public void Test(int rowIndex, int[] expected)
    {
        var result = GetRow(rowIndex);
        Assert.Equal(expected, result);
    }

    public IList<int> GetRow(int rowIndex)
    {
        var result = new int[rowIndex][];
        for (int i = 0; i < rowIndex + 1; i++)
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

            if (i == rowIndex)
            {
                return row;
            }
            else
            {
                result[i] = row;
            }
        }
        return [];
    }
}