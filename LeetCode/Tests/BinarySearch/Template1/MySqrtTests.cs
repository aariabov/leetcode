namespace Tests.BinarySearch.Template1;

/// <summary>
/// [Корень числа](https://leetcode.com/explore/learn/card/binary-search/125/template-i/950/)
/// </summary>
public class MySqrtTests
{
    [Theory]
    [InlineData(4, 2)]
    [InlineData(8, 2)]
    [InlineData(2147395600, 46340)]
    [InlineData(6, 2)]
    [InlineData(36, 6)]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    public void Test1(int x, int expected)
    {
        var result = MySqrt(x);
        Assert.Equal(expected, result);
    }

    public int MySqrt(int x)
    {
        if (x < 2)
        {
            return x;
        }

        var left = 2;
        var right = x / 2;
        var result = 1;
        while (left <= right)
        {
            var mid = left + (right - left + 1) / 2;
            if (mid <= x / mid)
            {
                left = mid + 1;
                result = mid;
            }
            else
            {
                right = mid - 1;
            }
        }
        return result;
    }

    public int MyBitMySqrt(int x)
    {
        var i = 1;
        while (i < 46341 && i * i < x)
        {
            i = i << 1;
        }
        i = i >> 1;
        while (i * i < x)
        {
            i++;
        }

        return i * i == x ? i : i - 1;
    }

    // решение в лоб, падает по таймауту
    public int MyMySqrt(int x)
    {
        var i = 1;
        var cur = 1;
        while (cur < x)
        {
            i++;
            cur = i * i;
        }

        return cur == x ? i : i - 1;
    }
}
