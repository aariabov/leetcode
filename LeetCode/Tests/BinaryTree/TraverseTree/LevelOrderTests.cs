namespace Tests.BinaryTree.TraverseTree;

/// <summary>
/// [Обход бинарного дерева в ширину](https://leetcode.com/explore/learn/card/data-structure-tree/134/traverse-a-tree/931/)
/// </summary>
public class LevelOrderTests
{
    [Fact]
    public void Test()
    {
        var e1 = new TreeNode(3);
        var e2 = new TreeNode(9);
        var e3 = new TreeNode(20);
        var e4 = new TreeNode(15);
        var e5 = new TreeNode(7);

        e1.left = e2;
        e1.right = e3;
        e3.left = e4;
        e3.right = e5;

        var result = LevelOrder(e1);
        var expected = new int[][] { [3], [9, 20], [15, 7] };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test1()
    {
        var e1 = new TreeNode(1);

        var result = LevelOrder(e1);
        var expected = new int[][] { [1] };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test2()
    {
        var result = LevelOrder(null);
        var expected = new int[][] { };
        Assert.Equal(expected, result);
    }

    // правильное решание BFS
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        var res = new List<IList<int>>();
        if (root == null)
        {
            return res;
        }

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var levelSize = queue.Count;
            var levelList = new List<int>();
            for (int i = 0; i < levelSize; i++)
            {
                var node = queue.Dequeue();
                levelList.Add(node.val);
                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }

                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }
            res.Add(levelList);
        }
        return res;
    }

    // рекурсивное решение - просто ради интереса
    public IList<IList<int>> LevelOrderRec(TreeNode root)
    {
        IList<IList<int>> result = new List<IList<int>>();
        DFS(root, 0, result);
        return result;
    }

    private void DFS(TreeNode node, int level, IList<IList<int>> result)
    {
        if (node == null)
            return;

        if (result.Count == level)
        {
            // создаем список, но заполняем не сразу, а по мере рекурсивного прохождения
            result.Add(new List<int>());
        }

        result[level].Add(node.val);

        // рекурсивно обходим дерево
        DFS(node.left, level + 1, result);
        DFS(node.right, level + 1, result);
    }
}
