namespace Tests.NaryTree.Traversal;

/// <summary>
/// [Обход n-арного девера в ширину](https://leetcode.com/explore/learn/card/n-ary-tree/130/traversal/915/)
/// </summary>
public class LevelOrderTests
{
    [Fact]
    public void Test()
    {
        var e1 = Node.BuildTree([1, null, 3, 2, 4, null, 5, 6]);

        var result = LevelOrder(e1);
        var expected = new int[][] { [1], [3, 2, 4], [5, 6] };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test1()
    {
        var e1 = Node.BuildTree(
            [
                1,
                null,
                2,
                3,
                4,
                5,
                null,
                null,
                6,
                7,
                null,
                8,
                null,
                9,
                10,
                null,
                null,
                11,
                null,
                12,
                null,
                13,
                null,
                null,
                14,
            ]
        );

        var result = LevelOrder(e1);
        var expected = new int[][] { [1], [2, 3, 4, 5], [6, 7, 8, 9, 10], [11, 12, 13], [14] };
        Assert.Equal(expected, result);
    }

    // решение аналогичное бинарному дереву
    public IList<IList<int>> LevelOrder(Node root)
    {
        var res = new List<IList<int>>();
        if (root == null)
        {
            return res;
        }

        var queue = new Queue<Node>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var levelSize = queue.Count;
            var levelList = new List<int>();
            for (int i = 0; i < levelSize; i++)
            {
                var node = queue.Dequeue();
                levelList.Add(node.val);
                foreach (var child in node.children)
                {
                    queue.Enqueue(child);
                }
            }
            res.Add(levelList);
        }
        return res;
    }
}
