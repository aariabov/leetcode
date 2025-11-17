namespace Tests;

public class FindDisappearedNumbersTests
{
    [Theory]
    [InlineData(new[] { 4, 3, 2, 7, 8, 2, 3, 1 }, new[] { 5, 6 })]
    [InlineData(new[] { 1, 1 }, new[] { 2 })]
    public void Test1(int[] nums, int[] expected)
    {
        var result = FindDisappearedNumbers(nums);
        Assert.Equal(expected, result);
    }

    public IList<int> FindDisappearedNumbers(int[] nums)
    {
        var arr = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            arr[nums[i] - 1] = nums[i];
        }
        var result = new List<int>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 0)
            {
                result.Add(i + 1);
            }
        }
        return result;
    }
}
