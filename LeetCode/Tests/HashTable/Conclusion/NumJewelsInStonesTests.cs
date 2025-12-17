namespace Tests.HashTable.Conclusion;

/// <summary>
/// Поиск символов в строке https://leetcode.com/explore/learn/card/hash-table/187/conclusion-hash-table/1136/
/// </summary>
public class NumJewelsInStonesTests
{
    [Theory]
    [InlineData("aA", "aAAbbbb", 3)]
    [InlineData("z", "ZZ", 0)]
    public void Test(string a, string b, int expected)
    {
        var result = NumJewelsInStones(a, b);
        Assert.Equal(expected, result);
    }
    
    public int NumJewelsInStones(string jewels, string stones)
    {
        var result = 0;
        var hashSet = new HashSet<char>(jewels);

        foreach (var stone in stones)
        {
            if (hashSet.Contains(stone))
            {
                result++;
            }
        }

        return result;
    }
}