namespace Tests.QueueStack.QueueFifo;

// [Среднее арифметическое в потоке данных](https://leetcode.com/explore/learn/card/queue-stack/228/first-in-first-out-data-structure/1368/)
public class MovingAverageTests
{
    [Fact]
    public void Test()
    {
        MovingAverage movingAverage = new MovingAverage(3);
        Assert.Equal(1, movingAverage.Next(1)); // return 1.0 = 1 / 1
        Assert.Equal(5.5, movingAverage.Next(10)); // return 5.5 = (1 + 10) / 2
        Assert.Equal(4.666666666666667, movingAverage.Next(3)); // return 4.66667 = (1 + 10 + 3) / 3
        Assert.Equal(6, movingAverage.Next(5)); // return 6.0 = (10 + 3 + 5) / 3
    }

    public class MovingAverage
    {
        private readonly int _maxSize;
        private readonly Queue<int> _queue;
        private double _sum;

        public MovingAverage(int size)
        {
            _maxSize = size;
            _queue = new Queue<int>();
            _sum = 0;
        }

        public double Next(int val)
        {
            // Если окно заполнено, удаляем старое значение из суммы и очереди
            if (_queue.Count == _maxSize)
            {
                _sum -= _queue.Dequeue();
            }

            // Добавляем новое значение
            _queue.Enqueue(val);
            _sum += val;

            // Возвращаем среднее арифметическое
            return _sum / _queue.Count;
        }
    }

    // работает, но медленно
    public class MyMovingAverage
    {
        private readonly int _size;
        private readonly List<int> _list;

        public MyMovingAverage(int size)
        {
            _size = size;
            _list = new List<int>(size);
        }

        public double Next(int val)
        {
            _list.Add(val);
            var sum = 0;
            var i = 0;
            while (i < _size && _list.Count - i > 0)
            {
                var first = _list[_list.Count - 1 - i];
                sum += first;
                i++;
            }

            var average = (double)sum / i;
            return average;
        }
    }
}
