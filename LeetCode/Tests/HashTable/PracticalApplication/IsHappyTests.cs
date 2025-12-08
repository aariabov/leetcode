namespace Tests.HashTable.PracticalApplication;

/// <summary>
/// Счастливое число https://leetcode.com/explore/learn/card/hash-table/183/combination-with-other-algorithms/1131/
/// </summary>
public class IsHappyTests
{
    [Theory]
    [InlineData(19, true)]
    [InlineData(2, false)]
    public void Test(int n, bool expected)
    {
        var result = IsHappy(n);
        Assert.Equal(expected, result);
    }

    // самое быстрое решение
    public bool IsHappy(int n)
    {
        int slow = n;
        int fast = Next(n);

        while (fast != 1 && slow != fast)
        {
            slow = Next(slow);
            fast = Next(Next(fast));
        }

        return fast == 1;
    }

    private int Next(int n)
    {
        int sum = 0;
        while (n > 0)
        {
            int digit = n % 10;
            sum += digit * digit;
            n /= 10;
        }
        return sum;
    }

    public bool IsHappy2(int n)
    {
        HashSet<int> seen = new HashSet<int>();

        while (n != 1 && !seen.Contains(n))
        {
            seen.Add(n);
            n = SumOfSquares(n);
        }

        return n == 1;
    }

    private int SumOfSquares(int n)
    {
        int sum = 0;
        while (n > 0)
        {
            int digit = n % 10;
            sum += digit * digit;
            n /= 10;
        }
        return sum;
    }

    public bool IsHappy1(int n)
    {
        var res = n;
        var hashSet = new HashSet<int>();
        while (res != 1)
        {
            res = GetSum(res);
            if (!hashSet.Add(res))
            {
                return false;
            }
        }

        return true;

        int GetSum(int number)
        {
            List<int> digits = new List<int>();

            while (number > 0)
            {
                digits.Add(number % 10);
                number /= 10;
            }

            digits.Reverse();
            return digits.Select(d => d * d).Sum();
        }
    }
}
