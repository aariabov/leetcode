namespace Tests.Graph.Kahn;

// [Найти корни деревьев с минимальной высотой](https://leetcode.com/explore/learn/card/graph/623/kahns-algorithm-for-topological-sorting/3953/)
public class FindMinHeightTreesTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { 4, new int[][] { [1, 0], [1, 2], [1, 3] }, new int[] { 1 } },
            new object[]
            {
                6,
                new int[][] { [3, 0], [3, 1], [3, 2], [3, 4], [5, 4] },
                new int[] { 3, 4 },
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int n, int[][] edges, int[] expected)
    {
        var result = FindMinHeightTrees(n, edges);
        Assert.Equal(expected, result);
    }

    // Идея: обрезаем листья до тех пор, пока не дойдем до центра, состоящего из 1ого или 2х узлов
    public IList<int> FindMinHeightTrees(int n, int[][] edges)
    {
        // Базовый случай: если узлов 1 или 2, все они являются центрами
        if (n <= 2)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < n; i++)
            {
                res.Add(i);
            }
            return res;
        }

        // Построение списка смежности и подсчет степеней узлов
        var adj = new List<int>[n];
        var degree = new int[n];
        for (int i = 0; i < n; i++)
        {
            adj[i] = new List<int>();
        }

        foreach (var edge in edges)
        {
            adj[edge[0]].Add(edge[1]);
            adj[edge[1]].Add(edge[0]);
            degree[edge[0]]++;
            degree[edge[1]]++;
        }

        // Очередь для текущих листьев (узлы с одной связью)
        var queue = new Queue<int>();
        for (int i = 0; i < n; i++)
        {
            if (degree[i] == 1)
            {
                queue.Enqueue(i);
            }
        }

        int remainingNodes = n;
        // Удаляем слои листьев, пока не останется 1 или 2 узла
        while (remainingNodes > 2)
        {
            int leafCount = queue.Count;
            remainingNodes -= leafCount;

            for (int i = 0; i < leafCount; i++)
            {
                int leaf = queue.Dequeue();

                foreach (int neighbor in adj[leaf])
                {
                    degree[neighbor]--;
                    // Если сосед стал листом после удаления текущего узла
                    if (degree[neighbor] == 1)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        // Оставшиеся в очереди узлы — это и есть корни MHT
        return queue.ToList();
    }

    // работает, но не проходит по времени
    public IList<int> FindMinHeightTreesBfs(int n, int[][] edges)
    {
        var res = new int[n];
        var min = int.MaxValue;
        for (int i = 0; i < n; i++)
        {
            var adj = new Dictionary<int, HashSet<int>>();
            for (int j = 0; j < n; j++)
            {
                adj.Add(j, []);
            }

            foreach (var edge in edges)
            {
                adj[edge[0]].Add(edge[1]);
                adj[edge[1]].Add(edge[0]);
            }

            var queue = new Queue<int>();
            queue.Enqueue(i);
            int depth = 0;

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                depth++;

                for (int k = 0; k < levelSize; k++)
                {
                    var node = queue.Dequeue();
                    if (adj.ContainsKey(node))
                    {
                        foreach (var to in adj[node])
                        {
                            queue.Enqueue(to);
                            adj[to].Remove(node);
                        }

                        adj.Remove(node);
                    }
                }
            }

            res[i] = depth;
            if (depth < min)
            {
                min = depth;
            }
        }

        var result = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (res[i] == min)
            {
                result.Add(i);
            }
        }

        return result;
    }
}
