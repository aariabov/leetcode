namespace Tests.Graph.MST;

// [Соединить все точки с минимальной стоимостью Прим](https://leetcode.com/explore/learn/card/graph/621/algorithms-to-construct-minimum-spanning-tree/3860/)
public class MinCostConnectPointsPrimTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { new int[][] { [0, 0], [2, 2], [3, 10], [5, 2], [7, 0] }, 20 },
            new object[] { new int[][] { [3, 12], [-2, 5], [-4, 1] }, 18 },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] nums, int expected)
    {
        var result = MinCostConnectPoints(nums);
        Assert.Equal(expected, result);
    }

    // вместо хранения всех возможных ребер, просто будем обновлять расстояния до дерева
    public int MinCostConnectPoints(int[][] points)
    {
        int n = points.Length;
        int totalCost = 0;
        int connectedNodes = 0;

        // Массив минимальных расстояний от MST до каждой точки, будет обновляться
        int[] minDist = new int[n];
        for (int i = 1; i < n; i++)
            minDist[i] = int.MaxValue;

        bool[] visited = new bool[n];

        while (connectedNodes < n)
        {
            int currNode = -1;
            int currMinDist = int.MaxValue;

            // Находим ближайшую точку, которую еще не добавили в MST
            for (int i = 0; i < n; i++)
            {
                if (!visited[i] && minDist[i] < currMinDist)
                {
                    currMinDist = minDist[i];
                    currNode = i;
                }
            }

            // Добавляем точку в остовное дерево
            totalCost += currMinDist;
            visited[currNode] = true;
            connectedNodes++;

            // Обновляем расстояния до остальных точек от текущей
            for (int nextNode = 0; nextNode < n; nextNode++)
            {
                if (!visited[nextNode])
                {
                    int dist =
                        Math.Abs(points[currNode][0] - points[nextNode][0])
                        + Math.Abs(points[currNode][1] - points[nextNode][1]);

                    // Если новое расстояние меньше, то обновляем (вместо хранения всех возможных ребер)
                    if (dist < minDist[nextNode])
                    {
                        minDist[nextNode] = dist;
                    }
                }
            }
        }

        return totalCost;
    }

    // для наименьших ребер используется PriorityQueue, но в хипе может храниться много ненужных ребер
    public int MinCostConnectPointsPriorityQueue(int[][] points)
    {
        int n = points.Length;
        // на верху будет точка с минимальным расстоянием до текущей точки
        var heap = new PriorityQueue<int, int>();

        bool[] inMST = new bool[n];

        // Добавляем начальную точку: (индекс 0, расстояние 0)
        heap.Enqueue(0, 0);

        int mstCost = 0;
        int edgesUsed = 0;

        while (edgesUsed < n)
        {
            if (!heap.TryDequeue(out int currNode, out int weight))
                break;

            // Если узел уже в MST, пропускаем
            if (inMST[currNode])
            {
                continue;
            }

            inMST[currNode] = true;
            mstCost += weight;
            edgesUsed++;

            for (int nextNode = 0; nextNode < n; ++nextNode)
            {
                if (!inMST[nextNode])
                {
                    // для не добавленных точек рассчитываем расстояние до других и закидываем в heap, в хипе может храниться много ненужных ребер
                    int nextWeight =
                        Math.Abs(points[currNode][0] - points[nextNode][0])
                        + Math.Abs(points[currNode][1] - points[nextNode][1]);

                    heap.Enqueue(nextNode, nextWeight);
                }
            }
        }

        return mstCost;
    }

    // работает, но медленнее, чем другие, хранит все возможные ребра
    public int MyMinCostConnectPoints(int[][] points)
    {
        int n = points.Length;
        var edges = new List<(int from, int to, int cost)>();

        // 1. Генерируем все возможные ребра и их веса
        for (int i = 0; i < n; i++)
        {
            var point1 = points[i];
            for (int j = i + 1; j < n; j++)
            {
                var point2 = points[j];
                int dist = Math.Abs(point1[0] - point2[0]) + Math.Abs(point1[1] - point2[1]);
                edges.Add((i, j, dist));
            }
        }

        // 2. Сортируем ребра по возрастанию веса
        edges.Sort((a, b) => b.cost.CompareTo(a.cost));

        var result = 0;
        var count = 0;
        var visited = new HashSet<int>();
        visited.Add(0);
        var k = edges.Count - 1;
        while (count < points.Length - 1)
        {
            var edge = edges[k];
            if (
                edge.cost != int.MaxValue
                    && (visited.Contains(edge.from) && !visited.Contains(edge.to))
                || (!visited.Contains(edge.from) && visited.Contains(edge.to))
            )
            {
                visited.Add(edge.from);
                visited.Add(edge.to);
                edges[k] = (edge.from, edge.to, int.MaxValue);
                result += edge.cost;
                count++;
                k = edges.Count - 1;
            }
            else
            {
                k--;
            }
        }
        return result;
    }
}
