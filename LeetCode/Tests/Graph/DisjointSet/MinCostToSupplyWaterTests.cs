namespace Tests.Graph.DisjointSet;

// [Провести воду в деревне](https://leetcode.com/explore/learn/card/graph/618/disjoint-set/3916/)
public class MinCostToSupplyWaterTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { 3, new int[] { 1, 2, 2 }, new int[][] { [1, 2, 1], [2, 3, 1] }, 3 },
            new object[] { 2, new int[] { 1, 1 }, new int[][] { [1, 2, 1], [1, 2, 2] }, 2 },
            new object[]
            {
                9,
                new int[] { 58732, 77988, 55446, 79246, 8265, 30789, 39905, 79968, 61679 },
                new int[][]
                {
                    [2, 1, 45475],
                    [3, 2, 41579],
                    [4, 1, 79418],
                    [5, 2, 17589],
                    [7, 5, 4371],
                    [8, 5, 82103],
                    [9, 7, 55500],
                },
                362782,
            },
        };

    // Задача сводится к поиску минимального остовного дерева (MST)
    // Главная хитрость здесь — превратить строительство колодцев в обычную прокладку труб. Мы вводим фиктивный «нулевой» дом (дом 0), который представляет собой виртуальный источник воды.
    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int n, int[] wells, int[][] pipes, int expected)
    {
        var result = MinCostToSupplyWater(n, wells, pipes);
        Assert.Equal(expected, result);
    }

    public int MinCostToSupplyWater(int n, int[] wells, int[][] pipes)
    {
        // 1. Строим список смежности. Узлы от 0 до n. 0 — это виртуальный источник воды, чтобы веса колодцев в доме перевести в веса ребер
        // Индекс внешнего списка, это дом, внутренний список это куда можно попасть из внешнего дома
        List<List<(int to, int cost)>> adj = new List<List<(int, int)>>();
        for (int i = 0; i <= n; i++)
            adj.Add(new List<(int, int)>());

        // Добавляем ребра от узла 0 к каждому дому (стоимость колодца)
        for (int i = 0; i < n; i++)
        {
            adj[0].Add((i + 1, wells[i]));
            adj[i + 1].Add((0, wells[i]));
        }

        // Добавляем трубы между домами
        foreach (var pipe in pipes)
        {
            int u = pipe[0],
                v = pipe[1],
                cost = pipe[2];
            adj[u].Add((v, cost));
            adj[v].Add((u, cost));
        }

        // 2. Алгоритм Прима
        int totalCost = 0;
        int visitedCount = 0;
        bool[] visited = new bool[n + 1];

        // PriorityQueue хранит пары (узел, стоимость_ребра), сортируя по стоимости
        PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

        // Начинаем с виртуального узла 0
        pq.Enqueue(0, 0);

        while (visitedCount <= n && pq.Count > 0)
        {
            // жадно проходим по самым дешевым ребрам
            pq.TryDequeue(out int u, out int cost);

            if (visited[u])
                continue;

            // Добавляем стоимость в общий результат
            visited[u] = true;
            totalCost += cost;
            visitedCount++;

            // Добавляем всех соседей текущего узла в очередь
            foreach (var neighbor in adj[u])
            {
                if (!visited[neighbor.to])
                {
                    pq.Enqueue(neighbor.to, neighbor.cost);
                }
            }
        }

        return totalCost;
    }

    // алгоритм Крускала
    public int MinCostToSupplyWaterКрускал(int n, int[] wells, int[][] pipes)
    {
        List<int[]> edges = new List<int[]>();

        // 1. Добавляем фиктивные ребра от "нулевого" узла к каждому дому.
        // Вес ребра (0, i) равен стоимости колодца в доме i.
        for (int i = 0; i < n; i++)
        {
            edges.Add(new int[] { 0, i + 1, wells[i] });
        }

        // 2. Добавляем существующие трубы в список ребер.
        foreach (var pipe in pipes)
        {
            edges.Add(pipe);
        }

        // 3. Сортируем все ребра по стоимости (алгортим Крускала, жадный выбор для MST)
        edges.Sort((a, b) => a[2].CompareTo(b[2]));

        // 4. Используем Union-Find для построения MST.
        UnionFind uf = new UnionFind(n + 1);
        int minCost = 0;
        int edgesCount = 0;

        foreach (var edge in edges)
        {
            if (uf.Union(edge[0], edge[1]))
            {
                minCost += edge[2];
                edgesCount++;
                // Если соединили все n+1 узлов (нужно n ребер), выходим.
                if (edgesCount == n)
                    break;
            }
        }

        return minCost;
    }

    // Вспомогательный класс Union-Find (DSU) с оптимизациями
    public class UnionFind
    {
        private int[] parent;
        private int[] rank;

        public UnionFind(int size)
        {
            parent = new int[size];
            rank = new int[size];
            for (int i = 0; i < size; i++)
            {
                parent[i] = i;
            }
        }

        public int Find(int i)
        {
            if (parent[i] == i)
                return i;
            return parent[i] = Find(parent[i]); // Сжатие путей
        }

        public bool Union(int i, int j)
        {
            int rootI = Find(i);
            int rootJ = Find(j);
            if (rootI != rootJ)
            {
                // Объединение по рангу
                if (rank[rootI] < rank[rootJ])
                {
                    parent[rootI] = rootJ;
                }
                else if (rank[rootI] > rank[rootJ])
                {
                    parent[rootJ] = rootI;
                }
                else
                {
                    parent[rootI] = rootJ;
                    rank[rootJ]++;
                }
                return true;
            }
            return false;
        }
    }
}
