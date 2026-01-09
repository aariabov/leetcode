using FluentAssertions;

namespace Tests.BinaryTree.Conclusion;

/// <summary>
/// [Восстановить бинарное дерево из preorder и inorder](https://leetcode.com/explore/learn/card/data-structure-tree/133/conclusion/943/)
/// </summary>
public class BuildTree1Tests
{
    [Fact]
    public void Test()
    {
        var expected = TreeNode.BuildTree([3, 9, 20, null, null, 15, 7]);
        var result = BuildTree([3, 9, 20, 15, 7], [9, 3, 15, 20, 7]);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Test1()
    {
        var expected = TreeNode.BuildTree([-1]);
        var result = BuildTree([-1], [-1]);
        result.Should().BeEquivalentTo(expected);
    }

    public TreeNode BuildTree(int[] preorder, int[] inorder)
    {
        var idx = 0;
        var dict = new Dictionary<int, int>();
        for (int i = 0; i < inorder.Length; i++)
        {
            dict[inorder[i]] = i;
        }

        var res = Rec(0, inorder.Length - 1);
        return res;

        TreeNode? Rec(int left, int right)
        {
            if (left > right)
            {
                return null;
            }

            var val = preorder[idx++];
            var node = new TreeNode(val);
            var valIdx = dict[val];
            node.left = Rec(left, valIdx - 1);
            node.right = Rec(valIdx + 1, right);
            return node;
        }
    }
}
