namespace Tests.Graph.DisjointSet;

// [Является ли граф валидным деревом](https://leetcode.com/explore/learn/card/graph/618/disjoint-set/3910/)
public class ValidTreeTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { 5, new int[][] { [0, 1], [0, 2], [0, 3], [1, 4] }, true },
            new object[] { 5, new int[][] { [0, 1], [1, 2], [2, 3], [1, 3], [1, 4] }, false },
            new object[] { 5, new int[][] { [0, 1], [0, 4], [1, 4], [2, 3] }, false },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int n, int[][] mat, bool expected)
    {
        var result = ValidTree(n, mat);
        Assert.Equal(expected, result);
    }

    public bool ValidTree(int n, int[][] edges)
    {
        if (edges.Length != n - 1)
        {
            return false;
        }

        var unionFind = new UnionFind(n);
        foreach (var edge in edges)
        {
            unionFind.Union(edge[0], edge[1]);
        }

        return unionFind.GetSetsCount() == 1;
    }

    private class UnionFind
    {
        public int[] root;
        public int[] rank;

        public UnionFind(int size)
        {
            root = new int[size];
            rank = new int[size];
            for (int i = 0; i < size; i++)
            {
                root[i] = i;
                rank[i] = 1;
            }
        }

        // Find with Path Compression
        public int Find(int x)
        {
            if (x == root[x])
            {
                return x;
            }
            // Recursively finds the root and flattens the structure
            return root[x] = Find(root[x]);
        }

        // Union by Rank
        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX != rootY)
            {
                if (rank[rootX] > rank[rootY])
                {
                    root[rootY] = rootX;
                }
                else if (rank[rootX] < rank[rootY])
                {
                    root[rootX] = rootY;
                }
                else
                {
                    // If ranks are equal, pick one as root and increase its rank
                    root[rootY] = rootX;
                    rank[rootX] += 1;
                }
            }
        }

        public int GetSetsCount()
        {
            int count = 0;
            for (int i = 0; i < root.Length; i++)
            {
                if (root[i] == i)
                    count++;
            }
            return count;
        }
    }
}
