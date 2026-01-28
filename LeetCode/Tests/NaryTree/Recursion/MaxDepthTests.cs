namespace Tests.NaryTree.Recursion;

/// <summary>
/// [Глубина n-арного дерева](https://leetcode.com/explore/learn/card/n-ary-tree/131/recursion/919/)
/// </summary>
public class MaxDepthTests
{
    [Fact]
    public void Test()
    {
        var e1 = Node.BuildTree([1, null, 3, 2, 4, null, 5, 6]);

        var result = MaxDepth(e1);
        Assert.Equal(3, result);
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

        var result = MaxDepth(e1);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test2()
    {
        var e1 = Node.BuildTree([]);

        var result = MaxDepth(e1);
        Assert.Equal(0, result);
    }

    // Top-Down (передаем значение depth + 1 детям) рекурсивное решение - для меня более понятное
    public int MaxDepth(Node root)
    {
        if (root == null)
        {
            return 0;
        }

        var result = 0;
        Rec(root, 1);
        return result;

        void Rec(Node node, int depth)
        {
            if (node.children.Count == 0)
            {
                if (depth > result)
                {
                    result = depth;
                }
                return;
            }

            foreach (var child in node.children)
            {
                Rec(child, depth + 1);
            }
        }
    }

    // Bottom-up (c самого низа возвращаем значение) рекурсивное решение
    public int MaxDepthBottomUp(Node root)
    {
        if (root == null)
        {
            return 0;
        }

        if (root.children.Count == 0)
            return 1;

        var res = new List<int>();
        foreach (var child in root.children)
        {
            res.Add(MaxDepthBottomUp(child));
        }

        // высота для текущего узла
        return res.Max() + 1;
    }
}
