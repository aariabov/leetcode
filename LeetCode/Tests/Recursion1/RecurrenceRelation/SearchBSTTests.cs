namespace Tests.Recursion1.RecurrenceRelation;

/// <summary>
/// [Рекурсивный поиск в бинарном дереве](https://leetcode.com/explore/learn/card/recursion-i/251/scenario-i-recurrence-relation/3233/)
/// </summary>
public class SearchBSTTests
{
    [Fact]
    public void Test()
    {
        var e1 = new TreeNode(1);
        var e2 = new TreeNode(2);
        var e3 = new TreeNode(3);
        var e4 = new TreeNode(4);
        var e7 = new TreeNode(7);

        e2.left = e1;
        e2.right = e3;
        e4.left = e2;
        e4.right = e7;

        var result = SearchBST(e4, 2);
        Assert.Equal(e2, result);
    }

    [Fact]
    public void Test1()
    {
        var e1 = new TreeNode(1);
        var e2 = new TreeNode(2);
        var e3 = new TreeNode(3);
        var e4 = new TreeNode(4);
        var e7 = new TreeNode(7);

        e2.left = e1;
        e2.right = e3;
        e4.left = e2;
        e4.right = e7;

        var result = SearchBST(e4, 5);
        Assert.Null(result);
    }

    public TreeNode SearchBST(TreeNode root, int val)
    {
        if (root == null || root.val == val)
            return root;

        if (val < root.val)
            return SearchBST(root.left, val);
        else
            return SearchBST(root.right, val);
    }

    // работает, но я забыл, что дерево бинарное, т.е можно не проверять всех соседей, как в классическом BFS
    public TreeNode SearchBSTMy(TreeNode root, int val)
    {
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        return Rec();

        TreeNode Rec()
        {
            if (queue.Count == 0)
            {
                return null;
            }

            var node = queue.Dequeue();
            if (node.val == val)
            {
                return node;
            }

            if (node.left != null && val < node.val)
            {
                queue.Enqueue(node.left);
            }

            if (node.right != null && val > node.val)
            {
                queue.Enqueue(node.right);
            }

            return Rec();
        }
    }
}
