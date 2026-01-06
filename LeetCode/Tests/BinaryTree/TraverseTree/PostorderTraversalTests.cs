namespace Tests.BinaryTree.TraverseTree;

// Обратный обход (Postorder): Левое поддерево → Правое поддерево → Корень
// [Postorder обход бинарного дерева](https://leetcode.com/explore/learn/card/data-structure-tree/134/traverse-a-tree/930/)
public class PostorderTraversalTests
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

        var result = PostorderTraversal(e1);
        var expected = new int[] { 4, 6, 7, 5, 2, 9, 8, 3, 1 };
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

        var result = PostorderTraversal(e1);
        var expected = new int[] { 3, 2, 1 };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test2()
    {
        var e1 = new TreeNode(1);

        var result = PostorderTraversal(e1);
        var expected = new int[] { 1 };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test3()
    {
        var result = PostorderTraversal(null);
        var expected = new int[] { };
        Assert.Equal(expected, result);
    }

    public IList<int> PostorderTraversal(TreeNode root)
    {
        var result = new List<int>();
        if (root == null)
            return result;

        Stack<TreeNode> stack = new Stack<TreeNode>();
        TreeNode current = root;
        TreeNode lastVisited = null;

        while (current != null || stack.Count > 0)
        {
            // Идём максимально влево
            while (current != null)
            {
                stack.Push(current); // Запоминаем путь назад
                current = current.left;
            }

            TreeNode peekNode = stack.Peek();

            // Если правого поддерева нет или оно уже обработано
            if (peekNode.right == null || peekNode.right == lastVisited)
            {
                result.Add(peekNode.val); // Оба поддерева обработаны → можно добавить корень
                lastVisited = stack.Pop();
            }
            else
            {
                current = peekNode.right;
            }
        }

        return result;
    }

    public IList<int> PostorderTraversalTwoStacks(TreeNode root)
    {
        var result = new List<int>();
        if (root == null)
        {
            return result;
        }

        Stack<TreeNode> stack1 = new Stack<TreeNode>(); // Для обхода дерева
        Stack<TreeNode> stack2 = new Stack<TreeNode>(); // Для хранения результата

        stack1.Push(root);

        // обходим дерево
        while (stack1.Count > 0)
        {
            TreeNode node = stack1.Pop();
            stack2.Push(node);

            if (node.left != null)
                stack1.Push(node.left);
            if (node.right != null)
                stack1.Push(node.right);
        }

        while (stack2.Count > 0)
            result.Add(stack2.Pop().val);

        return result;
    }

    // работает
    public IList<int> PostorderTraversalMy(TreeNode root)
    {
        var res = new List<int>();
        if (root == null)
        {
            return res;
        }

        var hashSet = new HashSet<TreeNode>();
        var stack = new Stack<TreeNode>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            var node = stack.Peek();
            if (
                (node.left == null || hashSet.Contains(node.left))
                && (node.right == null || hashSet.Contains(node.right))
            )
            {
                stack.Pop();
                res.Add(node.val);
                hashSet.Add(node);
                continue;
            }

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

    // рекурсивное решение: красивое и чистое
    public IList<int> PostorderTraversalRec(TreeNode root)
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
            Rec(node.left);
            Rec(node.right);
            res.Add(node.val);
        }
    }
}
