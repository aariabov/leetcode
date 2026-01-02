namespace Tests.Recursion1.RecurrenceRelation;

/// <summary>
/// [Получить n-ую строку в треугольнике Паскаля](https://leetcode.com/explore/learn/card/recursion-i/251/scenario-i-recurrence-relation/3234/)
/// </summary>
public class GetRowTests
{
    [Theory]
    [InlineData(3, new int[] { 1, 3, 3, 1 })]
    [InlineData(0, new int[] { 1 })]
    [InlineData(1, new int[] { 1, 1 })]
    public void Test1(int rowIdx, int[] expected)
    {
        var result = GetRow(rowIdx);
        Assert.Equal(expected, result);
    }

    // Идея: рекурсивно вычисляем предыдущую строку
    public IList<int> GetRow(int rowIndex)
    {
        if (rowIndex == 0)
            return new List<int> { 1 };

        var prev = GetRow(rowIndex - 1);
        var curr = new List<int> { 1 }; // первый элемент

        for (int i = 1; i < prev.Count; i++)
        {
            // сумма элементов из предыдущей строки
            curr.Add(prev[i - 1] + prev[i]);
        }

        curr.Add(1); // последний элемент
        return curr;
    }

    // работает, но медленно
    // идея: рекурсивно вычисляем левый и правый элемент предыдущей строки
    public IList<int> GetRowMy(int rowIndex)
    {
        var dict = new Dictionary<(int level, int idx), int>();
        var res = new int[rowIndex + 1];
        res[0] = 1;
        res[rowIndex] = 1;
        for (int i = 1; i < rowIndex; i++)
        {
            var item = Rec(rowIndex, i);
            res[i] = item;
        }

        return res;

        int Rec(int level, int idx)
        {
            if (idx == 0 || idx == level)
            {
                return 1;
            }
            var left = int.MinValue;
            if (dict.ContainsKey((level - 1, idx - 1)))
            {
                left = dict[(level - 1, idx - 1)];
            }
            else
            {
                left = Rec(level - 1, idx - 1);
                dict.Add((level - 1, idx - 1), left);
            }

            var right = int.MinValue;
            if (dict.ContainsKey((level - 1, idx)))
            {
                right = dict[(level - 1, idx)];
            }
            else
            {
                right = Rec(level - 1, idx);
                dict.Add((level - 1, idx), right);
            }

            return left + right;
        }
    }
}
