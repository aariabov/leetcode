namespace Tests.BinaryTree.SolveProblemsRecursively;

/// <summary>
/// [Глубина бинарного дерева](https://leetcode.com/explore/learn/card/data-structure-tree/17/solve-problems-recursively/535/)
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

    // итеративное BFS решение
    public int MaxDepth(TreeNode root)
    {
        if (root == null)
            return 0;

        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        int depth = 0;

        while (queue.Count > 0)
        {
            int levelSize = queue.Count;
            depth++;

            for (int i = 0; i < levelSize; i++)
            {
                TreeNode node = queue.Dequeue();
                if (node.left != null)
                    queue.Enqueue(node.left);
                if (node.right != null)
                    queue.Enqueue(node.right);
            }
        }

        return depth;
    }

    // Top-Down (передаем значение depth + 1 детям) рекурсивное решение
    public int MaxDepthTopDown(TreeNode root)
    {
        var result = 0;
        Rec(root, 0);

        void Rec(TreeNode? node, int depth)
        {
            if (node == null)
            {
                if (depth > result)
                {
                    result = depth;
                }
                return;
            }
            Rec(node.left, depth + 1);
            Rec(node.right, depth + 1);
        }
        return result;
    }

    // Bottom-up (c самого низа возвращаем значение) рекурсивное решение
    public int MaxDepthBottomUp(TreeNode root)
    {
        if (root == null)
            return 0;

        // рекурсивно вычисляем высоту левого и правого поддеревьев
        int leftDepth = MaxDepth(root.left);
        int rightDepth = MaxDepth(root.right);

        // высота для текущего узла
        return Math.Max(leftDepth, rightDepth) + 1;
    }
}
