namespace Tests;

/// <summary>
/// Сколько детей стоит не на своем месте https://leetcode.com/explore/learn/card/fun-with-arrays/523/conclusion/3228/
/// </summary>
public class HeightCheckerTests
{
    [Theory]
    [InlineData(new int[] { 1, 1, 4, 2, 1, 3 }, 3, new int[] { 1, 1, 1, 2, 3, 4 })]
    [InlineData(new int[] { 5, 1, 2, 3, 4 }, 5, new int[] { 1, 2, 3, 4, 5 })]
    [InlineData(new int[] { 1, 2, 3, 4, 5 }, 0, new int[] { 1, 2, 3, 4, 5 })]
    public void Test(int[] nums, int expectedK, int[] expectedArr)
    {
        var k = HeightChecker(nums);
        Assert.Equal(expectedK, k);
        Assert.Equal(expectedArr.Take(expectedK).OrderBy(x => x), nums.Take(k).OrderBy(x => x));
    }

    [Theory]
    [InlineData(new int[] { 1, 1, 4, 2, 1, 3 }, 3, new int[] { 1, 1, 1, 2, 3, 4 })]
    [InlineData(new int[] { 5, 1, 2, 3, 4 }, 5, new int[] { 1, 2, 3, 4, 5 })]
    [InlineData(new int[] { 1, 2, 3, 4, 5 }, 0, new int[] { 1, 2, 3, 4, 5 })]
    public void Test1(int[] nums, int expectedK, int[] expectedArr)
    {
        var k = HeightChecker1(nums);
        Assert.Equal(expectedK, k);
        Assert.Equal(expectedArr.Take(expectedK).OrderBy(x => x), nums.Take(k).OrderBy(x => x));
    }

    public int HeightChecker1(int[] heights)
    {
        var result = new int[heights.Length];
        Array.Copy(heights, result, heights.Length);
        Array.Sort(heights);

        var k = 0;
        for (int i = 0; i < heights.Length; i++)
        {
            if (heights[i] != result[i])
            {
                k++;
            }
        }
        return k;
    }

    public int HeightChecker(int[] heights)
    {
        var result = new int[heights.Length];
        Array.Copy(heights, result, heights.Length);
        for (int i = 0; i < heights.Length; i++)
        {
            for (int j = i + 1; j < heights.Length; j++)
            {
                if (heights[i] > heights[j])
                {
                    var temp = heights[i];
                    heights[i] = heights[j];
                    heights[j] = temp;
                }
            }
        }

        var k = 0;
        for (int i = 0; i < heights.Length; i++)
        {
            if (heights[i] != result[i])
            {
                k++;
            }
        }
        return k;
    }
}
