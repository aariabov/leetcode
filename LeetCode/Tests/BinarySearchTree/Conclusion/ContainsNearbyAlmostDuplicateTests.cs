namespace Tests.BinarySearchTree.Conclusion;

/// <summary>
/// [Проверка дублей](https://leetcode.com/explore/learn/card/introduction-to-data-structure-binary-search-tree/142/conclusion/1013/)
/// </summary>
public class ContainsNearbyAlmostDuplicateTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 1 }, 3, 0, true)]
    [InlineData(new[] { 1, 5, 9, 1, 5, 9 }, 2, 3, false)]
    public void Test(int[] nums, int indexDiff, int valueDiff, bool expected)
    {
        var res = ContainsNearbyAlmostDuplicate(nums, indexDiff, valueDiff);
        Assert.Equal(expected, res);
    }

    // хитрое решение на SortedSet
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int indexDiff, int valueDiff)
    {
        if (nums == null || nums.Length == 0)
        {
            return false;
        }

        var set = new SortedSet<long>();

        for (int i = 0; i < nums.Length; i++)
        {
            long num = nums[i];

            // Ищем минимальный элемент >= num - valueDiff
            long lowerBound = num - valueDiff;

            var view = set.GetViewBetween(lowerBound, long.MaxValue);

            if (view.Count > 0)
            {
                long candidate = Min(view);

                if (Math.Abs(candidate - num) <= valueDiff)
                    return true;
            }

            set.Add(num);

            // Поддерживаем окно indexDiff
            if (i >= indexDiff)
                set.Remove(nums[i - indexDiff]);
        }

        return false;
    }

    private long Min(SortedSet<long> set)
    {
        foreach (var v in set)
            return v;
        throw new InvalidOperationException();
    }

    // хитрое решение, проверяем бакеты
    public bool ContainsNearbyAlmostDuplicateBacket(int[] nums, int indexDiff, int valueDiff)
    {
        if (valueDiff < 0)
        {
            return false;
        }

        Dictionary<long, long> buckets = new Dictionary<long, long>();
        long width = (long)valueDiff + 1;

        for (int i = 0; i < nums.Length; i++)
        {
            long num = nums[i];
            long bucketId = GetBucketId(num, width);

            // Если в той же корзине уже есть число
            if (buckets.ContainsKey(bucketId))
            {
                return true;
            }

            // Проверяем соседние корзины
            if (
                buckets.ContainsKey(bucketId - 1)
                && Math.Abs(num - buckets[bucketId - 1]) <= valueDiff
            )
            {
                return true;
            }

            if (
                buckets.ContainsKey(bucketId + 1)
                && Math.Abs(num - buckets[bucketId + 1]) <= valueDiff
            )
            {
                return true;
            }

            // Добавляем текущее число
            buckets[bucketId] = num;

            // Удаляем элемент, который выходит за indexDiff
            if (i >= indexDiff)
            {
                long oldBucket = GetBucketId(nums[i - indexDiff], width);
                buckets.Remove(oldBucket);
            }
        }

        return false;
    }

    private long GetBucketId(long num, long width)
    {
        if (num >= 0)
        {
            return num / width;
        }
        else
        {
            return ((num + 1) / width) - 1;
        }
    }

    // решение в лоб - работает, но не проходит по времени
    public bool MyContainsNearbyAlmostDuplicate(int[] nums, int indexDiff, int valueDiff)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length && j - i <= indexDiff; j++)
            {
                if (Math.Abs(nums[i] - nums[j]) <= valueDiff)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
