namespace Tests;

/// <summary>
/// Найти максимальное количество последовательно идущих единиц
/// </summary>
public class FindMaxConsecutiveOnesTests
{
    [Theory]
    [InlineData(new int[] { 1, 1, 0, 1, 1, 1 }, 3)]
    [InlineData(new int[] { 1, 0, 1, 1, 0, 1 }, 2)]
    public void Test1(int[] nums, int expected)
    {
        var result = FindMaxConsecutiveOnes(nums);
        Assert.Equal(expected, result);
    }

    private static int FindMaxConsecutiveOnes(int[] nums)
    {
        var max = 0;
        var current = 0;
        foreach (var num in nums)
        {
            if (num == 1)
            {
                current++;
            }
            else
            {
                if (current > max)
                {
                    max = current;
                }
                current = 0;
            }
        }
        if (current > max)
        {
            max = current;
        }
        return max;
    }
}
