namespace Tests.TrieCourse.PracticalApplication2;

/// <summary>
/// [Поиск слов на доске](https://leetcode.com/explore/learn/card/trie/149/practical-application-ii/1056/)
/// </summary>
public class FindWordsTests
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
                new string[] { "aba", "aaa", "aaab", "baa", "aaba" },
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

    private class TrieNode
    {
        public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
        public string? Word = null;
    }

    // Идея решения
    // Строим Trie из списка words
    // Проходим DFS от каждой клетки доски
    // Двигаемся только: вверх / вниз / влево / вправо, не посещаем одну клетку дважды
    // Если дошли до конца слова в Trie → добавляем в ответ
    // Чтобы избежать дубликатов — после нахождения слова обнуляем его в Trie
    public IList<string> FindWords(char[][] board, string[] words)
    {
        TrieNode root = BuildTrie(words);
        HashSet<string> result = new HashSet<string>();

        int rows = board.Length;
        int cols = board[0].Length;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                DFS(board, i, j, root, result);
            }
        }

        return new List<string>(result);
    }

    private void DFS(char[][] board, int row, int col, TrieNode node, HashSet<string> result)
    {
        char c = board[row][col];
        if (c == '#' || !node.Children.ContainsKey(c))
            return;

        node = node.Children[c];

        if (node.Word != null)
        {
            result.Add(node.Word);
            node.Word = null; // избегаем дубликатов
        }

        board[row][col] = '#'; // помечаем как посещённую

        int[] dirs = [0, 1, 0, -1, 0];
        for (int d = 0; d < 4; d++)
        {
            int newRow = row + dirs[d];
            int newCol = col + dirs[d + 1];

            if (newRow >= 0 && newRow < board.Length && newCol >= 0 && newCol < board[0].Length)
            {
                DFS(board, newRow, newCol, node, result);
            }
        }

        board[row][col] = c; // восстанавливаем
    }

    private TrieNode BuildTrie(string[] words)
    {
        TrieNode root = new TrieNode();

        foreach (string word in words)
        {
            TrieNode node = root;
            foreach (char c in word)
            {
                if (!node.Children.ContainsKey(c))
                    node.Children[c] = new TrieNode();

                node = node.Children[c];
            }
            node.Word = word;
        }

        return root;
    }
}
