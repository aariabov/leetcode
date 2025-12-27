namespace Tests.QueueStack.StackLifo;

/// <summary>
/// [Дневные температуры](https://leetcode.com/explore/learn/card/queue-stack/230/usage-stack/1363/)
/// </summary>
public class DailyTemperaturesTests
{
    [Theory]
    [InlineData(new int[] { 73,74,75,71,69,72,76,73 }, new int[] { 1,1,4,2,1,1,0,0 })]
    [InlineData(new int[] { 3,4,5,1,0,2,6,3 }, new int[] { 1,1,4,2,1,1,0,0 })]
    [InlineData(new int[] { 30,40,50,60 }, new int[] { 1,1,1,0 })]
    [InlineData(new int[] { 30,60,90 }, new int[] { 1,1,0 })]
    [InlineData(new int[] { 89,62,70,58,47,47,46,76,100,70 }, new int[] { 8,1,5,4,3,2,1,1,0,0 })]
    public void Test(int[] arr, int[] expected)
    {
        var result = DailyTemperatures(arr);
        Assert.Equal(expected, result);
    }
    
    public int[] DailyTemperatures(int[] temperatures)
    {
        // Идея: не сразу вычисляем нужное кол-во шагов для каждого элемента,
        // а записываем их индексы в стек, типа невычисленные элементы,
        // и начинаем вычислять только когда cur больше верхнего элемента стека
        var stack = new Stack<int>();
        var res = new int[temperatures.Length];
        for (int i = 0; i < temperatures.Length; i++)
        {
            var cur = temperatures[i];
            while (stack.Count > 0 && temperatures[stack.Peek()] < cur)
            {
                var idx = stack.Pop();
                var steps = i - idx;
                res[idx] = steps;
            }
            stack.Push(i);
        }

        return res;
    }

    // работает, но падает по времени
    public int[] DailyTemperaturesMy(int[] temperatures)
    {
        var res = new int[temperatures.Length];
        var stack = new Stack<int>();
        var tempStack = new Stack<int>();
        for (int i = temperatures.Length - 1; i > -1; i--)
        {
            var cur = temperatures[i];
            if (stack.Count == 0)
            {
                res[i] = 0;
                stack.Push(cur);
                continue;
            }

            if (stack.Peek() > cur)
            {
                res[i] = 1;
                stack.Push(cur);
                continue;
            }

            var days = 1;
            var item = stack.Pop();
            tempStack.Push(item);
            var isInc = false;
            while (stack.Count > 0 && item <= cur)
            {
                days++;
                item = stack.Pop();
                tempStack.Push(item);
                if (item > cur)
                {
                    isInc = true;
                }
            }

            res[i] = isInc ? days : 0;
            while (tempStack.Count > 0)
            {
                var xz = tempStack.Pop();
                stack.Push(xz);
            }
            stack.Push(cur);
        }

        return res;
    }
        
    
    // работает, но падает по времени
    public int[] DailyTemperaturesIter1(int[] temperatures)
    {
        var res = new int[temperatures.Length];
        for (int i = temperatures.Length-1; i > -1 ; i--)
        {
            var curTemp = temperatures[i];
            var days = 0;
            var j = i;
            var next = curTemp;
            var isInc = false;
            while (j < temperatures.Length - 1 && next <= curTemp)
            {
                days++;
                j++;
                next = temperatures[j];
                if (next > curTemp)
                {
                    isInc = true;
                }
            }

            res[i] = isInc ? days : 0;
        }

        return res;
    }
    
    // работает, но падает по времени
    public int[] DailyTemperaturesIter(int[] temperatures)
    {
        var res = new int[temperatures.Length];
        for (int i = 0; i < temperatures.Length - 1; i++)
        {
            var curTemp = temperatures[i];
            var days = 0;
            var j = i;
            var next = curTemp;
            var isInc = false;
            while (j < temperatures.Length - 1 && next <= curTemp)
            {
                days++;
                j++;
                next = temperatures[j];
                if (next > curTemp)
                {
                    isInc = true;
                }
            }

            res[i] = isInc ? days : 0;
        }

        return res;
    }
}