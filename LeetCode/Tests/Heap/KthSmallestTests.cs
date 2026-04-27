namespace Tests.Heap;

// [Наименьший k-ый элемент в матрице](https://leetcode.com/explore/learn/card/heap/646/practices/4086/)
public class KthSmallestTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { new int[][] { [1, 5, 9], [10, 11, 13], [12, 13, 15] }, 8, 13 },
            new object[] { new int[][] { [-5] }, 1, -5 },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] mat, int k, int expected)
    {
        var result = KthSmallest(mat, k);
        Assert.Equal(expected, result);
    }

    public int KthSmallest(int[][] matrix, int k)
    {
        int n = matrix.Length;
        int low = matrix[0][0];
        int high = matrix[n - 1][n - 1];

        while (low < high)
        {
            int mid = low + (high - low) / 2;

            // Считаем количество элементов <= mid
            if (CountLessEqual(matrix, mid) < k)
            {
                low = mid + 1;
            }
            else
            {
                high = mid;
            }
        }

        return low;
    }

    private int CountLessEqual(int[][] matrix, int mid)
    {
        int count = 0;
        int n = matrix.Length;
        int row = n - 1; // Начинаем с левого нижнего угла
        int col = 0;

        while (row >= 0 && col < n)
        {
            if (matrix[row][col] <= mid)
            {
                // Если элемент подходит, значит все элементы выше него в этом столбце тоже подходят
                count += (row + 1);
                col++;
            }
            else
            {
                // Если элемент слишком большой, поднимаемся выше по строке
                row--;
            }
        }
        return count;
    }

    // работает, чуть быстрее
    public int KthSmallestPriorityQueue(int[][] matrix, int k)
    {
        int n = matrix.Length;
        // PriorityQueue хранит координаты (row, col), приоритет — само значение в матрице
        var minHeap = new PriorityQueue<(int r, int c), int>();

        // Кладем первый элемент каждой строки (не больше n элементов)
        for (int r = 0; r < Math.Min(n, k); r++)
        {
            minHeap.Enqueue((r, 0), matrix[r][0]);
        }

        int result = 0;
        for (int i = 0; i < k; i++)
        {
            // Достаем самый маленький элемент
            minHeap.TryDequeue(out (int r, int c) cell, out result);

            // Если в этой строке есть еще элементы, кладем следующий в кучу
            if (cell.c + 1 < n)
            {
                minHeap.Enqueue((cell.r, cell.c + 1), matrix[cell.r][cell.c + 1]);
            }
        }

        return result;
    }

    // работает, но медленно
    public int MyKthSmallest(int[][] matrix, int k)
    {
        var minHeap = new PriorityQueue<int, int>();
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                var element = matrix[i][j];
                minHeap.Enqueue(element, element);
            }
        }

        for (int i = 0; i < k - 1; i++)
        {
            minHeap.Dequeue();
        }

        return minHeap.Peek();
    }
}
