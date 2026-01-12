namespace Tests.TrieCourse.BasicOperations;

/// <summary>
/// [Реализовать префиксное дерево](https://leetcode.com/explore/learn/card/trie/147/basic-operations/1047/)
/// </summary>
public class TrieTests
{
    [Fact]
    public void Test2()
    {
        var trie = new Trie();
        trie.Insert("apple");
        Assert.True(trie.Search("apple")); // return True
        Assert.False(trie.Search("app")); // return False
        Assert.True(trie.StartsWith("app")); // return True
        trie.Insert("app");
        Assert.True(trie.Search("app")); // return True
    }

    public class Trie
    {
        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
            public bool IsEnd;
        }

        private readonly TrieNode root;

        /** Initialize your data structure here. */
        public Trie()
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

        /** Returns true if the word is in the trie. */
        public bool Search(string word)
        {
            TrieNode node = FindNode(word);
            return node != null && node.IsEnd;
        }

        /** Returns true if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            return FindNode(prefix) != null;
        }

        private TrieNode FindNode(string s)
        {
            TrieNode node = root;

            foreach (char c in s)
            {
                if (!node.Children.TryGetValue(c, out TrieNode next))
                {
                    return null;
                }
                node = next;
            }

            return node;
        }
    }

    // работает быстро
    public class TrieArr
    {
        private class TrieNode
        {
            public TrieNode[] Children = new TrieNode[26];
            public bool IsEnd;
        }

        private readonly TrieNode root;

        /** Initialize your data structure here. */
        public TrieArr()
        {
            root = new TrieNode();
        }

        /** Inserts a word into the trie. */
        public void Insert(string word)
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

            node.IsEnd = true;
        }

        /** Returns true if the word is in the trie. */
        public bool Search(string word)
        {
            TrieNode node = FindNode(word);
            return node != null && node.IsEnd;
        }

        /** Returns true if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            return FindNode(prefix) != null;
        }

        private TrieNode FindNode(string s)
        {
            TrieNode node = root;

            foreach (char c in s)
            {
                int index = c - 'a';
                if (node.Children[index] == null)
                {
                    return null;
                }
                node = node.Children[index];
            }

            return node;
        }
    }

    // работает, но не оч быстро
    public class TrieMy
    {
        private Dictionary<char, TrieMy> Children = new();
        private char Value;
        private bool isWord = false;

        public TrieMy() { }

        public TrieMy(char val)
        {
            Value = val;
        }

        public void Insert(string word)
        {
            // 1. Initialize: cur = root
            // 2. for each char c in target string S:
            // 3.      if cur does not have a child c:
            // 4.          cur.children[c] = new Trie node
            // 5.      cur = cur.children[c]
            // 6. cur is the node which represents the string S
            var cur = this;
            for (int i = 0; i < word.Length; i++)
            {
                char chr = word[i];
                var node = new TrieMy(chr);
                if (i == word.Length - 1)
                {
                    if (!cur.Children.TryGetValue(chr, out var child))
                    {
                        node.isWord = true;
                        cur.Children.Add(chr, node);
                    }
                    else
                    {
                        child.isWord = true;
                    }
                    cur = cur.Children[chr];
                    continue;
                }

                cur.Children.TryAdd(chr, node);
                cur = cur.Children[chr];
            }
        }

        public bool Search(string word)
        {
            // 1. Initialize: cur = root
            // 2. for each char c in target string S:
            // 3.   if cur does not have a child c:
            // 4.     search fails
            // 5.   cur = cur.children[c]
            // 6. search successes

            return LastExists(word, out var value) && value.isWord;
        }

        public bool StartsWith(string prefix)
        {
            return LastExists(prefix, out _);
        }

        private bool LastExists(string prefix, out TrieMy? value)
        {
            var cur = this;
            foreach (var chr in prefix)
            {
                if (!cur.Children.TryGetValue(chr, out value))
                {
                    return false;
                }

                cur = value;
            }

            value = cur;
            return true;
        }
    }
}
