namespace Tests.BinarySearch.MorePractices2;

// [Медиана двух отсортированных массивов](https://leetcode.com/explore/learn/card/binary-search/146/more-practices-ii/1040/)
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
        var idx = (nums1.Length + nums2.Length) / 2;
        var isOdd = (nums1.Length + nums2.Length) % 2 == 1;
        var merged = new int[idx + 2];
        var i = 0;
        var p1 = 0;
        var p2 = 0;
        while (true)
        {
            if (p1 + p2 > idx)
            {
                if (isOdd)
                {
                    return merged[i - 1];
                }

                var first = merged[i - 1];
                var second = merged[i - 2];
                return (double)(first + second) / 2;
            }

            if (p1 < nums1.Length && (p2 == nums2.Length || nums1[p1] < nums2[p2]))
            {
                merged[i] = nums1[p1];
                p1++;
            }
            else
            {
                merged[i] = nums2[p2];
                p2++;
            }

            i++;
        }
    }
}
