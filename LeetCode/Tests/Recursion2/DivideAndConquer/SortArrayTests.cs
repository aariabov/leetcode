namespace Tests.Recursion2.DivideAndConquer;

/// <summary>
/// [Merge sort](https://leetcode.com/explore/learn/card/recursion-ii/470/divide-and-conquer/2944/)
/// </summary>
public class SortArrayTests
{
    [Theory]
    [InlineData(new int[] { 5, 2, 3, 1 }, new int[] { 1, 2, 3, 5 })]
    [InlineData(new int[] { 5, 1, 1, 2, 0, 0 }, new int[] { 0, 0, 1, 1, 2, 5 })]
    public void Test(int[] arr, int[] expected)
    {
        var res = SortArray(arr);
        Assert.Equal(expected, res);
    }

    public int[] SortArray(int[] nums)
    {
        return MergeSort(nums);
    }

    int[] MergeSort(int[] arr)
    {
        if (arr.Length <= 1)
        {
            return arr;
        }

        var center = arr.Length / 2;
        var left = SortArray(arr[0..center]);
        var right = SortArray(arr[center..]);
        var res = Merge(left, right);
        return res;
    }

    int[] Merge(int[] left, int[] right)
    {
        var res = new int[left.Length + right.Length];
        var leftCursor = 0;
        var rightCursor = 0;
        var resCursor = 0;

        while (leftCursor < left.Length && rightCursor < right.Length)
        {
            var leftItem = left[leftCursor];
            var rightItem = right[rightCursor];
            if (leftItem <= rightItem)
            {
                res[resCursor] = leftItem;
                leftCursor++;
            }
            else
            {
                res[resCursor] = rightItem;
                rightCursor++;
            }
            resCursor++;
        }

        while (leftCursor < left.Length)
        {
            var leftItem = left[leftCursor];
            res[resCursor] = leftItem;
            leftCursor++;
            resCursor++;
        }

        while (rightCursor < right.Length)
        {
            var rightItem = right[rightCursor];
            res[resCursor] = rightItem;
            rightCursor++;
            resCursor++;
        }

        return res;
    }
}
