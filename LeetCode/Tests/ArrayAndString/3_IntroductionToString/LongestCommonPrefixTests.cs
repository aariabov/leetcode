using System.Text;

namespace Tests.ArrayAndString._3_IntroductionToString;

/// <summary>
/// Найти самый длинные префикс в массиве https://leetcode.com/explore/learn/card/array-and-string/203/introduction-to-string/1162/
/// </summary>
public class LongestCommonPrefixTests
{
    [Theory]
    [InlineData(new[] { "flower", "flow", "flight" }, "fl")]
    [InlineData(new[] { "dog", "racecar", "car" }, "")]
    public void Test(string[] a, string expected)
    {
        var result = LongestCommonPrefix(a);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new[] { "flower", "flow", "flight" }, "fl")]
    [InlineData(new[] { "dog", "racecar", "car" }, "")]
    public void Test1(string[] a, string expected)
    {
        var result = LongestCommonPrefix1(a);
        Assert.Equal(expected, result);
    }

    // чутка побыстрее и меньше по памяти
    public string LongestCommonPrefix1(string[] strs)
    {
        if (strs == null || strs.Length == 0)
            return "";

        // Берём первую строку как базу
        string first = strs[0];

        for (int i = 0; i < first.Length; i++)
        {
            char c = first[i];

            // Проверяем символ i у всех строк
            for (int j = 1; j < strs.Length; j++)
            {
                // Если строка короче или символ не совпал — возвращаем найденный префикс
                if (i == strs[j].Length || strs[j][i] != c)
                {
                    return first.Substring(0, i);
                }
            }
        }

        // Если все символы первой строки совпали
        return first;
    }

    public string LongestCommonPrefix(string[] strs)
    {
        var i = 0;
        var builder = new StringBuilder();
        while (true)
        {
            if (i > strs[0].Length - 1)
            {
                return builder.ToString();
            }

            var chr = strs[0][i];
            for (int j = 0; j < strs.Length; j++)
            {
                if (i > strs[j].Length - 1 || strs[j][i] != chr)
                {
                    return builder.ToString();
                }
            }
            builder.Append(chr);
            i++;
        }
    }
}
