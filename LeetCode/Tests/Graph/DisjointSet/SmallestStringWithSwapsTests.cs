namespace Tests.Graph.DisjointSet;

// [Минимальная строка с перестановками](https://leetcode.com/explore/learn/card/graph/618/disjoint-set/3913/)
public class SmallestStringWithSwapsTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { "dcab", new int[][] { [0, 3], [1, 2] }, "bacd" },
            new object[] { "dcab", new int[][] { [0, 3], [1, 2], [0, 2] }, "abcd" },
            new object[] { "cba", new int[][] { [0, 1], [1, 2] }, "abc" },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(string s, IList<IList<int>> pairs, string expected)
    {
        var result = SmallestStringWithSwaps(s, pairs);
        Assert.Equal(expected, result);
    }

    public string SmallestStringWithSwaps(string s, IList<IList<int>> pairs)
    {
        // рассматриваем символы, как узлы, а пары как ребра (связи).
        // по итогу получаем группы символов
        var unionFind = new UnionFind(s.Length);
        foreach (var pair in pairs)
        {
            unionFind.Union(pair[0], pair[1]);
        }

        // символы внутри группы можно по разному перемещать.
        // перемещаем их в алфавитном порядке, используем PriorityQueue
        // по итогу в каждой группе будут отсортированные символы
        var dict = new Dictionary<int, PriorityQueue<char, char>>();
        for (int i = 0; i < s.Length; i++)
        {
            // получаем индекс рута для каждой позиции
            var rootIdx = unionFind.Find(i);
            var priorityQueue = new PriorityQueue<char, char>();
            priorityQueue.Enqueue(s[i], s[i]);
            if (!dict.TryAdd(unionFind.root[rootIdx], priorityQueue))
            {
                dict[unionFind.root[rootIdx]].Enqueue(s[i], s[i]);
            }
        }

        // теперь символы надо просто по очереди достать
        var result = new char[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            // для каждой позиции находим индекс рута, это и будет индекс группы, из которой будем доставать
            var root = unionFind.root[i];
            var chr = dict[root].Dequeue();
            result[i] = chr;
        }

        return new string(result);
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
    }
}
