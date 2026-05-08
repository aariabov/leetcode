namespace Tests.Graph.ShortestPath;

// [Найти самый дешевый перелет](https://leetcode.com/explore/learn/card/graph/622/single-source-shortest-path-algorithm/3866/)
public class FindCheapestPriceTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { 3, new int[][] { [0, 1, 100], [1, 2, 100], [0, 2, 500] }, 0, 2, 0, 500 },
            new object[] { 3, new int[][] { [0, 1, 100], [1, 2, 100], [0, 2, 500] }, 0, 2, 1, 200 },
            new object[]
            {
                4,
                new int[][] { [0, 1, 100], [1, 2, 100], [2, 0, 100], [1, 3, 600], [2, 3, 200] },
                0,
                3,
                1,
                700,
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int n, int[][] flights, int src, int dst, int k, int expected)
    {
        var result = FindCheapestPrice(n, flights, src, dst, k);
        Assert.Equal(expected, result);
    }

    public int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k)
    {
        var distances = new double[n]; // space N
        Array.Fill(distances, double.PositiveInfinity);
        distances[src] = 0;

        // Выполняем релаксацию ребер k + 1 раз
        for (var i = 0; i <= k; i++) // K + 1
        {
            // Создаем копию текущих цен, чтобы на текущей итерации
            // использовать данные только с предыдущей итерации (строго i шагов)
            var tempDistances = (double[])distances.Clone(); // N, space N, но заменяется на каждой итерации

            foreach (var flight in flights) // E (edges)
            {
                var from = flight[0]; // space O(1), можно не учитывать
                var to = flight[1];
                var price = flight[2];

                if (double.IsPositiveInfinity(distances[from]))
                {
                    continue;
                }

                if (distances[from] + price < tempDistances[to])
                {
                    tempDistances[to] = distances[from] + price;
                }
            }
            distances = tempDistances;
        }

        // временная сложность: (K + 1) * (N + E), если упрощать, то K*E, т.к N обычно меньше, чем E
        // пространственная сложность 2N или N
        return double.IsPositiveInfinity(distances[dst]) ? -1 : (int)distances[dst];
    }

    public int FindCheapestPriceBSF(int n, int[][] flights, int src, int dst, int k)
    {
        // 1. Создаем список смежности: город -> список (сосед, цена)
        List<(int to, int price)>[] adj = new List<(int, int)>[n]; // space N
        for (int i = 0; i < n; i++)
            adj[i] = new List<(int, int)>();
        foreach (var f in flights) // E
        {
            adj[f[0]].Add((f[1], f[2]));
        }

        // 2. Массив для хранения минимальной стоимости достижения каждого города
        int[] minPrices = new int[n]; // space N
        Array.Fill(minPrices, int.MaxValue); // N
        minPrices[src] = 0;

        // 3. Очередь для BFS: (текущий_город, текущая_стоимость)
        Queue<(int city, int cost)> queue = new Queue<(int, int)>();
        queue.Enqueue((src, 0));

        int stops = 0;
        // Ограничиваем цикл количеством остановок k
        while (queue.Count > 0 && stops <= k) // K, max space E*K
        {
            int size = queue.Count;
            for (int i = 0; i < size; i++)
            {
                var (currCity, currCost) = queue.Dequeue();

                foreach (var neighbor in adj[currCity]) // max E
                {
                    int nextCity = neighbor.to;
                    int nextCost = currCost + neighbor.price;

                    // Оптимизация: идем дальше, только если нашли путь дешевле
                    if (nextCost < minPrices[nextCity])
                    {
                        minPrices[nextCity] = nextCost;
                        queue.Enqueue((nextCity, nextCost));
                    }
                }
            }
            stops++;
        }

        // временная сложность: E + N + K * E, если упрощать, то K*E
        // пространственная сложность 2N + E*K
        return minPrices[dst] == int.MaxValue ? -1 : minPrices[dst];
    }

    public int FindCheapestPriceDijkstra(int n, int[][] flights, int src, int dst, int k)
    {
        // 1. Построение графа
        var adj = new List<(int to, int price)>[n];
        for (int i = 0; i < n; i++)
            adj[i] = new List<(int, int)>();
        foreach (var f in flights)
            adj[f[0]].Add((f[1], f[2]));

        // 2. Массив для хранения минимального количества остановок до города
        // Это важно: мы заходим в город снова, только если нашли путь с меньшим числом остановок
        int[] stopsToCity = new int[n];
        Array.Fill(stopsToCity, int.MaxValue);

        // 3. Приоритетная очередь: (стоимость, текущий_город, количество_остановок)
        // В C# PriorityQueue хранит (элемент, приоритет). В нашем случае приоритет — это стоимость.
        var pq = new PriorityQueue<(int city, int stops), int>();
        pq.Enqueue((src, 0), 0);

        while (pq.Count > 0)
        {
            pq.TryDequeue(out var current, out int cost);
            int currCity = current.city;
            int currStops = current.stops;

            // Если достигли цели — это гарантированно самый дешевый путь (с учетом k)
            if (currCity == dst)
                return cost;

            // Если лимит остановок исчерпан или мы уже посещали этот город
            // с меньшим или таким же количеством остановок — пропускаем
            if (currStops > k || currStops >= stopsToCity[currCity])
                continue;

            stopsToCity[currCity] = currStops;

            foreach (var neighbor in adj[currCity])
            {
                pq.Enqueue((neighbor.to, currStops + 1), cost + neighbor.price);
            }
        }

        return -1;
    }
}
