using System.Text;

namespace Tests.ArrayAndString._5_Conclusion;

/// <summary>
/// Реверс букв во всех словах строки https://leetcode.com/explore/learn/card/array-and-string/204/conclusion/1165/
/// </summary>
public class ReverseWords2Tests
{
    [Theory]
    [InlineData("Let's take LeetCode contest", "s'teL ekat edoCteeL tsetnoc")]
    [InlineData("Mr Ding", "rM gniD")]
    public void Test(string s, string expected)
    {
        var result = ReverseWords(s);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Let's take LeetCode contest", "s'teL ekat edoCteeL tsetnoc")]
    [InlineData("Mr Ding", "rM gniD")]
    public void Test1(string s, string expected)
    {
        var result = ReverseWords1(s);
        Assert.Equal(expected, result);
    }

    public string ReverseWords1(string s)
    {
        var words = s.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            char[] arr = words[i].ToCharArray();
            Array.Reverse(arr);
            words[i] = new string(arr);
        }
        return string.Join(" ", words);
    }

    public string ReverseWords(string s)
    {
        // найти начало и конец слова
        // перевернуть его
        var leftIdx = 0;
        var rightIdx = 0;
        var sb = new StringBuilder();
        while (rightIdx < s.Length)
        {
            if (s[rightIdx] == ' ' || rightIdx == s.Length - 1)
            {
                for (
                    int i = rightIdx == s.Length - 1 ? rightIdx : rightIdx - 1;
                    i > leftIdx - 1;
                    i--
                )
                {
                    sb.Append(s[i]);
                }

                if (rightIdx != s.Length - 1)
                {
                    sb.Append(" ");
                }
                leftIdx = rightIdx + 1;
            }
            rightIdx++;
        }
        return sb.ToString();
    }
}
