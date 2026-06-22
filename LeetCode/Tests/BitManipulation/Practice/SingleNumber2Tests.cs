namespace Tests.BitManipulation.Practice;

// [Найти единственный неповторяющийся элемент в массиве, где остальные повторяются по 3 раза](https://leetcode.com/explore/learn/card/bit-manipulation/670/bit-manipulation-practice/4267/)
public class SingleNumber2Tests
{
    [Theory]
    [InlineData(new int[] { 2, 2, 3, 2 }, 3)]
    [InlineData(new int[] { 0, 1, 0, 1, 0, 1, 99 }, 99)]
    [InlineData(new int[] { 30000, 500, 100, 30000, 100, 30000, 100 }, 500)]
    public void Test(int[] arr, int expected)
    {
        var result = SingleNumber(arr);
        Assert.Equal(expected, result);
    }

    public int SingleNumber(int[] nums)
    {
        var ones = 0;
        var twos = 0;
        foreach (var num in nums)
        {
            // при трех одинаковых числах, биты в ones обнуляться
            ones = (ones ^ num) & ~twos;
            twos = (twos ^ num) & ~ones;
        }
        return ones;
    }
}
