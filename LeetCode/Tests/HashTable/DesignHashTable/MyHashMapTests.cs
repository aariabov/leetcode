namespace Tests.HashTable.DesignHashTable;

public class MyHashMapTests
{
    [Fact]
    public void Test()
    {
        MyHashMap myHashMap = new MyHashMap();
        myHashMap.Put(1, 1); // The map is now [[1,1]]
        myHashMap.Put(2, 2); // The map is now [[1,1], [2,2]]
        Assert.Equal(1, myHashMap.Get(1)); // return 1, The map is now [[1,1], [2,2]]
        Assert.Equal(-1, myHashMap.Get(3)); // return -1 (i.e., not found), The map is now [[1,1], [2,2]]
        myHashMap.Put(2, 1); // The map is now [[1,1], [2,1]] (i.e., update the existing value)
        Assert.Equal(1, myHashMap.Get(2)); // return 1, The map is now [[1,1], [2,1]]
        myHashMap.Remove(2); // remove the mapping for 2, The map is now [[1,1]]
        Assert.Equal(-1, myHashMap.Get(2)); // return -1 (i.e., not found), The map is now [[1,1]]
    }

    public class MyHashMap
    {
        private class Node
        {
            public int Key;
            public int Value;
            public Node Next;

            public Node(int key, int value, Node next = null)
            {
                Key = key;
                Value = value;
                Next = next;
            }
        }

        private readonly int size = 10000;
        private Node[] buckets;

        public MyHashMap()
        {
            buckets = new Node[size];
        }

        private int GetIndex(int key)
        {
            return key % size;
        }

        public void Put(int key, int value)
        {
            int index = GetIndex(key);
            Node head = buckets[index];

            // Если список пуст — просто добавляем
            if (head == null)
            {
                buckets[index] = new Node(key, value);
                return;
            }

            // Иначе ищем ключ
            Node cur = head;
            while (cur != null)
            {
                if (cur.Key == key)
                {
                    cur.Value = value; // обновление
                    return;
                }
                if (cur.Next == null)
                    break;
                cur = cur.Next;
            }

            // Добавляем в конец
            cur.Next = new Node(key, value);
        }

        public int Get(int key)
        {
            int index = GetIndex(key);
            Node cur = buckets[index];

            while (cur != null)
            {
                if (cur.Key == key)
                    return cur.Value;
                cur = cur.Next;
            }

            return -1;
        }

        public void Remove(int key)
        {
            int index = GetIndex(key);
            Node cur = buckets[index];
            Node prev = null;

            while (cur != null)
            {
                if (cur.Key == key)
                {
                    if (prev == null)
                        buckets[index] = cur.Next; // удаляем голову
                    else
                        prev.Next = cur.Next; // удаляем узел внутри списка
                    return;
                }

                prev = cur;
                cur = cur.Next;
            }
        }
    }

    public class MyHashMap1
    {
        private const int Size = 1000000;

        // массив списков для разрешения коллизий
        private readonly int?[] _buckets;

        public MyHashMap1()
        {
            _buckets = new int?[Size];
        }

        private int GetIndex(int key)
        {
            return key % Size;
        }

        public void Put(int key, int value)
        {
            int index = GetIndex(key);
            _buckets[index] = value;
        }

        public int Get(int key)
        {
            int index = GetIndex(key);
            return _buckets[index] ?? -1;
        }

        public void Remove(int key)
        {
            int index = GetIndex(key);
            _buckets[index] = null;
        }
    }
}
