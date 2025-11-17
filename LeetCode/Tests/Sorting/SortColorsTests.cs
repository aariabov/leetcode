namespace Tests.Sorting;

public class SortColorsTests
{
    [Theory]
    [InlineData(new[] { 2, 0, 2, 1, 1, 0 }, new[] { 0, 0, 1, 1, 2, 2 })]
    [InlineData(new[] { 2, 0, 1 }, new[] { 0, 1, 2 })]
    public void Test(int[] nums, int[] expected)
    {
        SortColors(nums);
        Assert.Equal(expected, nums);
    }

    [Theory]
    [InlineData(new[] { 2, 0, 2, 1, 1, 0 }, new[] { 0, 0, 1, 1, 2, 2 })]
    [InlineData(new[] { 2, 0, 1 }, new[] { 0, 1, 2 })]
    public void Test1(int[] nums, int[] expected)
    {
        SortColors1(nums);
        Assert.Equal(expected, nums);
    }

    public void SortColors1(int[] nums)
    {
        int low = 0,
            mid = 0,
            high = nums.Length - 1;

        while (mid <= high)
        {
            if (nums[mid] == 0)
            {
                // Меняем местами nums[low] и nums[mid]
                (nums[low], nums[mid]) = (nums[mid], nums[low]);
                low++;
                mid++;
            }
            else if (nums[mid] == 1)
            {
                mid++;
            }
            else // nums[mid] == 2
            {
                (nums[mid], nums[high]) = (nums[high], nums[mid]);
                high--;
            }
        }
    }

    public void SortColors(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            var minIdx = i;
            var minVal = nums[i];
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] > nums[j] && nums[j] < minVal)
                {
                    minIdx = j;
                    minVal = nums[j];
                }
            }

            if (i != minIdx)
            {
                (nums[i], nums[minIdx]) = (nums[minIdx], nums[i]);
            }
        }
    }
}
