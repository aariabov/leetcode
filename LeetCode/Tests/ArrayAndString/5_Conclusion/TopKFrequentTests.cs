namespace Tests.ArrayAndString._5_Conclusion;

/// <summary>
/// Топ часто встречающихся элементов https://leetcode.com/explore/learn/card/hash-table/187/conclusion-hash-table/1133/
/// </summary>
public class TopKFrequentTests
{
    [Theory]
    [InlineData(new[] { 1,1,1,2,2,3 }, 2, new[] { 1,2 })]
    [InlineData(new[] { 1 }, 1, new[] { 1 })]
    [InlineData(new[] { 1,2,1,2,1,2,3,1,3,2 }, 2, new[] { 1,2 })]
    [InlineData(new[] { 3,2,3,1,2,4,5,5,6,7,7,8,2,3,1,1,1,10,11,5,6,2,4,7,8,5,6 }, 10, new[] { 1,2,5,3,6,7,4,8,10,11 })]
    public void Test(int[] nums, int k, int[] expected)
    {
        var result = TopKFrequent(nums, k);
        Array.Sort(expected);
        Array.Sort(result);
        Assert.Equal(expected, result);
    }
    
    // работает быстрее, за счет Bucket Sort
    public int[] TopKFrequent(int[] nums, int k)
    {
        // 1. элемент - сколько раз встречается
        Dictionary<int, int> freq = new Dictionary<int, int>();
        foreach (int num in nums)
        {
            if (!freq.ContainsKey(num))
                freq[num] = 0;
            freq[num]++;
        }

        // 2. частота - список элементов (Bucket Sort, уже отсортировано по частоте, т.к есть индекс)
        List<int>[] buckets = new List<int>[nums.Length + 1];
        foreach (var pair in freq)
        {
            int count = pair.Value;
            if (buckets[count] == null)
                buckets[count] = new List<int>();
            buckets[count].Add(pair.Key);
        }

        // 3. проходим от конца к началу
        List<int> result = new List<int>();
        for (int i = buckets.Length - 1; i >= 0 && result.Count < k; i--)
        {
            if (buckets[i] != null)
            {
                result.AddRange(buckets[i]);
            }
        }

        return result.ToArray();
    }
    
    public int[] TopKFrequent1(int[] nums, int k)
    {
        // 1. элемент - сколько раз встречается
        var dict = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            if (!dict.TryAdd(num, 1))
            {
                dict[num]++;
            }
        }

        // 2. частота - список элементов (можно было использовать массив, чтоб потом не сортировать)
        var hashSet = new HashSet<int>();
        var countNumDict = new Dictionary<int, List<int>>();
        foreach (var item in dict)
        {
            hashSet.Add(item.Value);
            if (!countNumDict.TryAdd(item.Value, [item.Key]))
            {
                countNumDict[item.Value].Add(item.Key);
            }
        }

        // 3. сортируем по частоте, чтобы потом взять топ
        var list = hashSet.ToArray();
        Array.Sort(list);

        var res = new List<int>(k);
        var j = 0;
        for (int i = list.Length - 1; i > - 1; i--)
        {
            var items = countNumDict[list[i]];
            foreach (var item in items)
            {
                res.Add(item);
                j++;
                if (j == k)
                {
                    return res.ToArray();
                }
            }
        }

        return res.ToArray();
    }
}