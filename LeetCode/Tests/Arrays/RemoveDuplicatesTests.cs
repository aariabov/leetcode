namespace Tests;

/// <summary>
/// Удалить дубликаты элементов https://leetcode.com/explore/learn/card/fun-with-arrays/526/deleting-items-from-an-array/3248/
/// </summary>
public class RemoveDuplicatesTests
{
    [Theory]
    [InlineData(new int[] { 1, 1, 2 }, 2, new int[] { 1, 2, 0 })]
    [InlineData(
        new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 },
        5,
        new int[] { 0, 1, 2, 3, 4, 0, 0, 0, 0, 0 }
    )]
    public void Test1(int[] nums, int expectedK, int[] expectedArr)
    {
        var k = RemoveDuplicates(nums);
        Assert.Equal(expectedK, k);
        Assert.Equal(expectedArr.Take(expectedK).OrderBy(x => x), nums.Take(k).OrderBy(x => x));
    }

    public int RemoveDuplicates(int[] nums)
    {
        int k = 1;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] != nums[k - 1])
            {
                nums[k] = nums[i];
                k++;
            }
        }
        return k;
    }
}
