namespace Tests.BitManipulation.Practice;

// [Логическое И для всех чисел из диапазона](https://leetcode.com/explore/learn/card/bit-manipulation/670/bit-manipulation-practice/4264/)
public class RangeBitwiseAndTests
{
    [Theory]
    [InlineData(5, 7, 4)]
    [InlineData(0, 0, 0)]
    [InlineData(1, 2147483647, 0)]
    public void Test1(int x, int y, int expected)
    {
        var result = RangeBitwiseAnd(x, y);
        Assert.Equal(expected, result);
    }

    // идея: найти общий префикс (то, что слева), то что справа гарантированно превратится в 0, т.к. 0 все обнулит
    public int RangeBitwiseAnd(int left, int right)
    {
        int shift = 0;
        while (left != right)
        {
            left >>= 1;
            right >>= 1;
            shift++;
        }
        return left << shift;
    }
}
