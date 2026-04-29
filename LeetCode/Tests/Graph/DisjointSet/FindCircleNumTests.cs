namespace Tests.Graph.DisjointSet;

// [Количество провинций](https://leetcode.com/explore/learn/card/graph/618/disjoint-set/3845/)
public class FindCircleNumTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { new int[][] { [1, 1, 0], [1, 1, 0], [0, 0, 1] }, 2 },
            new object[] { new int[][] { [1, 0, 0], [0, 1, 0], [0, 0, 1] }, 3 },
            new object[]
            {
                new int[][]
                {
                    [1, 1, 1, 0, 1, 1, 1, 0, 0, 0],
                    [1, 1, 0, 0, 0, 0, 0, 1, 0, 0],
                    [1, 0, 1, 0, 0, 0, 0, 0, 0, 0],
                    [0, 0, 0, 1, 1, 0, 0, 0, 1, 0],
                    [1, 0, 0, 1, 1, 0, 0, 0, 0, 0],
                    [1, 0, 0, 0, 0, 1, 0, 0, 0, 0],
                    [1, 0, 0, 0, 0, 0, 1, 0, 1, 0],
                    [0, 1, 0, 0, 0, 0, 0, 1, 0, 1],
                    [0, 0, 0, 1, 0, 0, 1, 0, 1, 1],
                    [0, 0, 0, 0, 0, 0, 0, 1, 1, 1],
                },
                1,
            },
            new object[]
            {
                new int[][]
                {
                    [1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0],
                    [1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                    [0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                    [0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0],
                    [0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0],
                    [0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0],
                    [1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0],
                    [0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1],
                    [0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0],
                    [0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1],
                },
                3,
            },
            new object[]
            {
                new int[][] { [1, 0, 0, 1], [0, 1, 1, 0], [0, 1, 1, 1], [1, 0, 1, 1] },
                1,
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int[][] mat, int expected)
    {
        var result = FindCircleNum(mat);
        Assert.Equal(expected, result);
    }

    // работает и быстро
    public int FindCircleNum(int[][] isConnected)
    {
        var unionFind = new UnionFind(isConnected.Length);
        for (int i = 0; i < isConnected.Length; i++)
        {
            var row = isConnected[i];
            for (int j = i + 1; j < isConnected.Length; j++)
            {
                if (row[j] == 1)
                {
                    unionFind.Union(i, j);
                }
            }
        }

        return unionFind.GetSetsCount();
    }

    private class UnionFind
    {
        private int[] root;
        private int[] rank;

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

        public bool Connected(int x, int y)
        {
            return Find(x) == Find(y);
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
