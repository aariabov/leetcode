namespace Tests.BinarySearch.Template1;

/// <summary>
/// [Угадай число](https://leetcode.com/explore/learn/card/binary-search/125/template-i/951/)
/// </summary>
public class GuessNumberTests
{
    [Theory]
    [InlineData(10, 6)]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(2147483647, 2147483647)]
    public void Test(int n, int expected)
    {
        pick = expected;
        var result = GuessNumber(n);
        Assert.Equal(expected, result);
    }

    private int pick;

    public int GuessNumber(int n)
    {
        int left = 1;
        int right = n;

        while (left <= right)
        {
            // Используем такую формулу, чтобы избежать переполнения int
            int mid = left + (right - left) / 2;
            int res = guess(mid);

            if (res == 0)
            {
                return mid; // Число найдено
            }
            else if (res == -1)
            {
                right = mid - 1; // Загаданное число меньше, идем влево
            }
            else
            {
                left = mid + 1; // Загаданное число больше, идем вправо
            }
        }

        return left;
    }

    // работает и быстро
    public int MyGuessNumber(int n)
    {
        var res = int.MinValue;
        var left = 0;
        var right = n;
        while (res != 0)
        {
            var mid = left + (right - left) / 2;
            res = guess(mid);
            if (res == 0)
            {
                return mid;
            }
            else if (res == 1)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return int.MinValue;
    }

    private int guess(int num)
    {
        if (num == pick)
        {
            return 0;
        }
        else if (num > pick)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
