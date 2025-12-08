namespace Tests.HashTable.PracticalApplication;

/// <summary>
/// Contains Duplicate https://leetcode.com/explore/learn/card/hash-table/183/combination-with-other-algorithms/1112/
/// </summary>
public class ContainsDuplicateTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3, 1 }, true)]
    [InlineData(new int[] { 1, 2, 3, 4 }, false)]
    [InlineData(new int[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 }, true)]
    public void Test(int[] arr, bool expected)
    {
        var result = ContainsDuplicate(arr);
        Assert.Equal(expected, result);
    }

    public bool ContainsDuplicate(int[] nums)
    {
        var hashSet = new HashSet<int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (!hashSet.Add(nums[i]))
            {
                return true;
            }
        }
        return false;
    }
}
