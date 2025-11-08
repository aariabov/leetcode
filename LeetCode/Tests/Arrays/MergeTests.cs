namespace Tests;

/// <summary>
/// Смержить 2 массива
/// </summary>
public class MergeTests
{
    [Theory]
    [InlineData(
        new int[] { 1, 2, 3, 0, 0, 0 },
        3,
        new int[] { 2, 5, 6 },
        3,
        new int[] { 1, 2, 2, 3, 5, 6 }
    )]
    [InlineData(new int[] { 1 }, 1, new int[] { }, 0, new int[] { 1 })]
    [InlineData(new int[] { 0 }, 0, new int[] { 1 }, 1, new int[] { 1 })]
    public void Test(int[] nums1, int m, int[] nums2, int n, int[] expected)
    {
        Merge(nums1, m, nums2, n);
        Assert.Equal(expected, nums1);
    }

    [Theory]
    [InlineData(
        new int[] { 1, 2, 3, 0, 0, 0 },
        3,
        new int[] { 2, 5, 6 },
        3,
        new int[] { 1, 2, 2, 3, 5, 6 }
    )]
    [InlineData(new int[] { 1 }, 1, new int[] { }, 0, new int[] { 1 })]
    [InlineData(new int[] { 0 }, 0, new int[] { 1 }, 1, new int[] { 1 })]
    public void Test1(int[] nums1, int m, int[] nums2, int n, int[] expected)
    {
        Merge1(nums1, m, nums2, n);
        Assert.Equal(expected, nums1);
    }

    // проще логика, но медленнее работает
    public void Merge1(int[] nums1, int m, int[] nums2, int n)
    {
        var arr1 = new int[m];
        Array.Copy(nums1, 0, arr1, 0, m);
        Array.Copy(arr1.Concat(nums2).Order().ToArray(), nums1, nums1.Length);
    }

    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        var result = new int[nums1.Length];
        int i = 0;
        int i1 = 0;
        int i2 = 0;
        while (i < nums1.Length)
        {
            if (i2 >= nums2.Length)
            {
                result[i] = nums1[i1];
                i1++;
                i++;
                continue;
            }

            if (i1 >= m)
            {
                result[i] = nums2[i2];
                i2++;
                i++;
                continue;
            }

            if (nums2[i2] < nums1[i1])
            {
                result[i] = nums2[i2];
                i2++;
            }
            else
            {
                result[i] = nums1[i1];
                i1++;
            }

            i++;
        }

        Array.Copy(result, nums1, nums1.Length);
    }
}
