namespace Tests.BinarySearch.Template3;

// [Найти диапазон](https://leetcode.com/explore/learn/card/binary-search/135/template-iii/944/)
public class SearchRangeTests
{
    [Theory]
    [InlineData(new int[] { 5, 7, 7, 8, 8, 10 }, 8, new[] { 3, 4 })]
    [InlineData(new int[] { 5, 7, 7, 8, 8, 10 }, 6, new[] { -1, -1 })]
    [InlineData(new int[] { 2, 2 }, 1, new[] { -1, -1 })]
    [InlineData(new int[] { 2, 2 }, 2, new[] { 0, 1 })]
    [InlineData(new int[] { 1 }, 1, new[] { 0, 0 })]
    public void Test1(int[] nums, int target, int[] expected)
    {
        var result = SearchRange(nums, target);
        Assert.Equal(expected, result);
    }

    public int[] SearchRange(int[] nums, int target)
    {
        int left = FindLeft(nums, target);
        int right = FindRight(nums, target);

        return new int[] { left, right };
    }

    private int FindLeft(int[] nums, int target)
    {
        int left = 0,
            right = nums.Length - 1;
        int result = -1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] == target)
            {
                result = mid;
                right = mid - 1; // идем влево
            }
            else if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return result;
    }

    private int FindRight(int[] nums, int target)
    {
        int left = 0,
            right = nums.Length - 1;
        int result = -1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] == target)
            {
                result = mid;
                left = mid + 1; // идем вправо
            }
            else if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return result;
    }
}
