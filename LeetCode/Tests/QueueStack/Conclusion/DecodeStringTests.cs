using System.Text;

namespace Tests.QueueStack.Conclusion;

/// <summary>
/// [Декодирование строки](https://leetcode.com/explore/learn/card/queue-stack/239/conclusion/1379/)
/// Очень сложно
/// </summary>
public class DecodeStringTests
{
    [Theory]
    [InlineData("3[a]2[bc]", "aaabcbc")]
    [InlineData("3[a2[c]]", "accaccacc")]
    [InlineData("2[abc]3[cd]ef", "abcabccdcdcdef")]
    [InlineData("abc3[cd]xyz", "abccdcdcdxyz")]
    [InlineData("leetcode", "leetcode")]
    [InlineData("2[2[y]pq4[2[jk]e1[f]]]ef", "yypqjkjkefjkjkefjkjkefjkjkefyypqjkjkefjkjkefjkjkefjkjkefef")]
    [InlineData("10[leetcode]", "leetcodeleetcodeleetcodeleetcodeleetcodeleetcodeleetcodeleetcodeleetcodeleetcode")]
    public void Test(string a, string expected)
    {
        var result = DecodeString(a);
        Assert.Equal(expected, result);
    }
    
    public string DecodeString(string s)
    {
        Stack<int> countStack = new Stack<int>();
        Stack<StringBuilder> stringStack = new Stack<StringBuilder>();

        StringBuilder current = new StringBuilder();
        int k = 0;

        foreach (char c in s)
        {
            if (char.IsDigit(c))
            {
                k = k * 10 + (c - '0');
            }
            else if (c == '[')
            {
                countStack.Push(k);
                stringStack.Push(current);
                current = new StringBuilder();
                k = 0;
            }
            else if (c == ']')
            {
                int repeat = countStack.Pop();
                StringBuilder previous = stringStack.Pop();

                for (int i = 0; i < repeat; i++)
                    previous.Append(current);

                current = previous;
            }
            else
            {
                current.Append(c);
            }
        }

        return current.ToString();
    }

    // почти работает, не разобрался только со сложным примером, скорей всего есть маленький косяк
    public string DecodeStringMy(string s)
    {
        var numStack = new Stack<int>();
        var queue = new Queue<string>();
        var list = new List<string>();
        var sb = new StringBuilder();
        var i = 0;
        while (i < s.Length)
        {
            if (char.IsLetter(s[i]))
            {
                sb.Append(s[i]);
            }

            if (char.IsDigit(s[i]))
            {
                if (numStack.Count == 0)
                {
                    if (sb.Length > 0)
                    {
                        list.Add(sb.ToString());
                    }
                }
                else if(sb.Length > 0)
                {
                    queue.Enqueue(sb.ToString());
                }
                sb.Clear();
                
                var startNum = i;
                while (s[i] != '[')
                {
                    i++;
                }
                var num = int.Parse(s.Substring(startNum, i - startNum));
                numStack.Push(num);
            }

            if (s[i] == ']')
            {
                var num = numStack.Pop();
                var xz = sb.Length > 0 ? sb.ToString() : queue.Dequeue();
                var str = string.Concat(Enumerable.Repeat(xz, num));
                sb.Clear();
                if (queue.Count > 0)
                {
                    while (queue.Count > 0)
                    {
                        sb.Append(queue.Dequeue());
                    }
                    var repStr = sb.ToString();
                    str = repStr + str;
                }
                if (numStack.Count == 0)
                {
                    list.Add(str);
                }
                else
                {
                    queue.Enqueue(str);
                    sb.Clear();
                }
                sb.Clear();
            }

            i++;
        }

        if (sb.Length > 0)
        {
            list.Add(sb.ToString());
        }

        return string.Join("", list);
    }

