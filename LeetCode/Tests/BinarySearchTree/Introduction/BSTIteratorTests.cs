namespace Tests.BinarySearchTree.Introduction;

/// <summary>
/// [Итератор для бинарного дерева поиска](https://leetcode.com/explore/learn/card/introduction-to-data-structure-binary-search-tree/140/introduction-to-a-bst/1008/)
/// </summary>
public class BSTIteratorTests
{
    [Fact]
    public void Test()
    {
        var bSTIterator = new BSTIterator(TreeNode.BuildTree([7, 3, 15, null, null, 9, 20]));
        Assert.Equal(3, bSTIterator.Next()); // return 3
        Assert.Equal(7, bSTIterator.Next()); // return 7
        Assert.True(bSTIterator.HasNext()); // return True
        Assert.Equal(9, bSTIterator.Next()); // return 9
        Assert.True(bSTIterator.HasNext()); // return True
        Assert.Equal(15, bSTIterator.Next()); // return 15
        Assert.True(bSTIterator.HasNext()); // return True
        Assert.Equal(20, bSTIterator.Next()); // return 20
        Assert.False(bSTIterator.HasNext()); // return False
    }

    public class BSTIterator
    {
        private readonly Stack<TreeNode> stack = new Stack<TreeNode>();
        private TreeNode current;

        public BSTIterator(TreeNode root)
        {
            current = root;
        }

        public int Next()
        {
            while (current != null)
            {
                stack.Push(current);
                current = current.left;
            }

            // Обрабатываем узел
            current = stack.Pop();
            var res = current.val;

            // Переходим вправо
            current = current.right;

            return res;
        }

        public bool HasNext()
        {
            return current != null || stack.Count > 0;
        }
    }
}
