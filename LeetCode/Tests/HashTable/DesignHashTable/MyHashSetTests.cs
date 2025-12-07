namespace Tests.HashTable.DesignHashTable;

/// <summary>
/// Design HashSet https://leetcode.com/explore/learn/card/hash-table/182/practical-applications/1139/
/// </summary>
public class MyHashSetTests
{
    [Fact]
    public void Test()
    {
        MyHashSet myHashSet = new MyHashSet();
        myHashSet.Add(1); // set = [1]
        myHashSet.Add(2); // set = [1, 2]
        Assert.True(myHashSet.Contains(1)); // return True
        Assert.False(myHashSet.Contains(3)); // return False, (not found)
        myHashSet.Add(2); // set = [1, 2]
        Assert.True(myHashSet.Contains(2)); // return True
        myHashSet.Remove(2); // set = [1]
        Assert.False(myHashSet.Contains(2)); // return False, (already removed)
    }

    public class MyHashSet
    {
        private const int Size = 10000;

        // массив списков для разрешения коллизий
        private readonly List<int>[] buckets;

        public MyHashSet()
        {
            buckets = new List<int>[Size];
        }

        private int GetIndex(int key)
        {
            return key % Size;
        }

        public void Add(int key)
        {
            int index = GetIndex(key);
            // если баскет еще не создан - создаем
            if (buckets[index] == null)
                buckets[index] = new List<int>();

            // если в баскете нет значения - добавляем
            if (!buckets[index].Contains(key))
                buckets[index].Add(key);
        }

        public void Remove(int key)
        {
            int index = GetIndex(key);
            if (buckets[index] == null)
                return;

            // если есть бакет - удаляем значение
            buckets[index].Remove(key);
        }

        public bool Contains(int key)
        {
            int index = GetIndex(key);
            if (buckets[index] == null)
                return false;

            return buckets[index].Contains(key);
        }
    }

    public class MyHashSet1
    {
        private List<int> _arr = new List<int>();

        public MyHashSet1() { }

        public void Add(int key)
        {
            if (!_arr.Contains(key))
            {
                _arr.Add(key);
            }
        }

        public void Remove(int key)
        {
            _arr.Remove(key);
        }

        public bool Contains(int key)
        {
            return _arr.Contains(key);
        }
    }
}
