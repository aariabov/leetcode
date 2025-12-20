namespace Tests.HashTable;

public class MyHashSetExamples
{
    [Fact]
    public void Test()
    {
        MyHashSet myHashSet = new MyHashSet();
        myHashSet.Add(1); // set = [1]
        myHashSet.Add(2); // set = [1, 2]
        myHashSet.Add(11); // пример с коллизией, добавится в тот же бакет, что и 1, тк 11 % 10 == 1
        Assert.True(myHashSet.Contains(1)); // return True
        Assert.True(myHashSet.Contains(11));
        Assert.False(myHashSet.Contains(3)); // return False, (not found)
        myHashSet.Add(2); // set = [1, 2]
        Assert.True(myHashSet.Contains(2)); // return True
        myHashSet.Remove(2); // set = [1]
        Assert.False(myHashSet.Contains(2)); // return False, (already removed)
    }
    
    [Fact]
    public void my_hash_set_capacity_increase()
    {
        var myHashSet = new MyHashSet();
        Assert.Null(myHashSet.Buckets[1]);
        // после добавления первого элемента для баскета с индексом 1 выделяется массив размером 4
        myHashSet.Add(1);
        Assert.Equal(1, myHashSet.Buckets[1].Count);
        Assert.Equal(4, myHashSet.Buckets[1].Capacity);
        
        myHashSet.Add(11);
        Assert.Equal(2, myHashSet.Buckets[1].Count);
        Assert.Equal(4, myHashSet.Buckets[1].Capacity);
        
        myHashSet.Add(21);
        Assert.Equal(3, myHashSet.Buckets[1].Count);
        Assert.Equal(4, myHashSet.Buckets[1].Capacity);
        
        // заполняем последний элемент выделенного массива
        myHashSet.Add(31);
        Assert.Equal(4, myHashSet.Buckets[1].Count);
        Assert.Equal(4, myHashSet.Buckets[1].Capacity);
        
        // происходит выделение памяти под новый массив
        myHashSet.Add(41);
        Assert.Equal(5, myHashSet.Buckets[1].Count);
        Assert.Equal(8, myHashSet.Buckets[1].Capacity);
        
        myHashSet.Add(51);
        Assert.Equal(6, myHashSet.Buckets[1].Count);
        Assert.Equal(8, myHashSet.Buckets[1].Capacity);
        
        myHashSet.Add(61);
        Assert.Equal(7, myHashSet.Buckets[1].Count);
        Assert.Equal(8, myHashSet.Buckets[1].Capacity);
        
        // заполняем последний элемент выделенного массива
        myHashSet.Add(71);
        Assert.Equal(8, myHashSet.Buckets[1].Count);
        Assert.Equal(8, myHashSet.Buckets[1].Capacity);
        
        // происходит выделение памяти под новый массив
        myHashSet.Add(81);
        Assert.Equal(9, myHashSet.Buckets[1].Count);
        Assert.Equal(16, myHashSet.Buckets[1].Capacity);
    }

    private class MyHashSet
    {
        private const int Size = 10;

        // массив списков для разрешения коллизий
        public readonly List<int>[] Buckets;

        public MyHashSet()
        {
            Buckets = new List<int>[Size];
        }

        private int GetIndex(int key)
        {
            return key % Size;
        }

        public void Add(int key)
        {
            int index = GetIndex(key);
            // если баскет еще не создан - создаем
            if (Buckets[index] == null)
                Buckets[index] = new List<int>();

            // если в баскете нет значения - добавляем
            if (!Buckets[index].Contains(key))
                Buckets[index].Add(key);
        }

        public void Remove(int key)
        {
            int index = GetIndex(key);
            if (Buckets[index] == null)
                return;

            // если есть бакет - удаляем значение
            Buckets[index].Remove(key);
        }

        public bool Contains(int key)
        {
            int index = GetIndex(key);
            if (Buckets[index] == null)
                return false;

            return Buckets[index].Contains(key);
        }
    }
}