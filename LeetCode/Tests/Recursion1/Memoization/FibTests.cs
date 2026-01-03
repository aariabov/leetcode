namespace Tests.Recursion1.Memoization;

/// <summary>
/// [Числа Фибоначи](https://leetcode.com/explore/learn/card/recursion-i/255/recursion-memoization/1661/)
/// </summary>
public class FibTests
{
    [Theory]
    [InlineData(2, 1)]
    [InlineData(3, 2)]
    [InlineData(4, 3)]
    [InlineData(5, 5)]
    public void Test(int n, int expected)
    {
        var result = Fib(n);
        Assert.Equal(expected, result);
    }

    private Dictionary<int, int> _dict = new Dictionary<int, int>();

    public int Fib(int n)
    {
        if (_dict.TryGetValue(n, out var val))
        {
            return val;
        }

        if (n == 1)
        {
            return 1;
        }
        if (n == 0)
        {
            return 0;
        }

        var first = Fib(n - 1);
        _dict.TryAdd(n - 1, first);

        var second = Fib(n - 2);
        _dict.TryAdd(n - 2, second);

        return first + second;
    }
}
