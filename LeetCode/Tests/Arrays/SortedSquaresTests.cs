namespace Tests;

/// <summary>
/// Дан возрастающий массив, необходимо получить возрастающий массив квадратов
/// Squares of a Sorted Array
/// </summary>
public class SortedSquaresTests
{
    [Theory]
    [InlineData(new int[] { -4, -1, 0, 3, 10 }, new[] { 0, 1, 9, 16, 100 })]
    [InlineData(new int[] { -7, -3, 2, 3, 11 }, new[] { 4, 9, 9, 49, 121 })]
    public void Test(int[] nums, int[] expected)
    {
        var result = SortedSquares(nums);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new int[] { -4, -1, 0, 3, 10 }, new[] { 0, 1, 9, 16, 100 })]
    [InlineData(new int[] { -7, -3, 2, 3, 11 }, new[] { 4, 9, 9, 49, 121 })]
    public void Test1(int[] nums, int[] expected)
    {
        var result = SortedSquares1(nums);
        Assert.Equal(expected, result);
    }

    private static int[] SortedSquares1(int[] nums)
    {
        // используем тот факт, что массив возрастающий, значит максимальный квадрат это либо самое большое (правое), либо самое маленькое (левое, если оно отрицательное) число
        int n = nums.Length;
        int[] result = new int[n];
        int left = 0,
            right = n - 1;
        int pos = n - 1;

        while (left <= right)
        {
            int leftVal = nums[left] * nums[left];
            int rightVal = nums[right] * nums[right];

            if (leftVal > rightVal)
            {
                result[pos] = leftVal;
                left++;
            }
            else
            {
                result[pos] = rightVal;
                right--;
            }

            pos--;
        }

        return result;
    }

    private static int[] SortedSquares(int[] nums)
    {
        return nums.Select(n => n * n).Order().ToArray();
    }
}
