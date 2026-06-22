using FluentAssertions;

namespace Tests.BitManipulation.Practice;

// [Найти два уникальных элемента в массиве](https://leetcode.com/explore/learn/card/bit-manipulation/670/bit-manipulation-practice/4268/)
public class SingleNumber3Tests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 1, 3, 2, 5 }, new int[] { 3, 5 })]
    [InlineData(new int[] { -1, 0 }, new int[] { -1, 0 })]
    [InlineData(new int[] { 0, 1 }, new int[] { 0, 1 })]
    public void Test(int[] arr, int[] expected)
    {
        var result = SingleNumber(arr);
        result.Should().BeEquivalentTo(expected);
    }

    public int[] SingleNumber(int[] nums)
    {
        // Шаг 1: Находим XOR всех элементов массива
        int xorAll = 0;
        foreach (int num in nums)
        {
            xorAll ^= num;
        }

        // Шаг 2: Выделяем самый правый установленный бит (бит-разделитель).
        // Используем long, чтобы избежать переполнения (OverflowException)
        // в случае, если xorAll равен int.MinValue.
        int mask = (int)((long)xorAll & -(long)xorAll);

        // Шаг 3: Разделяем числа на две группы и находим уникальные элементы
        int num1 = 0;
        int num2 = 0;

        foreach (int num in nums)
        {
            if ((num & mask) == 0)
            {
                // Группа 1: у чисел этот бит равен 0
                num1 ^= num;
            }
            else
            {
                // Группа 2: у чисел этот бит равен 1
                num2 ^= num;
            }
        }

        return new int[] { num1, num2 };
    }
}
