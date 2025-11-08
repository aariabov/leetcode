using System.Runtime.CompilerServices;
using Xunit.Abstractions;

namespace Tests;

/// <summary>
/// Продублировать 0 и сдвинуть оставшиеся элементы https://leetcode.com/explore/learn/card/fun-with-arrays/525/inserting-items-into-an-array/3245/
/// </summary>
public class DuplicateZerosTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DuplicateZerosTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData(new int[] { 1, 0, 2, 3, 0, 4, 5, 0 }, new[] { 1, 0, 0, 2, 3, 0, 0, 4 })]
    [InlineData(new int[] { 1, 2, 3 }, new[] { 1, 2, 3 })]
    [InlineData(new int[] { 0, 4, 1, 0, 0, 8, 0, 0, 3 }, new[] { 0, 0, 4, 1, 0, 0, 0, 0, 8 })]
    public void Test(int[] nums, int[] expected)
    {
        DuplicateZeros(nums);
        Assert.Equal(expected, nums);
    }

    [Theory]
    [InlineData(new int[] { 1, 0, 2, 3, 0, 4, 5, 0 }, new[] { 1, 0, 0, 2, 3, 0, 0, 4 })]
    [InlineData(new int[] { 1, 2, 3 }, new[] { 1, 2, 3 })]
    [InlineData(new int[] { 0, 4, 1, 0, 0, 8, 0, 0, 3 }, new[] { 0, 0, 4, 1, 0, 0, 0, 0, 8 })]
    public void Test1(int[] nums, int[] expected)
    {
        _testOutputHelper.WriteLine($"До вызова: {RuntimeHelpers.GetHashCode(nums)}");
        DuplicateZeros1(nums);
        _testOutputHelper.WriteLine($"После вызова: {RuntimeHelpers.GetHashCode(nums)}");
        Assert.Equal(expected, nums);
    }

    // быстрее, но использует больше памяти
    private void DuplicateZeros1(int[] arr)
    {
        _testOutputHelper.WriteLine($"Внутри (до arr = ...): {RuntimeHelpers.GetHashCode(arr)}");
        var result = new int[arr.Length];
        var resultIdx = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (resultIdx > arr.Length - 1)
            {
                break;
            }

            if (arr[i] == 0)
            {
                result[resultIdx] = 0;
                if (resultIdx + 1 < arr.Length)
                {
                    result[resultIdx + 1] = 0;
                }

                resultIdx += 2;
            }
            else
            {
                result[resultIdx] = arr[i];
                resultIdx++;
            }
        }

        // так не сработает, ибо меняется только копия ссылки, nums по прежнему будет указывать на исходный массив
        // arr = result;

        Array.Copy(result, arr, arr.Length);
        _testOutputHelper.WriteLine($"Внутри (после arr = ...): {RuntimeHelpers.GetHashCode(arr)}");
    }

    private static void DuplicateZeros(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 0)
            {
                for (int j = arr.Length - 1; j > i; j--)
                {
                    arr[j] = arr[j - 1];
                }

                if (i < arr.Length - 1)
                {
                    arr[i + 1] = 0;
                    i = i + 1;
                }
            }
        }
    }
}
