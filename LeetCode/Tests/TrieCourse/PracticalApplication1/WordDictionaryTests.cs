namespace Tests.TrieCourse.PracticalApplication1;

/// <summary>
/// [Поиск по шаблону](https://leetcode.com/explore/learn/card/trie/148/practical-application-i/1052/)
/// </summary>
public class WordDictionaryTests
{
    [Fact]
    public void Test()
    {
        var wordDictionary = new WordDictionary();
        wordDictionary.AddWord("bad");
        wordDictionary.AddWord("dad");
        wordDictionary.AddWord("mad");
        Assert.False(wordDictionary.Search("pad")); // return False
        Assert.True(wordDictionary.Search("bad")); // return True
        Assert.True(wordDictionary.Search(".ad")); // return True
        Assert.True(wordDictionary.Search("b..")); // return True
    }

    [Fact]
    public void Test1()
    {
        var wordDictionary = new WordDictionary();
        wordDictionary.AddWord("a");
        wordDictionary.AddWord("a");
        Assert.True(wordDictionary.Search("."));
        Assert.True(wordDictionary.Search("a"));
        Assert.False(wordDictionary.Search("aa"));
        Assert.True(wordDictionary.Search("a"));
        Assert.False(wordDictionary.Search(".a"));
        Assert.False(wordDictionary.Search("a."));
    }

    [Fact]
    public void Test2()
    {
        var wordDictionary = new WordDictionary();
        wordDictionary.AddWord("at");
        wordDictionary.AddWord("and");
        wordDictionary.AddWord("an");
        wordDictionary.AddWord("add");
        Assert.False(wordDictionary.Search("a"));
        Assert.False(wordDictionary.Search(".at"));
        wordDictionary.AddWord("bat");

        Assert.True(wordDictionary.Search(".at"));
        Assert.True(wordDictionary.Search("an."));
        Assert.False(wordDictionary.Search("a.d."));
        Assert.False(wordDictionary.Search("b."));
        Assert.True(wordDictionary.Search("a.d"));
        Assert.False(wordDictionary.Search("."));
    }

    public class WordDictionary
    {
        private class TrieNode
        {
            public TrieNode[] Children = new TrieNode[26];
            public bool IsWord = false;
        }

        private readonly TrieNode root;

        public WordDictionary()
        {
            root = new TrieNode();
        }

        public void AddWord(string word)
        {
            TrieNode node = root;
            foreach (char c in word)
            {
                int index = c - 'a';
                if (node.Children[index] == null)
                {
                    node.Children[index] = new TrieNode();
                }
                node = node.Children[index];
            }
            node.IsWord = true;
        }

        public bool Search(string word)
        {
            return SearchFromNode(word, 0, root);
        }

        // рекурсивное решение, но идея, как у меня
        private bool SearchFromNode(string word, int index, TrieNode node)
        {
            if (node == null)
                return false;

            if (index == word.Length)
                return node.IsWord;

            char c = word[index];

            if (c == '.')
            {
                // Try all possible children
                foreach (TrieNode child in node.Children)
                {
                    if (child != null && SearchFromNode(word, index + 1, child))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return SearchFromNode(word, index + 1, node.Children[c - 'a']);
            }
        }
    }

    // не работает на большом тесте, не стал разбираться
    public class WordDictionaryMy
    {
        private readonly TrieMy _trieMy = new();

        public void AddWord(string word)
        {
            _trieMy.Insert(word);
        }

        public bool Search(string word)
        {
            return _trieMy.Search(word);
        }
    }

    public class TrieMy
    {
        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
            public bool IsEnd;
        }

        private readonly TrieNode root;

        /** Initialize your data structure here. */
        public TrieMy()
        {
            root = new TrieNode();
        }

        /** Inserts a word into the trie. */
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

        public bool Search(string word)
        {
            TrieNode node = FindNode(word);
            return node != null && node.IsEnd;
        }

        private TrieNode? FindNode(string s)
        {
            var arr = s.ToCharArray();

            var stack = new Stack<TrieNode>();
            stack.Push(root);

            var i = 0;
            var node = root;
            while (stack.Count > 0 && i < s.Length)
            {
                var chr = arr[i];
                node = stack.Pop();
                if (node.Children.Count == 0)
                {
                    return null;
                }
                if (chr == '.')
                {
                    foreach (var childNode in node.Children.Values)
                    {
                        stack.Push(childNode);
                    }
                }
                else
                {
                    if (!node.Children.TryGetValue(chr, out TrieNode? next))
                    {
                        return null;
                    }
                    stack.Push(next);
                }

                i++;
            }

            return stack.Count == 0 ? null : stack.Pop();
        }
    }
}
