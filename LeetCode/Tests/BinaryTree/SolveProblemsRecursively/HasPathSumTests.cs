namespace Tests.BinaryTree.SolveProblemsRecursively;

/// <summary>
/// [Сумма элементов от корня до листа](https://leetcode.com/explore/learn/card/data-structure-tree/17/solve-problems-recursively/537/)
/// </summary>
public class HasPathSumTests
{
    [Fact]
    public void Test()
    {
        var e1 = TreeNode.BuildTree([5, 4, 8, 11, null, 13, 4, 7, 2, null, null, null, 1]);
        var result = HasPathSum(e1, 22);
        Assert.True(result);
    }

    [Fact]
    public void Test1()
    {
        var e1 = TreeNode.BuildTree([1, 2, 3]);
        var result = HasPathSum(e1, 5);
        Assert.False(result);
    }

    [Fact]
    public void Test2()
    {
        var result = HasPathSum(null, 0);
        Assert.False(result);
    }

    public bool HasPathSum(TreeNode root, int targetSum)
    {
        if (root == null)
            return false;

        // Если это лист
        if (root.left == null && root.right == null)
            return targetSum == root.val;

        // Рекурсивно проверяем поддеревья
        int remainingSum = targetSum - root.val;

        return HasPathSum(root.left, remainingSum) || HasPathSum(root.right, remainingSum);
    }

    // работает
    public bool HasPathSumMy(TreeNode root, int targetSum)
    {
        if (root == null)
        {
            return false;
        }

        var result = false;
        Rec(root, targetSum);
        void Rec(TreeNode node, int target)
        {
            if (node.left == null && node.right == null)
            {
                if (node.val == target)
                {
                    result = true;
                }
                return;
            }

            if (node.left != null)
            {
                Rec(node.left, target - node.val);
            }

            if (node.right != null)
            {
                Rec(node.right, target - node.val);
            }
        }

        return result;
    }
}
