namespace Tests.BinarySearch.TemplateAnalysis;

// [Медиана двух отсортированных массивов](https://leetcode.com/problems/median-of-two-sorted-arrays/description/)
public class FindMedianSortedArraysTests
{
    [Theory]
    [InlineData(new int[] { 1, 3 }, new int[] { 2 }, 2)]
    [InlineData(new int[] { 1, 2 }, new int[] { 3, 4 }, 2.5)]
    [InlineData(new int[] { }, new int[] { 1 }, 1)]
    [InlineData(new int[] { 2, 2, 4, 4 }, new int[] { 2, 2, 2, 4, 4 }, 2)]
    public void Test1(int[] nums1, int[] nums2, double expected)
    {
        var result = FindMedianSortedArrays(nums1, nums2);
        Assert.Equal(expected, result);
    }

    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        // всегда бинарим по меньшему массиву
        if (nums1.Length > nums2.Length)
            return FindMedianSortedArrays(nums2, nums1);

        int m = nums1.Length;
        int n = nums2.Length;

        int left = 0,
            right = m;

        while (left <= right)
        {
            int i = (left + right) / 2;
            int j = (m + n + 1) / 2 - i;

            int maxLeft1 = (i == 0) ? int.MinValue : nums1[i - 1];
            int minRight1 = (i == m) ? int.MaxValue : nums1[i];

            int maxLeft2 = (j == 0) ? int.MinValue : nums2[j - 1];
            int minRight2 = (j == n) ? int.MaxValue : nums2[j];

            if (maxLeft1 <= minRight2 && maxLeft2 <= minRight1)
            {
                // нашли правильный разрез
                if ((m + n) % 2 == 0)
                {
                    return (Math.Max(maxLeft1, maxLeft2) + Math.Min(minRight1, minRight2)) / 2.0;
                }
                else
                {
                    return Math.Max(maxLeft1, maxLeft2);
                }
            }
            else if (maxLeft1 > minRight2)
            {
                right = i - 1;
            }
            else
            {
                left = i + 1;
            }
        }

        throw new ArgumentException("Input arrays are not valid.");
    }

    // работает, но медленно
    public double MyFindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        var res = nums1.Concat(nums2).OrderBy(x => x).ToArray();
        var med = GetMedian(res);
        return med;

        double GetMedian(int[] arr)
        {
            if (arr.Length == 0)
            {
                return 0;
            }

            double med;
            if (arr.Length % 2 == 1)
            {
                var idx = (arr.Length - 1) / 2;
                med = arr[idx];
            }
            else
            {
                var idx1 = (arr.Length - 1) / 2;
                var val1 = arr[idx1];
                var val2 = arr[idx1 + 1];
                med = (double)(val1 + val2) / 2;
            }
            return med;
        }
    }
}
