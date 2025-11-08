namespace Tests;

/// <summary>
/// Найти количество чисел с четным количеством цифр
/// Find Numbers with Even Number of Digits
/// </summary>
public class FindNumbersTests
{
    [Theory]
    [InlineData(new int[] { 12, 345, 2, 6, 7896 }, 2)]
    [InlineData(new int[] { 555, 901, 482, 1771 }, 1)]
    public void Test(int[] nums, int expected)
    {
        var result = FindNumbers(nums);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new int[] { 12, 345, 2, 6, 7896 }, 2)]
    [InlineData(new int[] { 555, 901, 482, 1771 }, 1)]
    public void Test1(int[] nums, int expected)
    {
        var result = FindNumbers1(nums);
        Assert.Equal(expected, result);
    }

    private static int FindNumbers1(int[] nums)
    {
        return nums.Count(n => n.ToString().Length % 2 == 0);
    }

    private static int FindNumbers(int[] nums)
    {
        var res = 0;
        foreach (var num in nums)
        {
            var str = num.ToString();
            if (str.Length % 2 == 0)
            {
                res++;
            }
        }
        return res;
    }
}
