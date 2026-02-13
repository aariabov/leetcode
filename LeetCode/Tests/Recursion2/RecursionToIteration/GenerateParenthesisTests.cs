using System.Text;

namespace Tests.Recursion2.RecursionToIteration;

/// <summary>
/// [Генерация скобок](https://leetcode.com/explore/learn/card/recursion-ii/503/recursion-to-iteration/2772/)
/// </summary>
public class GenerateParenthesisTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                3,
                new [] { "((()))","(()())","(())()","()(())","()()()" },
            },
            new object[]
            {
                1,
                new [] { "()" },
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(int n, string[] expected)
    {
        var result = GenerateParenthesis(n);
        Assert.Equal(expected, result);
        // Assert.Equal(expected.OrderBy(i => i), result.OrderBy(i => i));
    }
    
    // рекурсия, переписанная на очередь, почему-то работает медленнее рекурсии
    public IList<string> GenerateParenthesis(int n)
    {
        var queue = new Queue<(List<string> result, string current, int open, int close, int max)>();
        var res = new List<string>();
        queue.Enqueue((res, "", 0, 0, n));
        while (queue.Count > 0)
        {
            (List<string> result, string current, int open, int close, int max) =  queue.Dequeue();
            
            // Если строка готова
            if (current.Length == max * 2)
            {
                result.Add(current);
                continue;
            }

            // Добавляем '(' если можно
            if (open < max)
            {
                queue.Enqueue((result, current + "(", open + 1, close, max));
            }

            // Добавляем ')' если можно
            if (close < open)
            {
                queue.Enqueue((result, current + ")", open, close + 1, max));
            }
        }

        return res;
    }
    
    // рекурсия, переписанная на стэк, почему-то работает медленнее рекурсии
    public IList<string> GenerateParenthesisStack(int n)
    {
        var stack = new Stack<(List<string> result, string current, int open, int close, int max)>();
        var res = new List<string>();
        stack.Push((res, "", 0, 0, n));
        while (stack.Count > 0)
        {
            (List<string> result, string current, int open, int close, int max) =  stack.Pop();
            
            // Если строка готова
            if (current.Length == max * 2)
            {
                result.Add(current);
                continue;
            }

            // Добавляем ')' если можно
            if (close < open)
            {
                stack.Push((result, current + ")", open, close + 1, max));
            }

            // Добавляем '(' если можно
            if (open < max)
            {
                stack.Push((result, current + "(", open + 1, close, max));
            }
        }

        return res;
    }
    
    public IList<string> GenerateParenthesisRec(int n)
    {
        List<string> result = new List<string>();
        Backtrack(result, "", 0, 0, n);
        return result;
    }

    // красиво и быстро
    // когда дошли до самого дна рекурсии "((()))", начинаем возвращаться и восстанавливать контекст,
    // и в 51 строке будет "((", и затем пойдем перебираться другие варианты
    private void Backtrack(List<string> result, string current, int open, int close, int max)
    {
        // Если строка готова
        if (current.Length == max * 2)
        {
            result.Add(current);
            return;
        }

        // Добавляем '(' если можно
        if (open < max)
        {
            Backtrack(result, current + "(", open + 1, close, max);
        }

        // Добавляем ')' если можно
        if (close < open)
        {
            Backtrack(result, current + ")", open, close + 1, max);
        }
    }
    
    // работает, но медленно
    public IList<string> MyGenerateParenthesis(int n) {
        
        var result = new List<string>();
        var current = new StringBuilder();
        Backtrack();
        return result;

        void Backtrack()
        {
            if (current.Length == n*2)
            {
                if(IsValid(current.ToString()))
                {
                    result.Add(current.ToString());
                }
                return;
            }

            var parentheses = new[] { '(', ')' };
            for (int i = 0; i < parentheses.Length; i++)
            {
                current.Append(parentheses[i]);
                Backtrack();
                current.Remove(current.Length - 1, 1);
            }
        }

        bool IsValid(string list)
        {
            var stack = new Stack<char>();

            foreach (var c in list)
            {
                if (c == '(')
                {
                    stack.Push(c);
                }
                else
                {
                    if (stack.Count == 0)
                        return false;

                    var top = stack.Pop();

                    if (c == ')' && top != '(')
                    {
                        return false;
                    }
                }
            }

            return stack.Count == 0;
        }
    }
}