namespace Tests.BinarySearchTree.HeightBalanced;

/// <summary>
/// [Сбалансировано ли дерево по высоте](https://leetcode.com/explore/learn/card/introduction-to-data-structure-binary-search-tree/143/appendix-height-balanced-bst/1027/)
/// </summary>
public class IsBalancedTests
{
    [Fact]
    public void Test()
    {
        var e1 = TreeNode.BuildTree([3, 9, 20, null, null, 15, 7]);

        var result = IsBalanced(e1);
        Assert.True(result);
    }

    [Fact]
    public void Test1()
    {
        var e1 = TreeNode.BuildTree([1, 2, 2, 3, 3, null, null, 4, 4]);

        var result = IsBalanced(e1);
        Assert.False(result);
    }

    [Fact]
    public void Test2()
    {
        var e1 = TreeNode.BuildTree([]);

        var result = IsBalanced(e1);
        Assert.True(result);
    }

    [Fact]
    public void Test3()
    {
        var e1 = TreeNode.BuildTree([1, null, 2, null, 3]);

        var result = IsBalanced(e1);
        Assert.False(result);
    }

    [Fact]
    public void Test4()
    {
        var e1 = TreeNode.BuildTree([1, 2, 3, 4, 5, 6, null, 8]);

        var result = IsBalanced(e1);
        Assert.True(result);
    }

    // дерево высотно-сбалансированное, если у каждого узла разница высот левого и правого поддерева ≤ 1
    public bool IsBalanced(TreeNode root)
    {
        var res = true;
        Rec(root);
        return res;

        int Rec(TreeNode? node)
        {
            if (node == null)
            {
                return 0;
            }

            var leftHeight = Rec(node.left);
            var rightHeight = Rec(node.right);
            if (Math.Abs(leftHeight - rightHeight) > 1)
            {
                res = false;
            }
            return Math.Max(leftHeight, rightHeight) + 1;
        }
    }
}
