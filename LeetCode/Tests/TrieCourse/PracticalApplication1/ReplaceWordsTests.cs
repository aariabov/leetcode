namespace Tests.TrieCourse.PracticalApplication1;

/// <summary>
/// [Замена слов на основу](https://leetcode.com/explore/learn/card/trie/148/practical-application-i/1053/)
/// </summary>
public class ReplaceWordsTests
{
    [Theory]
    [InlineData(
        new[] { "cat", "bat", "rat" },
        "the cattle was rattled by the battery",
        "the cat was rat by the bat"
    )]
    [InlineData(new[] { "a", "b", "c" }, "aadsfasf absbs bbab cadsfafs", "a a b c")]
    public void Test1(IList<string> dictionary, string sentence, string expected)
    {
        var result = ReplaceWords(dictionary, sentence);
        Assert.Equal(expected, result);
    }

    public string ReplaceWords(IList<string> dictionary, string sentence)
    {
        var res = new List<string>();
        var trie = new Trie();
        foreach (var item in dictionary)
        {
            trie.Insert(item);
        }

        var words = sentence.Split(' ');
        foreach (var word in words)
        {
            var str = trie.FindStr(word);
            if (str != null)
            {
                res.Add(str);
            }
            else
            {
                res.Add(word);
            }
        }

        return string.Join(' ', res);
    }

    public class Trie
    {
        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
            public bool IsEnd;
        }

        private readonly TrieNode root;

        public Trie()
        {
            root = new TrieNode();
        }

        public void Insert(string word)
        {
            TrieNode node = root;

            foreach (char c in word)
            {
                if (!node.Children.ContainsKey(c))
                {
                    node.Children[c] = new TrieNode();
                }
                node = node.Children[c];
            }

            node.IsEnd = true;
        }

        public string? FindStr(string s)
        {
            TrieNode node = root;

            var arr = s.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                var c = arr[i];
                if (!node.Children.TryGetValue(c, out TrieNode next))
                {
                    return null;
                }

                if (next.IsEnd)
                {
                    return s[0..(i + 1)];
                }
                node = next;
            }

            return null;
        }
    }
}
