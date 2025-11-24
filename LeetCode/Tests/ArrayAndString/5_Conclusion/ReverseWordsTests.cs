using System.Text;

namespace Tests.ArrayAndString._5_Conclusion;

/// <summary>
/// Реверс слов в строке https://leetcode.com/explore/learn/card/array-and-string/204/conclusion/1164/
/// </summary>
public class ReverseWordsTests
{
    [Theory]
    [InlineData("the sky is blue", "blue is sky the")]
    [InlineData("  hello world  ", "world hello")]
    [InlineData("a good   example", "example good a")]
    public void Test(string s, string expected)
    {
        var result = ReverseWords(s);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("the sky is blue", "blue is sky the")]
    [InlineData("  hello world  ", "world hello")]
    [InlineData("a good   example", "example good a")]
    public void Test1(string s, string expected)
    {
        var result = ReverseWords1(s);
        Assert.Equal(expected, result);
    }

    public string ReverseWords1(string s)
    {
        var words = s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Reverse();
        return string.Join(" ", words);
    }

    public string ReverseWords(string s)
    {
        var sb = new StringBuilder();
        var wasSpace = true;
        var lastIdx = s.Length - 1;
        for (int i = s.Length - 1; i > -1; i--)
        {
            if (s[i] == ' ')
            {
                if (wasSpace)
                {
                    lastIdx--;
                    continue; // дубль пробела
                }

                for (var j = i + 1; j < lastIdx + 1; j++)
                {
                    sb.Append(s[j]);
                }
                wasSpace = true;
                lastIdx = i - 1;
            }
            else
            {
                if (wasSpace)
                {
                    lastIdx = i;
                    wasSpace = false;
                    if (sb.Length > 0)
                    {
                        sb.Append(' ');
                    }
                }
            }
        }

        for (int i = 0; i < lastIdx + 1; i++)
        {
            sb.Append(s[i]);
        }
        return sb.ToString();
    }
}
