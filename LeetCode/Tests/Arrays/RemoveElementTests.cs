namespace Tests;

/// <summary>
/// Удалить определенные значения из массива со сдвигом https://leetcode.com/explore/learn/card/fun-with-arrays/526/deleting-items-from-an-array/3247/
/// </summary>
public class RemoveElementTests
{
    [Theory]
    [InlineData(new int[] { 3, 2, 2, 3 }, 3, 2, new int[] { 2, 2, 0, 0 })]
    [InlineData(new int[] { 0, 1, 2, 2, 3, 0, 4, 2 }, 2, 5, new int[] { 0, 1, 4, 0, 3, 0, 0, 0 })]
    public void Test(int[] nums, int val, int expectedK, int[] expectedArr)
    {
        var k = RemoveElement(nums, val);
        Assert.Equal(expectedK, k);
        Assert.Equal(expectedArr.Take(expectedK).OrderBy(x => x), nums.Take(k).OrderBy(x => x));
    }

    [Theory]
    [InlineData(new int[] { 3, 2, 2, 3 }, 3, 2, new int[] { 2, 2, 0, 0 })]
    [InlineData(new int[] { 0, 1, 2, 2, 3, 0, 4, 2 }, 2, 5, new int[] { 0, 1, 4, 0, 3, 0, 0, 0 })]
    public void Test1(int[] nums, int val, int expectedK, int[] expectedArr)
    {
        var k = RemoveElement1(nums, val);
        Assert.Equal(expectedK, k);
        Assert.Equal(expectedArr.Take(expectedK).OrderBy(x => x), nums.Take(k).OrderBy(x => x));
    }

    public int RemoveElement1(int[] nums, int val)
    {
        int k = 0; // количество элементов != val
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != val)
            {
                nums[k] = nums[i];
                k++;
            }
        }
        return k;
    }

    private int RemoveElement(int[] nums, int val)
    {
        var k = 0;
        var i = 0;
        while (i < nums.Length)
        {
            if (nums[i] == val)
            {
                k += 1;
            }
            else if (k > 0)
            {
                nums[i - k] = nums[i];
            }

            i++;
        }
        return nums.Length - k;
    }
}
