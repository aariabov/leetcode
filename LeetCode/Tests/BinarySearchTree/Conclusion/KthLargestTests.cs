using FluentAssertions;

namespace Tests.BinarySearchTree.Conclusion;

/// <summary>
/// [K-ый элемент с потоке элементов](https://leetcode.com/explore/learn/card/introduction-to-data-structure-binary-search-tree/142/conclusion/1018/)
/// </summary>
public class KthLargestTests
{
    [Fact]
    public void Test()
    {
        var kthLargest = new KthLargest(3, [4, 5, 8, 2]);
        Assert.Equal(4, kthLargest.Add(3)); // return 4
        Assert.Equal(5, kthLargest.Add(5)); // return 5
        Assert.Equal(5, kthLargest.Add(10)); // return 5
        Assert.Equal(8, kthLargest.Add(9)); // return 8
        Assert.Equal(8, kthLargest.Add(4)); // return 8
    }

    [Fact]
    public void Test1()
    {
        var kthLargest = new KthLargest(4, [7, 7, 7, 7, 8, 3]);
        Assert.Equal(7, kthLargest.Add(2));
        Assert.Equal(7, kthLargest.Add(10));
        Assert.Equal(7, kthLargest.Add(9));
        Assert.Equal(8, kthLargest.Add(9));
    }

    [Fact]
    public void Test2()
    {
        var kthLargest = new KthLargest(1, []);
        Assert.Equal(-3, kthLargest.Add(-3));
        Assert.Equal(-2, kthLargest.Add(-2));
        Assert.Equal(-2, kthLargest.Add(-4));
        Assert.Equal(0, kthLargest.Add(0));
        Assert.Equal(4, kthLargest.Add(4));
    }

    [Fact]
    public void InsertTest()
    {
        var kthLargest = new KthLargestMy(42, [5, 3, 8, 2, 10]);
        var n5 = new Node(5, 5);
        var n3 = new Node(3, 2);
        var n2 = new Node(2, 1);
        var n8 = new Node(8, 2);
        var n10 = new Node(10, 1);

        n5.Left = n3;
        n5.Right = n8;
        n3.Left = n2;
        n8.Right = n10;

        kthLargest.Root.Should().BeEquivalentTo(n5);
    }

    [Fact]
    public void InsertTest1()
    {
        var kthLargest = new KthLargestMy(42, [5, 2, 6, 1, 7, 4, 3]);
        var n1 = new Node(1, 1);
        var n2 = new Node(2, 4);
        var n3 = new Node(3, 1);
        var n4 = new Node(4, 2);
        var n5 = new Node(5, 7);
        var n6 = new Node(6, 2);
        var n7 = new Node(7, 1);

        n5.Left = n2;
        n5.Right = n6;
        n2.Left = n1;
        n2.Right = n4;
        n4.Left = n3;
        n6.Right = n7;

        kthLargest.Root.Should().BeEquivalentTo(n5);
    }

    // красивое решение с PriorityQueue
    public class KthLargest
    {
        private readonly int k;
        private PriorityQueue<int, int> minHeap;

        public KthLargest(int k, int[] nums)
        {
            this.k = k;
            minHeap = new PriorityQueue<int, int>();

            foreach (var num in nums)
            {
                Add(num);
            }
        }

        public int Add(int val)
        {
            minHeap.Enqueue(val, val);

            if (minHeap.Count > k)
            {
                minHeap.Dequeue();
            }

            return minHeap.Peek();
        }
    }

    // решение с bst
    public class KthLargestBst
    {
        private class Node
        {
            public int Val;
            public int Count; // количество одинаковых значений
            public int Size; // размер поддерева
            public Node Left;
            public Node Right;

            public Node(int val)
            {
                Val = val;
                Count = 1;
                Size = 1;
            }
        }

        private Node root;
        private int k;

        public KthLargestBst(int k, int[] nums)
        {
            this.k = k;

            foreach (var num in nums)
            {
                root = Insert(root, num);
            }
        }

        public int Add(int val)
        {
            root = Insert(root, val);
            return FindKthLargest(root, k);
        }

        // ===== Вставка в BST =====
        private Node Insert(Node node, int val)
        {
            if (node == null)
                return new Node(val);

            if (val == node.Val)
            {
                node.Count++;
            }
            else if (val < node.Val)
            {
                node.Left = Insert(node.Left, val);
            }
            else
            {
                node.Right = Insert(node.Right, val);
            }

            UpdateSize(node);
            return node;
        }

        private void UpdateSize(Node node)
        {
            node.Size = node.Count + GetSize(node.Left) + GetSize(node.Right);
        }

        private int GetSize(Node node)
        {
            return node == null ? 0 : node.Size;
        }

        // ===== Поиск k-го максимального =====
        private int FindKthLargest(Node node, int k)
        {
            int rightSize = GetSize(node.Right);

            if (k <= rightSize)
                return FindKthLargest(node.Right, k);

            if (k > rightSize + node.Count)
                return FindKthLargest(node.Left, k - rightSize - node.Count);

            return node.Val;
        }
    }

    // работает
    public class KthLargestMy
    {
        public Node? Root;
        private readonly int _k;

        public KthLargestMy(int k, int[] nums)
        {
            _k = k;
            for (int i = 0; i < nums.Length; i++)
            {
                Add(nums[i]);
            }
        }

        public int Add(int val)
        {
            if (Root == null)
            {
                Root = new Node(val, 1);
                return val;
            }
            Rec(Root);
            return GetK(Root, _k);

            void Rec(Node node)
            {
                if (node.Left is null && val <= node.Val)
                {
                    var newNode = new Node(val, 1);
                    node.Left = newNode;
                    node.Size++;
                    return;
                }

                if (node.Right is null && val >= node.Val)
                {
                    var newNode = new Node(val, 1);
                    node.Right = newNode;
                    node.Size++;
                    return;
                }

                Rec(val < node.Val ? node.Left : node.Right);
                node.Size++;
            }
        }

        private int GetK(Node node, int k)
        {
            var r = node.Right?.Size ?? 0;
            if (r + 1 == k)
            {
                return node.Val;
            }

            if (k <= r && node.Right != null)
            {
                return GetK(node.Right, k);
            }

            if (k > r && node.Left != null)
            {
                return GetK(node.Left, k - r - 1);
            }

            return 0;
        }
    }

    public class Node
    {
        public int Val;
        public int Size;
        public Node? Left;
        public Node? Right;

        public Node(int val, int size, Node? left = null, Node? right = null)
        {
            this.Val = val;
            this.Size = size;
            this.Left = left;
            this.Right = right;
        }

        public override string ToString()
        {
            return Val.ToString();
        }
    }
}
