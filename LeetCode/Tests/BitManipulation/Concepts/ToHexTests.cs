using System.Text;
using Xunit.Abstractions;

namespace Tests.BitManipulation.Concepts;

// [Перевести десятичное число в шестнадцатеричную систему счисления](https://leetcode.com/explore/learn/card/bit-manipulation/670/bit-manipulation-practice/4256/)
public class ToHexTests
{
    private readonly ITestOutputHelper _output;

    // Внедряем зависимость через конструктор
    public ToHexTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(26, "1a")]
    [InlineData(-1, "ffffffff")]
    [InlineData(-2, "fffffffe")]
    [InlineData(-2147483648, "80000000")]
    public void Test(int num, string expected)
    {
        var result = ToHex(num);
        Assert.Equal(expected, result);
    }

    public string ToHex(int num)
    {
        if (num == 0)
        {
            return "0";
        }

        // Преобразуем во фреймворк беззнакового 32-битного целого числа
        // для автоматической обработки дополнительного кода отрицательных чисел
        _output.WriteLine($"num {Convert.ToString(num, 2).PadLeft(8, '0')}");
        uint val = (uint)num;
        _output.WriteLine($"val {Convert.ToString(val, 2).PadLeft(8, '0')}");
        string hexDigits = "0123456789abcdef";
        string result = "";

        // Обрабатываем число по 4 бита за раз (одна шестнадцатеричная цифра)
        while (val > 0)
        {
            // Маска 0xF (1111 в двоичной) выделяет младшие 4 бита
            int digitIndex = (int)(val & 0xF);
            result = hexDigits[digitIndex] + result;

            // Сдвигаем число вправо на 4 бита
            val >>= 4;
        }

        return result;
    }

    // работает, но медленно
    public string MyToHex(int num)
    {
        var isNegative = num < 0;
        if (isNegative)
        {
            var original = GetString(num, 2);
            string binaryStr = original.PadLeft(32, '0');
            var inversArr = new int[32];
            for (int i = 0; i < binaryStr.Length; i++)
            {
                inversArr[i] = binaryStr[i] == '0' ? 1 : 0;
            }

            var add = 1;
            for (var i = inversArr.Length - 1; i >= 0; i--)
            {
                if (add == 1)
                {
                    if (inversArr[i] == 1)
                    {
                        inversArr[i] = 0;
                        add = 1;
                    }
                    else
                    {
                        inversArr[i] = 1;
                        add = 0;
                    }
                }
            }

            var res = new string[8];
            for (int i = 0; i < res.Length; i++)
            {
                var j = i * 4;
                var item =
                    inversArr[j] * 8
                    + inversArr[j + 1] * 4
                    + inversArr[j + 2] * 2
                    + inversArr[j + 3] * 1;
                res[i] = GetChar(item);
            }
            return string.Join("", res);
        }
        else
        {
            return GetString(num, 16);
        }

        string GetString(int num, int system)
        {
            var builder = new StringBuilder();
            long number = isNegative ? (long)num * (-1) : num;
            var list = new List<string>();
            do
            {
                var mod = number % system;
                var str = GetChar(mod);
                list.Add(str);
                number = number / system;
            } while (number > 0);

            for (int i = list.Count - 1; i >= 0; i--)
            {
                builder.Append(list[i]);
            }

            return builder.ToString();
        }

        string GetChar(long mod)
        {
            return mod switch
            {
                10 => "a",
                11 => "b",
                12 => "c",
                13 => "d",
                14 => "e",
                15 => "f",
                _ => mod.ToString(),
            };
        }
    }
}
