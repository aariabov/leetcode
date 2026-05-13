namespace Tests.DP.Intro;

// [Максимальное сумма произведений](https://leetcode.com/explore/learn/card/dynamic-programming/631/strategy-for-solving-dp-problems/4146/)
public class MaximumScoreTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3 }, new int[] { 3, 2, 1 }, 14)]
    [InlineData(new int[] { -5, -3, -3, -2, 7, 1 }, new int[] { -10, -5, 3, 4, 6 }, 102)]
    public void Test1(int[] nums1, int[] nums2, int expected)
    {
        var result = MaximumScore(nums1, nums2);
        Assert.Equal(expected, result);
    }

    public int MaximumScore(int[] nums, int[] multipliers)
    {
        var n = nums.Length;
        var m = multipliers.Length;
        int[][] dp = new int[m + 1][];
        for (int i = 0; i < dp.Length; i++)
        {
            dp[i] = new int[m + 1];
        }

        for (int i = m - 1; i >= 0; i--)
        {
            for (int left = i; left >= 0; left--)
            {
                var mult = multipliers[i];
                var right = n - 1 - (i - left);

                // если берем элемент с начала
                var leftDp = dp[i + 1][left + 1];
                var leftSum = mult * nums[left] + leftDp;

                // если берем элемент с конца
                var rightDp = dp[i + 1][left];
                var rightSum = mult * nums[right] + rightDp;

                dp[i][left] = Math.Max(leftSum, rightSum);
            }
        }

        return dp[0][0];
    }

    public int MaximumScoreRec(int[] nums, int[] multipliers)
    {
        var dict = new Dictionary<(int i, int left), int>();
        var res = Dp(0, 0);
        return res;

        int Dp(int i, int left)
        {
            if (i == multipliers.Length)
            {
                return 0;
            }

            if (dict.TryGetValue((i, left), out var value))
            {
                return value;
            }

            var rigth = nums.Length - 1 - (i - left);
            var mult = multipliers[i];

            // если берем элемент с начала
            var leftDp = Dp(i + 1, left + 1);
            var leftSum = mult * nums[left] + leftDp;

            // если берем элемент с конца
            var rightDp = Dp(i + 1, left);
            var rightSum = mult * nums[rigth] + rightDp;

            var result = Math.Max(leftSum, rightSum);
            dict.Add((i, left), result);
            return result;
        }
    }
}
