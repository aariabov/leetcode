using FluentAssertions;

namespace Tests.HashTable.DesignKey;

/// <summary>
/// Сгруппировать анаграммы https://leetcode.com/explore/learn/card/hash-table/185/hash_table_design_the_key/1124/
/// </summary>
public class GroupAnagramsTests
{
    [Fact]
    public void Test()
    {
        var expected = new List<List<string>> { new() { "eat", "tea", "ate" }, new() { "tan", "nat" }, new() { "bat" } };
        var result = GroupAnagrams(["eat","tea","tan","ate","nat","bat"]);
        result.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Test1()
    {
        var expected = new List<List<string>> { new() { "" } };
        var result = GroupAnagrams([""]);
        result.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Test2()
    {
        var expected = new List<List<string>> { new() { "a" } };
        var result = GroupAnagrams(["a"]);
        result.Should().BeEquivalentTo(expected);
    }
    
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        var dict = new Dictionary<string, IList<string>>();
        foreach (var str in strs)
        {
            char[] chars = str.ToCharArray();
            Array.Sort(chars);
            string key = new string(chars);
            if (!dict.ContainsKey(key))
            {
                dict[key] = new List<string>();
            }
            
            dict[key].Add(str);
        }

        return new List<IList<string>>(dict.Values);
    }
}