namespace Tests.DP.Intro;

// [Самая длинная подпоследовательность символов в двух строках](https://leetcode.com/explore/learn/card/dynamic-programming/631/strategy-for-solving-dp-problems/4045/)
public class LongestCommonSubsequenceTests
{
    [Theory]
    [InlineData("abcde", "ace", 3)]
    [InlineData("abc", "abc", 3)]
    [InlineData("abc", "def", 0)]
    public void Test(string a, string b, int expected)
    {
        var result = LongestCommonSubsequence(a, b);
        Assert.Equal(expected, result);
    }

    public int LongestCommonSubsequence(string text1, string text2)
    {
        var mat = new int[text1.Length + 1, text2.Length + 1];
        for (int i = text1.Length - 1; i >= 0; i--)
        {
            for (int j = text2.Length - 1; j >= 0; j--)
            {
                if (text1[i] == text2[j])
                {
                    mat[i, j] = mat[i + 1, j + 1] + 1;
                }
                else
                {
                    var first = mat[i + 1, j];
                    var second = mat[i, j + 1];
                    mat[i, j] = Math.Max(first, second);
                }
            }
        }

        return mat[0, 0];
    }

    public int LongestCommonSubsequenceRec(string text1, string text2)
    {
        var dict = new Dictionary<(int i, int j), int>();
        return Dp(text1.Length - 1, text2.Length - 1);
        int Dp(int i, int j)
        {
            if (i < 0 || j < 0)
            {
                return 0;
            }

            if (dict.TryGetValue((i, j), out var result))
            {
                return result;
            }

            if (text1[i] == text2[j])
            {
                // если символ совпадает, то к количеству предыдущих совпавших символов (Dp(i - 1, j - 1)) добавляем 1
                return Dp(i - 1, j - 1) + 1;
            }

            // иначе, берем большее от 2х направлений
            var first = Dp(i - 1, j);
            var second = Dp(i, j - 1);
            var res = Math.Max(first, second);
            dict[(i, j)] = res;
            return res;
        }
    }
}
