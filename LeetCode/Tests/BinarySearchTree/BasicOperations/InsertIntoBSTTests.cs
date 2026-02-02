namespace Tests.BinarySearchTree.BasicOperations;

/// <summary>
/// [Вставка в бинарное дерево](https://leetcode.com/explore/learn/card/introduction-to-data-structure-binary-search-tree/141/basic-operations-in-a-bst/1003/)
/// </summary>
public class InsertIntoBSTTests
{
    [Fact]
    public void Test()
    {
        var e1 = new TreeNode(1);
        var e2 = new TreeNode(2);
        var e3 = new TreeNode(3);
        var e4 = new TreeNode(4);
        var e7 = new TreeNode(7);

        e4.left = e2;
        e4.right = e7;
        e2.left = e1;
        e2.right = e3;

        var result = InsertIntoBST(e4, 5);
        var expected = new int[] { 4, 2, 7, 1, 3, 5 };
        Assert.Equal(expected, result.LevelOrder().ToArray());
    }

    public TreeNode InsertIntoBST(TreeNode root, int val)
    {
        Insert(ref root, val);
        return root;

        void Insert(ref TreeNode? node, int num)
        {
            if (node == null)
            {
                node = new TreeNode(num);
                return;
            }

            if (num < node.val)
            {
                Insert(ref node.left, num);
            }
            else
            {
                Insert(ref node.right, num);
            }
        }
    }
}
