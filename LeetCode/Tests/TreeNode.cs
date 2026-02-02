namespace Tests;

public class TreeNode
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;

    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }

    public override string ToString()
    {
        return val.ToString();
    }

    // ===== Preorder =====
    public IEnumerable<int> Preorder()
    {
        yield return val;

        if (left != null)
            foreach (var v in left.Preorder())
                yield return v;

        if (right != null)
            foreach (var v in right.Preorder())
                yield return v;
    }

    // ===== Inorder =====
    public IEnumerable<int> Inorder()
    {
        if (left != null)
            foreach (var v in left.Inorder())
                yield return v;

        yield return val;

        if (right != null)
            foreach (var v in right.Inorder())
                yield return v;
    }

    // ===== Postorder =====
    public IEnumerable<int> Postorder()
    {
        if (left != null)
            foreach (var v in left.Postorder())
                yield return v;

        if (right != null)
            foreach (var v in right.Postorder())
                yield return v;

        yield return val;
    }

    // ===== Level order (BFS) =====
    public IEnumerable<int> LevelOrder()
    {
        var queue = new Queue<TreeNode>();
        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            yield return node.val;

            if (node.left != null)
                queue.Enqueue(node.left);

            if (node.right != null)
                queue.Enqueue(node.right);
        }
    }

    public static TreeNode? BuildTree(int?[]? values)
    {
        if (values == null || values.Length == 0 || values[0] == null)
            return null;

        TreeNode root = new TreeNode(values[0]!.Value);
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        int i = 1;

        while (i < values.Length)
        {
            TreeNode current = queue.Dequeue();

            // левый потомок
            if (i < values.Length && values[i] != null)
            {
                current.left = new TreeNode(values[i].Value);
                queue.Enqueue(current.left);
            }
            i++;

            // правый потомок
            if (i < values.Length && values[i] != null)
            {
                current.right = new TreeNode(values[i].Value);
                queue.Enqueue(current.right);
            }
            i++;
        }

        return root;
    }
}
