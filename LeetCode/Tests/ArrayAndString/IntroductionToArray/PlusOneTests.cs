namespace Tests.ArrayAndString.IntroductionToArray;

/// <summary>
/// Дано число в виде массива цифр, надо прибавить к нему 1 и вернуть новое число в виде массива https://leetcode.com/explore/learn/card/array-and-string/201/introduction-to-array/1148/
/// </summary>
public class PlusOneTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 4 })]
    [InlineData(new[] { 9 }, new[] { 1, 0 })]
    public void Test(int[] nums, int[] expected)
    {
        var result = PlusOne(nums);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 4 })]
    [InlineData(new[] { 9 }, new[] { 1, 0 })]
    public void Test1(int[] nums, int[] expected)
    {
        var result = PlusOne1(nums);
        Assert.Equal(expected, result);
    }

    public int[] PlusOne1(int[] digits)
    {
        for (int i = digits.Length - 1; i >= 0; i--)
        {
            if (digits[i] < 9)
            {
                digits[i]++;
                return digits;
            }

            digits[i] = 0; // ставим 0 и продолжаем перенос
        }

        // Если дошли сюда — все цифры были 9 (например, 9, 99, 999)
        int[] result = new int[digits.Length + 1];
        result[0] = 1;
        return result;
    }

    public int[] PlusOne(int[] digits)
    {
        var len = digits.Length;
        var increase = true;
        for (int i = digits.Length - 1; i > -1; i--)
        {
            if (digits[i] != 9)
            {
                increase = false;
                break;
            }
        }

        var result = new int[increase ? len + 1 : len];
        var num = digits[digits.Length - 1] + 1;
        result[result.Length - 1] = num % 10;
        var prev = num / 10;
        for (int i = result.Length - 2; i > -1; i--)
        {
            var val = 0;
            if (increase)
            {
                val = i == 0 ? 0 : digits[i - 1];
            }
            else
            {
                val = digits[i];
            }

            num = val + prev;
            result[i] = num % 10;
            prev = num / 10;
        }

        return result;
    }
}
