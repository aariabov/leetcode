namespace Tests.Graph.Kahn;

// [Определить количество семестров для прохождения всех курсов](https://leetcode.com/explore/learn/card/graph/623/kahns-algorithm-for-topological-sorting/3954/)
public class MinimumSemestersTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { 3, new int[][] { [1, 3], [2, 3] }, 2 },
            new object[] { 3, new int[][] { [1, 2], [2, 3], [3, 1] }, -1 },
            new object[] { 4, new int[][] { [1, 2], [3, 4], [4, 3] }, -1 },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int n, int[][] mat, int expected)
    {
        var result = MinimumSemesters(n, mat);
        Assert.Equal(expected, result);
    }

    public int MinimumSemesters(int n, int[][] relations)
    {
        var ins = new int[n + 1];
        var adj = new List<int>[n + 1];
        for (int i = 0; i < n + 1; i++)
        {
            adj[i] = [];
        }

        foreach (var p in relations)
        {
            adj[p[0]].Add(p[1]);
            ins[p[1]]++;
        }

        var queue = new Queue<int>();
        for (int i = 1; i < ins.Length; i++)
        {
            if (ins[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        if (queue.Count == 0)
        {
            return -1;
        }

        var level = 0;
        var result = new List<int>(n);
        var visited = new bool[n + 1];
        while (queue.Count > 0)
        {
            int levelSize = queue.Count;
            level++;

            for (int k = 0; k < levelSize; k++)
            {
                var curr = queue.Dequeue();
                if (!visited[curr])
                {
                    visited[curr] = true;
                    result.Add(curr);

                    foreach (var neighbor in adj[curr])
                    {
                        ins[neighbor]--;
                        if (ins[neighbor] == 0)
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }
        }

        return result.Count == n ? level : -1;
    }
}
