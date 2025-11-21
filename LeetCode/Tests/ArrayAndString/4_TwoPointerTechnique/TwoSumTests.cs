namespace Tests.ArrayAndString._4_TwoPointerTechnique;

/// <summary>
/// Найти 2 числа в массиве, сумма которых равна таргету https://leetcode.com/explore/learn/card/array-and-string/205/array-two-pointer-technique/1153/
/// </summary>
public class TwoSumTests
{
    [Theory]
    [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 1, 2 })]
    [InlineData(new[] { 2, 3, 4 }, 6, new[] { 1, 3 })]
    [InlineData(new[] { -1, 0 }, -1, new[] { 1, 2 })]
    public void Test(int[] nums, int target, int[] expected)
    {
        var result = TwoSum(nums, target);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 1, 2 })]
    [InlineData(new[] { 2, 3, 4 }, 6, new[] { 1, 3 })]
    [InlineData(new[] { -1, 0 }, -1, new[] { 1, 2 })]
    public void Test1(int[] nums, int target, int[] expected)
    {
        var result = TwoSum1(nums, target);
        Assert.Equal(expected, result);
    }

    public int[] TwoSum1(int[] numbers, int target)
    {
        var i = 0;
        var j = numbers.Length - 1;
        while (i < j)
        {
            var sum = numbers[i] + numbers[j];
            if (sum == target)
            {
                return new int[] { i + 1, j + 1 };
            }
            
            if (sum < target)
            {
                i++;
            }
            else
            {
                j--;
            }
        }
        return [];
    }

    public int[] TwoSum(int[] numbers, int target)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            for (int j = i + 1; j < numbers.Length; j++)
            {
                if (numbers[i] + numbers[j] == target)
                {
                    return new int[] { i + 1, j + 1 };
                }
            }
        }
        return null;
    }
}
