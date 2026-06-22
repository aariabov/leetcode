using Xunit.Abstractions;

namespace Tests.BitManipulation.Practice;

// [Подсчет единичных битов](https://leetcode.com/explore/learn/card/bit-manipulation/670/bit-manipulation-practice/4265/)
public class CountBitsTests
{
    private readonly ITestOutputHelper _output;

    public CountBitsTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(2, new[] { 0, 1, 1 })]
    [InlineData(5, new[] { 0, 1, 1, 2, 1, 2 })]
    public void Test(int rowIndex, int[] expected)
    {
        var result = CountBits(rowIndex);
        Assert.Equal(expected, result);
    }

    public int[] CountBits(int n)
    {
        int[] ans = new int[n + 1];

        for (int i = 1; i <= n; i++)
        {
            // берем ранее вычисленное значение + значение последнего бита
            ans[i] = ans[i >> 1] + (i & 1);
        }

        return ans;
    }

    // работает и быстро
    public int[] MyCountBits(int n)
    {
        var result = new int[n + 1];
        for (int i = 0; i < n + 1; i++)
        {
            result[i] = GetOnesCount(i);
        }
        return result;

        int GetOnesCount(int num)
        {
            var res = 0;
            // зануляем правую единицу
            while (num > 0)
            {
                // _output.WriteLine(Convert.ToString(num, 2).PadLeft(8, '0'));
                // _output.WriteLine(Convert.ToString(num - 1, 2).PadLeft(8, '0'));
                // _output.WriteLine(Convert.ToString(num & (num - 1) - 1, 2).PadLeft(8, '0'));
                // _output.WriteLine("");
                num = num & (num - 1);
                res++;
            }
            return res;
        }
    }
}
