namespace Tests;

/// <summary>
/// Существует ли элемент, который в 2 раза больше любого другого элемента https://leetcode.com/explore/learn/card/fun-with-arrays/527/searching-for-items-in-an-array/3250/
/// </summary>
public class CheckIfExistTests
{
    [Theory]
    [InlineData(new int[] { 10, 2, 5, 3 }, true)]
    [InlineData(new int[] { 3, 1, 7, 11 }, false)]
    public void Test(int[] arr, bool expected)
    {
        var result = CheckIfExist(arr);
        Assert.Equal(expected, result);
    }

    public bool CheckIfExist(int[] arr)
    {
        if (arr == null || arr.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[i] * 2 == arr[j] || arr[j] * 2 == arr[i])
                {
                    return true;
                }
            }
        }
        return false;
    }
}
