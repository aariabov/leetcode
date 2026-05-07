namespace Tests.Graph.MST;

// [Соединить все точки с минимальной стоимостью](https://leetcode.com/explore/learn/card/graph/621/algorithms-to-construct-minimum-spanning-tree/3857/)
public class MinCostConnectPointsKraskalTests
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

    public int MinCostConnectPoints(int[][] points)
    {
        int n = points.Length;
        List<int[]> edges = new List<int[]>();

        // 1. Генерируем все возможные ребра и их веса
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                int dist =
                    Math.Abs(points[i][0] - points[j][0]) + Math.Abs(points[i][1] - points[j][1]);
                edges.Add(new int[] { dist, i, j });
            }
        }

        // 2. Сортируем ребра по возрастанию веса
        edges.Sort((a, b) => a[0].CompareTo(b[0]));

        // 3. Используем DSU для объединения точек
        UnionFind uf = new UnionFind(n);
        int totalCost = 0;
        int edgesUsed = 0;

        foreach (var edge in edges)
        {
            if (uf.Union(edge[1], edge[2]))
            {
                totalCost += edge[0];
                edgesUsed++;
                // Если соединили n-1 ребро, значит все точки связаны
                if (edgesUsed == n - 1)
                    break;
            }
        }

        return totalCost;
    }

    // Класс системы непересекающихся множеств
    public class UnionFind
    {
        private int[] parent;

        public UnionFind(int n)
        {
            parent = new int[n];
            for (int i = 0; i < n; i++)
                parent[i] = i;
        }

        public int Find(int i)
        {
            if (parent[i] == i)
                return i;
            return parent[i] = Find(parent[i]); // Сжатие пути
        }

        public bool Union(int i, int j)
        {
            int rootI = Find(i);
            int rootJ = Find(j);
            if (rootI != rootJ)
            {
                parent[rootI] = rootJ;
                return true;
            }
            return false;
        }
    }

    // работает, норм
    public int MyMinCostConnectPoints(int[][] points)
    {
        var dict = new Dictionary<(int x, int y), int>();
        var edges = new List<(int[] from, int[] to, int cost)>();
        for (int i = 0; i < points.Length; i++)
        {
            var point1 = points[i];
            dict.Add((point1[0], point1[1]), i);
            for (int j = i + 1; j < points.Length; j++)
            {
                var point2 = points[j];
                var cost = Math.Abs(point2[0] - point1[0]) + Math.Abs(point2[1] - point1[1]);
                edges.Add((point1, point2, cost));
            }
        }

        edges.Sort((a, b) => a.cost.CompareTo(b.cost));

        var unionFind = new MyUnionFind(points.Length);
        var result = 0;
        var count = 0;
        var edgeIdx = 0;
        while (count < points.Length - 1)
        {
            var (from, to, cost) = edges[edgeIdx];
            // проверить на цикл
            var point1Idx = dict[(from[0], from[1])];
            var point2Idx = dict[(to[0], to[1])];
            if (!unionFind.IsConnected(point1Idx, point2Idx))
            {
                unionFind.Union(point1Idx, point2Idx);
                result += cost;
                count++;
            }

            edgeIdx++;
        }
        return result;
    }

    private class MyUnionFind
    {
        private int[] root;

        public MyUnionFind(int size)
        {
            root = new int[size];
            for (int i = 0; i < size; i++)
            {
                root[i] = i;
            }
        }

        // поиск рута и обновление всех парентов до рутов, O(logN)
        public int Find(int x)
        {
            if (x == root[x])
            {
                return x;
            }

            int rootOfX = Find(root[x]);
            // присваиваем рут (сжатие пути)
            root[x] = rootOfX;

            return rootOfX;
        }

        // O(logN)
        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX != rootY)
            {
                root[rootY] = rootX;
            }
        }

        public bool IsConnected(int x, int y)
        {
            return Find(x) == Find(y);
        }
    }
}
