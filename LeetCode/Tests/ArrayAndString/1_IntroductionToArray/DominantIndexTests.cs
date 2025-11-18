namespace Tests.ArrayAndString.IntroductionToArray;

/// <summary>
/// Существует ли элемент, который является максимальным и в 2 раза больше остальных https://leetcode.com/explore/learn/card/array-and-string/201/introduction-to-array/1147/
/// </summary>
public class DominantIndexTests
{
    [Theory]
    [InlineData(new int[] { 3, 6, 1, 0 }, 1)]
    [InlineData(new int[] { 1, 2, 3, 4 }, -1)]
    public void Test(int[] nums, int expected)
    {
        var result = DominantIndex(nums);
        Assert.Equal(expected, result);
    }

    public int DominantIndex(int[] nums)
    {
        var maxIdx = 0;
        var maxVal = nums[0];
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] > maxVal)
            {
                maxIdx = i;
                maxVal = nums[i];
            }
        }

        for (int i = 0; i < nums.Length; i++)
        {
            if (i != maxIdx && nums[i] * 2 > maxVal)
            {
                return -1;
            }
        }

        return maxIdx;
    }
}
