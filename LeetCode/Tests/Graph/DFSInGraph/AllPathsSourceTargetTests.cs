namespace Tests.Graph.DFSInGraph;

// [Все пути между вершинами](https://leetcode.com/explore/learn/card/graph/619/depth-first-search-in-graph/3849/)
public class AllPathsSourceTargetTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new int[][] { [1, 2], [3], [3], [] },
                new int[][] { [0, 2, 3], [0, 1, 3] },
            },
            new object[]
            {
                new int[][] { [4, 3, 1], [3, 2, 4], [3], [4], [] },
                new int[][] { [0, 1, 4], [0, 1, 2, 3, 4], [0, 1, 3, 4], [0, 3, 4], [0, 4] },
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] nums, int[][] expected)
    {
        var result = AllPathsSourceTarget(nums);
        Assert.Equal(expected, result);
    }

    public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
    {
        var result = new List<IList<int>>();
        var start = 0;
        var end = graph.Length - 1;

        var stack = new Stack<List<int>>();
        stack.Push(new List<int> { start });
        while (stack.Count > 0)
        {
            var list = stack.Pop();
            var last = list[^1];
            if (last == end)
            {
                result.Add(list);
                continue;
            }

            var neibors = graph[last];
            foreach (var neibor in neibors)
            {
                var newList = new List<int>(list);
                newList.Add(neibor);
                stack.Push(newList);
            }
        }

        return result;
    }

    // самый быстрый вариант
    public IList<IList<int>> AllPathsSourceTargetBack(int[][] graph)
    {
        var result = new List<IList<int>>();
        var path = new List<int> { 0 }; // Начинаем с нулевого узла

        Dfs(0, graph, path, result);

        return result;
    }

    private void Dfs(int node, int[][] graph, List<int> path, List<IList<int>> result)
    {
        // Если дошли до последнего узла (n - 1)
        if (node == graph.Length - 1)
        {
            result.Add(new List<int>(path));
            return;
        }

        // Проходим по всем соседям текущего узла
        foreach (int neighbor in graph[node])
        {
            path.Add(neighbor); // Добавляем соседа в путь
            Dfs(neighbor, graph, path, result); // Рекурсивный переход
            path.RemoveAt(path.Count - 1); // Бэктрекинг: удаляем узел перед возвратом
        }
    }

    public IList<IList<int>> AllPathsSourceTargetRec(int[][] graph)
    {
        var result = new List<IList<int>>();
        var start = 0;
        var end = graph.Length - 1;
        DFS(new List<int> { start });
        return result;

        void DFS(List<int> list)
        {
            var last = list[^1];
            if (last == end)
            {
                result.Add(list);
                return;
            }

            var neibors = graph[last];
            foreach (var neibor in neibors)
            {
                var newList = new List<int>(list);
                newList.Add(neibor);
                DFS(newList);
            }
        }
    }
}
