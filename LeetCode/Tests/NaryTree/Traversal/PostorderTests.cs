namespace Tests.NaryTree.Traversal;

/// <summary>
/// [Postorder обход n-арного дерева](https://leetcode.com/explore/learn/card/n-ary-tree/130/traversal/926/)
/// </summary>
public class PostorderTests
{
    [Fact]
    public void Test1()
    {
        var e1 = Node.BuildTree([1, null, 3, 2, 4, null, 5, 6]);

        var result = Postorder(e1);
        var expected = new int[] { 5, 6, 3, 2, 4, 1 };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test2()
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

        var result = Postorder(e1);
        var expected = new int[] { 2, 6, 14, 11, 7, 3, 12, 8, 4, 13, 9, 10, 5, 1 };
        Assert.Equal(expected, result);
    }

    // решение аналогичное бинарному дереву
    public IList<int> Postorder(Node root)
    {
        var res = new List<int>();
        Rec(root);
        return res;

        void Rec(Node? node)
        {
            if (node == null)
            {
                return;
            }

            foreach (var child in node.children)
            {
                Rec(child);
            }

            res.Add(node.val);
        }
    }
}
