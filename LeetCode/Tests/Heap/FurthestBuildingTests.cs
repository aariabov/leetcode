namespace Tests.Heap;

// [Паркур по зданиям](https://leetcode.com/explore/learn/card/heap/646/practices/4091/)
public class FurthestBuildingTests
{
    [Theory]
    [InlineData(new int[] { 4, 2, 7, 6, 9, 14, 12 }, 5, 1, 4)]
    [InlineData(new int[] { 4, 12, 2, 7, 3, 18, 20, 3, 19 }, 10, 2, 7)]
    [InlineData(new int[] { 14, 3, 19, 3 }, 17, 0, 3)]
    [InlineData(new int[] { 1, 5, 1, 2, 3, 4, 10000 }, 4, 1, 5)]
    [InlineData(new int[] { 1, 2 }, 0, 0, 0)]
    public void Test1(int[] heights, int bricks, int ladders, int expected)
    {
        var result = FurthestBuilding(heights, bricks, ladders);
        Assert.Equal(expected, result);
    }

    // идея: сначала всегда используем лестницы и запоминаем расстояния (diff)
    // когда лестницы закончились (pq.Count > ladders), пробуем заменить самую низкую лестницу на кирпичи
    public int FurthestBuilding(int[] heights, int bricks, int ladders)
    {
        // Минимальная куча для хранения размеров прыжков, где были использованы лестницы
        PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

        for (int i = 0; i < heights.Length - 1; i++)
        {
            int diff = heights[i + 1] - heights[i];

            // Если следующее здание выше
            if (diff > 0)
            {
                pq.Enqueue(diff, diff);

                // Если количество использованных лестниц превысило лимит
                if (pq.Count > ladders)
                {
                    // Заменяем самую "низкую" лестницу на кирпичи
                    bricks -= pq.Dequeue();
                }

                // Если кирпичи ушли в минус — дальше идти нельзя
                if (bricks < 0)
                {
                    return i;
                }
            }
        }

        return heights.Length - 1;
    }

    // не проходит большие тесты, не стал разбираться
    public int MyFurthestBuilding(int[] heights, int bricks, int ladders)
    {
        var maxHeap = new PriorityQueue<int, int>();
        for (var i = 0; i < heights.Length - 1; i++)
        {
            var diff = heights[i + 1] - heights[i];
            if (diff > 0)
            {
                maxHeap.Enqueue(diff, -diff);
            }
        }

        var result = -1;
        var useMaxDiff = false;
        var maxHeapTemp = new PriorityQueue<int, int>(maxHeap.UnorderedItems);
        while (!useMaxDiff && maxHeapTemp.Count > 0)
        {
            var bricksTemp = bricks;
            var laddersTemp = ladders;
            var maxDiff = maxHeapTemp.Dequeue();
            var i = 0;
            for (i = 0; i < heights.Length - 1; i++)
            {
                var diff = heights[i + 1] - heights[i];
                if (diff <= 0)
                {
                    continue;
                }

                if (laddersTemp == 0 && bricksTemp < diff)
                {
                    result = i;
                    break;
                }

                if (diff == maxDiff && laddersTemp > 0)
                {
                    laddersTemp--;
                    if (laddersTemp == 0)
                    {
                        useMaxDiff = true;
                    }
                    if (maxHeapTemp.Count > 0)
                    {
                        maxDiff = maxHeapTemp.Dequeue();
                    }
                }
                else
                {
                    if (bricksTemp >= diff)
                    {
                        bricksTemp -= diff;
                    }
                    else if (laddersTemp > 0)
                    {
                        laddersTemp--;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (i == heights.Length - 1)
            {
                return i;
            }
        }
        return result;
    }
}
