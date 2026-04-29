namespace Tests.Graph.DisjointSet;

// [Момент, когда все стали друзьями](https://leetcode.com/explore/learn/card/graph/618/disjoint-set/3912/)
public class EarliestAcqTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new int[][]
                {
                    [20190101, 0, 1],
                    [20190104, 3, 4],
                    [20190107, 2, 3],
                    [20190211, 1, 5],
                    [20190224, 2, 4],
                    [20190301, 0, 3],
                    [20190312, 1, 2],
                    [20190322, 4, 5],
                },
                6,
                20190301,
            },
            new object[]
            {
                new int[][] { [0, 2, 0], [1, 0, 1], [3, 0, 3], [4, 1, 2], [7, 3, 1] },
                4,
                3,
            },
            new object[]
            {
                new int[][] { [9, 3, 0], [0, 2, 1], [8, 0, 1], [1, 3, 2], [2, 2, 0], [3, 3, 1] },
                4,
                2,
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] mat, int n, int expected)
    {
        var result = EarliestAcq(mat, n);
        Assert.Equal(expected, result);
    }

    public int EarliestAcq(int[][] logs, int n)
    {
        Array.Sort(logs, (x, y) => x[0].CompareTo(y[0]));
        var unionFind = new UnionFind(n);
        foreach (var edge in logs)
        {
            unionFind.Union(edge[1], edge[2]);
            var count = unionFind.GetSetsCount();
            if (count == 1)
            {
                return edge[0];
            }
        }

        return -1;
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
