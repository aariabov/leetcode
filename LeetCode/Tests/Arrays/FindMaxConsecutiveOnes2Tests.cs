namespace Tests;

// [Максимальное количество последовательно идущих единиц](https://leetcode.com/explore/learn/card/fun-with-arrays/523/conclusion/3230/)
public class FindMaxConsecutiveOnes2Tests
{
    [Theory]
    [InlineData(new int[] { 1, 0, 1, 1, 0 }, 4)]
    [InlineData(new int[] { 1, 0, 1, 1, 0, 1 }, 4)]
    [InlineData(new int[] { 1 }, 1)]
    [InlineData(new int[] { 1, 1 }, 2)]
    public void Test1(int[] nums, int expected)
    {
        var result = FindMaxConsecutiveOnes(nums);
        Assert.Equal(expected, result);
    }

    public int FindMaxConsecutiveOnes(int[] nums)
    {
        int maxLength = 0;
        int left = 0;
        int zeroCount = 0;

        for (int right = 0; right < nums.Length; right++)
        {
            // Если встретили ноль, увеличиваем счетчик нулей в окне
            if (nums[right] == 0)
            {
                zeroCount++;
            }

            // Если нулей больше одного, сдвигаем левую границу окна
            while (zeroCount > 1)
            {
                if (nums[left] == 0)
                {
                    zeroCount--;
                }
                left++;
            }

            // Вычисляем максимальную длину текущего валидного окна
            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }

    // работает и быстро, но не подходит для бесконечных стримов
    public int MyFindMaxConsecutiveOnes(int[] nums)
    {
        var result = int.MinValue;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 0)
            {
                var count = 1;
                var j = i - 1;
                while (j >= 0 && nums[j] == 1)
                {
                    count++;
                    j--;
                }

                j = i + 1;
                while (j < nums.Length && nums[j] == 1)
                {
                    count++;
                    j++;
                }

                if (count > result)
                {
                    result = count;
                }
            }
        }

        return result == int.MinValue ? nums.Length : result;
    }
}
