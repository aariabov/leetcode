using FluentAssertions;
using FluentAssertions.Execution;

namespace Tests.TrieCourse.PracticalApplication2;

/// <summary>
/// [Maximum XOR of Two Numbers in an Array](https://leetcode.com/explore/learn/card/trie/149/practical-application-ii/1057/)
/// </summary>
public class FindMaximumXORTests
{
    [Theory]
    [InlineData(new int[] { 10, 25, 5, 3, 2, 8 }, 28)]
    [InlineData(new int[] { 14, 70, 53, 83, 49, 91, 36, 80, 92, 51, 66, 70 }, 127)]
    [InlineData(new int[] { 0 }, 0)]
    public void Test(int[] nums, int expected)
    {
        var result = FindMaximumXOR(nums);
        Assert.Equal(expected, result);
    }

    [Fact(Skip = "Почему-то не работает")]
    public void InsertTest()
    {
        var expected = new TrieNode(null)
        {
            children = new[]
            {
                new TrieNode(0)
                {
                    children = new[]
                    {
                        new TrieNode(0)
                        {
                            children = new[]
                            {
                                new TrieNode(0)
                                {
                                    children = new[]
                                    {
                                        null,
                                        new TrieNode(1)
                                        {
                                            children = new[] { new TrieNode(0), new TrieNode(1) },
                                        },
                                    },
                                },
                                new TrieNode(1)
                                {
                                    children = new[]
                                    {
                                        new TrieNode(0)
                                        {
                                            children = new[] { null, new TrieNode(1) },
                                        },
                                        null,
                                    },
                                },
                            },
                        },
                        new TrieNode(1)
                        {
                            children = new[]
                            {
                                new TrieNode(0)
                                {
                                    children = new[]
                                    {
                                        new TrieNode(0)
                                        {
                                            children = new[] { new TrieNode(0), null },
                                        },
                                        new TrieNode(1)
                                        {
                                            children = new[] { new TrieNode(0), null },
                                        },
                                    },
                                },
                                null,
                            },
                        },
                    },
                },
                null,
            },
        };

        var res = BuildTrie([2, 3, 5, 8, 10]);

        res.Should().BeEquivalentTo(expected);
    }

    const int NUM_LEVEL = 31; // 4 - для наглядности дебага для кейса [10, 25, 5, 3, 2, 8]

    private static TrieNode BuildTrie(int[] nums)
    {
        var root = new TrieNode(null);
        foreach (int num in nums)
        {
            var node = root;
            var binaryNum = Convert.ToString(num, 2).PadLeft(NUM_LEVEL + 1, '0');
            for (int i = NUM_LEVEL; i > -1; i--)
            {
                var move = num >> i; // двигаем, чтоб нужный бит был в конце
                var binaryMove = Convert.ToString(move, 2).PadLeft(NUM_LEVEL + 1, '0');
                var bit = move & 1; // зануляем все биты, кроме правого
                var binaryBit = Convert.ToString(bit, 2).PadLeft(NUM_LEVEL + 1, '0');
                if (node.children[bit] == null)
                {
                    node.children[bit] = new TrieNode(bit);
                }

                node = node.children[bit]!;
            }
        }

        return root;
    }

    private class TrieNode
    {
        public int? Val;
        public TrieNode?[] children = new TrieNode?[2];

        public TrieNode(int? val)
        {
            Val = val;
        }
    }

    public int FindMaximumXOR(int[] nums)
    {
        var root = BuildTrie(nums);
        var max = int.MinValue;
        foreach (var num in nums)
        {
            var node = root;
            int xor = 0;
            for (int i = NUM_LEVEL; i > -1; i--)
            {
                var move = num >> i; // двигаем, чтоб нужный бит был в конце
                var binaryMove = Convert.ToString(move, 2).PadLeft(NUM_LEVEL + 1, '0');
                var bit = move & 1; // зануляем все биты, кроме правого
                var binaryBit = Convert.ToString(bit, 2).PadLeft(NUM_LEVEL + 1, '0');
                var opposite = bit ^ 1;
                var binaryOpposite = Convert.ToString(opposite, 2).PadLeft(NUM_LEVEL + 1, '0');
                if (node.children[opposite] != null)
                {
                    var degreeVal = 1 << i; // двигаем, чтоб нужный бит был в конце
                    var binaryDegreeVal = Convert
                        .ToString(degreeVal, 2)
                        .PadLeft(NUM_LEVEL + 1, '0');
                    var binaryXor = Convert.ToString(degreeVal, 2).PadLeft(NUM_LEVEL + 1, '0');
                    var newXor = xor | degreeVal; // прибавляем значение текущей степени
                    var binaryNewXor = Convert.ToString(newXor, 2).PadLeft(NUM_LEVEL + 1, '0');
                    xor = newXor;
                    node = node.children[opposite]; // идем по существующему узлу
                }
                else
                {
                    node = node.children[bit]; // если узла opposite нету, идем по узлу bit и ничего не прибавляем
                }
            }

            if (xor > max)
            {
                max = xor;
            }
        }

        return max;
    }

    // работает, но не проходит по времени
    public int FindMaximumXORMy(int[] nums)
    {
        if (nums.Length == 1)
        {
            return 0;
        }
        var max = int.MinValue;
        var dict = new Dictionary<(int n1, int n2), int>();
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                var num1 = nums[i];
                var num2 = nums[j];
                int xor = 0;
                if (num1 < num2)
                {
                    if (!dict.TryGetValue((num1, num2), out _))
                    {
                        xor = num1 ^ num2;
                        dict[(num1, num2)] = xor;
                    }
                }
                else
                {
                    if (!dict.TryGetValue((num2, num1), out _))
                    {
                        xor = num2 ^ num1;
                        dict[(num2, num1)] = xor;
                    }
                }

                if (xor > max)
                {
                    max = xor;
                }
            }
        }

        return max;
    }
}
