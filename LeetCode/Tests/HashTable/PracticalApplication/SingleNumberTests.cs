namespace Tests.HashTable.PracticalApplication;

/// <summary>
/// Найти число, у которого нет пары https://leetcode.com/explore/learn/card/hash-table/183/combination-with-other-algorithms/1176/
/// </summary>
public class SingleNumberTests
{
    [Theory]
    [InlineData(new int[] { 2, 2, 1 }, 1)]
    [InlineData(new int[] { 4, 1, 2, 1, 2 }, 4)]
    [InlineData(new int[] { 1 }, 1)]
    public void Test1(int[] nums, int expected)
    {
        var result = SingleNumber(nums);
        Assert.Equal(expected, result);
    }

    // идея в том, что одинаковые числи при XOR дают ноль. Надо числа представить в двоичном виде и сделать XOR
    public int SingleNumber(int[] nums)
    {
        int result = 0;
        foreach (int num in nums)
        {
            result ^= num; // XOR (исключающее или, 1 только когда числа различаются)
        }
        return result;
    }

    public int SingleNumber1(int[] nums)
    {
        var hashSet = new HashSet<int>();
        foreach (var t in nums)
        {
            if (!hashSet.Add(t))
            {
                hashSet.Remove(t);
            }
        }
        return hashSet.First();
    }
}
