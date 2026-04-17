namespace Tests.BinarySearch.Conclusion;

// [Является ли число квадратом](https://leetcode.com/explore/learn/card/binary-search/137/conclusion/978/)
public class IsPerfectSquareTests
{
    [Theory]
    [InlineData(16, true)]
    [InlineData(808201, true)]
    [InlineData(14, false)]
    public void Test1(int num, bool expected)
    {
        var result = IsPerfectSquare(num);
        Assert.Equal(expected, result);
    }

    public bool IsPerfectSquare(int num)
    {
        int left = 0;
        int right = num;

        while (left < right)
        {
            int mid = left + (right - left) / 2;

            var square = (long)mid * mid;
            if (square == num)
            {
                return true;
            }

            if (square > num)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return left * left == num;
    }
}
