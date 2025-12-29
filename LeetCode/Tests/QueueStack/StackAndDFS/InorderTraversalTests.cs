namespace Tests.QueueStack.StackAndDFS;

/// <summary>
/// [Обойти бинарное дерево в глубину: лево, корень, право](https://leetcode.com/explore/learn/card/queue-stack/232/practical-application-stack/1383/)
/// </summary>
public class InorderTraversalTests
{
    [Fact]
    public void Test()
    {
        // 1,2,3,4,5,null,8,null,null,6,7,9]
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

        var result = InorderTraversal(e1);
        var expected = new int[]{ 4,2,6,5,7,1,3,9,8 };
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void Test1()
    {
        // 1,null,2,3
        var e1 = new TreeNode(1);
        var e2 = new TreeNode(2);
        var e3 = new TreeNode(3);

        e1.right = e2;
        e2.left = e3;
        var result = InorderTraversal(e1);
        var expected = new int[]{ 1,3,2 };
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void Test2()
    {
        // 1,null,2,3
        var e1 = new TreeNode(3);
        var e2 = new TreeNode(1);
        var e3 = new TreeNode(2);

        e1.left = e2;
        e2.right = e3;
        var result = InorderTraversal(e1);
        var expected = new int[]{ 1,2,3 };
        Assert.Equal(expected, result);
    }
    
    // идея, как у меня, только вместо HashSet используется current
    public IList<int> InorderTraversal(TreeNode root) {
        List<int> result = new List<int>();
        Stack<TreeNode> stack = new Stack<TreeNode>();
        TreeNode current = root;

        while (current != null || stack.Count > 0) {
            // Идем влево до конца
            while (current != null) {
                stack.Push(current);
                current = current.left;
            }

            // Обрабатываем узел
            current = stack.Pop();
            result.Add(current.val);

            // Переходим вправо
            current = current.right;
        }

        return result;
    }

    public IList<int> InorderTraversalMy(TreeNode root)
    {
        var res = new List<int>();
        if (root is null)
        {
            return res;
        }

        var hashSet = new HashSet<TreeNode>();
        var stack = new Stack<TreeNode>();
        stack.Push(root);
        
        while (stack.Count > 0)
        {
            var node = stack.Peek();
            if (node.left != null && !hashSet.Contains(node.left))
            {
                stack.Push(node.left);
                continue;
            }
            
            res.Add(node.val);
            hashSet.Add(node);
            stack.Pop();
            if (node.right != null)
            {
                stack.Push(node.right);
            }
        }

        return res;
    }

    // работает, рекурсивный метод
    public IList<int> InorderTraversalRec(TreeNode root)
    {
        var res = new List<int>();
        Rec(root);

        void Rec(TreeNode? node)
        {
            if (node is null)
            {
                return;
            }
            
            Rec(node.left);
            res.Add(node.val);
            Rec(node.right);
        }

        return res;
    }
}