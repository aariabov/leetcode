namespace Tests.BinaryTree.Conclusion;

/// <summary>
/// [Ближайший общий предок](https://leetcode.com/explore/learn/card/data-structure-tree/133/conclusion/932/)
/// </summary>
public class LowestCommonAncestorTests
{
    [Fact]
    public void Test()
    {
        var e1 = new TreeNode(3);
        var e2 = new TreeNode(5);
        var e3 = new TreeNode(1);
        var e4 = new TreeNode(6);
        var e5 = new TreeNode(2);
        var e6 = new TreeNode(0);
        var e7 = new TreeNode(8);
        var e8 = new TreeNode(7);
        var e9 = new TreeNode(4);

        e1.left = e2;
        e1.right = e3;
        e2.left = e4;
        e2.right = e5;
        e3.left = e6;
        e3.right = e7;
        e5.left = e8;
        e5.right = e9;

        var result = LowestCommonAncestor(e1, e2, e3);
        Assert.Equal(e1, result);
    }

    [Fact]
    public void Test1()
    {
        var e1 = new TreeNode(3);
        var e2 = new TreeNode(5);
        var e3 = new TreeNode(1);
        var e4 = new TreeNode(6);
        var e5 = new TreeNode(2);
        var e6 = new TreeNode(0);
        var e7 = new TreeNode(8);
        var e8 = new TreeNode(7);
        var e9 = new TreeNode(4);

        e1.left = e2;
        e1.right = e3;
        e2.left = e4;
        e2.right = e5;
        e3.left = e6;
        e3.right = e7;
        e5.left = e8;
        e5.right = e9;

        var result = LowestCommonAncestor(e1, e2, e9);
        Assert.Equal(e2, result);
    }

    [Fact]
    public void Test2()
    {
        var e1 = new TreeNode(1);
        var e2 = new TreeNode(2);
        e1.left = e2;

        var result = LowestCommonAncestor(e1, e1, e2);
        Assert.Equal(e1, result);
    }

    // какое-то неочевидное решение, хотя быстро работает
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        // Базовый случай
        if (root == null || root == p || root == q)
            return root;

        // Ищем в левом и правом поддереве
        TreeNode left = LowestCommonAncestor(root.left, p, q);
        TreeNode right = LowestCommonAncestor(root.right, p, q);

        // Если p и q найдены в разных поддеревьях
        if (left != null && right != null)
            return root;

        // Иначе возвращаем найденный узел
        return left != null ? left : right;
    }

    // идея - использовать словарь ребенок -> родитель
    public TreeNode LowestCommonAncestorIter(TreeNode root, TreeNode p, TreeNode q)
    {
        // Словарь: ребенок -> родитель, типа аналог дерева
        Dictionary<TreeNode, TreeNode> parent = new Dictionary<TreeNode, TreeNode>();
        parent[root] = null;

        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(root);

        // Ищем p и q
        while (!parent.ContainsKey(p) || !parent.ContainsKey(q))
        {
            TreeNode node = stack.Pop();

            if (node.left != null)
            {
                parent[node.left] = node;
                stack.Push(node.left);
            }

            if (node.right != null)
            {
                parent[node.right] = node;
                stack.Push(node.right);
            }
        }

        // Все предки p
        HashSet<TreeNode> ancestors = new HashSet<TreeNode>();
        while (p != null)
        {
            ancestors.Add(p);
            p = parent[p];
        }

        // Поднимаемся от q вверх, пока не найдём общего предка
        while (!ancestors.Contains(q))
        {
            q = parent[q];
        }

        return q;
    }

    // работает, но медленно, и как-то костыльно
    public TreeNode LowestCommonAncestorMy(TreeNode root, TreeNode p, TreeNode q)
    {
        var path1 = GetPath(root, p).ToArray().Reverse().ToArray();
        var path2 = GetPath(root, q).ToArray().Reverse().ToArray();
        var len = path1.Length < path2.Length ? path1.Length : path2.Length;
        for (int i = 0; i < len; i++)
        {
            if (path1[i] != path2[i])
            {
                return path1[i - 1];
            }

            if (i == len - 1)
            {
                return path1[i];
            }
        }
        return root;

        Stack<TreeNode> GetPath(TreeNode root, TreeNode findNode)
        {
            var hashSet = new HashSet<TreeNode>();
            var stack = new Stack<TreeNode>();
            stack.Push(root);
            var cur = root;
            while (stack.Count > 0)
            {
                if (cur == findNode)
                {
                    return stack;
                }
                while (cur.left != null)
                {
                    stack.Push(cur.left);
                    cur = cur.left;
                    if (cur == findNode)
                    {
                        return stack;
                    }
                }
                while (cur.right == null || hashSet.Contains(cur))
                {
                    stack.Pop();
                    cur = stack.Peek();
                }
                hashSet.Add(cur);
                cur = cur.right;
                stack.Push(cur);
            }
            return stack;
        }
    }
}
