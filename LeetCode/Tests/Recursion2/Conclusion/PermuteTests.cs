namespace Tests.Recursion2.Conclusion;

/// <summary>
/// [Найти все перестановки](https://leetcode.com/explore/learn/card/recursion-ii/507/beyond-recursion/2903/)
/// </summary>
public class PermuteTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new[] { 1, 2, 3 },
                new int[][] { [1, 2, 3], [1, 3, 2], [2, 1, 3], [2, 3, 1], [3, 1, 2], [3, 2, 1] },
            },
            new object[] { new[] { 0, 1 }, new int[][] { [0, 1], [1, 0] } },
            new object[] { new[] { 1 }, new int[][] { [1] } },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[] nums, int[][] expected)
    {
        var result = Permute(nums);
        Assert.Equal(expected, result);
    }

    public IList<IList<int>> Permute(int[] nums)
    {
        var result = new List<IList<int>>();
        var currentPermutation = new List<int>();
        var used = new bool[nums.Length];

        Backtrack(nums, used, currentPermutation, result);

        return result;
    }

    private void Backtrack(int[] nums, bool[] used, List<int> current, IList<IList<int>> result)
    {
        // Базовый случай: если длина текущей перестановки равна длине массива, сохраняем результат
        if (current.Count == nums.Length)
        {
            result.Add(new List<int>(current));
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            // Если элемент уже используется, пропускаем его
            if (used[i])
                continue;

            // 1. Делаем выбор
            used[i] = true;
            current.Add(nums[i]);

            // 2. Рекурсивный переход
            Backtrack(nums, used, current, result);

            // 3. Откат (Backtrack) — убираем элемент, чтобы попробовать другой вариант
            used[i] = false;
            current.RemoveAt(current.Count - 1);
        }
    }

    // работает
    public IList<IList<int>> MyPermute(int[] nums)
    {
        var k = nums.Length;

        var res = new List<IList<int>>();
        var comb = new int[k];
        var hashSet = new HashSet<int>();
        Backtrack(0);
        return res;

        void Backtrack(int idx)
        {
            if (idx == k)
            {
                var arr = new int[k];
                comb.CopyTo(arr, 0);
                res.Add(arr);
                return;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                var num = nums[i];
                if (!hashSet.Add(num))
                {
                    continue;
                }
                comb[idx] = num;
                Backtrack(idx + 1);
                hashSet.Remove(num);
            }
        }
    }
}
