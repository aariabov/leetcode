namespace Tests.Graph.DFSInGraph;

// [Проверить, что все пути в графе ведут к заданному узлу](https://leetcode.com/explore/learn/card/graph/619/depth-first-search-in-graph/3951/)
public class LeadsToDestinationTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { 3, new int[][] { [0, 1], [0, 2] }, 0, 2, false },
            new object[] { 4, new int[][] { [0, 1], [0, 3], [1, 2], [2, 1] }, 0, 3, false },
            new object[] { 4, new int[][] { [0, 1], [0, 2], [1, 3], [2, 3] }, 0, 3, true },
            new object[] { 2, new int[][] { [0, 1], [1, 1] }, 0, 1, false },
            new object[]
            {
                5,
                new int[][]
                {
                    [0, 1],
                    [0, 2],
                    [0, 3],
                    [0, 3],
                    [1, 2],
                    [1, 3],
                    [1, 4],
                    [2, 3],
                    [2, 4],
                    [3, 4],
                },
                0,
                4,
                true,
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int n, int[][] edges, int source, int destination, bool expected)
    {
        var result = LeadsToDestination(n, edges, source, destination);
        Assert.Equal(expected, result);
    }

    public bool LeadsToDestination(int n, int[][] edges, int source, int destination)
    {
        // 1. Строим граф (список смежности)
        var adj = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            adj[i] = new List<int>();
        }

        foreach (var edge in edges)
        {
            adj[edge[0]].Add(edge[1]);
        }

        // Состояния узлов: 0 - не посещен, 1 - в процессе, 2 - проверен успешно
        int[] states = new int[n];

        return Dfs(source);

        bool Dfs(int curr)
        {
            // Если узел в процессе обхода — обнаружен цикл
            if (states[curr] == 1)
            {
                return false;
            }

            // Если узел уже полностью проверен ранее
            if (states[curr] == 2)
            {
                return true;
            }

            // Если у узла нет исходящих ребер, он должен быть пунктом назначения
            if (adj[curr].Count == 0)
            {
                return curr == destination;
            }

            // Помечаем как "в процессе"
            states[curr] = 1;

            // Рекурсивно проверяем всех соседей
            foreach (int next in adj[curr])
            {
                if (!Dfs(next))
                {
                    return false;
                }
            }

            // Помечаем как "полностью проверенный"
            states[curr] = 2;
            return true;
        }
    }
}
