namespace Tests.Graph.DFSInGraph;

// [Восстановить маршрут перелетов](https://leetcode.com/explore/learn/card/graph/619/depth-first-search-in-graph/3901/)
public class FindItineraryTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new string[][] { ["JFK", "B"], ["JFK", "A"], ["B", "JFK"] },
                new string[] { "JFK", "B", "JFK", "A" },
            },
            new object[]
            {
                new string[][] { ["MUC", "LHR"], ["JFK", "MUC"], ["SFO", "SJC"], ["LHR", "SFO"] },
                new string[] { "JFK", "MUC", "LHR", "SFO", "SJC" },
            },
            new object[]
            {
                new string[][]
                {
                    ["JFK", "SFO"],
                    ["JFK", "ATL"],
                    ["SFO", "ATL"],
                    ["ATL", "JFK"],
                    ["ATL", "SFO"],
                },
                new string[] { "JFK", "ATL", "JFK", "SFO", "ATL", "SFO" },
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(IList<IList<string>> tickets, string[] expected)
    {
        var result = FindItinerary(tickets);
        Assert.Equal(expected, result);
    }

    // Алгоритм Иерохольцера: прокладываем маршрут от конца к началу, как только из аэропорта больше нет вылетов, добавляем его в результат
    public IList<string> FindItinerary(IList<IList<string>> tickets)
    {
        // составляем матрицу смежности, вместо того, чтобы использовать visited, посещенные аэропорты можно сразу удалять из матрицы смежности
        var adj = new Dictionary<string, List<string>>();
        foreach (var ticket in tickets)
        {
            if (!adj.TryAdd(ticket[0], [ticket[1]]))
            {
                adj[ticket[0]].Add(ticket[1]);
            }
        }

        foreach (var dests in adj.Values)
        {
            // Сортируем в обратном порядке для удаления с конца, так быстрее
            dests.Sort((a, b) => b.CompareTo(a));
        }

        var res = new List<string> { };
        var stack = new Stack<string>(["JFK"]);
        while (stack.Count > 0)
        {
            string curr = stack.Peek();

            if (adj.ContainsKey(curr) && adj[curr].Count > 0)
            {
                // Если есть куда лететь — берем лексически первый (он в конце списка)
                string next = adj[curr][adj[curr].Count - 1];
                adj[curr].RemoveAt(adj[curr].Count - 1);
                stack.Push(next);
            }
            else
            {
                // Если тупик — записываем в маршрут и "откатываемся"
                res.Add(stack.Pop());
            }
        }

        res.Reverse();
        return res;
    }

    public IList<string> FindItineraryRec(IList<IList<string>> tickets)
    {
        // составляем матрицу смежности, вместо того, чтобы использовать visited, посещенные аэропорты можно сразу удалять из матрицы смежности
        var adj = new Dictionary<string, List<string>>();
        foreach (var ticket in tickets)
        {
            if (!adj.TryAdd(ticket[0], [ticket[1]]))
            {
                adj[ticket[0]].Add(ticket[1]);
            }
        }

        foreach (var dests in adj.Values)
        {
            // Сортируем в обратном порядке для удаления с конца
            dests.Sort((a, b) => b.CompareTo(a));
        }

        var res = new List<string> { };
        VisitRec("JFK");
        res.Reverse();
        return res;

        void VisitRec(string airport)
        {
            // Пока из текущего аэропорта есть доступные рейсы
            while (adj.ContainsKey(airport) && adj[airport].Count > 0)
            {
                // Выбираем лексически первый аэропорт (он в конце списка после Sort)
                string next = adj[airport][adj[airport].Count - 1];
                // удаляем маршрут
                adj[airport].RemoveAt(adj[airport].Count - 1);

                VisitRec(next);
            }
            // Добавляем в маршрут, когда из аеропорта больше нет вылетов
            res.Add(airport);
        }
    }
}
