namespace Tests.Graph.ShortestPath;

// [Определить задержку сети](https://leetcode.com/explore/learn/card/graph/622/single-source-shortest-path-algorithm/3863/)
public class NetworkDelayTimeTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { new int[][] { [2, 1, 1], [2, 3, 1], [3, 4, 1] }, 4, 2, 2 },
            new object[] { new int[][] { [1, 2, 1] }, 2, 1, 1 },
            new object[] { new int[][] { [1, 2, 1] }, 2, 2, -1 },
            new object[] { new int[][] { [1, 2, 1], [1, 3, 2], [2, 4, 10], [3, 4, 3] }, 4, 1, 5 },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] times, int n, int k, int expected)
    {
        var result = NetworkDelayTime(times, n, k);
        Assert.Equal(expected, result);
    }

    // Алгоритм Дейкстры
    public int NetworkDelayTime(int[][] times, int n, int k)
    {
        var adj = new Dictionary<int, List<(int to, int weight)>>();
        foreach (var t in times)
        {
            if (!adj.ContainsKey(t[0]))
                adj[t[0]] = new List<(int, int)>();
            adj[t[0]].Add((t[1], t[2]));
        }

        // Храним минимальное время до каждого узла
        var minTimes = new Dictionary<int, int>();
        for (int i = 1; i <= n; i++)
            minTimes[i] = int.MaxValue;
        minTimes[k] = 0;

        // наверху будет самый дешевый способ
        var pq = new PriorityQueue<int, int>();
        pq.Enqueue(k, 0);

        while (pq.Count > 0)
        {
            pq.TryDequeue(out int currNode, out int currTime);

            if (currTime > minTimes[currNode])
                continue;
            if (!adj.ContainsKey(currNode))
                continue;

            foreach (var edge in adj[currNode])
            {
                int timeToNeighbor = currTime + edge.weight;
                if (timeToNeighbor < minTimes[edge.to])
                {
                    minTimes[edge.to] = timeToNeighbor;
                    pq.Enqueue(edge.to, timeToNeighbor);
                }
            }
        }

        int maxDelay = minTimes.Values.Max();
        return maxDelay == int.MaxValue ? -1 : maxDelay;
    }
}
