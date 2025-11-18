namespace Tests.ArrayAndString.IntroductionToArray;

/// <summary>
/// Pivot Index - такой индекс, что сумма слева от этого элемента равна сумме справа
/// </summary>
public class PivotIndexTests
{
    [Theory]
    [InlineData(new int[] { 1, 7, 3, 6, 5, 6 }, 3)]
    [InlineData(new int[] { 1, 2, 3 }, -1)]
    [InlineData(new int[] { 2, 1, -1 }, 0)]
    public void Test(int[] nums, int expected)
    {
        var result = PivotIndex(nums);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new int[] { 1, 7, 3, 6, 5, 6 }, 3)]
    [InlineData(new int[] { 1, 2, 3 }, -1)]
    [InlineData(new int[] { 2, 1, -1 }, 0)]
    public void Test1(int[] nums, int expected)
    {
        var result = PivotIndex1(nums);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new int[] { 1, 7, 3, 6, 5, 6 }, 3)]
    [InlineData(new int[] { 1, 2, 3 }, -1)]
    [InlineData(new int[] { 2, 1, -1 }, 0)]
    public void Test2(int[] nums, int expected)
    {
        var result = PivotIndex2(nums);
        Assert.Equal(expected, result);
    }

    // Лучшее решение:
    // Сначала вычисляем общую сумму массива.
    // Идём по массиву и поддерживаем левую сумму.
    // Для каждого индекса проверяем: leftSum == totalSum - leftSum - nums[i]
    // Если условие выполняется — нашли pivot.
    public int PivotIndex2(int[] nums)
    {
        int totalSum = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            totalSum += nums[i];
        }

        int leftSum = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (leftSum == totalSum - leftSum - nums[i])
            {
                return i;
            }
            leftSum += nums[i];
        }

        return -1;
    }

    // Решение получше, чем в лоб: сначала считаем и запоминаем левые и правые суммы в отдельные массивы
    public int PivotIndex1(int[] nums)
    {
        var leftSumArr = new int[nums.Length];
        for (int i = 1; i < nums.Length + 1; i++)
        {
            leftSumArr[i - 1] = i == 1 ? nums[i - 1] : nums[i - 1] + leftSumArr[i - 2];
        }

        var rightSumArr = new int[nums.Length];
        for (int i = nums.Length - 2; i > -2; i--)
        {
            rightSumArr[i + 1] =
                i == nums.Length - 2 ? nums[i + 1] : nums[i + 1] + rightSumArr[i + 2];
        }

        for (int i = 0; i < nums.Length; i++)
        {
            var leftSum = 0;
            if (i > 0)
            {
                leftSum = leftSumArr[i - 1];
            }

            var rightSum = 0;
            if (i < nums.Length - 1)
            {
                rightSum = rightSumArr[i + 1];
            }

            if (leftSum == rightSum)
            {
                return i;
            }
        }
        return -1;
    }

    // Решение в лоб: проходим массив, считает сумму слева и сумму справа
    public int PivotIndex(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            var leftSum = 0;
            if (i > 0)
            {
                for (int j = i - 1; j > -1; j--)
                {
                    leftSum += nums[j];
                }
            }

            var rightSum = 0;
            if (i < nums.Length - 1)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    rightSum += nums[j];
                }
            }

            if (leftSum == rightSum)
            {
                return i;
            }
        }
        return -1;
    }
}
