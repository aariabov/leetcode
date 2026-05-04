namespace Tests.Graph.DisjointSet;

public class QuickFindTests
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
        private readonly int[] arr;

        public UnionFind(int size)
        {
            arr = new int[size];
            // изначально, все элементы сами по себе (сами являются рутом)
            for (int i = 0; i < size; i++)
            {
                arr[i] = i;
            }
        }

        // возвращает рута для элемента, работает как O(1), потому что держим актуальным массив рутов
        private int Find(int x)
        {
            return arr[x];
        }

        // присоединяем y к x, работает со скоростью O(n)
        public void Union(int x, int y)
        {
            // определяем рутов
            int rootX = Find(x);
            int rootY = Find(y);
            // если руты разные, значит это два разных дерева (набора) - соединяем их
            if (rootX != rootY)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    // для элементов, у которых рут был rootY, после объединения будет rootX
                    if (arr[i] == rootY)
                    {
                        arr[i] = rootX;
                    }
                }
            }
        }

        // если у элементов одинаковый рут - они связаны (находятся в одном наборе)
        public bool Connected(int x, int y)
        {
            return Find(x) == Find(y);
        }
    }
}
