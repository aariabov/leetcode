namespace Tests.Recursion1.Conclusion;

/// <summary>
/// [Двоичные строки](https://leetcode.com/explore/learn/card/recursion-i/253/conclusion/1675/)
/// </summary>
public class KthGrammarTests
{
    [Theory]
    [InlineData(1, 1, 0)]
    [InlineData(2, 1, 0)]
    [InlineData(2, 2, 1)]
    [InlineData(4, 1, 0)]
    [InlineData(4, 2, 1)]
    [InlineData(4, 3, 1)]
    [InlineData(4, 4, 0)]
    [InlineData(4, 5, 1)]
    [InlineData(4, 6, 0)]
    [InlineData(4, 7, 0)]
    [InlineData(4, 8, 1)]
    public void Test(int n, int k, int expected)
    {
        var result = KthGrammar(n, k);
        Assert.Equal(expected, result);
    }

    public int KthGrammar(int n, int k)
    {
        // Базовый случай
        if (n == 1)
            return 0;

        int length = 1 << (n - 1); // длина строки n
        int half = length / 2;

        if (k <= half)
        {
            return KthGrammar(n - 1, k);
        }
        else
        {
            return 1 - KthGrammar(n - 1, k - half);
        }
    }

    public int KthGrammarItem(int n, int k)
    {
        // k - 1, потому что индексация с 1
        int value = k - 1;
        int onesCount = 0;

        while (value > 0)
        {
            onesCount += value & 1;
            value >>= 1;
        }

        return onesCount % 2;
    }

    // работает, правильно и быстро
    public int KthGrammarMy(int n, int k)
    {
        return Rec(n, k - 1);

        int Rec(int row, int idx)
        {
            if (row == 1)
            {
                return 0;
            }

            var prevIdx = idx / 2;
            var prevVal = Rec(row - 1, prevIdx);
            var resIdx = idx % 2;
            if (prevVal == 0)
            {
                return resIdx == 0 ? 0 : 1;
            }
            else
            {
                return resIdx == 0 ? 1 : 0;
            }
        }
    }
}
