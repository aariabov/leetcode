using Xunit.Abstractions;

namespace Tests.BitManipulation.Practice;

// [Сложение двух чисел](https://leetcode.com/explore/learn/card/bit-manipulation/670/bit-manipulation-practice/4260/)
public class GetSumTests
{
    private readonly ITestOutputHelper _output;

    // Внедряем зависимость через конструктор
    public GetSumTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(2, 3, 5)]
    [InlineData(4, 5, 9)]
    public void Test1(int a, int b, int expected)
    {
        var result = GetSum(a, b);
        Assert.Equal(expected, result);
    }

    // аналогия - сложение в столбик, например, 18 + 25
    // сначала складываем без переноса 1+2 = 3 и 8+5 = 3 (так то 13, но пока без переноса), итого 33
    // а потом добавляем перенос 10, получаем 43
    public int GetSum(int a, int b)
    {
        while (b != 0)
        {
            // Находим перенос - он будет там, где оба разряда будут = 1
            int transfer = a & b;

            // Суммируем биты без учета переноса
            a = a ^ b;

            // Сдвигаем перенос влево, чтобы добавить его к следующему разряду
            b = transfer << 1;
        }
        return a;
    }

    // работает и быстро
    public int GetSumMy(int a, int b)
    {
        // _output.WriteLine(Convert.ToString(a, 2).PadLeft(8, '0'));
        // _output.WriteLine(Convert.ToString(b, 2).PadLeft(8, '0'));
        // _output.WriteLine("");

        var result = 0;
        var isTransfer = false;
        for (int i = 0; i < 32; i++)
        {
            if (a == 0 && b == 0 && !isTransfer)
            {
                break;
            }

            var lastA = a & 1;
            var lastB = b & 1;
            var val = lastB + lastA + (isTransfer ? 1 : 0);
            // _output.WriteLine(Convert.ToString(val, 2).PadLeft(8, '0'));

            if (
                (lastA == 0 && lastB == 0)
                || (!isTransfer && lastA == 0 && lastB == 1)
                || (!isTransfer && lastA == 1 && lastB == 0)
            )
            {
                var moveVal = val << i;
                // _output.WriteLine(Convert.ToString(moveVal, 2).PadLeft(8, '0'));
                result |= moveVal;
                // _output.WriteLine(Convert.ToString(result, 2).PadLeft(8, '0'));
                isTransfer = false;
            }
            else
            {
                var moveVal = (val & 1) << i;
                // _output.WriteLine(Convert.ToString(moveVal, 2).PadLeft(8, '0'));
                result |= moveVal;
                // _output.WriteLine(Convert.ToString(result, 2).PadLeft(8, '0'));
                isTransfer = true;
            }

            // _output.WriteLine("");
            a >>= 1;
            b >>= 1;
        }
        return result;
    }
}
