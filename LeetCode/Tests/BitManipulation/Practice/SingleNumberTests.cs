namespace Tests.BitManipulation.Practice;

public class SingleNumberTests
{
    [Theory]
    [InlineData(new int[] { 2, 2, 1 }, 1)]
    [InlineData(new int[] { 4, 1, 2, 1, 2 }, 4)]
    [InlineData(new int[] { 1 }, 1)]
    public void Test(int[] arr, int expected)
    {
        var result = SingleNumber(arr);
        Assert.Equal(expected, result);
    }

    public int SingleNumber(int[] nums)
    {
        var result = 0;
        foreach (var num in nums)
        {
            result ^= num;
        }
        return result;
    }
}
