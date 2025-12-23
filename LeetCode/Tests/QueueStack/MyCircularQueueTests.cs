namespace Tests.QueueStack;

/// <summary>
/// [Реализовать очередь](https://leetcode.com/explore/learn/card/queue-stack/228/first-in-first-out-data-structure/1337/)
/// </summary>
public class MyCircularQueueTests
{
    [Fact]
    public void Test()
    {
        MyCircularQueue myCircularQueue = new MyCircularQueue(3);
        Assert.True(myCircularQueue.IsEmpty());
        Assert.True(myCircularQueue.EnQueue(1)); // return True
        Assert.True(myCircularQueue.EnQueue(2)); // return True
        Assert.True(myCircularQueue.EnQueue(3)); // return True
        Assert.False(myCircularQueue.IsEmpty());
        Assert.True(myCircularQueue.IsFull());
        Assert.False(myCircularQueue.EnQueue(4)); // return False
        Assert.Equal(3, myCircularQueue.Rear());     // return 3
        Assert.True(myCircularQueue.IsFull());   // return True
        Assert.True(myCircularQueue.DeQueue());  // return True
        Assert.False(myCircularQueue.IsFull());
        Assert.True(myCircularQueue.EnQueue(4)); // return True
        Assert.Equal(4, myCircularQueue.Rear());     // return 4
    }
    
    [Fact]
    public void Test1()
    {
        MyCircularQueue myCircularQueue = new MyCircularQueue(6);
        Assert.True(myCircularQueue.EnQueue(6));
        Assert.Equal(6, myCircularQueue.Rear());
        Assert.Equal(6, myCircularQueue.Rear());
        Assert.True(myCircularQueue.DeQueue());
        Assert.True(myCircularQueue.EnQueue(5));
        Assert.Equal(5, myCircularQueue.Rear());
        Assert.True(myCircularQueue.DeQueue());
        Assert.Equal(-1, myCircularQueue.Front());
        Assert.False(myCircularQueue.DeQueue());
        Assert.False(myCircularQueue.DeQueue());
        Assert.False(myCircularQueue.DeQueue());
    }
    
    [Fact]
    public void Test2()
    {
        MyCircularQueue myCircularQueue = new MyCircularQueue(8);
        Assert.True(myCircularQueue.EnQueue(3));
        Assert.True(myCircularQueue.EnQueue(9));
        Assert.True(myCircularQueue.EnQueue(5));
        Assert.True(myCircularQueue.EnQueue(0));
        Assert.True(myCircularQueue.DeQueue());
        Assert.True(myCircularQueue.DeQueue());
        Assert.False(myCircularQueue.IsEmpty());
        Assert.False(myCircularQueue.IsEmpty());
        Assert.Equal(0, myCircularQueue.Rear());
        Assert.Equal(0, myCircularQueue.Rear());
        Assert.True(myCircularQueue.DeQueue());
    }
    
    [Fact]
    public void Test3()
    {
        MyCircularQueue myCircularQueue = new MyCircularQueue(2);
        Assert.True(myCircularQueue.EnQueue(1));
        Assert.True(myCircularQueue.EnQueue(2));
        Assert.True(myCircularQueue.DeQueue());
        Assert.True(myCircularQueue.EnQueue(3));
        Assert.True(myCircularQueue.DeQueue());
        Assert.True(myCircularQueue.EnQueue(3));
        Assert.True(myCircularQueue.DeQueue());
        Assert.True(myCircularQueue.EnQueue(3));
        Assert.True(myCircularQueue.DeQueue());
        Assert.Equal(3, myCircularQueue.Front());
    }
    
    // ["Rear","enQueue"]
    // [[],[3]]
    [Fact]
    public void Test4()
    {
        MyCircularQueue myCircularQueue = new MyCircularQueue(3);
        Assert.True(myCircularQueue.EnQueue(7));
        Assert.True(myCircularQueue.DeQueue());
        Assert.Equal(-1, myCircularQueue.Front());
        Assert.False(myCircularQueue.DeQueue());
        Assert.Equal(-1, myCircularQueue.Front());
        Assert.Equal(-1, myCircularQueue.Rear());
        Assert.True(myCircularQueue.EnQueue(0));
        Assert.False(myCircularQueue.IsFull());
        Assert.True(myCircularQueue.DeQueue());
        Assert.Equal(-1, myCircularQueue.Rear());
        Assert.True(myCircularQueue.EnQueue(3));
    }
    
    public class MyCircularQueue
    {
        private readonly int[] _arr;
        private int _head;
        private int _tail;
        private int _size;
        private readonly int _capacity;

        public MyCircularQueue(int k)
        {
            _capacity = k;
            _arr = new int[k];
            _head = 0;
            _tail = -1;
            _size = 0;
        }

        public bool EnQueue(int value)
        {
            if (IsFull())
                return false;

            _tail = (_tail + 1) % _capacity;
            _arr[_tail] = value;
            _size++;
            return true;
        }

        public bool DeQueue()
        {
            if (IsEmpty())
                return false;

            _head = (_head + 1) % _capacity;
            _size--;
            return true;
        }

        public int Front()
        {
            if (IsEmpty())
                return -1;

            return _arr[_head];
        }

        public int Rear()
        {
            if (IsEmpty())
                return -1;

            return _arr[_tail];
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public bool IsFull()
        {
            return _size == _capacity;
        }
    }

    // мое решение
    public class MyCircularQueue1
    {
        private readonly int[] _arr;
        private int? _head;
        private int? _tail;

        public MyCircularQueue1(int k)
        {
            _arr = new int[k];
        }

        public bool EnQueue(int value)
        {
            if (IsFull())
            {
                return false;
            }
            
            if (IsEmpty())
            {
                _head = 0;
                _tail = 0;
                _arr[0] = value;
                return true;
            }

            if (_tail == _arr.Length - 1)
            {
                _tail = 0;
                _arr[_tail!.Value] = value;
                return true;
            }
            
            _tail++;
            _arr[_tail!.Value] = value;
            return true;
        }

        public bool DeQueue()
        {
            if (IsEmpty())
            {
                return false;
            }

            if (_tail == _head)
            {
                _head = null;
                _tail = null;
                return true;
            }

            if (_head == _arr.Length - 1)
            {
                _head = 0;
                return true;
            }
            
            _head++;
            return true;
        }

        public int Front()
        {
            if (IsEmpty())
            {
                return -1;
            }
            
            return _arr[_head.Value];
        }

        public int Rear()
        {
            if (IsEmpty())
            {
                return -1;
            }
            
            return _arr[_tail.Value];
        }

        public bool IsEmpty()
        {
            return _head is null && _tail is null;
        }

        public bool IsFull()
        {
            return (_head == 0 && _tail == _arr.Length - 1) || (_head - _tail == 1);
        }
    }
}