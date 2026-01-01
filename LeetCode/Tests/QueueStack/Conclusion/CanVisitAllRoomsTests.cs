namespace Tests.QueueStack.Conclusion;

/// <summary>
/// [Комнаты и ключи](https://leetcode.com/explore/learn/card/queue-stack/239/conclusion/1391/)
/// </summary>
public class CanVisitAllRoomsTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { new int[][] { [1], [2], [3], [] }, true },
            new object[] { new int[][] { [1, 3], [3, 0, 1], [2], [0] }, false },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(IList<IList<int>> rooms, bool expected)
    {
        var result = CanVisitAllRooms(rooms);
        Assert.Equal(expected, result);
    }

    public bool CanVisitAllRooms(IList<IList<int>> rooms)
    {
        var hashSet = new HashSet<int>();
        var queue = new Queue<int>();
        queue.Enqueue(0);

        while (queue.Count > 0)
        {
            var roomIdx = queue.Dequeue();
            var keys = rooms[roomIdx];
            foreach (var key in keys)
            {
                if (!hashSet.Contains(key))
                {
                    queue.Enqueue(key);
                }
            }
            hashSet.Add(roomIdx);
        }

        return hashSet.Count == rooms.Count;
    }
}
