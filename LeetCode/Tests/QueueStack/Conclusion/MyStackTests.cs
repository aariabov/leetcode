namespace Tests.QueueStack.Conclusion;

public class MyStackTests
{
    [Fact]
    public void Test()
    {
        MyStack myStack = new MyStack();
        myStack.Push(1);
        myStack.Push(2);
        Assert.Equal(2, myStack.Top()); // return 2
        Assert.Equal(2, myStack.Pop()); // return 2
        Assert.False(myStack.Empty()); // return False
    }

    public class MyStack
    {
        private Queue<int> q1;
        private Queue<int> q2;

        public MyStack()
        {
            q1 = new Queue<int>();
            q2 = new Queue<int>();
        }

        // Push element x onto stack.
        public void Push(int x)
        {
            q2.Enqueue(x);

            while (q1.Count > 0)
            {
                q2.Enqueue(q1.Dequeue());
            }

            // Swap q1 and q2
            var temp = q1;
            q1 = q2;
            q2 = temp;
        }

        // Removes the element on top of the stack and returns it.
        public int Pop()
        {
            return q1.Dequeue();
        }

        // Get the top element.
        public int Top()
        {
            return q1.Peek();
        }

        // Returns whether the stack is empty.
        public bool Empty()
        {
            return q1.Count == 0;
        }
    }

    
    public class MyStackMy
    {
        private Queue<int> _inQueue = new Queue<int>();
        private Queue<int> _outQueue = new Queue<int>();

        public MyStackMy() {
        
        }
    
        public void Push(int x) {
            _inQueue.Enqueue(x);
        }
    
        public int Pop()
        {
            var res = 0;
            var queue = GetQueue();
            var emptyQueue = GetEmptyQueue();
            while (queue.Count > 0)
            {
                if (queue.Count == 1)
                {
                    res = queue.Peek();
                    queue.Clear();
                    return res;
                }
                emptyQueue.Enqueue(queue.Dequeue());
            }

            return 0;
        }
    
        public int Top()
        {
            var res = 0;
            var queue = GetQueue();
            var emptyQueue = GetEmptyQueue();
            while (queue.Count > 0)
            {
                if (queue.Count == 1)
                {
                    res = queue.Peek();
                }
                emptyQueue.Enqueue(queue.Dequeue());
            }
            return res;
        }
    
        public bool Empty()
        {
            return _inQueue.Count == 0 && _outQueue.Count == 0;
        }

        private Queue<int> GetQueue()
        {
            return _inQueue.Count == 0 ? _outQueue : _inQueue;
        }

        private Queue<int> GetEmptyQueue()
        {
            return _inQueue.Count == 0 ? _inQueue : _outQueue;
        }

        private void CopyIfNeeded()
        {
            var queue = GetQueue();
            var emptyQueue = GetEmptyQueue();
            while (queue.Count > 0)
            {
                emptyQueue.Enqueue(queue.Dequeue());
            }
        }
    }
}