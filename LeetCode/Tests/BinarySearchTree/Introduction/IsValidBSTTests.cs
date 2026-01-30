namespace Tests.BinarySearchTree.Introduction;

/// <summary>
/// [Валидация бинарного дерева поиска](https://leetcode.com/explore/learn/card/introduction-to-data-structure-binary-search-tree/140/introduction-to-a-bst/997/)
/// </summary>
public class IsValidBSTTests
{
    [Fact]
    public void Test()
    {
        var e1 = TreeNode.BuildTree([2, 1, 3]);

        var result = IsValidBST(e1);
        Assert.True(result);
    }

    [Fact]
    public void Test1()
    {
        var e1 = TreeNode.BuildTree([5, 1, 4, null, null, 3, 6]);

        var result = IsValidBST(e1);
        Assert.False(result);
    }

    [Fact]
    public void Test2()
    {
        var e1 = TreeNode.BuildTree([5, 4, 6, null, null, 3, 7]);

        var result = IsValidBST(e1);
        Assert.False(result);
    }

    [Fact]
    public void Test3()
    {
        var e1 = TreeNode.BuildTree([2147483647]);

        var result = IsValidBST(e1);
        Assert.True(result);
    }

    [Fact]
    public void Test4()
    {
        var e1 = TreeNode.BuildTree([2, 2, 2]);

        var result = IsValidBST(e1);
        Assert.False(result);
    }

    // inorder способ, бинарное дерево поиска должно быть строго возрастающим
    public bool IsValidBST(TreeNode root)
    {
        int? prev = null;
        return Rec(root);

        bool Rec(TreeNode? node)
        {
            if (node is null)
            {
                return true;
            }

            var isLeft = Rec(node.left);
            if (!isLeft)
            {
                return false;
            }

            if (prev != null && node.val <= prev)
            {
                return false;
            }
            prev = node.val;

            var isRight = Rec(node.right);
            return isRight;
        }
    }

    // работает, рекурсивный способ с диапазонами
    public bool IsValidBSTRec(TreeNode root)
    {
        return Rec(root, null, null);
        bool Rec(TreeNode? node, int? min, int? max)
        {
            if (node == null)
            {
                return true;
            }

            if (node.val <= min || node.val >= max)
            {
                return false;
            }

            var isLeft = Rec(node.left, min, node.val);
            var isRight = Rec(node.right, node.val, max);
            return isLeft && isRight;
        }
    }
}