    public string DecodeString3(string s)
    {
        if (!s.Contains('['))
        {
            return s;
        }
        
        var sb1 = new StringBuilder();
        var stack = new Stack<int>();
        var stackStr = new Stack<string>();
        var i = 0;
        while (i < s.Length)
        {
            if (s[i] == '[')
            {
                stack.Push(i);
            }
            if (s[i] == ']')
            {
                var j = stack.Pop();
                var idx = j;
                var subStr = string.Empty;
                if (stackStr.Count == 0)
                {
                    subStr = s.Substring(j + 1, i - j - 1);
                }
                else
                {
                    var sb = new StringBuilder();
                    var k = j + 1;
                    while (char.IsLetter(s[k]))
                    {
                        sb.Append(s[k]);
                        k++;
                    }

                    sb.Append(stackStr.Pop());
                    subStr = sb.ToString();
                    sb.Clear();
                    var m = i - 1;
                    while (char.IsLetter(s[m]))
                    {
                        sb.Append(s[m]);
                        m--;
                    }

                    if (sb.Length > 0)
                    {
                        subStr += sb.ToString().Reverse();
                    }
                }
                
                while (idx > 0 && char.IsDigit(s[idx-1]))
                {
                    idx--;
                }
                var num = int.Parse(s.Substring(idx, j - idx));
                var strCenter = string.Concat(Enumerable.Repeat(subStr, num));
                if (i > j)
                {
                    stackStr.Push(strCenter);
                }
                else
                {
                    sb1.Append(strCenter);
                }
            }

            i++;
        }

        return sb1.ToString();
    }

    public string DecodeString2(string s)
    {
        var stask = new Stack<int>();
        var list = new List<string>();
        var k = 0;
        var xz = new StringBuilder();
        while (k < s.Length)
        {
            if (s[k] == '[')
            {
                stask.Push(k);
                if (xz.Length > 0)
                {
                    list.Add(xz.ToString());
                    xz.Clear();
                }
            }
            if (s[k] == ']')
            {
                if(stask.Count == 1)
                {
                    var startIdx = stask.Pop();
                    var idx = startIdx;
                    while (idx > 0 && char.IsDigit(s[idx-1]))
                    {
                        idx--;
                    }
                    var subStr = s.Substring(idx, k - idx + 1);
                    list.Add(subStr);
                }
                else
                {
                    stask.Pop();
                }
            }

            if (stask.Count == 0 && char.IsLetter(s[k]))
            {
                xz.Append(s[k]);
            }

            k++;
        }

        if (list.Count == 0 && xz.Length == 0)
        {
            list.Add(s);
        }

        if (xz.Length > 0)
        {
            list.Add(xz.ToString());
        }

        var sb = new StringBuilder();
        foreach (var str in list)
        {
            var res = Rec(str);
            sb.Append(res);
        }
        
        return sb.ToString();
        
        

        string Rec(string str)
        {
            if (!str.Contains('['))
            {
                return str;
            }
            
            var sb = new StringBuilder();
            var i = 0;
            // начало
            while (char.IsLetter(str[i]))
            {
                sb.Append(str[i]);
                i++;
            }
            // число
            var startNumIdx = i;
            while (str[i] != '[')
            {
                i++;
            }
            var num = int.Parse(str.Substring(startNumIdx, i - startNumIdx));
            
            // конец
            var sb1 = new StringBuilder();
            var j = str.Length-1;
            while (char.IsLetter(str[j]))
            {
                sb1.Append(str[j]);
                j--;
            }
            // число
            var subStr = str.Substring(i + 1, j - i - 1);
            var recStr = Rec(subStr);
            var strCenter = string.Concat(Enumerable.Repeat(recStr, num));
            var result = sb.ToString() + strCenter + sb1.ToString();
            return result;
        }
    }
    
    public string DecodeString1(string s)
    {
        var i = 0;
        var stack = new Stack<int>();
        var queue = new Queue<string>();
        var sb1 = new StringBuilder();
        while (i < s.Length)
        {
            if (char.IsDigit(s[i]))
            {
                stack.Push(i);
            }
            else if (s[i] == ']')
            {
                var sb = new StringBuilder();
                var startIdx = stack.Pop();
                var startNumIdx = startIdx;
                while (s[startIdx] != '[')
                {
                    startIdx++;
                }

                var num = int.Parse(s.Substring(startNumIdx, startIdx - startNumIdx));
                startIdx++;
                var startStrIdx = startIdx;
                while (s[startIdx] != ']')
                {
                    startIdx++;
                }
                // подстрока
                // подстрока + выражение
                // выражение + подстрока
                // подстрока + выражение + подстрока

                var strTemplate = s.Substring(startStrIdx, startIdx - startStrIdx);
                var str = string.Concat(Enumerable.Repeat(strTemplate, num));
                sb.Append(str);
                queue.Enqueue(sb.ToString());
            }
            else if(char.IsLetter(s[i]))
            {
                sb1.Append(s[i]);
            }

            i++;
        }

        return string.Join("", queue) + sb1.ToString();
    }
}