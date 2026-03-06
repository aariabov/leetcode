namespace Tests.Recursion2.Conclusion;

/// <summary>
/// [Линия горизонта города](https://leetcode.com/explore/learn/card/recursion-ii/507/beyond-recursion/3006/)
/// Сложное задание, не смог решить
/// </summary>
public class GetSkylineTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { new int[][] { [2, 9, 10] }, new int[][] { [2, 10], [9, 0] } }, // одна (синий)
            new object[] { new int[][] { [0, 2, 3], [2, 5, 3] }, new int[][] { [0, 3], [5, 0] } }, // две одинаковой высоты
            new object[] // столбик вверх
            {
                new int[][] { [1, 2, 1], [1, 2, 2], [1, 2, 3] },
                new int[][] { [1, 3], [2, 0] },
            },
            new object[] // 2 столбика
            {
                new int[][] { [1, 2, 1], [1, 2, 2], [1, 2, 3], [2, 3, 1], [2, 3, 2], [2, 3, 3] },
                new int[][] { [1, 3], [3, 0] },
            },
            new object[] // зеленая и фиолетовая (разрыв)
            {
                new int[][] { [5, 12, 12], [15, 20, 10] },
                new int[][] { [5, 12], [12, 0], [15, 10], [20, 0] },
            },
            new object[] // синяя и зеленая (подъем)
            {
                new int[][] { [2, 9, 10], [5, 12, 12] },
                new int[][] { [2, 10], [5, 12], [12, 0] },
            },
            new object[] // фиолетовая и желтая (спад)
            {
                new int[][] { [15, 20, 10], [19, 24, 8] },
                new int[][] { [15, 10], [20, 8], [24, 0] },
            },
            new object[] // синяя, красная и зеленая
            {
                new int[][] { [2, 9, 10], [3, 7, 15], [5, 12, 12] },
                new int[][] { [2, 10], [3, 15], [7, 12], [12, 0] },
            },
            new object[]
            {
                new int[][] { [2, 9, 10], [3, 7, 15], [5, 12, 12], [15, 20, 10], [19, 24, 8] },
                new int[][] { [2, 10], [3, 15], [7, 12], [12, 0], [15, 10], [20, 8], [24, 0] },
            },
            new object[]
            {
                new int[][] { [0, 2, 3], [2, 4, 3], [4, 6, 3] },
                new int[][] { [0, 3], [6, 0] },
            },
            new object[]
            {
                new int[][] { [1, 2, 1], [2147483646, 2147483647, 2147483647] },
                new int[][] { [1, 1], [2, 0], [2147483646, 2147483647], [2147483647, 0] },
            },
            new object[] // error
            {
                new int[][]
                {
                    [3, 7, 8],
                    [3, 8, 7],
                    [3, 9, 6],
                    [3, 10, 5],
                    [3, 11, 4],
                    [3, 12, 3],
                    [3, 13, 2],
                    [3, 14, 1],
                },
                new int[][]
                {
                    [3, 8],
                    [7, 7],
                    [8, 6],
                    [9, 5],
                    [10, 4],
                    [11, 3],
                    [12, 2],
                    [13, 1],
                    [14, 0],
                },
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] nums, int[][] expected)
    {
        var result = GetSkyline(nums);
        Assert.Equal(expected, result);
    }

    // правильный алгоритм, не разобрался, как работает
    public IList<IList<int>> GetSkyline(int[][] buildings)
    {
        if (buildings.Length == 0)
            return new List<IList<int>>();
        return DivideAndConquer(buildings, 0, buildings.Length - 1);
    }

    private List<IList<int>> DivideAndConquer(int[][] buildings, int start, int end)
    {
        if (start == end)
        {
            var res = new List<IList<int>>();
            res.Add(new List<int> { buildings[start][0], buildings[start][2] });
            res.Add(new List<int> { buildings[start][1], 0 });
            return res;
        }

        int mid = start + (end - start) / 2;
        var leftSkyline = DivideAndConquer(buildings, start, mid);
        var rightSkyline = DivideAndConquer(buildings, mid + 1, end);

        return Merge(leftSkyline, rightSkyline);
    }

    private List<IList<int>> Merge(List<IList<int>> left, List<IList<int>> right)
    {
        var res = new List<IList<int>>();
        int h1 = 0,
            h2 = 0;
        int i = 0,
            j = 0;

        while (i < left.Count && j < right.Count)
        {
            int x;
            if (left[i][0] < right[j][0])
            {
                x = left[i][0];
                h1 = left[i][1];
                i++;
            }
            else if (left[i][0] > right[j][0])
            {
                x = right[j][0];
                h2 = right[j][1];
                j++;
            }
            else
            {
                x = left[i][0];
                h1 = left[i][1];
                h2 = right[j][1];
                i++;
                j++;
            }

            int maxHeight = Math.Max(h1, h2);
            // Добавляем точку только если высота изменилась (условие отсутствия дубликатов)
            if (res.Count == 0 || res[res.Count - 1][1] != maxHeight)
            {
                res.Add(new List<int> { x, maxHeight });
            }
        }

        // Добавляем оставшиеся части
        while (i < left.Count)
            res.Add(left[i++]);
        while (j < right.Count)
            res.Add(right[j++]);

        return res;
    }

    // мой, проходит не все тесты, надоело заставлять работать, кривой алгоритм
    public IList<IList<int>> MyGetSkyline(int[][] buildings)
    {
        var result = new List<IList<int>>();
        var minLeft = buildings.Select(b => b[0]).Min();
        var maxRight = buildings.Select(b => b[1]).Max();

        result.Add([maxRight, 0]);
        var qwe = buildings.Where(b => b[1] == maxRight).MaxBy(b => b[2]);
        GetNextPoint(qwe[0], maxRight, qwe[2]);
        result.Reverse();
        return result;

        void GetNextPoint(int left, int right, int height)
        {
            if (left == minLeft)
            {
                var qwe = buildings.Where(b => b[0] == left).MaxBy(b => b[2]);
                result.Add([left, qwe[2]]);
                return;
            }

            // выше и правее левого края
            var items = buildings
                .Where(b => b[2] > height && b[1] > left && b[1] < right)
                .ToArray();
            // в уровень
            var items1 = buildings
                .Where(b => b[2] == height && b[1] >= left && b[1] < right)
                .ToArray();
            // ниже
            var items3 = buildings
                .Where(b => b[2] < height && b[0] < left && b[1] >= left)
                .ToArray();
            // разрыв
            var items2 = buildings.Where(b => b[1] < left).ToArray();
            if (items.Length > 0)
            {
                var find = items.MaxBy(i => i[1]);
                var x = find[1];
                result.Add([x, height]);
                GetNextPoint(find[0], find[1], find[2]);
            }
            else if (items1.Length > 0)
            {
                var find = items1.MinBy(i => i[0]);
                var prev = new int[] { find[0], find[1], find[2] };
                while (find != null)
                {
                    prev = new int[] { find[0], find[1], find[2] };
                    find = buildings
                        .Where(b => b[2] == height && b[1] >= find[0] && b[1] < find[1])
                        .MinBy(i => i[0]);
                }
                var x = prev[0];
                if (x != minLeft)
                {
                    result.Add([x, height]);
                }
                GetNextPoint(prev[0], prev[1], prev[2]);
            }
            else if (items3.Length > 0)
            {
                var find = items3.MaxBy(i => i[2]);
                var x = find[1];
                result.Add([left, height]);
                GetNextPoint(find[0], find[1], find[2]);
            }
            else
            {
                var find = items2.MaxBy(i => i[1]);
                if (find != null)
                {
                    result.Add([left, height]);
                    result.Add([find[1], 0]);
                    GetNextPoint(find[0], find[1], find[2]);
                }
                else
                {
                    var xz = buildings.Where(b => b[1] == right).MaxBy(b => b[2]);
                    result.Add([minLeft, xz[2]]);
                }
            }
        }
    }

    // не работает 2 столбика
    public IList<IList<int>> ItemGetSkyline(int[][] buildings)
    {
        var result = new List<IList<int>>();
        var prevHeight = 0;
        var prevRight = 0;
        var maxRigth = 0;
        for (int i = 0; i < buildings.Length; i++)
        {
            var left = buildings[i][0];
            var right = buildings[i][1];
            var height = buildings[i][2];

            if (i < buildings.Length - 1 && buildings[i + 1][0] == left)
            {
                if (buildings[i + 1][2] > height)
                {
                    prevHeight = height;
                    prevRight = right;
                    continue;
                }
            }

            var isGap = maxRigth > 0 && left > maxRigth;
            if (isGap)
            {
                result.Add([maxRigth, 0]);
                result.Add([left, height]);
                prevHeight = height;
                prevRight = right;
            }

            if (right > maxRigth)
            {
                maxRigth = right;
            }

            if (height == prevHeight)
            {
                continue;
            }

            if (height > prevHeight)
            {
                result.Add([left, height]);
            }
            else
            {
                result.Add([prevRight, height]);
            }

            prevHeight = height;
            prevRight = right;
        }
        result.Add([maxRigth, 0]);
        return result;
    }
}
