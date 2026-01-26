using FluentAssertions;

namespace Tests.HashTable.DesignKey;

// [Найти дубли поддеревьев](https://leetcode.com/explore/learn/card/hash-table/185/hash_table_design_the_key/1127/)
public class FindDuplicateSubtreesTests
{
    [Fact]
    public void Test()
    {
        var e0 = new TreeNode(1);
        var e1 = new TreeNode(2);
        var e2 = new TreeNode(3);
        var e3 = new TreeNode(4);
        var e4 = new TreeNode(2);
        var e5 = new TreeNode(4);
        var e6 = new TreeNode(4);

        e0.left = e1;
        e0.right = e2;
        e1.left = e3;
        e2.left = e4;
        e2.right = e5;
        e4.left = e6;

        var result = FindDuplicateSubtrees(e0);
        var expected = new List<TreeNode> { e3, e1 };
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Test1()
    {
        var e0 = new TreeNode(2);
        var e1 = new TreeNode(1);
        var e2 = new TreeNode(1);

        e0.left = e1;
        e0.right = e2;

        var result = FindDuplicateSubtrees(e0);
        var expected = new List<TreeNode> { e1 };
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Test3()
    {
        var e0 = new TreeNode(2);
        var e1 = new TreeNode(2);
        var e2 = new TreeNode(2);
        var e3 = new TreeNode(3);
        var e4 = new TreeNode(3);

        e0.left = e1;
        e0.right = e2;
        e1.left = e3;
        e2.left = e4;

        var result = FindDuplicateSubtrees(e0);
        var expected = new List<TreeNode> { e3, e1 };
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Test4()
    {
        var e0 = new TreeNode(0);
        var e1 = new TreeNode(0);
        var e2 = new TreeNode(0);
        var e3 = new TreeNode(0);
        var e4 = new TreeNode(0);
        var e5 = new TreeNode(0);

        e0.left = e1;
        e0.right = e2;
        e1.left = e3;
        e2.right = e4;
        e4.left = e5;

        var result = FindDuplicateSubtrees(e0);
        var expected = new List<TreeNode> { e3, e1 };
        result.Should().BeEquivalentTo(expected);
    }

    // мое решение
    public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
    {
        var result = new List<TreeNode>();
        var dict = new Dictionary<string, int>();
        InOrder(root);
        string InOrder(TreeNode? node)
        {
            if (node == null)
            {
                return "#";
            }

            var leftStr = InOrder(node.left);
            var rightStr = InOrder(node.right);
            var nodeStr = node.val + "," + leftStr + "," + rightStr;
            if (!dict.ContainsKey(nodeStr))
            {
                dict[nodeStr] = 0;
            }
            dict[nodeStr]++;

            if (dict[nodeStr] == 2)
            {
                result.Add(node);
            }

            return nodeStr;
        }

        return result;
    }

    [Fact]
    public void TestStack()
    {
        var e0 = new TreeNode(1);
        var e1 = new TreeNode(2);
        var e2 = new TreeNode(3);
        var e3 = new TreeNode(4);
        var e4 = new TreeNode(2);
        var e5 = new TreeNode(4);
        var e6 = new TreeNode(4);

        e0.left = e1;
        e0.right = e2;
        e1.left = e3;
        e2.left = e4;
        e2.right = e5;
        e4.left = e6;

        var result = GetValsStack(e0);
        var expected = new int?[] { 1, 2, 4, 3, 2, 4, 4 };
        result.Should().BeEquivalentTo(expected);
    }

    public int?[] GetValsStack(TreeNode root)
    {
        var result = new List<int?>();
        var stack = new Stack<TreeNode>();
        TreeNode? cur = root;
        while (cur != null || stack.Count > 0)
        {
            while (cur != null)
            {
                stack.Push(cur);
                cur = cur.left;
            }

            cur = stack.Pop();
            result.Add(cur.val);
            cur = cur.right;
        }

        return result.ToArray();
    }

    [Fact]
    public void TestRecursive()
    {
        var e0 = new TreeNode(1);
        var e1 = new TreeNode(2);
        var e2 = new TreeNode(3);
        var e3 = new TreeNode(4);
        var e4 = new TreeNode(2);
        var e5 = new TreeNode(4);
        var e6 = new TreeNode(4);

        e0.left = e1;
        e0.right = e2;
        e1.left = e3;
        e2.left = e4;
        e2.right = e5;
        e4.left = e6;

        var result = GetValsRecursive(e0);
        var expected = new int?[] { 1, 2, 4, 3, 2, 4, 4 };
        result.Should().BeEquivalentTo(expected);
    }

    public int?[] GetValsRecursive(TreeNode root)
    {
        var result = new List<int?>();
        InOrder(root);
        void InOrder(TreeNode? node)
        {
            if (node == null)
            {
                return;
            }

            InOrder(node.left);
            result.Add(node.val);
            InOrder(node.right);
        }

        return result.ToArray();
    }
}
