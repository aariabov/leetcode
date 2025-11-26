namespace Tests.LinkedLists;

public class MyLinkedListTests
{
    [Fact]
    public void Test()
    {
        MyLinkedList myLinkedList = new MyLinkedList();
        myLinkedList.AddAtHead(1);
        myLinkedList.AddAtTail(3);
        myLinkedList.AddAtIndex(1, 2);    // linked list becomes 1->2->3
        Assert.Equal(2, myLinkedList.Get(1));              // return 2
        myLinkedList.DeleteAtIndex(1);    // now the linked list is 1->3
        Assert.Equal(3, myLinkedList.Get(1));              // return 3
    }
    
    [Fact]
    public void Test1()
    {
        MyLinkedList myLinkedList = new MyLinkedList();
        myLinkedList.AddAtHead(7);
        myLinkedList.AddAtHead(2);
        myLinkedList.AddAtHead(1); // 1,2,7
        myLinkedList.AddAtIndex(3, 0);
        myLinkedList.DeleteAtIndex(2);
        myLinkedList.AddAtHead(6);
        myLinkedList.AddAtTail(4); // 6 1 2 4
        Assert.Equal(4, myLinkedList.Get(4));
        myLinkedList.AddAtHead(4);
        myLinkedList.AddAtIndex(5, 0);
        myLinkedList.AddAtHead(6);
    }
    
    [Fact]
    public void Test2()
    {
        MyLinkedList myLinkedList = new MyLinkedList();
        myLinkedList.AddAtHead(1);
        myLinkedList.DeleteAtIndex(0);
    }
    
    [Fact]
    public void Test3()
    {
        MyLinkedList myLinkedList = new MyLinkedList();
        myLinkedList.AddAtHead(1);
        myLinkedList.AddAtTail(3);
        myLinkedList.AddAtIndex(1, 2);    // 1 2 3
        Assert.Equal(2, myLinkedList.Get(1));
        myLinkedList.DeleteAtIndex(0);    // 2 3
        Assert.Equal(2, myLinkedList.Get(0));
    }
    
    [Fact]
    public void Test4()
    {
        MyLinkedList myLinkedList = new MyLinkedList();
        myLinkedList.AddAtIndex(0, 10);
        myLinkedList.AddAtIndex(0, 20);
        myLinkedList.AddAtIndex(1, 30);
        Assert.Equal(20, myLinkedList.Get(0));
    }

    public class MyLinkedList
    {
        private class Node
        {
            public int val;
            public Node next;

            public Node(int value)
            {
                val = value;
            }
        }

        private Node head; // sentinel node (не содержит данных списка)
        private Node tail; // последний реальный узел
        private int size;

        public MyLinkedList()
        {
            head = new Node(0); // фиктивный
            tail = head; // список пуст — tail = sentinel
            size = 0;
        }

        public int Get(int index)
        {
            if (index < 0 || index >= size) return -1;

            Node cur = head.next;
            for (int i = 0; i < index; i++)
                cur = cur.next;

            return cur.val;
        }

        public void AddAtHead(int val)
        {
            Node newNode = new Node(val);
            newNode.next = head.next;
            head.next = newNode;

            // если список был пуст — обновляем tail
            if (size == 0)
                tail = newNode;

            size++;
        }

        public void AddAtTail(int val)
        {
            Node newNode = new Node(val);
            tail.next = newNode;
            tail = newNode;
            size++;
        }

        public void AddAtIndex(int index, int val)
        {
            if (index < 0 || index > size) return;

            if (index == 0)
            {
                AddAtHead(val);
                return;
            }

            if (index == size)
            {
                AddAtTail(val);
                return;
            }

            Node prev = head;
            for (int i = 0; i < index; i++)
                prev = prev.next;

            Node newNode = new Node(val);
            newNode.next = prev.next;
            prev.next = newNode;
            size++;
        }

        public void DeleteAtIndex(int index)
        {
            if (index < 0 || index >= size) return;

            Node prev = head;
            for (int i = 0; i < index; i++)
                prev = prev.next;

            // удаляем prev.next
            Node deleted = prev.next;
            prev.next = deleted.next;

            // если удалили хвост — обновляем tail
            if (deleted == tail)
                tail = prev;

            size--;
        }
    }



    public class MyLinkedList1
    {
        private Node? _head = null;
        private int _count = 0;
        public MyLinkedList1() 
        {
        
        }
    
        public int Get(int index) 
        {
            if (index < 0 || index > _count - 1)
            {
                return -1;
            }
            Node? cur = null;
            var i = 0;
            while (i < index + 1)
            {
                cur = i == 0 ? _head : cur.Next;
                i++;
            }

            return cur.Val;
        }
    
        public void AddAtHead(int val)
        {
            if (_head == null)
            {
                _head = new Node
                {
                    Val = val,
                    Next = null
                };
            }
            else
            {
                var newHead = new Node
                {
                    Val = val,
                    Next = _head
                };
                _head = newHead;
            }
            _count++;
        }
    
        public void AddAtTail(int val)
        {
            if (_head == null)
            {
                _head = new Node
                {
                    Val = val,
                    Next = null
                };
            }
            else
            {
                var last = _head;
                while (last.Next != null)
                {
                    last = last.Next;
                }
                var newLast = new Node
                {
                    Val = val,
                    Next = null
                };
                last.Next = newLast;
            }
            _count++;
        }
    
        public void AddAtIndex(int index, int val)
        {
            if (index < 0 || index > _count)
            {
                return;
            }
            Node? cur = null;
            Node? prev = null;
            var i = 0;
            while (i < index + 1)
            {
                prev = i == 0 ? null : cur;
                cur = i == 0 ? _head : cur.Next;
                i++;
            }
            
            var newNode = new Node
            {
                Val = val,
                Next = cur
            };
            if (prev != null)
            {
                prev.Next = newNode;
            }
            else
            {
                _head = newNode;
            }
            _count++;
        }
    
        public void DeleteAtIndex(int index) 
        {
            if (index < 0 || index > _count - 1)
            {
                return;
            }
            Node? cur = null;
            Node? prev = null;
            var i = 0;
            while (i < index + 1)
            {
                prev = i == 0 ? null : cur;
                cur = i == 0 ? _head : cur.Next;
                i++;
            }

            if (prev != null && cur != null)
            {
                prev.Next = cur.Next;
            }
            else
            {
                _head = cur.Next;
            }

            _count--;
        }
        
        private class Node
        {
            public int Val { get; set; }
            public Node? Next { get; set; }
        }
    }
}