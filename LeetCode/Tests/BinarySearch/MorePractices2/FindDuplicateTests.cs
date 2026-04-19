namespace Tests.BinarySearch.MorePractices2;

// [Найти дубль в массиве](https://leetcode.com/explore/learn/card/binary-search/146/more-practices-ii/1039/)
public class FindDuplicateTests
{
    [Theory]
    [InlineData(new int[] { 1, 3, 4, 2, 2 }, 2)]
    [InlineData(new int[] { 3, 1, 3, 4, 2 }, 3)]
    [InlineData(new int[] { 3, 3, 3, 3, 3 }, 3)]
    public void Test(int[] nums, int expected)
    {
        var result = FindDuplicate(nums);
        Assert.Equal(expected, result);
    }

    // Бинарный поиск в этой задаче применяется не к индексам массива, а к диапазону возможных значений. Мы знаем, что числа лежат в промежутке от 1 до n.
    // Если мы возьмем середину диапазона mid, то в идеальном массиве (без дубликатов) количество чисел, которые меньше или равны mid, должно быть ровно mid
    // Самое медленное решение
    public int FindDuplicate(int[] nums)
    {
        int low = 1,
            high = nums.Length - 1;

        while (low < high)
        {
            int mid = low + (high - low) / 2;
            int count = 0;

            // Считаем количество элементов, попадающих в левую половину диапазона
            foreach (int num in nums)
            {
                if (num <= mid)
                    count++;
            }

            if (count > mid)
            {
                high = mid; // Дубликат слева
            }
            else
            {
                low = mid + 1; // Дубликат справа
            }
        }

        return low;
    }

    // Главная хитрость здесь в том, чтобы перестать смотреть на массив как на набор чисел и увидеть в нем связный список.
    // А потом использовать метод черепахи и зайца
    public int FindDuplicate1(int[] nums)
    {
        // Фаза 1: Находим точку встречи внутри цикла
        int tortoise = nums[0];
        int hare = nums[0];

        do
        {
            tortoise = nums[tortoise]; // Шаг в 1 единицу
            hare = nums[nums[hare]]; // Шаг в 2 единицы
        } while (tortoise != hare);

        // Фаза 2: Находим вход в цикл (это и будет дубликат)
        tortoise = nums[0];
        while (tortoise != hare)
        {
            tortoise = nums[tortoise];
            hare = nums[hare];
        }

        return hare;
    }

    // работает, достаточно быстро
    public int MyFindDuplicate(int[] nums)
    {
        var hashSet = new HashSet<int>(nums.Length);
        for (int i = 0; i < nums.Length; i++)
        {
            if (!hashSet.Add(nums[i]))
            {
                return nums[i];
            }
        }

        return -1;
    }
}
