namespace Tests.Heap;

// [Комнаты для совещаний](https://leetcode.com/explore/learn/card/heap/646/practices/4087/)
public class MinMeetingRoomsTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { new int[][] { [0, 30], [5, 10], [15, 20] }, 2 },
            new object[] { new int[][] { [7, 10], [2, 4] }, 1 },
            new object[] { new int[][] { [9, 10], [4, 9], [4, 17] }, 2 },
            new object[] { new int[][] { [1, 5], [8, 9], [8, 9] }, 2 },
            new object[] { new int[][] { [2, 11], [6, 16], [11, 16] }, 2 },
            new object[] { new int[][] { [9, 16], [6, 16], [1, 9], [3, 15] }, 3 },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] intervals, int expected)
    {
        var result = MinMeetingRooms(intervals);
        Assert.Equal(expected, result);
    }

    // работает, побыстрее моего
    public int MinMeetingRooms(int[][] intervals)
    {
        if (intervals == null || intervals.Length == 0)
            return 0;

        // 1. Сортируем интервалы по времени начала
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        // 2. Используем PriorityQueue для хранения времени окончания совещаний
        // Это Min-Heap: сверху всегда время самого раннего окончания
        PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();

        // Добавляем время окончания первого совещания
        minHeap.Enqueue(intervals[0][1], intervals[0][1]);

        for (int i = 1; i < intervals.Length; i++)
        {
            // Если текущее совещание начинается после окончания самого раннего в куче
            if (intervals[i][0] >= minHeap.Peek())
            {
                // Извлекаем "освободившуюся" комнату
                minHeap.Dequeue();
            }

            // Добавляем новое время окончания в кучу
            // (либо в освободившуюся комнату, либо как новую комнату)
            minHeap.Enqueue(intervals[i][1], intervals[i][1]);
        }

        // Количество элементов в куче — это и есть количество необходимых комнат
        return minHeap.Count;
    }

    // решение на массивах, не особо быстрое и менее понятное
    public int MinMeetingRoomsArrays(int[][] intervals)
    {
        if (intervals == null || intervals.Length == 0)
            return 0;

        int n = intervals.Length;
        int[] startTimes = new int[n];
        int[] endTimes = new int[n];

        for (int i = 0; i < n; i++)
        {
            startTimes[i] = intervals[i][0];
            endTimes[i] = intervals[i][1];
        }

        // Сортируем оба массива
        Array.Sort(startTimes);
        Array.Sort(endTimes);

        int rooms = 0;
        int endPtr = 0;

        // Проходим по всем началам совещаний
        for (int startPtr = 0; startPtr < n; startPtr++)
        {
            // Если совещание начинается раньше, чем освобождается комната
            if (startTimes[startPtr] < endTimes[endPtr])
            {
                rooms++;
            }
            else
            {
                // Если комната освободилась, просто сдвигаем указатель конца
                endPtr++;
            }
        }

        return rooms;
    }

    // работает, но медленно
    public int MyMinMeetingRooms(int[][] intervals)
    {
        var sortedIntervals = intervals.OrderBy(i => i[0]).ToArray();
        var minHeap = new PriorityQueue<int, int>();
        var roomIdx = 1;
        minHeap.Enqueue(roomIdx, sortedIntervals[0][1]);
        for (int i = 1; i < sortedIntervals.Length; i++)
        {
            var interval = sortedIntervals[i];
            minHeap.TryPeek(out int room, out var priority);
            if (interval[0] >= priority)
            {
                minHeap.Dequeue();
                minHeap.Enqueue(room, interval[1]);
            }
            else
            {
                roomIdx++;
                minHeap.Enqueue(roomIdx, interval[1]);
            }
        }

        return roomIdx;
    }
}
