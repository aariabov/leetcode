using System.Text;

namespace Tests.BitManipulation.Concepts;

// [Перевести десятичное число в семиричную систему счисления](https://leetcode.com/explore/learn/card/bit-manipulation/670/bit-manipulation-practice/4254/)
public class ConvertToBase7Tests
{
    [Theory]
    [InlineData(100, "202")]
    [InlineData(-7, "-10")]
    public void Test(int num, string expected)
    {
        var result = ConvertToBase7(num);
        Assert.Equal(expected, result);
    }

    public string ConvertToBase7(int num)
    {
        var builder = new StringBuilder();

        var isNegative = num < 0;

        var number = isNegative ? num * (-1) : num;
        var list = new List<int>();
        do
        {
            var mod = number % 7;
            list.Add(mod);
            number = number / 7;
        } while (number > 0);

        if (isNegative)
        {
            builder.Append("-");
        }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            builder.Append(list[i]);
        }

        return builder.ToString();
    }
}
