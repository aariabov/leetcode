namespace Tests.TrieCourse.PracticalApplication2;

/// <summary>
/// [Найти палиндромы](https://leetcode.com/explore/learn/card/trie/149/practical-application-ii/1138/)
/// </summary>
public class PalindromePairsTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new[] { "abcd", "dcba", "lls", "s", "sssll" },
                new int[][] { [0, 1], [1, 0], [2, 4], [3, 2] },
            },
            new object[] { new[] { "bat", "tab", "cat" }, new int[][] { [0, 1], [1, 0] } },
            new object[] { new[] { "a", "" }, new int[][] { [0, 1], [1, 0] } },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(string[] words, int[][] expected)
    {
        var result = PalindromePairs(words);
        Assert.Equal(expected, result);
    }

    private class TrieNode
    {
        public TrieNode[] Children = new TrieNode[26];
        public int Index = -1;
        public List<int> PalindromeSuffixes = new List<int>();
    }

    private TrieNode root = new TrieNode();

    // не разобрался, как работает, что-то замудреное
    public IList<IList<int>> PalindromePairs(string[] words)
    {
        var result = new List<IList<int>>();

        // 1. Добавляем слова в Trie (в перевёрнутом виде)
        for (int i = 0; i < words.Length; i++)
        {
            AddWord(words[i], i);
        }

        // 2. Ищем палиндромные пары
        for (int i = 0; i < words.Length; i++)
        {
            Search(words[i], i, result);
        }

        return result;
    }

    private void AddWord(string word, int index)
    {
        TrieNode node = root;

        for (int i = word.Length - 1; i >= 0; i--)
        {
            if (IsPalindrome(word, 0, i))
                node.PalindromeSuffixes.Add(index);

            int c = word[i] - 'a';
            if (node.Children[c] == null)
                node.Children[c] = new TrieNode();

            node = node.Children[c];
        }

        node.Index = index;
        node.PalindromeSuffixes.Add(index);
    }

    private void Search(string word, int index, List<IList<int>> result)
    {
        TrieNode node = root;

        for (int i = 0; i < word.Length; i++)
        {
            // Case 1
            if (node.Index >= 0 && node.Index != index && IsPalindrome(word, i, word.Length - 1))
            {
                result.Add(new List<int> { index, node.Index });
            }

            int c = word[i] - 'a';
            if (node.Children[c] == null)
                return;

            node = node.Children[c];
        }

        // Case 2
        foreach (int j in node.PalindromeSuffixes)
        {
            if (j != index)
                result.Add(new List<int> { index, j });
        }
    }

    private bool IsPalindrome(string s, int left, int right)
    {
        while (left < right)
        {
            if (s[left++] != s[right--])
                return false;
        }
        return true;
    }

    // работает, но не проходит один тест по времени
    public IList<IList<int>> PalindromePairsMy(string[] words)
    {
        var res = new List<IList<int>>();
        for (int i = 0; i < words.Length; i++)
        {
            for (int j = i + 1; j < words.Length; j++)
            {
                var word1 = words[i];
                var word2 = words[j];

                if (isPalindrome(word1 + word2))
                {
                    res.Add([i, j]);
                }

                if (isPalindrome(word2 + word1))
                {
                    res.Add([j, i]);
                }
            }
        }

        return res;

        bool isPalindrome(string str)
        {
            var i = 0;
            var j = str.Length - 1;
            while (i < j)
            {
                if (str[i] != str[j])
                {
                    return false;
                }

                i++;
                j--;
            }

            return true;
        }
    }
}
