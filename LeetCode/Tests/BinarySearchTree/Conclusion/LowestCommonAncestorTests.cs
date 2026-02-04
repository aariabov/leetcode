namespace Tests.BinarySearchTree.Conclusion;

/// <summary>
/// [Ближайлий общий предок](https://leetcode.com/explore/learn/card/introduction-to-data-structure-binary-search-tree/142/conclusion/1012/)
/// </summary>
public class LowestCommonAncestorTests
{
    [Fact]
    public void Test()
    {
        var root = TreeNode.BuildTree([6, 2, 8, 0, 4, 7, 9, null, null, 3, 5]);

        var result = LowestCommonAncestor(root, root.left, root.right);
        Assert.Equal(root, result);
    }

    [Fact]
    public void Test1()
    {
        var root = TreeNode.BuildTree([6, 2, 8, 0, 4, 7, 9, null, null, 3, 5]);

        var result = LowestCommonAncestor(root, root.left, root.left.right);
        Assert.Equal(root.left, result);
    }

    [Fact]
    public void Test2()
    {
        var root = TreeNode.BuildTree([2, 1]);

        var result = LowestCommonAncestor(root, root, root.left);
        Assert.Equal(root, result);
    }

    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
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
}
