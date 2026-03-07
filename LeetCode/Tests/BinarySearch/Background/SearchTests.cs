namespace Tests.BinarySearch.Background;

/// <summary>
/// [Бинарный поиск в массиве](https://leetcode.com/explore/learn/card/binary-search/138/background/1038/)
/// </summary>
public class SearchTests
{
    [Theory]
    [InlineData(new int[] { -1, 0, 3, 5, 9, 12 }, 9, 4)]
    [InlineData(new int[] { -1, 0, 3, 5, 9, 12 }, 2, -1)]
    [InlineData(new int[] { 2, 5 }, 0, -1)]
    public void Test1(int[] nums, int target, int expected)
    {
        var result = Search(nums, target);
        Assert.Equal(expected, result);
    }

    public int Search(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            // Вычисляем средний индекс так, чтобы избежать переполнения целого числа
            int mid = left + (right - left) / 2;

            if (nums[mid] == target)
            {
                return mid; // Элемент найден
            }
            else if (nums[mid] < target)
            {
                left = mid + 1; // Цель в правой половине
            }
            else
            {
                right = mid - 1; // Цель в левой половине
            }
        }

        return -1; // Элемент не найден
    }

    // работает и быстро, рекурсия
    public int MySearch(int[] nums, int target)
    {
        return Rec(0, nums.Length - 1);

        int Rec(int startIdx, int endIdx)
        {
            if (
                startIdx < 0
                || startIdx > nums.Length - 1
                || endIdx < 0
                || endIdx > nums.Length - 1
            )
            {
                return -1;
            }
            if (startIdx == endIdx)
            {
                return nums[startIdx] == target ? startIdx : -1;
            }

            var midIdx = ((endIdx - startIdx) / 2) + startIdx;
            if (nums[midIdx] == target)
            {
                return midIdx;
            }
            else if (target < nums[midIdx])
            {
                return Rec(startIdx, midIdx - 1);
            }
            return Rec(midIdx + 1, endIdx);
        }
    }
}
