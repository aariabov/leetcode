using FluentAssertions;

namespace Tests.BinaryTree.Conclusion;

/// <summary>
/// [Восстановить бинарное дерево из inorder и postorder](https://leetcode.com/explore/learn/card/data-structure-tree/133/conclusion/942/)
/// </summary>
public class BuildTreeTests
{
    [Fact]
    public void Test()
    {
        var expected = TreeNode.BuildTree([3, 9, 20, null, null, 15, 7]);
        var result = BuildTree([9, 3, 15, 20, 7], [9, 15, 7, 20, 3]);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Test1()
    {
        var expected = TreeNode.BuildTree([-1]);
        var result = BuildTree([-1], [-1]);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Test2()
    {
        var expected = TreeNode.BuildTree([1, 2]);
        var result = BuildTree([2, 1], [2, 1]);
        result.Should().BeEquivalentTo(expected);
    }

    private int postIndex;
    private Dictionary<int, int> inorderIndex;

    public TreeNode BuildTree(int[] inorder, int[] postorder)
    {
        postIndex = postorder.Length - 1;

        inorderIndex = new Dictionary<int, int>();
        for (int i = 0; i < inorder.Length; i++)
        {
            inorderIndex[inorder[i]] = i;
        }

        return BuildSubtree(inorder, postorder, 0, inorder.Length - 1);
    }

    // Идея, как и у меня, но используются индексы left и right, а у меня слайсы, dictionary для быстрого поиска и другое базовое условие
    private TreeNode BuildSubtree(int[] inorder, int[] postorder, int left, int right)
    {
        if (left > right)
        {
            return null;
        }

        int rootVal = postorder[postIndex--];
        TreeNode root = new TreeNode(rootVal);

        int index = inorderIndex[rootVal];

        // Сначала строим правое поддерево!
        root.right = BuildSubtree(inorder, postorder, index + 1, right);
        root.left = BuildSubtree(inorder, postorder, left, index - 1);

        return root;
    }

    public TreeNode BuildTreeMy(int[] inorder, int[] postorder)
    {
        var postStack = new Stack<int>(postorder);
        var res = Rec(inorder);
        return res;

        TreeNode Rec(int[] arr)
        {
            var val = postStack.Pop();
            var idx = Array.IndexOf(arr, val);
            var leftArr = arr[..idx];
            var rightArr = arr[(idx + 1)..];

            var node = new TreeNode(val);
            if (leftArr.Length == 0 && rightArr.Length == 0)
            {
                return node;
            }

            if (rightArr.Length > 0)
            {
                node.right = Rec(rightArr);
            }

            if (leftArr.Length > 0)
            {
                node.left = Rec(leftArr);
            }
            return node;
        }
    }
}
