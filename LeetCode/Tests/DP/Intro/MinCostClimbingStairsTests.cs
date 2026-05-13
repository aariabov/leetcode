namespace Tests.DP.Intro;

// [Минимальная стоимость подъема на лестницу](https://leetcode.com/explore/learn/card/dynamic-programming/631/strategy-for-solving-dp-problems/4040/)
public class MinCostClimbingStairsTests
{
    [Theory]
    [InlineData(new int[] { 10, 15, 20 }, 15)]
    [InlineData(new int[] { 1, 100, 1, 1, 1, 100, 1, 1, 100, 1 }, 6)]
    public void Test(int[] nums, int expected)
    {
        var result = MinCostClimbingStairs(nums);
        Assert.Equal(expected, result);
    }

    public int MinCostClimbingStairs(int[] cost)
    {
        var dp = new int[cost.Length + 1];
        for (int i = 2; i < cost.Length + 1; i++)
        {
            var fromPrev = dp[i - 1] + cost[i - 1];
            var fromPrevPrev = dp[i - 2] + cost[i - 2];
            dp[i] = Math.Min(fromPrevPrev, fromPrev);
        }

        return dp[cost.Length];
    }

    public int MinCostClimbingStairsRec(int[] cost)
    {
        var hash = new int?[cost.Length + 1];
        return Dp(cost.Length);

        int Dp(int i)
        {
            if (i == 0 || i == 1)
            {
                return 0;
            }

            if (hash[i].HasValue)
            {
                return hash[i].Value;
            }

            // на текущую ступеньку можно подняться с предыдущей, стоимость = до предыдущей + ее стоимость
            var fromPrev = Dp(i - 1) + cost[i - 1];

            // либо с предпредыдущей, стоимость = до предпредыдущей + ее стоимость
            var fromPrevPrev = Dp(i - 2) + cost[i - 2];
            var result = Math.Min(fromPrevPrev, fromPrev);
            hash[i] = result;
            return result;
        }
    }
}
