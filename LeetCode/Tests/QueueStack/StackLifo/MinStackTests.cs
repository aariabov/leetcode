namespace Tests.QueueStack.StackLifo;

/// <summary>
/// [Реализовать стек с минимальным значением](https://leetcode.com/explore/learn/card/queue-stack/230/usage-stack/1360/)
/// </summary>
public class MinStackTests
{
    [Fact]
    public void Test()
    {
        MinStack minStack = new MinStack();
        minStack.Push(-2);
        minStack.Push(0);
        minStack.Push(-3);
        Assert.Equal(-3, minStack.GetMin()); // return -3
        minStack.Pop();
        Assert.Equal(0, minStack.Top()); // return 0
        Assert.Equal(-2, minStack.GetMin()); // return -2
    }

    [Fact]
    public void Test2()
    {
        MinStack minStack = new MinStack();
        minStack.Push(-1);
        Assert.Equal(-1, minStack.Top());
        Assert.Equal(-1, minStack.GetMin());
    }

    [Fact]
    public void Test3()
    {
        MinStack minStack = new MinStack();
        minStack.Push(0);
        minStack.Push(1);
        minStack.Push(0);
        Assert.Equal(0, minStack.GetMin());
        minStack.Pop();
        Assert.Equal(0, minStack.GetMin());
        minStack.Pop();
        Assert.Equal(0, minStack.GetMin());
        minStack.Pop();

        minStack.Push(-2);
        minStack.Push(-1);
        minStack.Push(-2);
        Assert.Equal(-2, minStack.GetMin());
        minStack.Pop();
        Assert.Equal(-1, minStack.Top());
        Assert.Equal(-1, minStack.Top());
        Assert.Equal(-2, minStack.GetMin());
        Assert.Equal(-2, minStack.GetMin());
        minStack.Pop();
        Assert.Equal(-2, minStack.GetMin());
        minStack.Pop();
    }

    public class MinStack
    {
        private Stack<int> stack;

        // для хранения минимумов
        private Stack<int> minStack;

        public MinStack()
        {
            stack = new Stack<int>();
            minStack = new Stack<int>();
        }

        public void Push(int val)
        {
            stack.Push(val);

            // если val <= мин значения
            if (minStack.Count == 0 || val <= minStack.Peek())
            {
                minStack.Push(val);
            }
        }

        public void Pop()
        {
            // при удалении минимального значения, удаляем его из стека
            if (stack.Peek() == minStack.Peek())
            {
                minStack.Pop();
            }

            stack.Pop();
        }

        public int Top()
        {
            return stack.Peek();
        }

        public int GetMin()
        {
            return minStack.Peek();
        }
    }

    public class MinStackMy
    {
        private readonly List<int> _list = new List<int>();
        private int _min = int.MaxValue;

        public MinStackMy() { }

        public void Push(int val)
        {
            if (val < _min)
            {
                _min = val;
            }
            _list.Add(val);
        }

        public void Pop()
        {
            if (_list.Count > 0)
            {
                _list.RemoveAt(_list.Count - 1);
            }
        }

        public int Top()
        {
            if (_list.Count > 0)
            {
                var last = _list.Last();
                return last;
            }

            return -1;
        }

        public int GetMin()
        {
            if (_list.Count > 0)
            {
                return _list.Min();
            }

            return -1;
        }
    }
}
