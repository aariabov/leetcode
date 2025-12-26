namespace Tests.QueueStack.StackLifo;

/// <summary>
/// [Проверить валидность скобок](https://leetcode.com/explore/learn/card/queue-stack/230/usage-stack/1361/)
/// </summary>
public class ValidParenthesesTests
{
    [Theory]
    [InlineData("()", true)]
    [InlineData("()[]{}", true)]
    [InlineData("(]", false)]
    [InlineData("([])", true)]
    [InlineData("([)]", false)]
    [InlineData("]", false)]
    public void Test(string a, bool expected)
    {
        var result = IsValid(a);
        Assert.Equal(expected, result);
    }
    
    public bool IsValid(string s)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char c in s)
        {
            if (c == '(' || c == '{' || c == '[')
            {
                stack.Push(c);
            }
            else
            {
                if (stack.Count == 0)
                    return false;

                char top = stack.Pop();

                if ((c == ')' && top != '(') ||
                    (c == '}' && top != '{') ||
                    (c == ']' && top != '['))
                {
                    return false;
                }
            }
        }

        return stack.Count == 0;
    }
    
    public bool IsValidMy(string s)
    {
        var stack = new Stack<char>();
        var dict = new Dictionary<char, char>
        {
            {'(', ')'},
            {'[', ']'},
            {'{', '}'}
        };
        foreach (var chr in s)
        {
            if (dict.ContainsKey(chr))
            {
                stack.Push(chr);
            }
            else
            {
                if (stack.Count == 0)
                {
                    return false;
                }
                
                var open = stack.Pop();
                if (dict[open] != chr)
                {
                    return false;
                }
            }
        }

        return stack.Count == 0;
    }
}