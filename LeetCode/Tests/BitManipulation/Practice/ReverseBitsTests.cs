using Xunit.Abstractions;

namespace Tests.BitManipulation.Practice;

// [Реверс битов числа](https://leetcode.com/explore/learn/card/bit-manipulation/670/bit-manipulation-practice/4259/)
public class ReverseBitsTests
{
    private readonly ITestOutputHelper _output;
    private static readonly byte[] ByteReverseTable = new byte[256];

    // Внедряем зависимость через конструктор
    public ReverseBitsTests(ITestOutputHelper output)
    {
        _output = output;

        // разворачиваем все возможные комбинации для одного байта
        for (int i = 0; i < 256; i++)
        {
            uint reverse = 0;
            uint temp = (uint)i;
            for (int j = 0; j < 8; j++)
            {
                reverse <<= 1;
                reverse |= (temp & 1);
                temp >>= 1;
            }
            ByteReverseTable[i] = (byte)reverse;
        }
    }

    [Theory]
    [InlineData(43261596, 964176192)]
    [InlineData(2147483644, 1073741822)]
    // [InlineData(13, 176)]
    public void Test1(int x, int expected)
    {
        var result = ReverseBits(x);
        Assert.Equal(expected, result);
    }

    public int ReverseBits(int n)
    {
        return (ByteReverseTable[n & 0xFF] << 24)
            | (ByteReverseTable[(n >> 8) & 0xFF] << 16)
            | (ByteReverseTable[(n >> 16) & 0xFF] << 8)
            | ByteReverseTable[(n >> 24) & 0xFF];
    }

    public int ReverseBits1(int n)
    {
        var result = 0;
        for (int i = 0; i < 32; i++)
        {
            // Сдвигаем результат влево, освобождая место под новый бит
            result <<= 1;
            // Добавляем младший бит числа n к результату
            result |= (n & 1);
            // Сдвигаем исходное число вправо, чтобы обработать следующий бит
            n >>= 1;
        }
        return result;
    }

    public int MyReverseBits(int n)
    {
        var result = 0;
        var i = 0;
        // крутим вправо n, а result влево. Таким образом младшие биты n становятся старшими result
        while (i < 32)
        {
            var rightBit = n & 1;
            // _output.WriteLine(Convert.ToString(rightBit, 2).PadLeft(8, '0'));
            result <<= 1;
            result |= rightBit;
            // _output.WriteLine(Convert.ToString(result, 2).PadLeft(8, '0'));
            n >>= 1;
            i++;
            // _output.WriteLine("");
        }

        return result;
    }
}
