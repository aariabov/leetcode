namespace Tests.HashTable.HashMap;

/// <summary>
/// Найти два числа в массиве, сумма которых равна таргету https://leetcode.com/explore/learn/card/hash-table/184/comparison-with-other-data-structures/1115/
/// </summary>
public class TwoSumTests
{
    [Theory]
    [InlineData(new[] { 2,7,11,15 }, 9, new[] { 0,1 })]
    [InlineData(new[] { 3,2,4 }, 6, new[] { 1, 2 })]
    [InlineData(new[] { 3,3 }, 6, new[] { 0,1 })]
    public void Test(int[] nums, int target, int[] expected)
    {
        var result = TwoSum(nums, target);
        Assert.Equal(expected, result);
    }
    
    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];

            if (map.ContainsKey(complement))
            {
                return new int[] { map[complement], i };
            }

            if (!map.ContainsKey(nums[i]))
            {
                map.Add(nums[i], i);
            }
        }

        return Array.Empty<int>(); // На случай, если решения нет (но по условию оно всегда есть)
    }
    
    public int[] TwoSum1(int[] nums, int target)
    {
        var dict = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            dict.TryAdd(nums[i], i);
        }
        
        for (int i = 0; i < nums.Length; i++)
        {
            var num = target - nums[i];
            if (dict.TryGetValue(num, out var value) && value != i)
            {
                return new[] { i, value };
            }
        }

        return new[] { 0, 0 };
    }
}