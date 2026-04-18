namespace Tests.BinarySearch.MorePractices;

// [Найти минимум в отсортированном повернутом массиве с дублями](https://leetcode.com/explore/learn/card/binary-search/144/more-practices/1031/)
public class FindMinTests
{
    [Theory]
    [InlineData(new int[] { 1, 3, 5 }, 1)]
    [InlineData(new int[] { 2, 2, 2, 0, 1 }, 0)]
    [InlineData(new int[] { 3, 3, 1, 3 }, 1)]
    [InlineData(new int[] { 1, 3, 3 }, 1)]
    [InlineData(new int[] { 1 }, 1)]
    [InlineData(new int[] { 10, 1, 10, 10, 10 }, 1)]
    public void Test(int[] nums, int expected)
    {
        var result = FindMin(nums);
        Assert.Equal(expected, result);
    }

    public int FindMin(int[] nums)
    {
        int left = 0;
        int right = nums.Length - 1;

        while (left < right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] < nums[right])
            {
                right = mid;
            }
            else if (nums[mid] > nums[right])
            {
                left = mid + 1;
            }
            else
            {
                right--; // убираем дубликат
            }
        }

        return nums[left];
    }

    // работает и быстро
    public int MyFindMin(int[] nums)
    {
        if (nums[0] < nums[^1] || nums.Length == 1)
        {
            return nums[0];
        }

        return Rec(0, nums.Length - 1);

        int Rec(int left, int right)
        {
            if (left + 1 == right)
            {
                return nums[left] < nums[right] ? nums[left] : nums[right];
            }

            int mid = left + (right - left) / 2;
            var minLeft = Rec(left, mid);
            var minRight = Rec(mid, right);
            return minLeft < minRight ? minLeft : minRight;
        }
    }
}
