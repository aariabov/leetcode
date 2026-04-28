namespace Tests.Heap;

// [Найти медиану в потоке данных](https://leetcode.com/explore/learn/card/heap/646/practices/4092/)
public class MedianFinderTests
{
    [Fact]
    public void Test()
    {
        MedianFinder medianFinder = new MedianFinder();
        medianFinder.AddNum(1); // arr = [1]
        medianFinder.AddNum(2); // arr = [1, 2]
        Assert.Equal(1.5, medianFinder.FindMedian()); // return 1.5 (i.e., (1 + 2) / 2)
        medianFinder.AddNum(3); // arr[1, 2, 3]
        Assert.Equal(2, medianFinder.FindMedian()); // return 2.0
    }

    [Fact]
    public void Test1()
    {
        // ["addNum","findMedian","addNum","findMedian","addNum","findMedian","addNum","findMedian","addNum","findMedian","addNum","findMedian","addNum","findMedian","addNum","findMedian"]
        // [[6],[],[5],[],[0],[],[6],[],[3],[],[1],[],[0],[],[0],[]]
        // [null,6.00000,null,6.00000,null,5.50000,null,6.00000,null,5.50000,null,5.00000,null,4.00000,null,3.00000]
        var medianFinder = new MedianFinder();
        medianFinder.AddNum(6);
        Assert.Equal(6, medianFinder.FindMedian());
        medianFinder.AddNum(10);
        Assert.Equal(8, medianFinder.FindMedian());
        medianFinder.AddNum(2);
        Assert.Equal(6, medianFinder.FindMedian());
    }

    [Fact]
    public void Test2()
    {
        var medianFinder = new MedianFinder();
        medianFinder.AddNum(-1);
        Assert.Equal(-1, medianFinder.FindMedian());
        medianFinder.AddNum(-2);
        Assert.Equal(-1.5, medianFinder.FindMedian());
        medianFinder.AddNum(-3);
        Assert.Equal(-2, medianFinder.FindMedian());
        medianFinder.AddNum(-4);
        Assert.Equal(-2.5, medianFinder.FindMedian());
        medianFinder.AddNum(-5);
        Assert.Equal(-3, medianFinder.FindMedian());
    }

    [Fact]
    public void Test3()
    {
        // ["addNum","findMedian","addNum","findMedian","addNum","findMedian","addNum","findMedian","addNum","findMedian","addNum","findMedian","addNum","findMedian","addNum","findMedian","addNum","findMedian","addNum","findMedian"]
        // [[1],[],[2],[],[3],[],[4],[],[5],[],[6],[],[7],[],[8],[],[9],[],[10],[]]
        var medianFinder = new MedianFinder();
        medianFinder.AddNum(1);
        Assert.Equal(1, medianFinder.FindMedian());
        medianFinder.AddNum(2);
        Assert.Equal(1.5, medianFinder.FindMedian());
        medianFinder.AddNum(3);
        Assert.Equal(2, medianFinder.FindMedian());
        medianFinder.AddNum(4);
        Assert.Equal(2.5, medianFinder.FindMedian());
        medianFinder.AddNum(5);
        Assert.Equal(3, medianFinder.FindMedian());
    }

    public class MedianFinder
    {
        // Результирующая левая половина (на вершине самое большое число)
        private PriorityQueue<int, int> leftMaxHeap;

        // Результирующая правая половина (на вершине самое маленькое число)
        private PriorityQueue<int, int> rightMinHeap;

        public MedianFinder()
        {
            leftMaxHeap = new PriorityQueue<int, int>(
                Comparer<int>.Create((a, b) => b.CompareTo(a))
            );
            rightMinHeap = new PriorityQueue<int, int>();
        }

        public void AddNum(int num)
        {
            // 1. Добавляем в левую кучу
            leftMaxHeap.Enqueue(num, num);

            // 2. Перебрасываем верхушку в правую кучу (балансировка значений)
            int topFromLeft = leftMaxHeap.Dequeue();
            rightMinHeap.Enqueue(topFromLeft, topFromLeft);

            // 3. Поддерживаем размер: левая куча может быть на 1 элемент больше правой
            if (leftMaxHeap.Count < rightMinHeap.Count)
            {
                int topFromRight = rightMinHeap.Dequeue();
                leftMaxHeap.Enqueue(topFromRight, topFromRight);
            }
        }

        public double FindMedian()
        {
            if (leftMaxHeap.Count > rightMinHeap.Count)
            {
                return leftMaxHeap.Peek();
            }
            return (leftMaxHeap.Peek() + rightMinHeap.Peek()) / 2.0;
        }
    }

    public class MyMedianFinderPriorityQueue
    {
        private PriorityQueue<int, int> maxHeap = new();
        private PriorityQueue<int, int> minHeap = new();

        public void AddNum(int num)
        {
            if (minHeap.Count > 0)
            {
                if (num > minHeap.Peek())
                {
                    minHeap.Enqueue(num, num);
                    if (minHeap.Count > maxHeap.Count)
                    {
                        var temp = minHeap.Dequeue();
                        maxHeap.Enqueue(temp, -temp);
                    }
                }
                else
                {
                    maxHeap.Enqueue(num, -num);
                    if (maxHeap.Count - minHeap.Count > 1)
                    {
                        var temp = maxHeap.Dequeue();
                        minHeap.Enqueue(temp, temp);
                    }
                }
                return;
            }

            maxHeap.Enqueue(num, -num);
            if (maxHeap.Count - minHeap.Count > 1)
            {
                var temp = maxHeap.Dequeue();
                minHeap.Enqueue(temp, temp);
            }
        }

        public double FindMedian()
        {
            if (maxHeap.Count == minHeap.Count)
            {
                var num1 = maxHeap.Peek();
                var num2 = minHeap.Peek();
                return ((double)num1 + num2) / 2;
            }

            return maxHeap.Peek();
        }
    }

    // работает и быстро
    public class MedianFinderList
    {
        private List<int> nums = new();

        public void AddNum(int num)
        {
            if (nums.Count == 0)
            {
                nums.Add(num);
                return;
            }

            // BinarySearch спроектирован так, чтобы возвращать одну переменную, которая сообщает сразу о двух вещах: найден ли элемент и куда его вставить, если он не найден
            var index = nums.BinarySearch(num);
            if (index < 0)
            {
                // такие извраты нужны, для кейса, когда BinarySearch возвращает 0, непонятно элемент найден на позиции 0 или его надо вставить на позицию 0
                index = ~index; // index = -index - 1
            }

            nums.Insert(index, num);
        }

        public double FindMedian()
        {
            if (nums.Count == 0)
            {
                return 0;
            }

            if (nums.Count == 1)
            {
                return nums[0];
            }

            if (nums.Count % 2 == 1)
            {
                return nums[nums.Count / 2];
            }

            var rightIdx = nums.Count / 2;
            return ((double)nums[rightIdx - 1] + nums[rightIdx]) / 2;
        }
    }
}
