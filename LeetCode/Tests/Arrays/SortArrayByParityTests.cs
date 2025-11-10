namespace Tests;

public class SortArrayByParityTests
{
    [Theory]
    [InlineData(new int[] { 3, 1, 2, 4 }, new int[] { 4, 2, 1, 3 })]
    [InlineData(new int[] { 0 }, new int[] { 0 })]
    public void Test(int[] arr, int[] expected)
    {
        var result = SortArrayByParity(arr);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new int[] { 3, 1, 2, 4 }, new int[] { 4, 2, 1, 3 })]
    [InlineData(new int[] { 0 }, new int[] { 0 })]
    public void Test1(int[] arr, int[] expected)
    {
        var result = SortArrayByParity1(arr);
        Assert.Equal(expected, result);
    }

    public int[] SortArrayByParity1(int[] nums)
    {
        int left = 0; // указатель на начало массива
        int right = nums.Length - 1; // указатель на конец массива

        while (left < right)
        {
            // Если левый элемент чётный — двигаем указатель вперёд
            if (nums[left] % 2 == 0)
            {
                left++;
            }
            // Если правый элемент нечётный — двигаем указатель назад
            else if (nums[right] % 2 == 1)
            {
                right--;
            }
            // Если слева нечётное, а справа чётное — меняем местами
            else
            {
                int temp = nums[left];
                nums[left] = nums[right];
                nums[right] = temp;
                left++;
                right--;
            }
        }

        return nums;
    }

    public int[] SortArrayByParity(int[] nums)
    {
        var leftIdx = 0;
        var rightIdx = nums.Length - 1;
        while (leftIdx < rightIdx)
        {
            // находим левый индекс нечетного
            while (nums[leftIdx] % 2 == 0 && leftIdx < rightIdx)
            {
                leftIdx++;
            }

            // находим правый индекс четного
            while (nums[rightIdx] % 2 != 0 & rightIdx > leftIdx)
            {
                rightIdx--;
            }

            var temp = nums[leftIdx];
            nums[leftIdx] = nums[rightIdx];
            nums[rightIdx] = temp;
        }
        return nums;
    }
}
