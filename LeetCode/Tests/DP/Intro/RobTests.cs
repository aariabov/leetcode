namespace Tests.DP.Intro;

// [Ограбление домов](https://leetcode.com/explore/learn/card/dynamic-programming/631/strategy-for-solving-dp-problems/4148/)
public class RobTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3, 1 }, 4)]
    [InlineData(new int[] { 2, 7, 9, 3, 1 }, 12)]
    [InlineData(new int[] { 0 }, 0)]
    public void Test(int[] nums, int expected)
    {
        var result = Rob(nums);
        Assert.Equal(expected, result);
    }

    public int Rob(int[] nums)
    {
        if (nums.Length == 1)
        {
            return nums[0];
        }

        var dp = new int[nums.Length];
        dp[0] = nums[0];
        dp[1] = Math.Max(nums[0], nums[1]);

        for (var i = 2; i < nums.Length; i++)
        {
            dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i]);
        }
        return dp[nums.Length - 1];
    }

    public int RobRec(int[] nums)
    {
        var hash = new int?[nums.Length];
        return Dp(nums.Length - 1);

        int Dp(int i)
        {
            if (hash[i] != null)
            {
                return hash[i].Value;
            }

            if (i == 0)
            {
                hash[i] = nums[0];
                return nums[0];
            }

            if (i == 1)
            {
                hash[i] = Math.Max(nums[0], nums[1]);
                return Math.Max(nums[0], nums[1]);
            }

            var prevPrev = Dp(i - 2);
            var prev = Dp(i - 1);
            var result = Math.Max(prevPrev + nums[i], prev);
            hash[i] = result;
            return result;
        }
    }
}
