using FluentAssertions;

namespace Tests.BinarySearchTree.BasicOperations;

/// <summary>
/// [Удаление в бинарном дереве поиска](https://leetcode.com/explore/learn/card/introduction-to-data-structure-binary-search-tree/141/basic-operations-in-a-bst/1006/)
/// </summary>
public class DeleteNodeTests
{
    [Fact]
    public void Test()
    {
        var root = TreeNode.BuildTree([5, 3, 6, 2, 4, null, 7]);

        var result = DeleteNode(root, 3);
        var expected = TreeNode.BuildTree([5, 4, 6, 2, null, null, 7]);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Test1()
    {
        var root = TreeNode.BuildTree([0]);

        var result = DeleteNode(root, 0);
        var expected = TreeNode.BuildTree([]);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Test2()
    {
        var root = TreeNode.BuildTree([5, 3, 6, 2, 4, null, 7]);

        var result = DeleteNode(root, 5);
        var expected = TreeNode.BuildTree([6, 3, 7, 2, 4]);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Test3()
    {
        var root = TreeNode.BuildTree([1, null, 2]);

        var result = DeleteNode(root, 1);
        var expected = TreeNode.BuildTree([2]);
        result.Should().BeEquivalentTo(expected);
    }

    public TreeNode DeleteNode(TreeNode root, int key)
    {
        if (root == null)
        {
            return null;
        }

        if (key < root.val)
        {
            root.left = DeleteNode(root.left, key);
        }
        else if (key > root.val)
        {
            root.right = DeleteNode(root.right, key);
        }
        else
        {
            // Узел найден
            if (root.left == null)
            {
                return root.right;
            }

            if (root.right == null)
            {
                return root.left;
            }

            // Два потомка
            TreeNode minNode = FindMin(root.right);
            root.val = minNode.val;
            root.right = DeleteNode(root.right, minNode.val);
        }

        return root;
    }

    private TreeNode FindMin(TreeNode node)
    {
        while (node.left != null)
        {
            node = node.left;
        }
        return node;
    }
}
