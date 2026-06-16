using System.Numerics;
using Xunit.Abstractions;

namespace Tests.BitManipulation.Practice;

// [Количество единичных битов в числе](https://leetcode.com/explore/learn/card/bit-manipulation/670/bit-manipulation-practice/4258/)
public class HammingWeightTests
{
    private readonly ITestOutputHelper _output;
    // Массив, хранящий количество единиц для каждого числа от 0 до 255
    private static readonly int[] BitCountTable = new int[256];

    // Внедряем зависимость через конструктор
    public HammingWeightTests(ITestOutputHelper output)
    {
        _output = output;
        
        // предварительно считаем количество единиц для всех чисел в одном байте
        for (int i = 0; i < 256; i++) {
            BitCountTable[i] = (i & 1) + BitCountTable[i >> 1];
        }
    }
    
    [Theory]
    [InlineData(10, 2)]
    [InlineData(11, 3)]
    [InlineData(128, 1)]
    [InlineData(2147483645, 30)]
    public void Test1(int x, int expected)
    {
        var result = HammingWeight(x);
        Assert.Equal(expected, result);
    }
    
    // с кешированием
    public int HammingWeight4(int n) {
        uint un = (uint)n;
        return BitCountTable[un & 0xFF] +           // Первый байт
               BitCountTable[(un >> 8) & 0xFF] +    // Второй байт
               BitCountTable[(un >> 16) & 0xFF] +   // Третий байт
               BitCountTable[(un >> 24) & 0xFF];    // Четвертый байт
    }
    
    public int HammingWeight(int n)
    {
        //   например, 10 это 00001010
        //        10 - 1, это 00001001
        // сделаем И, получим 00001000 - т.е скинули правую единицу
        
        int count = 0;
        while (n != 0) {
            _output.WriteLine(Convert.ToString(n, 2).PadLeft(8, '0'));
            _output.WriteLine(Convert.ToString(n - 1, 2).PadLeft(8, '0'));
            _output.WriteLine("");
            n &= (n - 1); // Сбрасывает последнюю единицу
            count++;
        }
        return count;
    }
    
    // встроенное решение
    public int HammingWeight1(int n) {
        // Приводим к uint, так как PopCount работает с беззнаковыми типами
        return BitOperations.PopCount((uint)n);
    }

    // работает
    public int MyHammingWeight(int n)
    {
        var str=Convert.ToString(n, 2).PadLeft(8, '0');
        var result=0;
        foreach (var chr in str)
        {
            if (chr == '1')
            {
                result += 1;
            }
        }
        return result;
    }
}