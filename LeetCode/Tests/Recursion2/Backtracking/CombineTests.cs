namespace Tests.Recursion2.Backtracking;

/// <summary>
/// [Комбинации цифр](https://leetcode.com/explore/learn/card/recursion-ii/472/backtracking/2798/)
/// </summary>
public class CombineTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { 4, 2, new int[][] { [1, 2], [1, 3], [1, 4], [2, 3], [2, 4], [3, 4] } },
            new object[] { 1, 1, new int[][] { [1] } },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int n, int k, int[][] expected)
    {
        var result = Combine(n, k); 
        Assert.Equal(expected, result);
    }
    
    public IList<IList<int>> Combine(int n, int k)
    {
        var result = new List<IList<int>>();
        var current = new List<int>();

        Backtrack(1, n, k, current, result);

        return result;
    }

    private void Backtrack(int start, int n, int k, List<int> current, List<IList<int>> result)
    {
        // Если комбинация готова
        if (current.Count == k)
        {
            result.Add(new List<int>(current));
            return;
        }

        for (int i = start; i <= n; i++)
        {
            current.Add(i);                 // выбираем число
            Backtrack(i + 1, n, k, current, result);
            current.RemoveAt(current.Count - 1); // откат
        }
    }

    // работает и быстро
    public IList<IList<int>> MyCombine(int n, int k)
    {
        var nums = new int[n];
        for (int i = 0; i < n; i++)
        {
            nums[i] = i + 1;
        }

        var res = new  List<IList<int>>();
        var comb = new int[k];
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
            
            var start = 1;
            if (idx > 0)
            {
                start = comb[idx - 1] + 1;
            }
            for (int i = start; i <= n; i++)
            {
                comb[idx] = i;
                Backtrack(idx + 1);
            }
        }
    }
}
