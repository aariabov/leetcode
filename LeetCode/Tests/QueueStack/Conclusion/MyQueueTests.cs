namespace Tests.QueueStack.Conclusion;

/// <summary>
/// [Очередь на двух стеках](https://leetcode.com/explore/learn/card/queue-stack/239/conclusion/1386/)
/// </summary>
public class MyQueueTests
{
    [Fact]
    public void Test()
    {
        MyQueue myQueue = new MyQueue();
        myQueue.Push(1); // queue is: [1]
        myQueue.Push(2); // queue is: [1, 2] (leftmost is front of the queue)
        Assert.Equal(1, myQueue.Peek()); // return 1
        Assert.Equal(1, myQueue.Pop()); // return 1, queue is [2]
        Assert.False(myQueue.Empty()); // return false
    }
    
    [Fact]
    public void Test1()
    {
        MyQueue myQueue = new MyQueue();
        myQueue.Push(1);
        myQueue.Push(2);
        myQueue.Push(3);
        Assert.False(myQueue.Empty());
        Assert.Equal(1, myQueue.Peek());
        Assert.Equal(1, myQueue.Pop());
        Assert.Equal(2, myQueue.Pop());
        Assert.Equal(3, myQueue.Pop());
        Assert.True(myQueue.Empty());
    }
    
    // Идея: не надо всегда хранить актуальными стеке, можно перекладывать только при необходимости
    public class MyQueue
    {
        private Stack<int> inStack;
        private Stack<int> outStack;

        public MyQueue()
        {
            inStack = new Stack<int>();
            outStack = new Stack<int>();
        }

        public void Push(int x)
        {
            inStack.Push(x);
        }

        public int Pop()
        {
            MoveIfNeeded();
            return outStack.Pop();
        }

        public int Peek()
        {
            MoveIfNeeded();
            return outStack.Peek();
        }

        public bool Empty()
        {
            return inStack.Count == 0 && outStack.Count == 0;
        }

        private void MoveIfNeeded()
        {
            if (outStack.Count == 0)
            {
                while (inStack.Count > 0)
                {
                    outStack.Push(inStack.Pop());
                }
            }
        }
    }
    
    // работает, но всегда держит актуальными стеки, из-за этого частые перекладывания
    public class MyQueueMy
    {
        private Stack<int> stack = new Stack<int>();
        private Stack<int> queue = new Stack<int>();

        public MyQueueMy() {
        
        }
    
        public void Push(int x)
        {
            stack.Push(x);
            var arr = new int[stack.Count];
            var i = 0;
            queue.Clear();
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                queue.Push(item);
                arr[i] = item;
                i++;
            }

            for (int j = arr.Length-1; j > -1; j--)
            {
                stack.Push(arr[j]);
            }
        }
    
        public int Pop()
        {
            var first = queue.Pop();
            var arr = new int[queue.Count];
            var i = 0;
            stack.Clear();
            while (queue.Count > 0)
            {
                var item = queue.Pop();
                stack.Push(item);
                arr[i] = item;
                i++;
            }

            for (int j = arr.Length-1; j > -1; j--)
            {
                queue.Push(arr[j]);
            }

            return first;
        }
    
        public int Peek() 
        {
            return queue.Peek();
        }
    
        public bool Empty() 
        {
            return queue.Count == 0;
        }
    }
}