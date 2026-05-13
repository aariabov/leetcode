namespace Tests.DP.Intro;

// [Числа Трибоначи](https://leetcode.com/explore/learn/card/dynamic-programming/631/strategy-for-solving-dp-problems/4041/)
public class TribonacciTests
{
    [Theory]
    [InlineData(4, 4)]
    [InlineData(25, 1389537)]
    [InlineData(0, 0)]
    public void Test(int n, int expected)
    {
        var result = Tribonacci(n);
        Assert.Equal(expected, result);
    }

    public int Tribonacci(int n)
    {
        if (n == 0)
        {
            return 0;
        }

        var prevPrevPrev = 0;
        var prevPrev = 1;
        var prev = 1;

        for (var i = 3; i < n + 1; i++)
        {
            var val = prevPrevPrev + prevPrev + prev;
            prevPrevPrev = prevPrev;
            prevPrev = prev;
            prev = val;
        }
        return prev;
    }

    public int TribonacciRec(int n)
    {
        var hash = new int?[n + 1];
        return Dp(n);

        int Dp(int i)
        {
            if (i == 0)
            {
                return 0;
            }

            if (i == 1 || i == 2)
            {
                return 1;
            }

            if (hash[i].HasValue)
            {
                return hash[i].Value;
            }

            var prev = Dp(i - 1);
            var prevPrev = Dp(i - 2);
            var prevPrevPrev = Dp(i - 3);
            var res = prev + prevPrev + prevPrevPrev;
            hash[i] = res;
            return res;
        }
    }
}
