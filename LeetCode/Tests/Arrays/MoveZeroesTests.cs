namespace Tests;

/// <summary>
/// Сдвинуть все нули в конец https://leetcode.com/explore/learn/card/fun-with-arrays/511/in-place-operations/3157/
/// </summary>
public class MoveZeroesTests
{
    [Theory]
    [InlineData(new int[] { 0, 1, 0, 3, 12 }, new int[] { 1, 3, 12, 0, 0 })]
    [InlineData(new int[] { 0 }, new int[] { 0 })]
    public void Test(int[] arr, int[] expected)
    {
        MoveZeroes(arr);
        Assert.Equal(expected, arr);
    }

    public void MoveZeroes(int[] nums)
    {
        var writeIdx = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0)
            {
                nums[writeIdx] = nums[i];
                writeIdx++;
            }
        }

        for (int i = nums.Length - 1; i > writeIdx - 1; i--)
        {
            nums[i] = 0;
        }
    }
}
