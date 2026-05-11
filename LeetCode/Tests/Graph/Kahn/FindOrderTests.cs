using FluentAssertions;

namespace Tests.Graph.Kahn;

// [Последовательность прохождения курсов](https://leetcode.com/explore/learn/card/graph/623/kahns-algorithm-for-topological-sorting/3868/)
public class FindOrderTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { 2, new int[][] { [1, 0] }, new[] { 0, 1 } },
            new object[]
            {
                4,
                new int[][] { [1, 0], [2, 0], [3, 1], [3, 2] },
                new[] { 0, 2, 1, 3 },
            },
            new object[] { 1, new int[][] { }, new[] { 0 } },
            new object[] { 3, new int[][] { [1, 0], [1, 2], [0, 1] }, new int[] { } },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int numCourses, int[][] mat, int[] expected)
    {
        var result = FindOrder(numCourses, mat);
        result.Should().BeEquivalentTo(expected);
    }

    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        var ins = new int[numCourses];
        var adj = new List<int>[numCourses];
        for (int i = 0; i < numCourses; i++)
        {
            adj[i] = [];
        }

        // составляем матрицу смежности, и определяем количество входящих
        foreach (var p in prerequisites)
        {
            adj[p[1]].Add(p[0]);
            ins[p[0]]++;
        }

        // добавляем в очередь узлы, которые ни от кого не зависят
        var queue = new Queue<int>();
        for (int i = 0; i < ins.Length; i++)
        {
            if (ins[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        var result = new List<int>(numCourses);
        var visited = new bool[numCourses];
        while (queue.Count > 0)
        {
            var curr = queue.Dequeue();
            if (!visited[curr])
            {
                visited[curr] = true;
                result.Add(curr);

                foreach (var neighbor in adj[curr])
                {
                    // уменьшаем количество входящих
                    ins[neighbor]--;

                    // если узел больше ни от кого не зависит, то добавляем его в очередь
                    if (ins[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return result.Count == numCourses ? result.ToArray() : [];
    }
}
