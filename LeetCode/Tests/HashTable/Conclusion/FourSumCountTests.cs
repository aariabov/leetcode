namespace Tests.ArrayAndString._5_Conclusion;

/// <summary>
/// Сумма элементов 4х массивов https://leetcode.com/explore/learn/card/hash-table/187/conclusion-hash-table/1134/
/// </summary>
public class FourSumCountTests
{
    [Theory]
    [InlineData(new int[] { 1,2 }, new int[] { -2,-1 }, new int[] { -1,2 }, new int[] { 0,2 }, 2)]
    [InlineData(new int[] { 0 }, new int[] { 0 }, new int[] { 0 }, new int[] { 0 }, 1)]
    public void Test1(int[] nums1, int[] nums2, int[] nums3, int[] nums4, int expected)
    {
        var result = FourSumCount(nums1, nums2, nums3, nums4);
        Assert.Equal(expected, result);
    }
    
    // правильно и быстро работает, On2
    public int FourSumCount(int[] nums1, int[] nums2, int[] nums3, int[] nums4)
    {
        var sumMap = new Dictionary<int, int>();
        int count = 0;

        // Считаем суммы nums1 + nums2
        foreach (int a in nums1)
        {
            foreach (int b in nums2)
            {
                int sum = a + b;
                if (sumMap.ContainsKey(sum))
                    sumMap[sum]++;
                else
                    sumMap[sum] = 1;
            }
        }

        // Ищем противоположные суммы nums3 + nums4
        foreach (int c in nums3)
        {
            foreach (int d in nums4)
            {
                int target = -(c + d);
                if (sumMap.ContainsKey(target))
                    count += sumMap[target];
            }
        }

        return count;
    }
    
    // ниже правильные решения, но не проходят по времени
    public int FourSumCount3(int[] nums1, int[] nums2, int[] nums3, int[] nums4)
    {
        var res = 0;
        var dict1 = new Dictionary<int, int>();
        var dict2 = new Dictionary<int, int>();
        var dict3 = new Dictionary<int, int>();
        var dict4 = new Dictionary<int, int>();

        var minValue = int.MaxValue;
        foreach (var t in nums1)
        {
            if (t < minValue)
            {
                minValue = t;
            }
            if (!dict1.TryAdd(t, 1))
            {
                dict1[t]++;
            }
        }
        foreach (var t in nums2)
        {
            if (t < minValue)
            {
                minValue = t;
            }
            if (!dict2.TryAdd(t, 1))
            {
                dict2[t]++;
            }
        }
        foreach (var t in nums3)
        {
            if (t < minValue)
            {
                minValue = t;
            }
            if (!dict3.TryAdd(t, 1))
            {
                dict3[t]++;
            }
        }
        foreach (var t in nums4)
        {
            if (t < minValue)
            {
                minValue = t;
            }
            if (!dict4.TryAdd(t, 1))
            {
                dict4[t]++;
            }
        }

        var delta = 0 - minValue;
        var target = 4 * delta;
        var n1 = dict1.Keys.OrderBy(k => k).Select(k => k + delta).ToArray();
        var n2 = dict2.Keys.OrderBy(k => k).Select(k => k + delta).ToArray();
        var n3 = dict3.Keys.OrderBy(k => k).Select(k => k + delta).ToArray();
        var n4 = dict4.Keys.OrderBy(k => k).Select(k => k + delta).ToArray();

        foreach (var i1 in n1)
        {
            if (i1 > target)
            {
                break;
            }
            foreach (var i2 in n2)
            {
                if (i2 > target)
                {
                    break;
                }
                foreach (var i3 in n3)
                {
                    if (i3 > target)
                    {
                        break;
                    }
                    foreach (var i4 in n4)
                    {
                        if (i1+i2+i3+i4 == target)
                        {
                            res += dict1[i1-delta] * dict2[i2-delta] * dict3[i3-delta] * dict4[i4-delta];
                        }
                    }
                }
            }
        }

        return res;
    }
    
