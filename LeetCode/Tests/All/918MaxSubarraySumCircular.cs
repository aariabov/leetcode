namespace Tests.All;

/// <summary>
/// [Наибольшая сумма элементов подмассива в кольцевом массиве](https://leetcode.com/problems/maximum-sum-circular-subarray/description/)
/// </summary>
public class MaxSubarraySumCircularTests
{
    [Theory]
    [InlineData(new int[] { 1, -2, 3, -2 }, 3)]
    [InlineData(new int[] { 5, -3, 5 }, 10)]
    [InlineData(new int[] { -3, -2, -3 }, -2)]
    public void Test(int[] nums, int expected)
    {
        var result = MaxSubarraySumCircular(nums);
        Assert.Equal(expected, result);
    }

    public int MaxSubarraySumCircular(int[] nums)
    {
        int totalSum = 0;

        int maxSum = nums[0];
        int currentMax = 0;

        int minSum = nums[0];
        int currentMin = 0;

        foreach (int num in nums)
        {
            // Считаем общую сумму всех элементов
            totalSum += num;

            // Классический Кадане: ищем максимальный обычный подмассив
            currentMax = Math.Max(num, currentMax + num);
            maxSum = Math.Max(maxSum, currentMax);

            // Обратный Кадане: ищем минимальный подмассив
            // Если мы забираем края массива, значит, в середине остаётся нетронутый кусок. Чтобы сумма краёв была максимальной, сумма выкинутой середины должна быть минимальной (minSum). Поэтому сумма кольцевого куска равна totalSum - minSum
            currentMin = Math.Min(num, currentMin + num);
            minSum = Math.Min(minSum, currentMin);
        }

        // Если все числа отрицательные, maxSum будет < 0.
        // В этом случае кольцевой вариант (totalSum - minSum) даст 0 (пустой массив), что запрещено.
        if (maxSum < 0)
        {
            return maxSum;
        }

        // Выбираем лучший вариант: обычный подмассив или кольцевой
        return Math.Max(maxSum, totalSum - minSum);
    }
}
