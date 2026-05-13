namespace Tests.DP.Intro;

// [Удали и заработай](https://leetcode.com/explore/learn/card/dynamic-programming/631/strategy-for-solving-dp-problems/4147/)
public class DeleteAndEarnTests
{
    [Theory]
    [InlineData(new int[] { 3, 4, 2 }, 6)]
    [InlineData(new int[] { 2, 2, 3, 3, 3, 4 }, 9)]
    public void Test(int[] nums, int expected)
    {
        var result = DeleteAndEarn(nums);
        Assert.Equal(expected, result);
    }

    public int DeleteAndEarn(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        int maxVal = nums.Max();
        int[] sum = new int[maxVal + 1];

        // Группируем суммы для каждого числа
        foreach (int num in nums)
        {
            sum[num] += num;
        }

        int prev2 = 0; // Максимум для i - 2
        int prev1 = 0; // Максимум для i - 1

        // Итеративно вычисляем максимальный заработок
        for (int i = 0; i <= maxVal; i++)
        {
            int current = Math.Max(prev1, prev2 + sum[i]);
            prev2 = prev1;
            prev1 = current;
        }

        return prev1;
    }

    public int DeleteAndEarnRec(int[] nums)
    {
        var max = nums.Max();
        var arr = new int[max + 1];
        foreach (var num in nums)
        {
            arr[num] += num;
        }

        var hash = new int?[arr.Length];
        return Dp(max);

        int Dp(int i)
        {
            if (i == 0)
            {
                return arr[0];
            }

            if (i == 1)
            {
                return Math.Max(arr[0], arr[1]);
            }

            if (hash[i].HasValue)
            {
                return hash[i].Value;
            }

            var prev = Dp(i - 1);
            var curr = Dp(i - 2) + arr[i];
            var res = Math.Max(prev, curr);
            hash[i] = res;
            return res;
        }
    }
}
