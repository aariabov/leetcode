namespace Tests.BinaryTree.SolveProblemsRecursively;

/// <summary>
/// [Симметричность бинарного дерева](https://leetcode.com/explore/learn/card/data-structure-tree/17/solve-problems-recursively/536/)
/// </summary>
public class IsSymmetricTests
{
    [Fact]
    public void Test()
    {
        var e1 = new TreeNode(1);
        var l2 = new TreeNode(2);
        var l3 = new TreeNode(3);
        var l4 = new TreeNode(4);
        var r2 = new TreeNode(2);
        var r3 = new TreeNode(3);
        var r4 = new TreeNode(4);

        e1.left = l2;
        e1.right = r2;
        l2.left = l3;
        l2.right = l4;
        r2.left = r4;
        r2.right = r3;

        var result = IsSymmetric(e1);
        Assert.True(result);
    }

    [Fact]
    public void Test1()
    {
        var e1 = TreeNode.BuildTree([1, 2, 2, null, 3, null, 3]);
        var result = IsSymmetric(e1);
        Assert.False(result);
    }

    public bool IsSymmetric(TreeNode root)
    {
        if (root == null)
            return true;

        return IsMirror(root.left, root.right);
    }

    private bool IsMirror(TreeNode t1, TreeNode t2)
    {
        // оба узла пустые — симметрично
        if (t1 == null && t2 == null)
            return true;

        // один пустой, другой нет — несимметрично
        if (t1 == null || t2 == null)
            return false;

        // значения должны совпадать
        if (t1.val != t2.val)
            return false;

        // рекурсивная проверка зеркальности
        return IsMirror(t1.left, t2.right) && IsMirror(t1.right, t2.left);
    }

    public bool IsSymmetricBFS(TreeNode root)
    {
        var queue = new Queue<TreeNode?>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var levelSize = queue.Count;
            var levelList = new List<int?>();
            for (int i = 0; i < levelSize; i++)
            {
                var node = queue.Dequeue();
                levelList.Add(node?.val);
                queue.Enqueue(node?.left);
                queue.Enqueue(node?.right);
            }

            var isAllNull = levelList.All(item => item == null);
            if (isAllNull)
            {
                return true;
            }

            var l = 0;
            var r = levelSize - 1;
            while (l < r)
            {
                if (levelList[l] != levelList[r])
                {
                    return false;
                }
                l++;
                r--;
            }
        }
        return true;
    }
}
