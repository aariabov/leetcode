namespace Tests.QueueStack.StackLifo;

public class EvalRPNTests
{
    [Theory]
    [InlineData(new[] { "2","1","+","3","*" }, 9)]
    [InlineData(new[] { "4","13","5","/","+" }, 6)]
    [InlineData(new[] { "10","6","9","3","+","-11","*","/","*","17","+","5","+" }, 22)]
    public void Test(string[] tokens, int expected)
    {
        var result = EvalRPN(tokens);
        Assert.Equal(expected, result);
    }
    
    public int EvalRPN(string[] tokens)
    {
        var stack = new Stack<int>();
        foreach (var token in tokens)
        {
            if (token is "+" or "-" or "*" or "/")
            {
                var rightNum = stack.Pop();
                var leftNum = stack.Pop();
                var res = token switch
                {
                    "+" => leftNum + rightNum,
                    "-" => leftNum - rightNum,
                    "*" => leftNum * rightNum,
                    "/" => leftNum / rightNum,
                };
                stack.Push(res);
            }
            else
            {
                var num = int.Parse(token);
                stack.Push(num);
            }
        }

        return stack.Pop();
    }
}