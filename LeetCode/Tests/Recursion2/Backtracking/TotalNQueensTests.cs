namespace Tests.Recursion2.Backtracking;

/// <summary>
/// [Расстановка ферзей](https://leetcode.com/explore/learn/card/recursion-ii/472/backtracking/2804/)
/// </summary>
public class TotalNQueensTests
{
    [Theory]
    [InlineData(4, 2)]
    [InlineData(1, 1)]
    public void Test(int n, int expected)
    {
        var result = TotalNQueens(n);
        Assert.Equal(expected, result);
    }

    public int TotalNQueens(int n)
    {
        bool[] columns = new bool[n];
        bool[] diag1 = new bool[2 * n]; // row - col
        bool[] diag2 = new bool[2 * n]; // row + col

        return Backtrack(0, n, columns, diag1, diag2);
    }

    // быстрое решение, толком не разобрался, но строки и диагонали запоминаются в массив, а не каждую ячейку, как я делал
    private int Backtrack(int row, int n, bool[] columns, bool[] diag1, bool[] diag2)
    {
        // Если разместили ферзей во всех строках
        if (row == n)
            return 1;

        int count = 0;

        for (int col = 0; col < n; col++)
        {
            int d1 = row - col + n;
            int d2 = row + col;

            if (columns[col] || diag1[d1] || diag2[d2])
                continue;

            // Ставим ферзя
            columns[col] = true;
            diag1[d1] = true;
            diag2[d2] = true;

            count += Backtrack(row + 1, n, columns, diag1, diag2);

            // Убираем ферзя (backtracking)
            columns[col] = false;
            diag1[d1] = false;
            diag2[d2] = false;
        }

        return count;
    }

    // работает, но медленно
    public int MyTotalNQueens(int n)
    {
        var dict = new Dictionary<(int row, int col), HashSet<(int row, int col)>>();
        var result = new int?[n];
        var count = 0;
        BacktrackQueen(0);
        return count;

        void BacktrackQueen(int row)
        {
            for (int col = 0; col < n; col++)
            {
                if (!IsUnderAttack(row, col))
                {
                    PlaceQueen(row, col);

                    if (row == n - 1)
                    {
                        count++;
                        RemoveQueen(row, result[row].Value);
                    }
                    else
                    {
                        BacktrackQueen(row + 1);
                    }
                }
            }

            if (row - 1 >= 0)
            {
                RemoveQueen(row - 1, result[row - 1].Value);
            }
        }

        bool IsUnderAttack(int row, int col)
        {
            foreach (var pair in dict)
            {
                if (pair.Value.Contains((row, col)))
                {
                    return true;
                }
            }

            return false;
        }

        void RemoveQueen(int row, int col)
        {
            result[row] = null;
            dict.Remove((row, col));
        }

        void PlaceQueen(int row, int col)
        {
            result[row] = col;
            dict.Add((row, col), new HashSet<(int row, int col)>());
            var hashSet = dict[(row, col)];
            for (int i = 0; i < n; i++)
            {
                hashSet.Add((i, col));
                hashSet.Add((row, i));

                if (i > 0)
                {
                    if (row + i < n && col + i < n)
                    {
                        hashSet.Add((row + i, col + i));
                    }
                    if (row - i >= 0 && col - i >= 0)
                    {
                        hashSet.Add((row - i, col - i));
                    }
                    if (row + i < n && col - i >= 0)
                    {
                        hashSet.Add((row + i, col - i));
                    }
                    if (row - i >= 0 && col + i < n)
                    {
                        hashSet.Add((row - i, col + i));
                    }
                }
            }
        }
    }
}
