namespace Tests.Graph.DisjointSet;

// [Вывод результата деления](https://leetcode.com/explore/learn/card/graph/618/disjoint-set/3914/)
public class CalcEquationTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new List<IList<string>>
                {
                    new List<string> { "a", "b" },
                    new List<string> { "b", "c" },
                },
                new double[] { 2.0, 3.0 },
                new List<IList<string>>
                {
                    new List<string> { "a", "c" },
                    new List<string> { "b", "a" },
                    new List<string> { "a", "e" },
                    new List<string> { "a", "a" },
                    new List<string> { "x", "x" },
                },
                new double[] { 6.00000, 0.50000, -1.00000, 1.00000, -1.00000 },
            },
            new object[]
            {
                new List<IList<string>>
                {
                    new List<string> { "a", "b" },
                    new List<string> { "b", "c" },
                    new List<string> { "bc", "cd" },
                },
                new double[] { 1.5, 2.5, 5.0 },
                new List<IList<string>>
                {
                    new List<string> { "a", "c" },
                    new List<string> { "c", "b" },
                    new List<string> { "bc", "cd" },
                    new List<string> { "cd", "bc" },
                },
                new double[] { 3.75000, 0.40000, 5.00000, 0.20000 },
            },
            new object[]
            {
                new List<IList<string>>
                {
                    new List<string> { "a", "b" },
                },
                new double[] { 0.5 },
                new List<IList<string>>
                {
                    new List<string> { "a", "b" },
                    new List<string> { "b", "a" },
                    new List<string> { "a", "c" },
                    new List<string> { "x", "y" },
                },
                new double[] { 0.50000, 2.00000, -1.00000, -1.00000 },
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(
        IList<IList<string>> equations,
        double[] values,
        IList<IList<string>> queries,
        double[] expected
    )
    {
        var result = CalcEquation(equations, values, queries);
        Assert.Equal(expected, result);
    }

    public double[] CalcEquation(
        IList<IList<string>> equations,
        double[] values,
        IList<IList<string>> queries
    )
    {
        UnionFind uf = new UnionFind();

        for (int i = 0; i < equations.Count; i++)
        {
            uf.Union(equations[i][0], equations[i][1], values[i]);
        }

        double[] results = new double[queries.Count];
        for (int i = 0; i < queries.Count; i++)
        {
            results[i] = uf.IsConnected(queries[i][0], queries[i][1]);
        }

        return results;
    }

    private class UnionFind
    {
        // parent хранит идентификатор родителя
        private Dictionary<string, string> childToParentDict = new Dictionary<string, string>();

        // weight хранит отношение: node / parent[node]
        private Dictionary<string, double> childToParentWeightDict =
            new Dictionary<string, double>();

        public void Add(string x)
        {
            if (!childToParentDict.ContainsKey(x))
            {
                // изначально, все элементы сами по себе являются рутами, соответственно и вес = 1
                childToParentDict[x] = x;
                childToParentWeightDict[x] = 1.0;
            }
        }

        public string Find(string x)
        {
            if (childToParentDict[x] == x)
            {
                // дошли до рута
                return x;
            }

            string oldParent = childToParentDict[x];
            // DFS: рекурсивно поднимается к руту и обновляем веса
            string root = Find(oldParent);

            // Сжатие пути: обновляем вес текущего узла относительно нового корня
            childToParentWeightDict[x] *= childToParentWeightDict[oldParent];
            childToParentDict[x] = root;

            return root;
        }

        public void Union(string a, string b, double value)
        {
            Add(a);
            Add(b);
            string rootA = Find(a);
            string rootB = Find(b);

            if (rootA != rootB)
            {
                // обновляем веса
                childToParentDict[rootA] = rootB;
                var weightB = childToParentWeightDict[b];
                var weightA = childToParentWeightDict[a];
                childToParentWeightDict[rootA] = value * weightB / weightA;
            }
        }

        public double IsConnected(string x, string y)
        {
            if (!childToParentDict.ContainsKey(x) || !childToParentDict.ContainsKey(y))
                return -1.0;

            string rootX = Find(x);
            string rootY = Find(y);

            if (rootX != rootY)
                return -1.0;

            return childToParentWeightDict[x] / childToParentWeightDict[y];
        }
    }
}
