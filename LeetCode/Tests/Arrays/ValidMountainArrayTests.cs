namespace Tests;

/// <summary>
/// Является ли массив горным https://leetcode.com/explore/learn/card/fun-with-arrays/527/searching-for-items-in-an-array/3251/
/// </summary>
public class ValidMountainArrayTests
{
    [Theory]
    [InlineData(new int[] { 2, 1 }, false)]
    [InlineData(new int[] { 3, 5, 5 }, false)]
    [InlineData(new int[] { 0, 3, 2, 1 }, true)]
    [InlineData(new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }, false)]
    public void Test(int[] arr, bool expected)
    {
        var result = ValidMountainArray(arr);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new int[] { 2, 1 }, false)]
    [InlineData(new int[] { 3, 5, 5 }, false)]
    [InlineData(new int[] { 0, 3, 2, 1 }, true)]
    [InlineData(new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }, false)]
    public void Test1(int[] arr, bool expected)
    {
        var result = ValidMountainArray1(arr);
        Assert.Equal(expected, result);
    }

    public bool ValidMountainArray1(int[] arr)
    {
        int n = arr.Length;
        if (n < 3)
        {
            return false;
        }

        int i = 0;

        // Поднимаемся вверх
        while (i + 1 < n && arr[i] < arr[i + 1])
        {
            i++;
        }

        // Пик не может быть первым или последним элементом
        if (i == 0 || i == n - 1)
        {
            return false;
        }

        // Спускаемся вниз
        while (i + 1 < n && arr[i] > arr[i + 1])
        {
            i++;
        }

        // Если дошли до конца — это гора
        return i == n - 1;
    }

    public bool ValidMountainArray(int[] arr)
    {
        if (arr == null || arr.Length < 3)
        {
            return false;
        }

        if (arr[0] >= arr[1])
        {
            return false;
        }

        var isUp = true;
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] == arr[i - 1])
            {
                return false;
            }

            if (isUp && arr[i - 1] > arr[i])
            {
                isUp = false;
                continue;
            }

            if (!isUp && arr[i - 1] <= arr[i])
            {
                return false;
            }
        }

        return !isUp;
    }
}
