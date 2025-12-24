namespace Tests.QueueStack.QueueAndBFS;

/// <summary>
/// [Открыть кодовый замок](https://leetcode.com/explore/learn/card/queue-stack/231/practical-application-queue/1375/)
/// </summary>
public class OpenLockTests
{
    [Theory]
    [InlineData(new[] { "0201","0101","0102","1212","2002" }, "0202", 6)]
    [InlineData(new[] { "8888" }, "0009", 1)]
    [InlineData(new[] { "0000" }, "8888", -1)]
    [InlineData(new[] { "8887","8889","8878","8898","8788","8988","7888","9888" }, "8888", -1)]
    public void Test(string[] deadends, string target, int expected)
    {
        var result = OpenLock(deadends, target);
        Assert.Equal(expected, result);
    }
    
    public int OpenLock(string[] deadends, string target)
    {
        HashSet<string> dead = new HashSet<string>(deadends);
        if (dead.Contains("0000"))
        {
            return -1;
        }

        Queue<string> queue = new Queue<string>();
        HashSet<string> visited = new HashSet<string>();

        queue.Enqueue("0000");
        visited.Add("0000");

        int steps = 0;

        while (queue.Count > 0)
        {
            int size = queue.Count;
            for (int i = 0; i < size; i++)
            {
                string current = queue.Dequeue();
                if (current == target)
                {
                    return steps;
                }

                foreach (string next in GetNextStates(current))
                {
                    if (!dead.Contains(next) && !visited.Contains(next))
                    {
                        visited.Add(next);
                        queue.Enqueue(next);
                    }
                }
            }
            steps++;
        }

        return -1;
    }

    private IEnumerable<string> GetNextStates(string state)
    {
        char[] chars = state.ToCharArray();

        for (int i = 0; i < 4; i++)
        {
            char original = chars[i];

            // вращение вперёд
            chars[i] = original == '9' ? '0' : (char)(original + 1);
            yield return new string(chars);

            // вращение назад
            chars[i] = original == '0' ? '9' : (char)(original - 1);
            yield return new string(chars);

            // вернуть обратно
            chars[i] = original;
        }
    }
    
    // моя реализация, логика такая же
    public int OpenLockMy(string[] deadends, string target)
    {
        var res = 0;
        var deadlocks = new HashSet<string>(deadends);
        if (deadlocks.Contains("0000"))
        {
            return -1;
        }
        
        var visited = new HashSet<string>();
        visited.Add("0000");
        var queue = new Queue<string>();
        queue.Enqueue("0000");
        // поиск в ширину (BFS) гарантирует минимальное количество шагов
        while (queue.Count > 0)
        {
            var initQueueSize = queue.Count;
            // обработка ближайших вариантов, на конкретном шаге
            for (int j = 0; j < initQueueSize; j++)
            {
                var cur = queue.Dequeue();
                if (cur == target)
                {
                    return res;
                }
                // каждый из 4х колес можно повернуть вверх или вниз
                for (int i = 0; i < 4; i++)
                {
                    var num = cur[i] - '0';
                    var num1 = num == 9 ? 0 : num + 1;
                    var num2 = num == 0 ? 9 : num - 1;
                    var state1 = cur.ToCharArray();
                    var state2 = cur.ToCharArray();
                    state1[i] = (char)(num1 + '0');
                    state2[i] = (char)(num2 + '0');
                    var str1 = new string(state1);
                    var str2 = new string(state2);
                    if (!deadlocks.Contains(str1) && visited.Add(str1))
                    {
                        queue.Enqueue(str1);
                    }
                    if (!deadlocks.Contains(str2) && visited.Add(str2))
                    {
                        queue.Enqueue(str2);
                    }
                }
            }

            res++;
        }

        return -1;
    }
}