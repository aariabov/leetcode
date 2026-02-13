namespace Tests.Recursion2.Conclusion;

/// <summary>
/// [Самый большой прямоугольник в гистограмме](https://leetcode.com/explore/learn/card/recursion-ii/507/beyond-recursion/2901/)
/// </summary>
public class LargestRectangleAreaTests
{
    [Theory]
    [InlineData(new int[] { 2, 1, 5, 6, 2, 3 }, 10)]
    [InlineData(new int[] { 2, 4 }, 4)]
    [InlineData(new int[] { 1 }, 1)]
    [InlineData(new int[] { 1, 1 }, 2)]
    [InlineData(new int[] { 4, 2 }, 4)]
    [InlineData(new int[] { 0, 9 }, 9)]
    [InlineData(new int[] { 2, 1, 2 }, 3)]
    public void Test1(int[] heights, int expected)
    {
        var result = LargestRectangleArea(heights);
        Assert.Equal(expected, result);
    }

    // идея: в стеке лежат возрастающие значения и мы может не скакать по одному столбцу, как делал я в изначальном решении
    public int LargestRectangleArea(int[] heights)
    {
        // в стеке будем хранить индексы возрастающих столбцов
        var stack = new Stack<int>();
        var maxArea = 0;
        for (int i = 0; i <= heights.Length; i++)
        {
            var height = i == heights.Length ? 0 : heights[i];
            // если встречаем уменьшение
            while (stack.Count > 0 && height < heights[stack.Peek()])
            {
                var col = stack.Pop();
                var width = stack.Count == 0 ? i : i - stack.Peek() - 1;
                var area = heights[col] * width;
                if (area > maxArea)
                {
                    maxArea = area;
                }
            }
            stack.Push(i);
        }

        return maxArea;
    }

    // правильно, но медленно, падает по времени
    public int LargestRectangleAreaIter1(int[] heights)
    {
        var maxArea = 0;
        for (int i = 0; i < heights.Length; i++)
        {
            var height = heights[i];
            var left = i;
            var right = i;
            while (left - 1 >= 0 && heights[left - 1] >= height)
            {
                left--;
            }
            while (right + 1 < heights.Length && heights[right + 1] >= height)
            {
                right++;
            }
            var len = right - left + 1;
            var area = height * len;
            if (area > maxArea)
            {
                maxArea = area;
            }
        }

        return maxArea;
    }

    // правильно, но медленно, падает по времени
    public int LargestRectangleAreaIter(int[] heights)
    {
        var maxArea = 0;
        for (int i = 1; i <= heights.Length; i++)
        {
            var height = heights[i - 1];
            for (int h = 1; h <= height; h++)
            {
                for (int col = i; col <= heights.Length; col++)
                {
                    if (heights[col - 1] < h)
                    {
                        break;
                    }

                    var len = col - i + 1;
                    var area = h * len;
                    if (area > maxArea)
                    {
                        maxArea = area;
                    }
                }
            }
        }

        return maxArea;
    }
}
