namespace Tests.Recursion1.ComplexityAnalysis;

/// <summary>
/// [Максимальная глубина бинарного дерева](https://leetcode.com/explore/learn/card/recursion-i/256/complexity-analysis/2375/)
/// </summary>
public class MaxDepthTests
{
    [Fact]
    public void Test()
    {
        var e1 = new TreeNode(3);
        var e2 = new TreeNode(9);
        var e3 = new TreeNode(20);
        var e4 = new TreeNode(15);
        var e5 = new TreeNode(7);

        e1.left = e2;
        e1.right = e3;
        e3.left = e4;
        e3.right = e5;

        var result = MaxDepth(e1);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test1()
    {
        var e1 = new TreeNode(1);
        var e2 = new TreeNode(2);

        e1.right = e2;

        var result = MaxDepth(e1);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test2()
    {
        var result = MaxDepth(null);
        Assert.Equal(0, result);
    }

    public int MaxDepth(TreeNode root)
    {
        if (root == null)
            return 0;

        int leftDepth = MaxDepth(root.left);
        int rightDepth = MaxDepth(root.right);

        return Math.Max(leftDepth, rightDepth) + 1;
    }

    public int MaxDepthMy(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }

        var max = 0;
        Rec(root, 1);
        return max;

        void Rec(TreeNode node, int depth)
        {
            if (node.right == null && node.left == null)
            {
                if (depth > max)
                {
                    max = depth;
                }
                return;
            }

            if (node.left != null)
            {
                Rec(node.left, depth + 1);
            }

            if (node.right != null)
            {
                Rec(node.right, depth + 1);
            }
        }
    }
}
