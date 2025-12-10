namespace Tests.HashTable.HashMap;

/// <summary>
/// Минимальная сумма индекса для общей строки https://leetcode.com/explore/learn/card/hash-table/184/comparison-with-other-data-structures/1177/
/// </summary>
public class FindRestaurantTests
{
    [Theory]
    [InlineData(new [] { "Shogun","Tapioca Express","Burger King","KFC" }, new [] { "Piatti","The Grill at Torrey Pines","Hungry Hunter Steakhouse","Shogun"}, new [] { "Shogun" })]
    [InlineData(new [] { "Shogun","Tapioca Express","Burger King","KFC" }, new [] { "KFC","Shogun","Burger King" }, new [] { "Shogun" })]
    [InlineData(new [] { "happy","sad","good" }, new [] { "sad","happy","good" }, new [] { "sad","happy" })]
    public void Test(string[] nums1, string[] nums2, string[] expected)
    {
        var result = FindRestaurant(nums1, nums2);
        Assert.Equal(expected, result);
    }
    
    public string[] FindRestaurant(string[] list1, string[] list2)
    {
        // Создаём словарь для быстрого поиска индексов из list1
        Dictionary<string, int> map = new Dictionary<string, int>();
        for (int i = 0; i < list1.Length; i++)
        {
            map[list1[i]] = i;
        }

        List<string> result = new List<string>();
        int minSum = int.MaxValue;

        // Проходим по второму списку и ищем совпадения
        for (int j = 0; j < list2.Length; j++)
        {
            if (map.ContainsKey(list2[j]))
            {
                int sum = j + map[list2[j]];

                if (sum < minSum)
                {
                    // Нашли меньший индекс-сумму — очищаем результат
                    minSum = sum;
                    result.Clear();
                    result.Add(list2[j]);
                }
                else if (sum == minSum)
                {
                    // Добавляем ещё один общий ресторан с той же минимальной суммой
                    result.Add(list2[j]);
                }
            }
        }

        return result.ToArray();
    }
    
    public string[] FindRestaurant1(string[] list1, string[] list2)
    {
        var dict1 = new Dictionary<string, int>();
        var dict2 = new Dictionary<string, int>();
        for (int i = 0; i < list1.Length; i++)
        {
            dict1.Add(list1[i], i);
        }
        for (int i = 0; i < list2.Length; i++)
        {
            dict2.Add(list2[i], i);
        }

        var minSum = int.MaxValue;
        var res = new List<string>();
        var list = list1.Length < list2.Length ? list1 : list2;
        for (int i = 0; i < list.Length; i++)
        {
            if (dict1.ContainsKey(list[i]) && dict2.ContainsKey(list[i]))
            {
                var sum = dict1[list[i]] + dict2[list[i]];
                if (sum < minSum)
                {
                    if (res.Count > 0)
                    {
                        res.Clear();
                    }
                    res.Add(list[i]);
                    minSum = sum;
                }else if (sum == minSum)
                {
                    res.Add(list[i]);
                }
            }
        }

        return res.ToArray();
    }
}