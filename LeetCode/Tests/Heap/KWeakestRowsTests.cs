namespace Tests.Heap;

// https://leetcode.com/explore/learn/card/heap/646/practices/4085/
public class KWeakestRowsTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new int[][]
                {
                    [1, 1, 0, 0, 0],
                    [1, 1, 1, 1, 0],
                    [1, 0, 0, 0, 0],
                    [1, 1, 0, 0, 0],
                    [1, 1, 1, 1, 1],
                },
                3,
                new[] { 2, 0, 3 },
            },
            new object[]
            {
                new int[][] { [1, 0, 0, 0], [1, 1, 1, 1], [1, 0, 0, 0], [1, 0, 0, 0] },
                2,
                new[] { 0, 2 },
            },
            new object[]
            {
                new int[][] { [1, 0], [1, 0], [1, 0], [1, 1] },
                4,
                new[] { 0, 1, 2, 3 },
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] mat, int k, int[] expected)
    {
        var result = KWeakestRows(mat, k);
        Assert.Equal(expected, result);
    }

    // работает быстро
    public int[] KWeakestRows(int[][] mat, int k)
    {
        // Очередь хранит индекс строки, а приоритет определяется
        // кортежем (количество солдат, индекс строки)
        var pq = new PriorityQueue<int, (int count, int index)>(
            Comparer<(int count, int index)>.Create(
                (a, b) =>
                {
                    if (a.count != b.count)
                        return a.count.CompareTo(b.count);
                    return a.index.CompareTo(b.index);
                }
            )
        );

        for (int i = 0; i < mat.Length; i++)
        {
            int count = CountSoldiers(mat[i]);
            pq.Enqueue(i, (count, i));
        }

        int[] result = new int[k];
        for (int i = 0; i < k; i++)
        {
            result[i] = pq.Dequeue();
        }

        return result;
    }

    private int CountSoldiers(int[] row)
    {
        // Линейный поиск или бинарный поиск (для 100 элементов разница невелика)
        int low = 0,
            high = row.Length;
        while (low < high)
        {
            int mid = low + (high - low) / 2;
            if (row[mid] == 1)
                low = mid + 1;
            else
                high = mid;
        }
        return low;
    }

    // работает норм, но переусложнил логику
    public int[] MyKWeakestRows(int[][] mat, int k)
    {
        var minHeap = new PriorityQueue<int, (int idx, int sum)>(
            Comparer<(int idx, int sum)>.Create(
                (a, b) =>
                {
                    if (a.sum < b.sum || (a.sum == b.sum && a.idx < b.idx))
                    {
                        return 1;
                    }

                    return -1;
                }
            )
        );
        for (int i = 0; i < mat.Length; i++)
        {
            var sum = mat[i].Sum();
            if (minHeap.Count < k)
            {
                minHeap.Enqueue(i, (i, sum));
            }
            else
            {
                minHeap.TryPeek(out var _, out var priority);
                if (sum < priority.sum)
                {
                    minHeap.Enqueue(i, (i, sum));
                    minHeap.Dequeue();
                }
            }
        }
        var res = new int[k];
        for (int i = 0; i < k; i++)
        {
            res[i] = minHeap.Dequeue();
        }
        return res.Reverse().ToArray();
    }

    // простой и прямолинейный подход
    public int[] SimpleKWeakestRows(int[][] mat, int k)
    {
        int m = mat.Length;
        // Создаем массив пар: {количество солдат, индекс строки}
        var rowStrengths = new List<(int count, int index)>();

        for (int i = 0; i < m; i++)
        {
            int count = 0;
            // Считаем количество солдат (1) в строке
            foreach (int cell in mat[i])
            {
                if (cell == 1)
                    count++;
                else
                    break; // Солдаты всегда в начале, встретили 0 — выходим
            }
            rowStrengths.Add((count, i));
        }

        // Сортируем: сначала по количеству солдат, затем по индексу
        return rowStrengths
            .OrderBy(r => r.count)
            .ThenBy(r => r.index)
            .Take(k)
            .Select(r => r.index)
            .ToArray();
    }
}
