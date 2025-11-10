namespace Tests;

/// <summary>
/// Заменить все элементы на максимальный справа от него https://leetcode.com/explore/learn/card/fun-with-arrays/511/in-place-operations/3259/
/// </summary>
public class ReplaceElementsTests
{
    [Theory]
    [InlineData(new int[] { 17, 18, 5, 4, 6, 1 }, new int[] { 18, 6, 6, 6, 1, -1 })]
    [InlineData(new int[] { 400 }, new int[] { -1 })]
    public void Test(int[] arr, int[] expected)
    {
        var result = ReplaceElements(arr);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new int[] { 17, 18, 5, 4, 6, 1 }, new int[] { 18, 6, 6, 6, 1, -1 })]
    [InlineData(new int[] { 400 }, new int[] { -1 })]
    public void Test1(int[] arr, int[] expected)
    {
        var result = ReplaceElements1(arr);
        Assert.Equal(expected, result);
    }

    public int[] ReplaceElements1(int[] arr)
    {
        // идем с конца массива и всегда знаем наибольший элемент
        int maxRight = -1;

        for (int i = arr.Length - 1; i >= 0; i--)
        {
            int temp = arr[i];
            arr[i] = maxRight;
            maxRight = Math.Max(maxRight, temp);
        }

        return arr;
    }

    public int[] ReplaceElements(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            var max = -1;
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[j] > max)
                {
                    max = arr[j];
                }
            }
            arr[i] = max;
        }
        return arr;
    }
}
