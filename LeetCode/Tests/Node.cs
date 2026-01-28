namespace Tests;

public class Node
{
    public int val;
    public List<Node> children = new List<Node>();

    public Node() { }

    public Node(int _val)
    {
        val = _val;
    }

    public Node(int _val, List<Node> _children)
    {
        val = _val;
        children = _children;
    }

    public static Node? BuildTree(int?[]? values)
    {
        if (values == null || values.Length == 0 || values[0] == null)
            return null;

        var root = new Node(values[0]!.Value);
        var queue = new Queue<Node>();
        queue.Enqueue(root);

        var i = 2;

        while (i < values.Length)
        {
            var current = queue.Dequeue();

            while (i < values.Length && values[i] != null)
            {
                var childNode = new Node(values[i].Value);
                current.children.Add(childNode);
                queue.Enqueue(childNode);
                i++;
            }

            i++;
        }

        return root;
    }
}
