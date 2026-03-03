namespace Tests.Recursion2.Conclusion;

/// <summary>
/// [Комбинация символов на телефонных кнопках](https://leetcode.com/explore/learn/card/recursion-ii/507/beyond-recursion/2905/)
/// </summary>
public class LetterCombinationsTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[] { "23", new[] { "ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf" } },
            new object[] { "2", new[] { "a", "b", "c" } },
            new object[] { "22", new[] { "aa", "ab", "ac", "ba", "bb", "bc", "ca", "cb", "cc" } },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(string digits, string[] expected)
    {
        var result = MyLetterCombinations(digits);
        Assert.Equal(expected, result);
        // Assert.Equal(expected.OrderBy(i => i), result.OrderBy(i => i));
    }

    private string[] mapping = { "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };

    public IList<string> LetterCombinations(string digits)
    {
        List<string> result = new List<string>();
        if (string.IsNullOrEmpty(digits))
            return result;

        Backtrack(result, digits, "", 0);
        return result;
    }

    private void Backtrack(List<string> result, string digits, string current, int index)
    {
        // Базовый случай: если длина текущей строки равна длине цифр, мы нашли комбинацию
        if (index == digits.Length)
        {
            result.Add(current);
            return;
        }

        // Получаем буквы для текущей цифры (например, '2' -> "abc")
        string letters = mapping[digits[index] - '0'];
        foreach (char letter in letters)
        {
            // Рекурсивный переход к следующей цифре
            Backtrack(result, digits, current + letter, index + 1);
        }
    }

    // работает и быстро
    public IList<string> MyLetterCombinations(string digits)
    {
        var k = digits.Length;

        var res = new List<string>();
        var comb = new char[k];
        Backtrack(0);
        return res;

        void Backtrack(int idx)
        {
            if (idx == k)
            {
                res.Add(new string(comb));
                return;
            }

            var btnIdx = digits[idx] - '0';
            for (int j = 0; j < chars[btnIdx].Length; j++)
            {
                var chr = chars[btnIdx][j];
                comb[idx] = chr;
                Backtrack(idx + 1);
            }
        }
    }

    private char[][] chars = new char[][]
    {
        [],
        [],
        ['a', 'b', 'c'],
        ['d', 'e', 'f'],
        ['g', 'h', 'i'],
        ['j', 'k', 'l'],
        ['m', 'n', 'o'],
        ['p', 'q', 'r', 's'],
        ['t', 'u', 'v'],
        ['w', 'x', 'y', 'z'],
    };
}
