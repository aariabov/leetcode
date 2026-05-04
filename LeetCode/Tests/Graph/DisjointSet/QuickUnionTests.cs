namespace Tests.Graph.DisjointSet;

public class QuickUnionTests
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

        public UnionFind(int size)
        {
            root = new int[size];
            // изначально, все элементы сами по себе (сами являются рутом)
            for (int i = 0; i < size; i++)
            {
                root[i] = i;
            }
        }

        // ищет рута, O(n)
        public int Find(int x)
        {
            // поднимаемся вверх до рута
            while (x != root[x])
            {
                x = root[x];
            }
            return x;
        }

        // выполняется быстрее, чем в QuickFind, O(n)
        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX != rootY)
            {
                // для элементов, у которых рут был rootY, после объединения будет rootX
                root[rootY] = rootX;
            }
        }

        public bool Connected(int x, int y)
        {
            return Find(x) == Find(y);
        }
    }
}
