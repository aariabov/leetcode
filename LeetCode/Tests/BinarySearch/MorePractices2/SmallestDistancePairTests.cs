namespace Tests.BinarySearch.MorePractices2;

// [Наименьшее расстояние между элементами](https://leetcode.com/explore/learn/card/binary-search/146/more-practices-ii/1041/)
public class SmallestDistancePairTests
{
    [Theory]
    [InlineData(new[] { 1, 3, 1 }, 1, 0)]
    [InlineData(new[] { 1, 1, 1 }, 2, 0)]
    [InlineData(new[] { 1, 6, 1 }, 3, 5)]
    public void Test(int[] nums, int k, int expected)
    {
        var res = SmallestDistancePair(nums, k);
        Assert.Equal(expected, res);
    }

    public int SmallestDistancePair(int[] nums, int k)
    {
        // 1. Сортировка массива для работы метода двух указателей
        Array.Sort(nums);

        int n = nums.Length;
        // 2. Устанавливаем границы бинарного поиска
        // Минимальная возможная дистанция — 0, максимальная — разница между краями массива
        int left = 0;
        int right = nums[n - 1] - nums[0];

        while (left < right)
        {
            int mid = left + (right - left) / 2;

            // 3. Считаем количество пар, дистанция которых <= mid
            if (CountPairs(nums, mid) < k)
            {
                // Если пар слишком мало, искомая дистанция больше mid
                left = mid + 1;
            }
            else
            {
                // Иначе искомая дистанция может быть mid или меньше
                right = mid;
            }
        }

        return left;
    }

    private int CountPairs(int[] nums, int mid)
    {
        int count = 0;
        int left = 0;

        // Используем два указателя (скользящее окно)
        for (int right = 0; right < nums.Length; right++)
        {
            // Пока разница между элементами больше допустимой (mid),
            // сдвигаем левую границу
            while (nums[right] - nums[left] > mid)
            {
                left++;
            }
            // Все пары между left и right включительно (с правым концом в right)
            // имеют дистанцию <= mid
            count += right - left;
        }

        return count;
    }

    // работает, но не проходит по скорости
    public int MySmallestDistancePair(int[] nums, int k)
    {
        var res = new List<int>();
        for (int i = 0; i < nums.Length - 1; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                res.Add(Math.Abs(nums[i] - nums[j]));
            }
        }

        res.Sort();
        return res[k - 1];
    }
}
