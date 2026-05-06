using FluentAssertions;

namespace Tests.Graph.BFSInGraph;

// [Все пути между вершинами](https://leetcode.com/explore/learn/card/graph/619/depth-first-search-in-graph/3849/)
// https://leetcode.com/explore/learn/card/graph/620/breadth-first-search-in-graph/3853/
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
        result.Should().BeEquivalentTo(expected);
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

    public IList<IList<int>> AllPathsSourceTargetBFS(int[][] graph)
    {
        var result = new List<IList<int>>();
        var queue = new Queue<List<int>>();
        queue.Enqueue(new List<int> { 0 });
        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            var last = path[^1];
            if (last == graph.Length - 1)
            {
                result.Add(path);
            }

            foreach (var neighbor in graph[last])
            {
                var newList = new List<int>(path);
                newList.Add(neighbor);
                queue.Enqueue(newList);
            }
        }

        return result;
    }

    // самый быстрый вариант
    // Идея: рекурсивно идем в глубину, собираем path, при возврате из рекурсии удаляем последний узел (Бэктрекинг), таким образом path всегда актуальный
    public IList<IList<int>> AllPathsSourceTargetBack(int[][] graph)
    {
        var result = new List<IList<int>>();
        var path = new List<int> { 0 };

        Dfs();
        return result;

        void Dfs()
        {
            int node = path[^1];
            // Если дошли до последнего узла (n - 1)
            if (node == graph.Length - 1)
            {
                result.Add(new List<int>(path)); // добавляем копию списка в результат
                return;
            }

            // Проходим по всем соседям текущего узла
            foreach (int neighbor in graph[node])
            {
                path.Add(neighbor);
                Dfs();
                path.RemoveAt(path.Count - 1); // Бэктрекинг: удаляем узел перед возвратом
            }
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
