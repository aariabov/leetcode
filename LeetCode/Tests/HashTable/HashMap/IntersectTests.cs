namespace Tests.HashTable.HashMap;

/// <summary>
/// Пересечение двух массивов https://leetcode.com/explore/learn/card/hash-table/184/comparison-with-other-data-structures/1178/
/// </summary>
public class IntersectTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 2, 1 }, new int[] { 2, 2 }, new int[] { 2, 2 })]
    [InlineData(new int[] { 4, 9, 5 }, new int[] { 9, 4, 9, 8, 4 }, new int[] { 9, 4 })]
    public void Test1(int[] nums1, int[] nums2, int[] expected)
    {
        var result = Intersect(nums1, nums2);
        Assert.Equal(expected, result);
    }
    
    public int[] Intersect3(int[] nums1, int[] nums2)
    {
        Array.Sort(nums1);
        Array.Sort(nums2);

        int i = 0, j = 0;
        List<int> result = new List<int>();

        while (i < nums1.Length && j < nums2.Length)
        {
            if (nums1[i] < nums2[j])
                i++;
            else if (nums1[i] > nums2[j])
                j++;
            else
            {
                result.Add(nums1[i]);
                i++;
                j++;
            }
        }

        return result.ToArray();
    }
    
    // эффективнее, т.к словать заполняется из меньшего массива
    public int[] Intersect(int[] nums1, int[] nums2)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();
        List<int> result = new List<int>();

        // Всегда удобнее заполнять словарь из меньшего массива
        if (nums1.Length > nums2.Length)
        {
            return Intersect(nums2, nums1);
        }

        foreach (int num in nums1)
        {
            if (map.ContainsKey(num))
                map[num]++;
            else
                map[num] = 1;
        }

        foreach (int num in nums2)
        {
            if (map.ContainsKey(num) && map[num] > 0)
            {
                result.Add(num);
                map[num]--;
            }
        }

        return result.ToArray();
    }
    
    // через массивы
    public int[] Intersect1(int[] nums1, int[] nums2)
    {
        var res = new List<int>();
        var dict = new Dictionary<int, HashSet<int>>();
        for (int i = 0; i < nums1.Length; i++)
        {
            if (dict.ContainsKey(nums1[i]))
            {
                dict[nums1[i]].Add(i);
            }
            else
            {
                var hashSet = new HashSet<int> { i };
                dict[nums1[i]] = hashSet;
            }
        }

        for (int i = 0; i < nums2.Length; i++)
        {
            if (dict.ContainsKey(nums2[i]) && dict[nums2[i]].Count > 0)
            {
                res.Add(nums2[i]);
                int toRemove = dict[nums2[i]].First(); // взять любой элемент
                dict[nums2[i]].Remove(toRemove);
            }
        }

        return res.ToArray();
    }
}