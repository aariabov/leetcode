namespace Tests.ArrayAndString._5_Conclusion;

/// <summary>
/// Прокрутить массив на количество элементов https://leetcode.com/explore/learn/card/array-and-string/204/conclusion/1182/
/// </summary>
public class RotateTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7 }, 3, new[] { 5, 6, 7, 1, 2, 3, 4 })]
    [InlineData(new[] { -1, -100, 3, 99 }, 2, new[] { 3, 99, -1, -100 })]
    public void Test(int[] nums, int k, int[] expected)
    {
        Rotate(nums, k);
        Assert.Equal(expected, nums);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7 }, 3, new[] { 5, 6, 7, 1, 2, 3, 4 })]
    [InlineData(new[] { -1, -100, 3, 99 }, 2, new[] { 3, 99, -1, -100 })]
    [InlineData(new[] { -1 }, 2, new[] { -1 })]
    [InlineData(new[] { 1, 2 }, 7, new[] { 2, 1 })]
    [InlineData(new[] { 1, 2 }, 2, new[] { 1, 2 })]
    public void Test1(int[] nums, int k, int[] expected)
    {
        Rotate1(nums, k);
        Assert.Equal(expected, nums);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7 }, 3, new[] { 5, 6, 7, 1, 2, 3, 4 })]
    [InlineData(new[] { -1, -100, 3, 99 }, 2, new[] { 3, 99, -1, -100 })]
    [InlineData(new[] { -1 }, 2, new[] { -1 })]
    [InlineData(new[] { 1, 2 }, 7, new[] { 2, 1 })]
    [InlineData(new[] { 1, 2 }, 2, new[] { 1, 2 })]
    [InlineData(new[] { 1, 2, 3 }, 2, new[] { 2, 3, 1 })]
    [InlineData(new[] { -1, -100, 3, 99 }, 3, new[] { -100, 3, 99, -1 })]
    public void Test2(int[] nums, int k, int[] expected)
    {
        Rotate2(nums, k);
        Assert.Equal(expected, nums);
    }

    public void Rotate2(int[] nums, int k)
    {
        if (k == 0 || nums.Length == 1 || k == nums.Length)
        {
            return;
        }

        if (k > nums.Length)
        {
            k = k % nums.Length;
        }

        Reverse(0, nums.Length - 1);
        Reverse(0, k - 1);
        Reverse(k, nums.Length - 1);

        void Reverse(int left, int right)
        {
            while (left < right)
            {
                (nums[left], nums[right]) = (nums[right], nums[left]);
                left++;
                right--;
            }
        }
    }

    public void Rotate1(int[] nums, int k)
    {
        if (k == 0 || nums.Length == 1)
        {
            return;
        }

        var x = k > nums.Length ? k % nums.Length : k;
        var last = new int[x];
        var j = nums.Length - 1;
        var l = x - 1;
        do
        {
            last[l] = nums[j];
            l--;
            j--;
        } while (l > -1);

        for (var i = nums.Length - 1 - x; i > -1; i--)
        {
            nums[i + x] = nums[i];
        }

        for (int m = 0; m < last.Length; m++)
        {
            nums[m] = last[m];
        }
    }

    public void Rotate(int[] nums, int k)
    {
        if (k == 0)
        {
            return;
        }

        do
        {
            var last = nums[nums.Length - 1];
            for (int i = nums.Length - 2; i > -1; i--)
            {
                nums[i + 1] = nums[i];
            }
            nums[0] = last;
            k--;
        } while (k > 0);
    }
}
