namespace Tests.BinarySearch.Template2;

// [Найти минимум в отсортированном повернутом массиве](https://leetcode.com/explore/learn/card/binary-search/126/template-ii/949/)
public class FindMinTests
{
    [Theory]
    [InlineData(new int[] { 3, 4, 5, 1, 2 }, 1)]
    [InlineData(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 0)]
    [InlineData(new int[] { 11, 13, 15, 17 }, 11)]
    [InlineData(new int[] { 2, 3, 4, 5, 1 }, 1)]
    [InlineData(new int[] { 5, 1, 2, 3, 4 }, 1)]
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

            // Если средний элемент больше самого правого,
            // значит минимум точно находится справа от mid
            if (nums[mid] > nums[right])
            {
                left = mid + 1;
            }
            // В противном случае минимум находится слева или это сам mid
            else
            {
                right = mid;
            }
        }

        return nums[left];
    }

    // работает и быстро
    public int MyFindMin(int[] nums)
    {
        int left = 0;
        int right = nums.Length - 1;

        while (left < right)
        {
            var mid = left + (right - left + 1) / 2;
            var prev = mid == 0 ? nums[^1] : nums[mid - 1];
            var cur = nums[mid];
            var next = mid == nums.Length - 1 ? nums[0] : nums[mid + 1];

            if (prev > cur && cur < next)
            {
                return cur;
            }

            if (nums[left] < nums[right])
            {
                right = mid - 1;
            }
            else if (nums[left] < cur)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return nums[left];
    }
}
