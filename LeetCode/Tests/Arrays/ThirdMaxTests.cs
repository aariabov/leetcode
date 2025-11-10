namespace Tests;

/// <summary>
/// Найти 3ий максимальный элемент, если такого нет - вернуть максимальный https://leetcode.com/explore/learn/card/fun-with-arrays/523/conclusion/3231/
/// </summary>
public class ThirdMaxTests
{
    [Theory]
    [InlineData(new int[] { 3, 2, 1 }, 1)]
    [InlineData(new int[] { 1, 2 }, 2)]
    [InlineData(new int[] { 2, 2, 3, 1 }, 1)]
    [InlineData(new int[] { 1, 1, 2 }, 2)]
    [InlineData(new int[] { 1, 2, -2147483648 }, -2147483648)]
    public void Test(int[] nums, int expected)
    {
        var result = ThirdMax(nums);
        Assert.Equal(expected, result);
    }

    public int ThirdMax(int[] nums)
    {
        var max1 = int.MinValue;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] >= max1)
            {
                max1 = nums[i];
            }
        }
        if (nums.Length == 1 || nums.Length == 2)
        {
            return max1;
        }

        var max2 = int.MinValue;
        var findMax2 = false;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] >= max2 && nums[i] != max1)
            {
                max2 = nums[i];
                findMax2 = true;
            }
        }

        var max3 = int.MinValue;
        var findMax3 = false;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] >= max3 && nums[i] != max1 && findMax2 && nums[i] != max2)
            {
                max3 = nums[i];
                findMax3 = true;
            }
        }

        if (findMax3)
        {
            return max3;
        }
        return max1;
    }
}
