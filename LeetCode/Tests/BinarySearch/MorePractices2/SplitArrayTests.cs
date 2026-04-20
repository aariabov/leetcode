namespace Tests.BinarySearch.MorePractices2;

// [Разбить массив на подмассивы с минимальной суммой](https://leetcode.com/explore/learn/card/binary-search/146/more-practices-ii/1042/)
public class SplitArrayTests
{
    [Theory]
    [InlineData(new[] { 7, 2, 5, 10, 8 }, 2, 18)]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 2, 9)]
    [InlineData(new[] { 1, 4, 4 }, 3, 4)]
    public void Test(int[] nums, int k, int expected)
    {
        var res = SplitArray(nums, k);
        Assert.Equal(expected, res);
    }

    public int SplitArray(int[] nums, int k)
    {
        int left = 0;
        int right = 0;

        foreach (int num in nums)
        {
            left = Math.Max(left, num); // Максимальный элемент
            right += num; // Сумма всех элементов
        }

        while (left < right)
        {
            int mid = left + (right - left) / 2;

            if (CanSplit(nums, k, mid))
            {
                right = mid; // Пробуем уменьшить максимальную сумму
            }
            else
            {
                left = mid + 1; // Текущий предел слишком мал
            }
        }

        return left;
    }

    private bool CanSplit(int[] nums, int k, int maxSum)
    {
        int currentSum = 0;
        int subarraysCount = 1;

        foreach (int num in nums)
        {
            if (currentSum + num > maxSum)
            {
                subarraysCount++;
                currentSum = num;
                if (subarraysCount > k)
                    return false;
            }
            else
            {
                currentSum += num;
            }
        }

        return true;
    }

    // работает, но медленно
    // Идея, Мы не знаем точно, как делить массив, но мы знаем диапазон, в котором лежит результат:
    // left - Самое большое число в массиве, right - Сумма всех элементов массива
    public int MySplitArray(int[] nums, int k)
    {
        var left = nums.Max();
        var right = nums.Sum();
        var sum = 0;

        while (left < right)
        {
            var mid = left + (right - left) / 2;
            var i = 0;
            var times = 0;
            while (i < nums.Length)
            {
                sum = nums[i];
                while (i < nums.Length - 1 && sum + nums[i + 1] <= mid)
                {
                    sum += nums[i + 1];
                    i++;
                }
                times++;
                i++;
            }

            if (times <= k)
            {
                right = mid;
            }
            else
            {
                left = mid + 1;
            }
        }

        var j = 0;
        var res = new List<int>(k);
        while (j < nums.Length)
        {
            sum = nums[j];
            while (j < nums.Length - 1 && sum + nums[j + 1] <= left)
            {
                sum += nums[j + 1];
                j++;
            }
            res.Add(sum);
            j++;
        }

        return res.Max();
    }
}
