namespace Tests.Recursion1.Memoization;

/// <summary>
/// [Подняться по лестнице](https://leetcode.com/explore/learn/card/recursion-i/255/recursion-memoization/1662/)
/// </summary>
public class ClimbStairsTests
{
    [Theory]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    [InlineData(4, 5)]
    [InlineData(5, 8)]
    [InlineData(6, 13)]
    public void Test(int n, int expected)
    {
        var result = ClimbStairs(n);
        Assert.Equal(expected, result);
    }

    private Dictionary<int, int> _dict = new Dictionary<int, int>();

    // Идея: Количество способов подняться на n ступенек — это числа Фибоначчи,
    // потому что на последнюю ступень можно прийти либо с n-1, либо с n-2
    public int ClimbStairs(int n)
    {
        if (_dict.TryGetValue(n, out var val))
        {
            return val;
        }
        if (n < 3)
        {
            return n;
        }

        var prev = ClimbStairs(n - 1);
        _dict.TryAdd(n - 1, prev);
        var prevPrev = ClimbStairs(n - 2);
        _dict.TryAdd(n - 2, prevPrev);
        return prev + prevPrev;
    }
}
