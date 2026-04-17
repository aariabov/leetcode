namespace Tests.BinarySearch.Template3;

// [Найти k ближайших элементов](https://leetcode.com/explore/learn/card/binary-search/135/template-iii/945/)
public class FindClosestElementsTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 4, 3, new[] { 1, 2, 3, 4 })]
    [InlineData(new[] { 1, 1, 2, 3, 4, 5 }, 4, -1, new[] { 1, 1, 2, 3 })]
    [InlineData(new[] { 0, 0, 0, 1, 3, 5, 6, 7, 8, 8 }, 2, 2, new[] { 1, 3 })]
    [InlineData(new[] { 1, 3 }, 1, 2, new[] { 1 })]
    [InlineData(new[] { 1, 10, 15, 25, 35, 45, 50, 59 }, 1, 30, new[] { 25 })]
    [InlineData(new[] { 1, 1, 1, 10, 10, 10 }, 1, 9, new[] { 10 })]
    public void Test1(int[] nums, int k, int x, int[] expected)
    {
        var result = FindClosestElements(nums, k, x);
        Assert.Equal(expected, result);
    }

    public IList<int> FindClosestElements(int[] arr, int k, int x)
    {
        int left = 0;
        int right = arr.Length - k;

        // Бинарным поиском ищем левую границу окна из k элементов
        while (left < right)
        {
            int mid = left + (right - left) / 2;

            // Сравниваем расстояние начала окна (mid) и элемента сразу за окном (mid + k)
            // Если элемент после окна ближе, значит сдвигаем всё окно вправо
            if (x - arr[mid] > arr[mid + k] - x)
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }

        // Возвращаем подмассив длиной k, начиная с найденного индекса
        var result = new List<int>();
        for (int i = left; i < left + k; i++)
        {
            result.Add(arr[i]);
        }
        return result;
    }

    // работает, но медленно
    public IList<int> MyFindClosestElements(int[] arr, int k, int x)
    {
        var minIdx = FindMinIdx();

        var result = new List<int>(k);
        result.Add(arr[minIdx]);
        var left = minIdx;
        var right = minIdx;
        var i = 1;
        while (i < k)
        {
            if (left > 0 && right < arr.Length - 1)
            {
                if (Math.Abs(arr[left - 1] - x) <= Math.Abs(arr[right + 1] - x))
                {
                    result.Add(arr[left - 1]);
                    left--;
                }
                else
                {
                    result.Add(arr[right + 1]);
                    right++;
                }
            }
            else
            {
                if (left == 0)
                {
                    result.Add(arr[right + 1]);
                    right++;
                }
                else
                {
                    result.Add(arr[left - 1]);
                    left--;
                }
            }

            i++;
        }

        return result.OrderBy(r => r).ToList();

        int FindMinIdx()
        {
            int left = 0;
            int right = arr.Length - 1;

            while (left < right)
            {
                int mid = left + (right - left + 1) / 2;

                if (arr[mid] - x == 0)
                {
                    return mid;
                }

                if (right - left == 1)
                {
                    var l = Math.Abs(arr[left] - x);
                    var r = Math.Abs(arr[right] - x);
                    if (l == r)
                    {
                        return left;
                    }

                    return l < r ? left : right;
                }

                if (arr[mid] > x)
                {
                    right = mid;
                }
                else
                {
                    left = mid;
                }
            }

            return left;
        }
    }
}
