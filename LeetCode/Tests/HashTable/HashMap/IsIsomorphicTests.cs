namespace Tests.HashTable.HashMap;

/// <summary>
/// Изоморфны ли строки https://leetcode.com/explore/learn/card/hash-table/184/comparison-with-other-data-structures/1117/
/// </summary>
public class IsIsomorphicTests
{
    [Theory]
    [InlineData("egg", "add", true)]
    [InlineData("foo", "bar", false)]
    [InlineData("paper", "title", true)]
    [InlineData("badc", "baba", false)]
    public void Test(string a, string b, bool expected)
    {
        var result = IsIsomorphic(a, b);
        Assert.Equal(expected, result);
    }
    
    public bool IsIsomorphic(string s, string t)
    {
        if (s.Length != t.Length)
            return false;

        Dictionary<char, char> mapST = new Dictionary<char, char>();
        Dictionary<char, char> mapTS = new Dictionary<char, char>();

        for (int i = 0; i < s.Length; i++)
        {
            char a = s[i];
            char b = t[i];

            // Если уже есть соответствие s → t, проверяем что оно совпадает
            if (mapST.ContainsKey(a))
            {
                if (mapST[a] != b)
                    return false;
            }
            else
            {
                mapST[a] = b;
            }

            // И обратное соответствие t → s
            if (mapTS.ContainsKey(b))
            {
                if (mapTS[b] != a)
                    return false;
            }
            else
            {
                mapTS[b] = a;
            }
        }

        return true;
    }
    
    public bool IsIsomorphic1(string s, string t)
    {
        var dict = new Dictionary<char, char>();
        var dict1 = new Dictionary<char, char>();
        for (int i = 0; i < s.Length; i++)
        {
            dict.TryAdd(s[i], t[i]);
            dict1.TryAdd(t[i], s[i]);
            
            if (!dict.ContainsKey(s[i]) || dict[s[i]] != t[i] || !dict1.ContainsKey(t[i]) || dict1[t[i]] != s[i])
            {
                return false;
            }
        }

        return true;
    }
}