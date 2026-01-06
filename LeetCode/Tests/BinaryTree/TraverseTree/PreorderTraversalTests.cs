namespace Tests.BinaryTree.TraverseTree;

// Preorder: Корень → Левое поддерево → Правое поддерево
// [Прямой обход бинарного дерева](https://leetcode.com/explore/learn/card/data-structure-tree/134/traverse-a-tree/928/)
public class PreorderTraversalTests
{
    [Fact]
    public void Test()
    {
        var e1 = new TreeNode(1);
        var e2 = new TreeNode(2);
        var e3 = new TreeNode(3);
        var e4 = new TreeNode(4);
        var e5 = new TreeNode(5);
        var e6 = new TreeNode(6);
        var e7 = new TreeNode(7);
        var e8 = new TreeNode(8);
        var e9 = new TreeNode(9);

        e1.left = e2;
        e1.right = e3;
        e2.left = e4;
        e2.right = e5;
        e5.left = e6;
        e5.right = e7;
        e3.right = e8;
        e8.left = e9;

        var result = PreorderTraversal(e1);
        var expected = new int[] { 1, 2, 4, 5, 6, 7, 3, 8, 9 };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test1()
    {
        var e1 = new TreeNode(1);
        var e2 = new TreeNode(2);
        var e3 = new TreeNode(3);

        e1.right = e2;
        e2.left = e3;

        var result = PreorderTraversal(e1);
        var expected = new int[] { 1, 2, 3 };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test2()
    {
        var e1 = new TreeNode(1);

        var result = PreorderTraversal(e1);
        var expected = new int[] { 1 };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test3()
    {
        var result = PreorderTraversal(null);
        var expected = new int[] { };
        Assert.Equal(expected, result);
    }

    // итеративное решение - чуть сложнее для понимания
    public IList<int> PreorderTraversal(TreeNode root)
    {
        var res = new List<int>();
        if (root == null)
        {
            return res;
        }

        var stack = new Stack<TreeNode>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            var node = stack.Pop();
            res.Add(node.val);
            if (node.right != null)
            {
                stack.Push(node.right);
            }
            if (node.left != null)
            {
                stack.Push(node.left);
            }
        }
        return res;
    }

    // рекурсивное решение - красивое и чистое. Для больших деревьев возможен stack overflow
    public IList<int> PreorderTraversalRec(TreeNode root)
    {
        var res = new List<int>();
        Rec(root);
        return res;

        void Rec(TreeNode? node)
        {
            if (node == null)
            {
                return;
            }
            res.Add(node.val);
            Rec(node.left);
            Rec(node.right);
        }
    }
}
