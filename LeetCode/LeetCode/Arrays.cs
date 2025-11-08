namespace LeetCode;

public class Arrays
{
    // [1,1,0,1,1,1] - 3
    public static int FindMaxConsecutiveOnes(int[] nums) {
        var max = 0;
        var current = 0;
        foreach (var num in nums)
        {
            if (num == 1)
            {
                current++;
            }
            else
            {
                if (current > max)
                {
                    max = current;
                }
                current = 0;
            }
        }
        if (current > max)
        {
            max = current;
        }
        return max;
    }
}