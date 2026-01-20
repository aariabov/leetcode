namespace Tests.TrieCourse.PracticalApplication1;

/// <summary>
/// [Сумма весов слов](https://leetcode.com/explore/learn/card/trie/148/practical-application-i/1058/)
/// </summary>
public class MapSumTests
{
    [Fact]
    public void Test()
    {
        var mapSum = new MapSum();
        mapSum.Insert("apple", 3);
        Assert.Equal(3, mapSum.Sum("ap")); // return 3 (apple = 3)
        mapSum.Insert("app", 2);
        Assert.Equal(5, mapSum.Sum("ap")); // return 5 (apple + app = 3 + 2 = 5)
        mapSum.Insert("apple", 2);
        Assert.Equal(4, mapSum.Sum("ap")); // return 4 (apple + app = 2 + 2 = 4)
    }

    [Fact]
    public void Test1()
    {
        var mapSum = new MapSum();
        mapSum.Insert("apple", 3);
        Assert.Equal(3, mapSum.Sum("ap")); // return 3 (apple = 3)
        mapSum.Insert("app", 2);
        Assert.Equal(5, mapSum.Sum("ap")); // return 5 (apple + app = 3 + 2 = 5)
    }

    public class MapSum
    {
        private readonly Trie _trie = new();
        private readonly Dictionary<string, int> _dict = new();

        public MapSum() { }

        public void Insert(string key, int val)
        {
            _dict.TryGetValue(key, out var lastVal);
            _trie.Insert(key, val, lastVal);
            _dict[key] = val;
        }

        public int Sum(string prefix)
        {
            return _trie.GetVal(prefix);
        }
    }

    public class Trie
    {
        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children = new();
            public int Val;
            public bool IsEnd;

            public TrieNode(int val)
            {
                Val = val;
            }
        }

        private readonly TrieNode root;

        public Trie()
        {
            root = new TrieNode(0);
        }

        public void Insert(string word, int val, int lastVal)
        {
            TrieNode node = root;

            foreach (char c in word)
            {
                if (!node.Children.ContainsKey(c))
                {
                    node.Children[c] = new TrieNode(val);
                }
                else
                {
                    node.Children[c].Val += val - lastVal;
                }

                node = node.Children[c];
            }

            node.IsEnd = true;
        }

        public int GetVal(string word)
        {
            var node = FindNode(word);
            return node == null ? 0 : node.Val;
        }

        private TrieNode? FindNode(string s)
        {
            TrieNode node = root;

            foreach (char c in s)
            {
                if (!node.Children.TryGetValue(c, out TrieNode? next))
                {
                    return null;
                }
                node = next;
            }

            return node;
        }
    }
}
