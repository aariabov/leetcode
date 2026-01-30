using System.Collections;
using System.Configuration.Internal;

namespace Tests.BinarySearchTree.Introduction;

// чисто ради интереса сделать через итератор
public class MyBSTIteratorTests
{
    [Fact]
    public void Test()
    {
        var bSTIterator = new MyBSTIterator(TreeNode.BuildTree([7, 3, 15, null, null, 9, 20]));
        var res = new List<int>();
        foreach (var item in bSTIterator)
        {
            res.Add(item);
        }

        var resStr = string.Join(",", res);
        Assert.Equal("3,7,9,15,20", resStr);
    }

    public class MyBSTIterator : IEnumerable<int>
    {
        private readonly Stack<TreeNode> stack = new Stack<TreeNode>();
        private TreeNode current;
        private TreeNode _root;

        public MyBSTIterator(TreeNode root)
        {
            current = root;
            _root = root;
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

        public IEnumerator<int> GetEnumerator()
        {
            // либо так, через yield
            // while (current != null || stack.Count > 0)
            // {
            //     yield return Next();
            // }
            return new MyEnumerator(_root);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private class MyEnumerator : IEnumerator<int>
    {
        private readonly Stack<TreeNode> stack = new Stack<TreeNode>();
        private TreeNode current;
        private TreeNode root;
        private int _res;

        public MyEnumerator(TreeNode root)
        {
            current = root;
        }

        public bool MoveNext()
        {
            if (current == null && stack.Count == 0)
            {
                return false;
            }

            while (current != null)
            {
                stack.Push(current);
                current = current.left;
            }

            // Обрабатываем узел
            current = stack.Pop();
            _res = current.val;

            // Переходим вправо
            current = current.right;

            return true;
        }

        public void Reset()
        {
            current = root;
        }

        public int Current => _res;

        object? IEnumerator.Current => Current;

        public void Dispose() { }
    }
}
