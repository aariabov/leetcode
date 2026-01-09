namespace Tests.BinaryTree.Conclusion;

// [Связать элементы одного уровня для неполного дерева](https://leetcode.com/explore/learn/card/data-structure-tree/133/conclusion/1016/)
public class Connect1Tests
{
    [Fact]
    public void Test()
    {
        // 1,2,3,4,5,null,8,null,null,6,7,9]
        var e1 = new Node(1);
        var e2 = new Node(2);
        var e3 = new Node(3);
        var e4 = new Node(4);
        var e5 = new Node(5);
        var e7 = new Node(7);

        e1.left = e2;
        e1.right = e3;
        e2.left = e4;
        e2.right = e5;
        e3.right = e7;

        var result = Connect(e1);
        var expected = "1,#,2,3,#,4,5,7,#";
        var queue = new Queue<Node>();
        queue.Enqueue(result);
        var list = new List<string>();
        while (queue.Count > 0)
        {
            int levelSize = queue.Count;
            for (int i = 0; i < levelSize; i++)
            {
                var node = queue.Dequeue();
                if (i == 0)
                {
                    list.Add(node.val.ToString());
                }
                if (node.next != null)
                {
                    list.Add(node.next.val.ToString());
                }
                else
                {
                    list.Add("#");
                }

                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }

                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }
        }
        var str = string.Join(",", list);
        Assert.Equal(expected, str);
    }

    public Node Connect(Node root)
    {
        if (root == null)
            return null;

        // соединяем левый ребёнок
        if (root.left != null)
        {
            if (root.right != null)
                root.left.next = root.right;
            else
                root.left.next = GetNext(root.next);
        }

        // соединяем правый ребёнок
        if (root.right != null)
        {
            root.right.next = GetNext(root.next);
        }

        // ВАЖНО: сначала правое поддерево
        Connect(root.right);
        Connect(root.left);

        return root;
    }

    private Node GetNext(Node node)
    {
        while (node != null)
        {
            if (node.left != null)
                return node.left;
            if (node.right != null)
                return node.right;

            node = node.next;
        }
        return null;
    }

    // работает
    public Node ConnectMy(Node root)
    {
        if (root == null)
        {
            return root;
        }

        var queue = new Queue<Node>();
        queue.Enqueue(root);
        int depth = 0;

        while (queue.Count > 0)
        {
            int levelSize = queue.Count;
            depth++;

            Node? prev = null;
            for (int i = 0; i < levelSize; i++)
            {
                var node = queue.Dequeue();
                if (prev != null)
                {
                    prev.next = node;
                }
                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }
                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }
                prev = node;
            }
        }

        return root;
    }
}
