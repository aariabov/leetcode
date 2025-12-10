namespace Tests.HashTable.HashMap;

/// <summary>
/// Вернуть первый уникальный индекс в строке https://leetcode.com/explore/learn/card/hash-table/184/comparison-with-other-data-structures/1120/
/// </summary>
public class FirstUniqCharTests
{
    [Theory]
    [InlineData("leetcode", 0)]
    [InlineData("loveleetcode", 2)]
    [InlineData("aabb", -1)]
    public void Test(string a, int expected)
    {
        var result = FirstUniqChar(a);
        Assert.Equal(expected, result);
    }
    
    public int FirstUniqChar(string s)
    {
        Dictionary<char, int> freq = new Dictionary<char, int>();

        // Первый проход — считаем количество вхождений каждого символа
        foreach (char c in s)
        {
            if (freq.ContainsKey(c))
                freq[c]++;
            else
                freq[c] = 1;
        }

        // Второй проход — ищем первый символ с частотой 1
        for (int i = 0; i < s.Length; i++)
        {
            if (freq[s[i]] == 1)
                return i;
        }

        return -1;
    }
}