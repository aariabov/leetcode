using System.Text;

namespace Tests.BinaryTree.Conclusion;

// [Связать элементы одного уровня](https://leetcode.com/explore/learn/card/data-structure-tree/133/conclusion/994/)
public class ConnectTests
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
        var e6 = new Node(6);
        var e7 = new Node(7);

        e1.left = e2;
        e1.right = e3;
        e2.left = e4;
        e2.right = e5;
        e3.left = e6;
        e3.right = e7;

        var result = Connect(e1);
        var expected = "1,#,2,3,#,4,5,6,7,#";
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

        ConnectNodes(root);
        return root;
    }

    private void ConnectNodes(Node node)
    {
        if (node == null || node.left == null)
            return;

        // 1. Соединяем детей одного родителя
        node.left.next = node.right;

        // 2. Соединяем детей разных родителей
        if (node.next != null)
        {
            node.right.next = node.next.left;
        }

        // 3. Рекурсия
        ConnectNodes(node.left);
        ConnectNodes(node.right);
    }

    public Node ConnectIter(Node root)
    {
        if (root == null)
            return null;

        Node levelStart = root;

        while (levelStart.left != null) // пока есть следующий уровень
        {
            Node current = levelStart;

            while (current != null)
            {
                // Связываем левого и правого детей
                current.left.next = current.right;

                // Связываем правого ребенка с левым ребенком следующего узла
                if (current.next != null)
                {
                    current.right.next = current.next.left;
                }

                current = current.next;
            }

            levelStart = levelStart.left;
        }

        return root;
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

    public class Node
    {
        public int val;
        public Node left;
        public Node right;
        public Node next;

        public Node() { }

        public Node(int _val)
        {
            val = _val;
        }

        public Node(int _val, Node _left, Node _right, Node _next)
        {
            val = _val;
            left = _left;
            right = _right;
            next = _next;
        }
    }
}
