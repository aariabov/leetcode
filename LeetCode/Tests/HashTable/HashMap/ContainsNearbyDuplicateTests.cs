namespace Tests.HashTable.HashMap;

/// <summary>
/// Существуют ли дубли, для индексов которых abs(i - j) <= k https://leetcode.com/explore/learn/card/hash-table/184/comparison-with-other-data-structures/1121/
/// </summary>
public class ContainsNearbyDuplicateTests
{
    [Theory]
    [InlineData(new[] { 1,2,3,1 }, 3, true)]
    [InlineData(new[] { 1,0,1,1 }, 1, true)]
    [InlineData(new[] { 1,2,3,1,2,3 }, 2, false)]
    public void Test(int[] nums, int k, bool expected)
    {
        var res = ContainsNearbyDuplicate(nums, k);
        Assert.Equal(expected, res);
    }
    
    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        var dict = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (dict.TryGetValue(nums[i], out int value))
            {
                if (Math.Abs(i - value) <= k)
                {
                    return true;
                }

                dict[nums[i]] = i;
            }
            else
            {
                dict[nums[i]] = i;
            }
        }

        return false;
    }
}