    public int FourSumCount2(int[] nums1, int[] nums2, int[] nums3, int[] nums4)
    {
        var res = 0;
        var dict1 = new Dictionary<int, int>();
        var dict2 = new Dictionary<int, int>();
        var dict3 = new Dictionary<int, int>();
        var dict4 = new Dictionary<int, int>();
        for (int i = 0; i < nums1.Length; i++)
        {
            if (!dict1.ContainsKey(nums1[i]))
            {
                dict1[nums1[i]] = 1;
            }
            else
            {
                dict1[nums1[i]]++;
            }
        }
        for (int i = 0; i < nums2.Length; i++)
        {
            if (!dict2.ContainsKey(nums2[i]))
            {
                dict2[nums2[i]] = 1;
            }
            else
            {
                dict2[nums2[i]]++;
            }
        }
        for (int i = 0; i < nums1.Length; i++)
        {
            if (!dict3.ContainsKey(nums3[i]))
            {
                dict3[nums3[i]] = 1;
            }
            else
            {
                dict3[nums3[i]]++;
            }
        }
        for (int i = 0; i < nums1.Length; i++)
        {
            if (!dict4.ContainsKey(nums4[i]))
            {
                dict4[nums4[i]] = 1;
            }
            else
            {
                dict4[nums4[i]]++;
            }
        }

        
        int minValue = Math.Min(Math.Min(Math.Min(dict1.Keys.Min(), dict2.Keys.Min()), dict3.Keys.Min()), dict4.Keys.Min());
        var delta = 0 - minValue;
        var target = 4 * delta;
        var n1 = dict1.Keys.OrderBy(k => k).Select(k => k + delta).ToArray();
        var n2 = dict2.Keys.OrderBy(k => k).Select(k => k + delta).ToArray();
        var n3 = dict3.Keys.OrderBy(k => k).Select(k => k + delta).ToArray();
        var n4 = dict4.Keys.OrderBy(k => k).Select(k => k + delta).ToArray();

        foreach (var i1 in n1)
        {
            if (i1 > target)
            {
                break;
            }
            foreach (var i2 in n2)
            {
                if (i2 > target)
                {
                    break;
                }
                foreach (var i3 in n3)
                {
                    if (i3 > target)
                    {
                        break;
                    }
                    foreach (var i4 in n4)
                    {
                        if (i1+i2+i3+i4 == target)
                        {
                            res += dict1[i1-delta] * dict2[i2-delta] * dict3[i3-delta] * dict4[i4-delta];
                        }
                    }
                }
            }
        }

        return res;
    }
    
    public int FourSumCount1(int[] nums1, int[] nums2, int[] nums3, int[] nums4)
    {
        var res = 0;
        var dict1 = new Dictionary<int, int>();
        var dict2 = new Dictionary<int, int>();
        var dict3 = new Dictionary<int, int>();
        var dict4 = new Dictionary<int, int>();
        for (int i = 0; i < nums1.Length; i++)
        {
            if (!dict1.ContainsKey(nums1[i]))
            {
                dict1[nums1[i]] = 1;
            }
            else
            {
                dict1[nums1[i]]++;
            }
        }
        for (int i = 0; i < nums2.Length; i++)
        {
            if (!dict2.ContainsKey(nums2[i]))
            {
                dict2[nums2[i]] = 1;
            }
            else
            {
                dict2[nums2[i]]++;
            }
        }
        for (int i = 0; i < nums1.Length; i++)
        {
            if (!dict3.ContainsKey(nums3[i]))
            {
                dict3[nums3[i]] = 1;
            }
            else
            {
                dict3[nums3[i]]++;
            }
        }
        for (int i = 0; i < nums1.Length; i++)
        {
            if (!dict4.ContainsKey(nums4[i]))
            {
                dict4[nums4[i]] = 1;
            }
            else
            {
                dict4[nums4[i]]++;
            }
        }

        var nums11 = new int[dict1.Count];
        var nums22 = new int[dict2.Count];
        var nums33 = new int[dict3.Count];
        var nums44 = new int[dict4.Count];
        Array.Sort(nums11);
        Array.Sort(nums22);
        Array.Sort(nums33);
        Array.Sort(nums44);
        int minValue = Math.Min(Math.Min(Math.Min(nums11.Min(), nums22.Min()), nums33.Min()), nums44.Min());
        var delta = 0 - minValue;
        var target = 4 * delta;
        for (int i = 0; i < nums11.Length; i++)
        {
            var coef1 = dict1[nums11[i]];
            if (nums11[i] + delta > target)
            {
                break;
            }
            for (int j = 0; j < nums22.Length; j++)
            {
                var coef2 = dict2[nums22[i]];
                if (nums11[i] + nums22[j] + 2*delta > target)
                {
                    break;
                }
                for (int k = 0; k < nums33.Length; k++)
                {
                    var coef3 = dict3[nums33[i]];
                    if (nums11[i] + nums22[j] + nums33[k] + 3*delta > target)
                    {
                        break;
                    }
                    for (int l = 0; l < nums44.Length; l++)
                    {
                        var coef4 = dict4[nums44[i]];
                        if (nums11[i] + nums22[j] + nums33[k] + nums44[l] == 0)
                        {
                            res += coef1 * coef2 * coef3 * coef4;
                        }
                    }
                }
            }
        }

        return res;
    }
}