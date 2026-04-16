namespace Tests.BinarySearch.Template2;

// [Первая плохая версия](https://leetcode.com/explore/learn/card/binary-search/126/template-ii/947/)
public class FirstBadVersionTests
{
    [Theory]
    [InlineData(5, 4)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(2126753390, 1702766719)]
    public void Test1(int x, int expected)
    {
        _firstBadVersion = expected;
        var result = FirstBadVersion(x);
        Assert.Equal(expected, result);
    }

    public int FirstBadVersion(int n)
    {
        if (n < 2)
        {
            return n;
        }

        var left = 1;
        var right = n;
        while (left < right)
        {
            var mid = left + (right - left) / 2;
            if (IsBadVersion(mid))
            {
                right = mid;
            }
            else
            {
                left = mid + 1;
            }
        }
        return left;
    }

    private int _firstBadVersion;

    bool IsBadVersion(int version)
    {
        return version >= _firstBadVersion;
    }
}
