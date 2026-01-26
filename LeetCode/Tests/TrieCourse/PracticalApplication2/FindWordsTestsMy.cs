namespace Tests.TrieCourse.PracticalApplication2;

/// <summary>
/// [Поиск слов на доске](https://leetcode.com/explore/learn/card/trie/149/practical-application-ii/1056/)
/// </summary>
public class FindWordsTestsMy
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new char[][]
                {
                    ['o', 'a', 'a', 'n'],
                    ['e', 't', 'a', 'e'],
                    ['i', 'h', 'k', 'r'],
                    ['i', 'f', 'l', 'v'],
                },
                new[] { "oath", "pea", "eat", "rain" },
                new[] { "oath", "eat" },
            },
            new object[]
            {
                new char[][] { ['a', 'b'], ['c', 'd'] },
                new[] { "abcb" },
                new string[] { },
            },
            new object[] { new char[][] { ['a', 'a'] }, new[] { "aaa" }, new string[] { } },
            new object[]
            {
                new char[][] { ['a', 'b'], ['a', 'a'] },
                new[] { "aba", "baa", "bab", "aaab", "aaa", "aaaa", "aaba" },
                new string[] { "aba", "aaab", "aaa", "baa", "aaba" },
            },
            new object[]
            {
                new char[][] { ['a', 'b', 'c'], ['a', 'e', 'd'], ['a', 'f', 'g'] },
                new[] { "eaafgdcba", "eaabcdgfa" },
                new string[] { "eaafgdcba", "eaabcdgfa" },
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(char[][] board, string[] words, string[] expected)
    {
        var result = FindWords(board, words);
        Assert.Equal(expected, result);
    }

    // мое решение, работает, но медленно, не проходит большие тесты
    public IList<string> FindWords(char[][] board, string[] words)
    {
        var dict = new Dictionary<(int row, int col), TrieNode>();
        int rows = board.Length;
        int cols = board[0].Length;
        var cellCount = rows * cols;

        CreateNodes(board, rows, cols, dict);
        FillChildren(board, rows, cols, dict);
        var res = new HashSet<string>();
        foreach (var node in dict.Values)
        {
            var root = new TrieNode('-');
            root.Top = node;
            foreach (var word in words)
            {
                if (word.Length <= cellCount && root.FindNode(word))
                {
                    res.Add(word);
                }
            }
        }

        return res.ToList();
    }

    private class TrieNode
    {
        public char Chr;
        public TrieNode? Right;
        public TrieNode? Left;
        public TrieNode? Top;
        public TrieNode? Bottom;

        public TrieNode(char chr)
        {
            Chr = chr;
        }

        public bool FindNode(string str)
        {
            var hash = new HashSet<TrieNode>();
            return Rec(this, 0);

            // стек или рекурсия
            bool Rec(TrieNode node, int i)
            {
                if (i == str.Length)
                {
                    return true;
                }
                var c = str[i];
                var res = false;
                if (node.Top != null && node.Top.Chr == c && hash.Add(node.Top))
                {
                    res = Rec(node.Top, i + 1);
                }
                if (!res && node.Bottom != null && node.Bottom.Chr == c && hash.Add(node.Bottom))
                {
                    res = Rec(node.Bottom, i + 1);
                }
                if (!res && node.Right != null && node.Right.Chr == c && hash.Add(node.Right))
                {
                    res = Rec(node.Right, i + 1);
                }
                if (!res && node.Left != null && node.Left.Chr == c && hash.Add(node.Left))
                {
                    res = Rec(node.Left, i + 1);
                }

                if (!res)
                {
                    hash.Remove(node);
                }

                return res;
            }
        }
    }

    private static void FillChildren(
        char[][] grid,
        int rows,
        int cols,
        Dictionary<(int row, int col), TrieNode> dict
    )
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                var node = dict[(row: row, col: col)];
                if (row < grid.Length - 1)
                {
                    node.Bottom = dict[(row: row + 1, col: col)];
                }

                if (col < grid[0].Length - 1)
                {
                    node.Right = dict[(row: row, col: col + 1)];
                }

                if (col > 0)
                {
                    node.Left = dict[(row: row, col: col - 1)];
                }

                if (row > 0)
                {
                    node.Top = dict[(row: row - 1, col: col)];
                }
            }
        }
    }

    private static void CreateNodes(
        char[][] grid,
        int rows,
        int cols,
        Dictionary<(int row, int col), TrieNode> dict
    )
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                dict.Add((row: row, col: col), new TrieNode(grid[row][col]));
            }
        }
    }
}
