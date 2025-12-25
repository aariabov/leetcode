namespace Tests.QueueStack.QueueAndBFS;

/// <summary>
/// Составить число из наименьшего количества квадратов https://leetcode.com/explore/learn/card/queue-stack/231/practical-application-queue/1371/
/// </summary>
public class NumSquaresTests
{
    [Theory]
    [InlineData(12, 3)]
    [InlineData(13, 2)]
    [InlineData(14, 3)]
    [InlineData(16, 1)]
    [InlineData(17, 2)]
    [InlineData(2, 2)]
    [InlineData(43, 3)]
    public void Test(int n, int expected)
    {
        var result = NumSquares(n);
        Assert.Equal(expected, result);
    }
    
    // решение через BFS
    public int NumSquares(int n)
    {
        List<int> squares = new List<int>();
        for (int i = 1; i * i <= n; i++)
            squares.Add(i * i);

        Queue<int> queue = new Queue<int>();
        bool[] visited = new bool[n + 1];

        queue.Enqueue(n);
        visited[n] = true;

        int level = 0;

        while (queue.Count > 0)
        {
            level++;
            int size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                int current = queue.Dequeue();

                foreach (int square in squares)
                {
                    // вычисляем новое состояние
                    int next = current - square;

                    if (next < 0)
                        break;

                    // когда достигли 0, значит нашли самый короткий путь
                    if (next == 0)
                        return level;

                    if (!visited[next])
                    {
                        visited[next] = true;
                        queue.Enqueue(next);
                    }
                }
            }
        }

        return level;
    }
    
    // какое-то замудренное решение, надо разбираться
    public int NumSquares1(int n)
    {
        int[] dp = new int[n + 1];

        // Инициализация максимальным значением
        for (int i = 1; i <= n; i++)
            dp[i] = int.MaxValue;

        dp[0] = 0;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j * j <= i; j++)
            {
                dp[i] = Math.Min(dp[i], dp[i - j * j] + 1);
            }
        }

        return dp[n];
    }

    public int NumSquaresMy(int n)
    {
        // находим корень
        int sqrt = (int)Math.Sqrt(n);

        if (sqrt * sqrt == n)
        {
            return 1;
        }

        var steps = 1;
        var queue = new Queue<int>();
        for (int i = sqrt; i > 0; i--)
        {
            queue.Enqueue(i*i);
        }
        
        // поиск в ширину (BFS) гарантирует минимальное количество шагов
        while (queue.Count > 0)
        {
            var initQueueSize = queue.Count;
            // обработка ближайших вариантов, на конкретном шаге
            for (int j = 0; j < initQueueSize; j++)
            {
                var cur = queue.Dequeue();
                if (cur == n)
                {
                    return steps;
                }

                for (int k = 1; k <= sqrt; k++)
                {
                    var next = cur + k * k;
                    if (next <= n)
                    {
                        queue.Enqueue(next);
                    }
                    else
                    {
                        break;
                    }
                }
                
            }

            steps++;
        }

        return steps;
    }
}