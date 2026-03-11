namespace Tests.BinarySearch.Template1;

/// <summary>
/// [Поиск в повернутом массиве](https://leetcode.com/explore/learn/card/binary-search/125/template-i/952/)
/// </summary>
public class SearchTests
{
    [Theory]
    [InlineData(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 0, 4)]
    [InlineData(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 3, -1)]
    [InlineData(new int[] { 1 }, 0, -1)]
    [InlineData(new int[] { 1, 3 }, 0, -1)]
    [InlineData(new int[] { 5, 1, 3 }, 2, -1)]
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
            int mid = left + (right - left) / 2;

            if (nums[mid] == target)
            {
                return mid;
            }

            // Проверяем, какая часть массива отсортирована
            if (nums[left] <= nums[mid])
            {
                // Левая часть [left...mid] отсортирована
                if (target >= nums[left] && target < nums[mid])
                {
                    right = mid - 1; // Таргет в левой части
                }
                else
                {
                    left = mid + 1; // Таргет в правой части
                }
            }
            else
            {
                // Правая часть [mid...right] отсортирована
                if (target > nums[mid] && target <= nums[right])
                {
                    left = mid + 1; // Таргет в правой части
                }
                else
                {
                    right = mid - 1; // Таргет в левой части
                }
            }
        }

        return -1;
    }

    // не работает, надоело
    public int MySearch(int[] nums, int target)
    {
        var k = 0;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] < nums[i - 1])
            {
                k = i;
                break;
            }
        }

        if (target < nums[k])
        {
            return -1;
        }

        var left = k;
        var right = (nums.Length - 1 + k) % nums.Length;
        while (left != right)
        {
            var mid = left > right ? ((right + left) / 2 + k) % nums.Length : (right + left) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }
            else if (nums[mid] > target)
            {
                right = mid == 0 ? nums.Length - 1 : mid - 1;
            }
            else
            {
                left = mid == nums.Length - 1 ? 0 : mid + 1;
            }
        }

        return nums[left] == target ? left : -1;
    }
}
