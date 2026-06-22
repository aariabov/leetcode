using Xunit.Abstractions;

namespace Tests.BitManipulation.Practice;

// [Код Грея](https://leetcode.com/explore/learn/card/bit-manipulation/670/bit-manipulation-practice/4262/)
public class GrayCodeTests
{
    private readonly ITestOutputHelper _output;

    public GrayCodeTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(2, new[] { 0, 1, 3, 2 })]
    [InlineData(1, new[] { 0, 1 })]
    public void Test(int rowIndex, int[] expected)
    {
        var result = GrayCode(rowIndex);
        Assert.Equal(expected, result);
    }

    public IList<int> GrayCode(int n)
    {
        // Количество элементов в последовательности равно 2^n
        int count = 1 << n;
        List<int> result = new List<int>(count);

        // Генерируем каждый элемент по формуле i ^ (i >> 1)
        for (int i = 0; i < count; i++)
        {
            result.Add(i ^ (i >> 1));
        }

        return result;
    }

    // работает, как эталон только подробно
    public IList<int> MyGrayCode(int n)
    {
        var result = new List<int>();
        var to = 1 << n; // или Math.Pow(2, n)
        for (int i = 0; i < to; i++)
        {
            var gray = GetGray(i);
            result.Add(gray);
        }
        return result;

        int GetGray(int num)
        {
            var move = num >> 1;
            _output.WriteLine(Convert.ToString(num, 2).PadLeft(8, '0'));
            _output.WriteLine(Convert.ToString(move, 2).PadLeft(8, '0'));
            var result = num ^ move;
            _output.WriteLine(Convert.ToString(result, 2).PadLeft(8, '0'));
            _output.WriteLine("");
            return result;
        }
    }
}
