namespace Tests.HashTable.PracticalApplication;

// Пересечение двух массивов https://leetcode.com/explore/learn/card/hash-table/183/combination-with-other-algorithms/1105/
public class IntersectionTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 2, 1 }, new int[] { 2, 2 }, new int[] { 2 })]
    [InlineData(new int[] { 4, 9, 5 }, new int[] { 9, 4, 9, 8, 4 }, new int[] { 9, 4 })]
    public void Test1(int[] nums1, int[] nums2, int[] expected)
    {
        var result = Intersection(nums1, nums2);
        Assert.Equal(expected, result);
    }

    public int[] Intersection(int[] nums1, int[] nums2)
    {
        var res = new HashSet<int>();
        var res1 = new HashSet<int>();
        foreach (var num in nums1)
        {
            res.Add(num);
        }

        foreach (var num in nums2)
        {
            if (res.Contains(num))
            {
                res1.Add(num);
            }
        }

        return res1.ToArray();
    }
}
