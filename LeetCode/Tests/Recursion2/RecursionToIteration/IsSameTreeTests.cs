namespace Tests.Recursion2.RecursionToIteration;

/// <summary>
/// [Сравнение деревьев](https://leetcode.com/explore/learn/card/recursion-ii/503/recursion-to-iteration/2894/)
/// </summary>
public class IsSameTreeTests
{
    [Fact]
    public void Test()
    {
        var p = TreeNode.BuildTree([1, 2, 3]);
        var q = TreeNode.BuildTree([1,2,3]);

        var result = IsSameTree(p, q);
        Assert.True(result);
    }
    
    [Fact]
    public void Test1()
    {
        var p = TreeNode.BuildTree([1,2]);
        var q = TreeNode.BuildTree([1,null,2]);

        var result = IsSameTree(p, q);
        Assert.False(result);
    }
    
    [Fact]
    public void Test2()
    {
        var p = TreeNode.BuildTree([1,2,1]);
        var q = TreeNode.BuildTree([1,1,2]);

        var result = IsSameTree(p, q);
        Assert.False(result);
    }
    
    // решние на очереди BFS
    public bool IsSameTree(TreeNode p, TreeNode q)
    {
        Queue<(TreeNode, TreeNode)> queue = new Queue<(TreeNode, TreeNode)>();
        queue.Enqueue((p, q));

        while (queue.Count > 0)
        {
            var (node1, node2) = queue.Dequeue();

            if (node1 == null && node2 == null)
                continue;

            if (node1 == null || node2 == null || node1.val != node2.val)
                return false;

            queue.Enqueue((node1.left, node2.left));
            queue.Enqueue((node1.right, node2.right));
        }

        return true;
    }

    // красивое решение на одном стеке (имитация рекурсии)
    public bool IsSameTreeStack(TreeNode p, TreeNode q)
    {
        Stack<(TreeNode, TreeNode)> stack = new Stack<(TreeNode, TreeNode)>();
        stack.Push((p, q));

        while (stack.Count > 0)
        {
            var (node1, node2) = stack.Pop();

            if (node1 == null && node2 == null)
                continue;

            if (node1 == null || node2 == null || node1.val != node2.val)
                return false;

            stack.Push((node1.left, node2.left));
            stack.Push((node1.right, node2.right));
        }

        return true;
    }
    
    // работает, но не оч красивое
    public bool IsSameTreeStackMy(TreeNode p, TreeNode q)
    {
        var stack = new Stack<TreeNode>();  
        var current = p;  
        var stack1 = new Stack<TreeNode>();  
        var current1= q;
  
        while ((current != null || stack.Count > 0) || (current1 != null || stack1.Count > 0))  
        {
            if (!Check(current, current1))
            {
                return false;
            }
            // Идем влево до конца  
            while (current != null || current1 != null)  
            {            
                if (!Check(current, current1))
                {
                    return false;
                }
                stack.Push(current);  
                current = current.left;  
                stack1.Push(current1);  
                current1 = current1.left;  
            }  
        
            // Обрабатываем узел  
            current = stack.Pop();
            current1 = stack1.Pop();
  
            // Переходим вправо  
            current = current.right;  
            current1 = current1.right;
        }
        
        return true;

        bool Check(TreeNode? tree1, TreeNode? tree2)
        {
            if (tree1 == null && tree2 == null)
            {
                return true;
            }

            if (tree1 == null || tree2 == null)
            {
                return false;
            }
            
            return tree1.val == tree2.val;
        }
    }
    
    public bool IsSameTreeRec(TreeNode p, TreeNode q)
    {
        return Rec(p, q);

        bool Rec(TreeNode? tree1, TreeNode? tree2)
        {
            if (tree1 == null && tree2 == null)
            {
                return true;
            }

            if (tree1 == null || tree2 == null)
            {
                return false;
            }
            
            var isLeftEquals = Rec(tree1.left, tree2.left);
            var isRightEquals = Rec(tree1.right, tree2.right);
            return isRightEquals && isLeftEquals && tree1.val == tree2.val;
        }
        
    }
}