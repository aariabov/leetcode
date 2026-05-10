using System.Text;

namespace Tests.Graph.Kahn;

// [Инопланетный словарь](https://leetcode.com/explore/learn/card/graph/623/kahns-algorithm-for-topological-sorting/3909/)
public class AlienOrderTests
{
    [Theory]
    [InlineData(new[] { "wrt", "wrf", "er", "ett", "rftt" }, "wertf")]
    [InlineData(new[] { "z", "x" }, "zx")]
    [InlineData(new[] { "z", "x", "z" }, "")]
    [InlineData(new[] { "z", "z" }, "z")]
    [InlineData(new[] { "ab", "adc" }, "abcd")]
    public void Test(string[] a, string expected)
    {
        var result = AlienOrder(a);
        Assert.Equal(expected, result);
    }

    public string AlienOrder(string[] words)
    {
        // 1. Создаем граф (список смежности) и счетчик входящих ребер (indegree)
        Dictionary<char, HashSet<char>> adj = new Dictionary<char, HashSet<char>>();
        Dictionary<char, int> indegree = new Dictionary<char, int>();

        // Инициализируем структуры для всех уникальных букв
        foreach (string word in words)
        {
            foreach (char c in word)
            {
                if (!indegree.ContainsKey(c))
                {
                    indegree[c] = 0;
                    adj[c] = new HashSet<char>();
                }
            }
        }

        // 2. Находим зависимости между буквами
        for (int i = 0; i < words.Length - 1; i++)
        {
            string w1 = words[i];
            string w2 = words[i + 1];

            // Проверка на ошибку: если w2 — префикс w1, но w1 длиннее (напр. "abc", "ab")
            if (w1.Length > w2.Length && w1.StartsWith(w2))
                return "";

            int len = Math.Min(w1.Length, w2.Length);
            for (int j = 0; j < len; j++)
            {
                if (w1[j] != w2[j])
                {
                    // Если ребра w1[j] -> w2[j] еще нет, добавляем его
                    if (!adj[w1[j]].Contains(w2[j]))
                    {
                        adj[w1[j]].Add(w2[j]);
                        indegree[w2[j]]++;
                    }
                    break; // Остальные буквы в словах не дают инфо о порядке
                }
            }
        }

        // 3. Топологическая сортировка (алгоритм Кана через BFS)
        Queue<char> queue = new Queue<char>();
        foreach (var pair in indegree)
        {
            if (pair.Value == 0)
                queue.Enqueue(pair.Key);
        }

        StringBuilder sb = new StringBuilder();
        while (queue.Count > 0)
        {
            char current = queue.Dequeue();
            sb.Append(current);

            foreach (char neighbor in adj[current])
            {
                indegree[neighbor]--;
                if (indegree[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        // 4. Если в итоговой строке меньше букв, чем в словаре — значит есть цикл
        return sb.Length == indegree.Count ? sb.ToString() : "";
    }
}
