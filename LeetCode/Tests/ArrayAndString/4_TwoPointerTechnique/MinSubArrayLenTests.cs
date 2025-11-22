namespace Tests.ArrayAndString._4_TwoPointerTechnique;

/// <summary>
/// Найти минимальную длину подмассива, сумма которого >= таргету https://leetcode.com/explore/learn/card/array-and-string/205/array-two-pointer-technique/1299/
/// </summary>
public class MinSubArrayLenTests
{
    [Theory]
    [InlineData(4, new int[] { 1, 4, 4 }, 1)]
    [InlineData(7, new int[] { 2, 3, 1, 2, 4, 3 }, 2)]
    [InlineData(11, new int[] { 1, 1, 1, 1, 1, 1, 1, 1 }, 0)]
    [InlineData(15, new int[] { 1, 2, 3, 4, 5 }, 5)]
    [InlineData(213, new int[] { 12, 28, 83, 4, 25, 26, 25, 2, 25, 25, 25, 12 }, 8)]
    public void Test(int target, int[] nums, int expected)
    {
        var result = MinSubArrayLen(target, nums);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(4, new int[] { 1, 4, 4 }, 1)]
    [InlineData(7, new int[] { 2, 3, 1, 2, 4, 3 }, 2)]
    [InlineData(11, new int[] { 1, 1, 1, 1, 1, 1, 1, 1 }, 0)]
    [InlineData(15, new int[] { 1, 2, 3, 4, 5 }, 5)]
    [InlineData(213, new int[] { 12, 28, 83, 4, 25, 26, 25, 2, 25, 25, 25, 12 }, 8)]
    public void Test1(int target, int[] nums, int expected)
    {
        var result = MinSubArrayLen1(target, nums);
        Assert.Equal(expected, result);
    }

    // суть в том, чтобы запоминать сумму подмассива и вычитать левый элемент (скользящее окно)
    public int MinSubArrayLen1(int target, int[] nums)
    {
        int left = 0;
        int sum = 0;
        int minLen = int.MaxValue;

        for (int right = 0; right < nums.Length; right++)
        {
            // добавляем новый элемент к сумме подмассива слева
            sum += nums[right];

            while (sum >= target)
            {
                var len = right - left + 1; // длина подмассива слева
                if (len < minLen)
                {
                    minLen = len;
                }

                // пробуем уменьшить длину убрав левый элемент (уменьшив окно)
                sum -= nums[left];
                left++;
            }
        }

        return minLen == int.MaxValue ? 0 : minLen;
    }

    // решение в лоб - каждый раз суммируем элементы - работает, но падает по времени
    public int MinSubArrayLen(int target, int[] nums)
    {
        var count = int.MaxValue;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] >= target)
            {
                return 1;
            }

            var currentSum = nums[i];
            var currentCount = 1;
            for (int j = i + 1; j < nums.Length; j++)
            {
                currentSum += nums[j];
                currentCount++;
                if (currentSum >= target)
                {
                    if (currentCount < count)
                    {
                        count = currentCount;
                    }
                    break;
                }
            }
        }
        return count == int.MaxValue ? 0 : count;
    }
}
