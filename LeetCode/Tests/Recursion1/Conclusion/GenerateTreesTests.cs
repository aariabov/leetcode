using FluentAssertions;

namespace Tests.Recursion1.Conclusion;

/// <summary>
/// [Количество бинарных деревьев](https://leetcode.com/explore/learn/card/recursion-i/253/conclusion/2384/)
/// </summary>
public class GenerateTreesTests
{
    [Fact]
    public void Test()
    {
        var e1_1 = new TreeNode(1);
        var e2_1 = new TreeNode(2);
        var e3_1 = new TreeNode(3);
        e1_1.right = e3_1;
        e3_1.left = e2_1;

        var e1_2 = new TreeNode(1);
        var e2_2 = new TreeNode(2);
        var e3_2 = new TreeNode(3);
        e1_2.right = e2_2;
        e2_2.right = e3_2;

        var e1_3 = new TreeNode(1);
        var e2_3 = new TreeNode(2);
        var e3_3 = new TreeNode(3);
        e2_3.left = e1_3;
        e2_3.right = e3_3;

        var e1_4 = new TreeNode(1);
        var e2_4 = new TreeNode(2);
        var e3_4 = new TreeNode(3);
        e3_4.left = e2_4;
        e2_4.left = e1_4;

        var e1_5 = new TreeNode(1);
        var e2_5 = new TreeNode(2);
        var e3_5 = new TreeNode(3);
        e3_5.left = e1_5;
        e1_5.right = e2_5;

        var result = GenerateTrees(3);
        var expected = new List<TreeNode> { e1_1, e1_2, e2_3, e3_4, e3_5 };
        result.Should().BeEquivalentTo(expected);
    }

    // итеративное решение: идем от листьев к корню
    public IList<TreeNode> GenerateTrees(int n)
    {
        if (n == 0)
            return new List<TreeNode>();

        // dp[i] — все BST с i узлами
        List<TreeNode>[] dp = new List<TreeNode>[n + 1];

        // База: пустое дерево
        dp[0] = new List<TreeNode> { null };

        for (int len = 1; len <= n; len++)
        {
            dp[len] = new List<TreeNode>();

            // размер левого поддерева
            for (int leftSize = 0; leftSize < len; leftSize++)
            {
                int rightSize = len - leftSize - 1;

                foreach (var left in dp[leftSize])
                {
                    foreach (var right in dp[rightSize])
                    {
                        TreeNode root = new TreeNode(leftSize + 1);
                        root.left = left;
                        root.right = Clone(right, leftSize + 1);

                        dp[len].Add(root);
                    }
                }
            }
        }

        return dp[n];
    }

    private TreeNode Clone(TreeNode node, int offset)
    {
        if (node == null)
            return null;

        TreeNode newNode = new TreeNode(node.val + offset);
        newNode.left = Clone(node.left, offset);
        newNode.right = Clone(node.right, offset);
        return newNode;
    }

    // рекурсивное решение
    public IList<TreeNode> GenerateTreesRec(int n)
    {
        if (n == 0)
            return new List<TreeNode>();

        return BuildTreesRec(1, n);
    }

    private IList<TreeNode> BuildTreesRec(int start, int end)
    {
        List<TreeNode> result = new List<TreeNode>();

        // Базовый случай: пустое дерево
        if (start > end)
        {
            result.Add(null);
            return result;
        }

        // Перебираем каждый возможный корень
        for (int i = start; i <= end; i++)
        {
            var leftTrees = BuildTreesRec(start, i - 1); // все возможные левые поддеревья
            var rightTrees = BuildTreesRec(i + 1, end); // все возможные правые поддеревья

            // Комбинируем левое и правое поддеревья
            foreach (var left in leftTrees)
            {
                foreach (var right in rightTrees)
                {
                    TreeNode root = new TreeNode(i);
                    root.left = left;
                    root.right = right;
                    result.Add(root);
                }
            }
        }

        return result;
    }

    // не работает, не смог дорешать, надоело
    public IList<TreeNode> GenerateTreesMy(int n)
    {
        var res = new List<TreeNode>();
        var nums = Enumerable.Range(1, n).ToArray();
        // var curr = new TreeNode(-1);
        for (int i = 1; i <= n; i++)
        {
            DFS(i, nums.Where(a => a != i).ToArray());
        }
        return res;

        TreeNode DFS(int nodeVal, int[] arr)
        {
            var node = new TreeNode(nodeVal);
            if (arr.Length == 0)
            {
                // res.Add(curr);
                return node;
            }
            var leftItems = arr.Where(a => a != nodeVal && a < nodeVal).ToArray();
            foreach (var leftVal in leftItems)
            {
                node.left = DFS(leftVal, leftItems.Where(i => i != leftVal).ToArray());
            }
            var rightItems = arr.Where(a => a != nodeVal && a > nodeVal).ToArray();
            foreach (var rightVal in rightItems)
            {
                node.right = DFS(rightVal, rightItems.Where(i => i != rightVal).ToArray());
            }

            var newNode = new TreeNode(node.val, node.left, node.right);
            if (nodeVal != newNode.val)
            {
                res.Add(newNode);
            }

            return newNode;
        }
    }

    public IList<TreeNode> GenerateTrees1(int n)
    {
        var res = new List<TreeNode>();
        var nums = Enumerable.Range(1, n).ToArray();
        for (int i = 1; i <= n; i++)
        {
            var node = GetTree(i, nums);
            res.Add(node);
        }

        TreeNode GetTree(int nodeVal, int[] arr)
        {
            if (arr.Count() == 0)
            {
                return new TreeNode(nodeVal);
            }
            var node = new TreeNode(nodeVal);
            foreach (var val in arr.Where(a => a != nodeVal))
            {
                if (val < nodeVal)
                {
                    node.left = GetTree(val, arr.Where(i => i != nodeVal && i < val).ToArray());
                }
                else
                {
                    node.right = GetTree(val, arr.Where(i => i != nodeVal && i > val).ToArray());
                }
            }

            return node;
        }

        return res;
    }
}
