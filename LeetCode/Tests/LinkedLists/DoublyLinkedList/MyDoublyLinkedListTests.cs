namespace Tests.LinkedLists.DoublyLinkedList;

public class MyDoublyLinkedListTests
{
    [Fact]
    public void Test()
    {
        var myLinkedList = new MyLinkedList();
        myLinkedList.AddAtHead(1);
        myLinkedList.AddAtTail(3);
        myLinkedList.AddAtIndex(1, 2);
        Assert.Equal(2, myLinkedList.Get(1));
        myLinkedList.DeleteAtIndex(1);
        Assert.Equal(3, myLinkedList.Get(1));
    }

    [Fact]
    public void Test1()
    {
        var myLinkedList = new MyLinkedList();
        myLinkedList.AddAtHead(1);
        Assert.Equal(1, myLinkedList.Get(0));
        myLinkedList.DeleteAtIndex(0);
    }

    private class MyLinkedList
    {
        private class Node
        {
            public int val;
            public Node prev;
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
            if (index < 0 || index >= size)
                return -1;

            Node cur = head.next;
            for (int i = 0; i < index; i++)
                cur = cur.next;

            return cur.val;
        }

        public void AddAtHead(int val)
        {
            Node newNode = new Node(val);
            newNode.next = head.next;
            newNode.prev = head;
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
            newNode.prev = tail;
            tail = newNode;
            size++;
        }

        public void AddAtIndex(int index, int val)
        {
            if (index < 0 || index > size)
                return;

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
            newNode.next.prev = newNode;
            prev.next = newNode;
            size++;
        }

        public void DeleteAtIndex(int index)
        {
            if (index < 0 || index >= size)
                return;

            Node prev = head;
            for (int i = 0; i < index; i++)
                prev = prev.next;

            // удаляем prev.next
            Node deleted = prev.next;
            prev.next = deleted.next;
            if (deleted.next != null)
            {
                deleted.next.prev = prev;
            }

            // если удалили хвост — обновляем tail
            if (deleted == tail)
                tail = prev;

            size--;
        }
    }
}
