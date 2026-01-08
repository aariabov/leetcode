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
