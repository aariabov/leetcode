namespace Tests.Graph.DisjointSet;

public class UnionByRankTests
{
    [Fact]
    public void Test()
    {
        var uf = new UnionFind(10);

        // 1-2-5-6-7 3-8-9 4
        uf.Union(1, 2);
        uf.Union(2, 5);
        uf.Union(5, 6);
        uf.Union(6, 7);
        uf.Union(3, 8);
        uf.Union(8, 9);

        Assert.True(uf.Connected(1, 5));
        Assert.True(uf.Connected(5, 7));
        Assert.False(uf.Connected(4, 9));

        // 1-2-5-6-7 3-8-9-4
        uf.Union(9, 4);
        Assert.True(uf.Connected(4, 9));
    }

    private class UnionFind
    {
        private int[] root;
        private int[] rank; // используется, чтобы дерево не разрасталось в высоту, ибо в высоком дереве дольше искать

        public UnionFind(int size)
        {
            root = new int[size];
            rank = new int[size];
            for (int i = 0; i < size; i++)
            {
                root[i] = i;
                rank[i] = 1; // изначально, ранг у всех 1
            }
        }

        // O(logN)
        public int Find(int x)
        {
            while (x != root[x])
            {
                x = root[x];
            }
            return x;
        }

        // идея: сделать сбалансированное дерево (с минимальной высотой), чтобы поиск работал за O(logN)
        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX != rootY)
            {
                if (rank[rootX] > rank[rootY])
                {
                    // если дерево rootX выше, то присоединяем к нему
                    root[rootY] = rootX;
                }
                else if (rank[rootX] < rank[rootY])
                {
                    // если дерево rootY выше, то присоединяем к нему
                    root[rootX] = rootY;
                }
                else
                {
                    // иначе, присоединяем к rootX и увеличиваем ранг (высоту)
                    root[rootY] = rootX;
                    rank[rootX] += 1;
                }
            }
        }

        public bool Connected(int x, int y)
        {
            return Find(x) == Find(y);
        }
    }
}
