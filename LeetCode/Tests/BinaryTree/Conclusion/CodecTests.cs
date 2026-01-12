using System.Text;
using FluentAssertions;

namespace Tests.BinaryTree.Conclusion;

// [Сериализация и десериализация дерева](https://leetcode.com/explore/learn/card/data-structure-tree/133/conclusion/995/)
public class CodecTests
{
    [Fact]
    public void Test()
    {
        var codec = new Codec();

        var e1 = new TreeNode(1);
        var e2 = new TreeNode(2);
        var e3 = new TreeNode(3);
        var e4 = new TreeNode(4);
        var e5 = new TreeNode(5);

        e1.left = e2;
        e1.right = e3;
        e3.left = e4;
        e3.right = e5;

        var str = codec.serialize(e1);
        TreeNode res = codec.deserialize(str);
        res.Should().BeEquivalentTo(e1);
    }

    public class Codec
    {
        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            if (root == null)
                return "";

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            List<string> result = new List<string>();

            while (queue.Count > 0)
            {
                TreeNode node = queue.Dequeue();

                if (node == null)
                {
                    result.Add("null");
                    continue;
                }

                result.Add(node.val.ToString());
                queue.Enqueue(node.left);
                queue.Enqueue(node.right);
            }

            return string.Join(",", result);
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            string[] values = data.Split(',');
            TreeNode root = new TreeNode(int.Parse(values[0]));

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            int index = 1;

            while (queue.Count > 0 && index < values.Length)
            {
                TreeNode node = queue.Dequeue();

                // Left child
                if (values[index] != "null")
                {
                    node.left = new TreeNode(int.Parse(values[index]));
                    queue.Enqueue(node.left);
                }
                index++;

                if (index >= values.Length)
                    break;

                // Right child
                if (values[index] != "null")
                {
                    node.right = new TreeNode(int.Parse(values[index]));
                    queue.Enqueue(node.right);
                }
                index++;
            }

            return root;
        }
    }

    // работает
    public class CodecMy
    {
        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            var depth = MaxDepth(root);
            var res = new List<string>();
            var queue = new Queue<TreeNode?>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                for (int i = 0; i < levelSize; i++)
                {
                    var node = queue.Dequeue();
                    if (node == null)
                    {
                        res.Add("null");
                    }
                    else
                    {
                        res.Add(node.val.ToString());
                        if (depth > 1)
                        {
                            queue.Enqueue(node.left);
                            queue.Enqueue(node.right);
                        }
                    }
                }

                depth--;
            }
            return string.Join(",", res);
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            var vals = new List<int?>();
            var items = data.Split(',');
            foreach (var val in items)
            {
                if (val == "null")
                {
                    vals.Add(null);
                }
                else
                {
                    vals.Add(int.Parse(val));
                }
            }

            int?[] values = vals.ToArray();

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

        public int MaxDepth(TreeNode root)
        {
            if (root == null)
                return 0;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            int depth = 0;

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                depth++;

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode node = queue.Dequeue();
                    if (node.left != null)
                        queue.Enqueue(node.left);
                    if (node.right != null)
                        queue.Enqueue(node.right);
                }
            }

            return depth;
        }
    }
}
