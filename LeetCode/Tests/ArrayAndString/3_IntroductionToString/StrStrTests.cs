namespace Tests.ArrayAndString._3_IntroductionToString;

/// <summary>
/// Поиск подстроки в строке https://leetcode.com/explore/learn/card/array-and-string/203/introduction-to-string/1161/
/// </summary>
public class StrStrTests
{
    [Theory]
    [InlineData("sadbutsad", "sad", 0)]
    [InlineData("leetcode", "leeto", -1)]
    [InlineData("a", "a", 0)]
    public void Test(string a, string b, int expected)
    {
        var result = StrStr(a, b);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("sadbutsad", "sad", 0)]
    [InlineData("leetcode", "leeto", -1)]
    [InlineData("a", "a", 0)]
    public void Test1(string a, string b, int expected)
    {
        var result = StrStr1(a, b);
        Assert.Equal(expected, result);
    }

    public int StrStr1(string haystack, string needle)
    {
        return haystack.IndexOf(needle);
    }

    [Theory]
    [InlineData("sadbutsad", "sad", 0)]
    [InlineData("leetcode", "leeto", -1)]
    [InlineData("a", "a", 0)]
    [InlineData("abababcd", "ababcd", 2)]
    [InlineData("abcababab", "ababab", 3)]
    [InlineData("aaaaaaaaaaaaaaaaaaaaab", "aaaab", 17)]
    public void Test2(string a, string b, int expected)
    {
        var result = StrStr2(a, b);
        Assert.Equal(expected, result);
    }

    // какая-то замудреная функция, но суть в том, чтоб не проверять начало подстроки
    public int StrStr2(string haystack, string needle)
    {
        if (needle.Length == 0)
            return 0;

        // Построение префикс-функции (LPS — longest prefix suffix)
        int[] lps = BuildLps(needle);

        int i = 0; // индекс в haystack
        int j = 0; // индекс в needle

        while (i < haystack.Length)
        {
            if (haystack[i] == needle[j])
            {
                i++;
                j++;

                if (j == needle.Length)
                    return i - j; // нашли совпадение
            }
            else
            {
                if (j > 0)
                {
                    j = lps[j - 1]; // откат назад по префикс-функции
                }
                else
                {
                    i++; // просто двигаемся дальше
                }
            }
        }

        return -1; // не найдено
    }

    [Theory]
    [InlineData("ababcd", new[] { 0, 0, 1, 2, 0, 0 })]
    [InlineData("abcdab", new[] { 0, 0, 0, 0, 1, 2 })]
    [InlineData("cdabab", new[] { 0, 0, 0, 0, 0, 0 })]
    [InlineData("aaaa", new[] { 0, 1, 2, 3 })]
    [InlineData("ababab", new[] { 0, 0, 1, 2, 3, 4 })]
    public void BuildLpsTest(string a, int[] expected)
    {
        var result = BuildLps(a);
        Assert.Equal(expected, result);
    }

    // функция, которая показывает, сколько символов вналаче можно не проверять
    private int[] BuildLps(string pattern)
    {
        int[] lps = new int[pattern.Length];
        int len = 0;
        int i = 1;

        while (i < pattern.Length)
        {
            if (pattern[i] == pattern[len])
            {
                len++;
                lps[i] = len;
                i++;
            }
            else
            {
                if (len > 0)
                {
                    len = lps[len - 1];
                }
                else
                {
                    lps[i] = 0;
                    i++;
                }
            }
        }

        return lps;
    }

    public int StrStr(string haystack, string needle)
    {
        for (int i = 0; i < haystack.Length; i++)
        {
            if (haystack[i] == needle[0])
            {
                if (needle.Length == 1)
                {
                    return i;
                }

                for (int j = 1; j < needle.Length; j++)
                {
                    if (i + j > haystack.Length - 1)
                    {
                        return -1;
                    }

                    if (haystack[i + j] != needle[j])
                    {
                        break;
                    }

                    if (j == needle.Length - 1)
                    {
                        return i;
                    }
                }
            }
        }
        return -1;
    }
}
