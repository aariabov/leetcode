namespace Tests.All;

/// <summary>
/// [Наибольшая сумма элементов подмассива](https://leetcode.com/problems/maximum-subarray/description/)
/// </summary>
public class MaxSubArrayTests
{
    [Theory]
    [InlineData(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }, 6)]
    [InlineData(new int[] { 1 }, 1)]
    [InlineData(new int[] { 5, 4, -1, 7, 8 }, 23)]
    public void Test(int[] nums, int expected)
    {
        var result = MaxSubArray(nums);
        Assert.Equal(expected, result);
    }

    public int MaxSubArray(int[] nums)
    {
        int maxSum = nums[0];
        int currentSum = nums[0];

        for (int i = 1; i < nums.Length; i++)
        {
            // Либо добавляем текущий элемент к накопленной сумме,
            // либо начинаем новый подмассив с текущего элемента.
            currentSum = Math.Max(nums[i], currentSum + nums[i]);

            // Обновляем глобальный максимум
            maxSum = Math.Max(maxSum, currentSum);
        }

        return maxSum;
    }

    public int MyMaxSubArray(int[] nums)
    {
        var max = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] + nums[i - 1] > nums[i])
            {
                nums[i] = nums[i] + nums[i - 1];
            }
            else
            {
                nums[i] = nums[i];
            }

            if (nums[i] > max)
            {
                max = nums[i];
            }
        }
        return max;
    }
}
