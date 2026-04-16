namespace Tests.BinarySearch.Template2;

// [Найти пик в массиве](https://leetcode.com/explore/learn/card/binary-search/126/template-ii/948/)
public class FindPeakElementTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3, 1 }, 2)]
    [InlineData(new int[] { 1, 2, 1, 3, 5, 6, 4 }, 5)]
    [InlineData(new int[] { 1 }, 0)]
    [InlineData(new int[] { 2, 1 }, 0)]
    [InlineData(new int[] { 1, 2, 1, 3, 2 }, 3)]
    [InlineData(new int[] { 1, 2, 3, 4, 3 }, 3)]
    public void Test(int[] nums, int expected)
    {
        var result = FindPeakElement(nums);
        Assert.Equal(expected, result);
    }

    public int FindPeakElement(int[] nums)
    {
        int left = 0;
        int right = nums.Length - 1;

        while (left < right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] < nums[mid + 1])
            {
                // Мы идем в гору, пик справа
                left = mid + 1;
            }
            else
            {
                // Мы идем с горы, пик слева (включая mid)
                right = mid;
            }
        }

        return left;
    }
}
