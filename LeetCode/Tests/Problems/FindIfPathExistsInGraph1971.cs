namespace Tests.Problems;

public class FindIfPathExistsInGraph1971
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { 3, new int[][] { [0, 1], [1, 2], [2, 0] }, 0, 2, true },
            new object[] { 1, new int[][] { [] }, 0, 0, true },
            new object[] { 6, new int[][] { [0, 1], [0, 2], [3, 5], [5, 4], [4, 3] }, 0, 5, false },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int n, int[][] edges, int source, int destination, bool expected)
    {
        var result = ValidPath(n, edges, source, destination);
        Assert.Equal(expected, result);
    }

    public bool ValidPath(int n, int[][] edges, int source, int destination)
    {
        if (source == destination)
            return true;

        // 1. Строим список смежности
        var adj = new List<int>[n];
        for (int i = 0; i < n; i++)
            adj[i] = new List<int>();

        foreach (var edge in edges)
        {
            adj[edge[0]].Add(edge[1]);
            adj[edge[1]].Add(edge[0]);
        }

        // 2. BFS
        var queue = new Queue<int>();
        var visited = new bool[n];

        queue.Enqueue(source);
        visited[source] = true;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            if (current == destination)
                return true;

            foreach (int neighbor in adj[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return false;
    }

    // Работает, быстрее рекурсивного варианта
    public bool ValidPathIter(int n, int[][] edges, int source, int destination)
    {
        if (source == destination)
        {
            return true;
        }

        if (edges.Length == 0)
        {
            return false;
        }

        var dict = new Dictionary<int, List<int>>(n);
        foreach (var edge in edges)
        {
            if (!dict.ContainsKey(edge[0]))
            {
                dict.Add(edge[0], [edge[1]]);
            }
            else
            {
                dict[edge[0]].Add(edge[1]);
            }

            if (!dict.ContainsKey(edge[1]))
            {
                dict.Add(edge[1], [edge[0]]);
            }
            else
            {
                dict[edge[1]].Add(edge[0]);
            }
        }

        var visited = new HashSet<int>();
        var stack = new Stack<int>();

        stack.Push(source);

        while (stack.Count > 0)
        {
            int vertex = stack.Pop();

            // Если нашли цель — немедленно выходим из всего метода
            if (vertex == destination)
                return true;

            // Если вершина уже была обработана, пропускаем её
            if (!visited.Add(vertex))
                continue;

            // Добавляем соседей в стек для дальнейшей проверки
            if (dict.TryGetValue(vertex, out var neighbors))
            {
                foreach (var neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        stack.Push(neighbor);
                    }
                }
            }
        }

        return false; // Если стек опустел, а цель не найдена
    }

    // работает, но не очень быстро
    public bool ValidPathRec(int n, int[][] edges, int source, int destination)
    {
        if (source == destination)
        {
            return true;
        }

        if (edges.Length == 0)
        {
            return false;
        }

        var dict = new Dictionary<int, List<int>>(n);
        foreach (var edge in edges)
        {
            if (!dict.ContainsKey(edge[0]))
            {
                dict.Add(edge[0], [edge[1]]);
            }
            else
            {
                dict[edge[0]].Add(edge[1]);
            }

            if (!dict.ContainsKey(edge[1]))
            {
                dict.Add(edge[1], [edge[0]]);
            }
            else
            {
                dict[edge[1]].Add(edge[0]);
            }
        }

        var visited = new HashSet<int>();
        var isFound = false;
        Rec(source);
        return isFound;

        void Rec(int vertex)
        {
            if (isFound)
            {
                return;
            }

            if (vertex == destination)
            {
                isFound = true;
                return;
            }

            if (!visited.Add(vertex))
            {
                return;
            }

            var vertexEdges = dict[vertex];
            foreach (var vertexEdge in vertexEdges)
            {
                if (visited.Contains(vertexEdge) || isFound)
                {
                    continue;
                }

                Rec(vertexEdge);
            }
        }
    }
}
