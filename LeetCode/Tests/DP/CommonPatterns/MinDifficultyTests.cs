namespace Tests.DP.CommonPatterns;

// [Минимальная сложность задач](https://leetcode.com/explore/learn/card/dynamic-programming/632/common-patterns-in-dp-problems/4110/)
public class MinDifficultyTests
{
    [Theory]
    [InlineData(new int[] { 6, 5, 10, 3, 2, 1 }, 3, 13)]
    [InlineData(new int[] { 6, 5, 4, 3, 2, 1 }, 2, 7)]
    [InlineData(new int[] { 9, 9, 9 }, 4, -1)]
    [InlineData(new int[] { 1, 1, 1 }, 3, 3)]
    public void Test(int[] nums, int d, int expected)
    {
        var result = MinDifficulty(nums, d);
        Assert.Equal(expected, result);
    }

    public int MinDifficulty(int[] jobDifficulty, int d)
    {
        int n = jobDifficulty.Length;

        // Если задач меньше, чем дней, составить расписание невозможно
        if (n < d)
            return -1;

        // dp[i, j] — минимальная сложность, чтобы распределить задачи,
        // начиная с j-й задачи, на i оставшихся дней.
        // Размерность: (d + 1) дней на (n + 1) задач для удобства границ.
        int[,] dp = new int[d + 1, n + 1];

        // Инициализируем таблицу "бесконечностью"
        for (int i = 0; i <= d; i++)
        {
            for (int j = 0; j <= n; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }

        // Базовый случай: для 1 дня и i-й стартовой задачи
        // сложность — это просто максимум от i до конца массива
        int currentMax = 0;
        for (int i = n - 1; i >= 0; i--)
        {
            currentMax = Math.Max(currentMax, jobDifficulty[i]);
            dp[1, i] = currentMax;
        }

        // Заполняем таблицу для дней от 2 до d
        for (int day = 2; day <= d; day++)
        {
            // Стартовый индекс задачи `i` для текущего дня.
            // Нужно оставить хотя бы (day - 1) задач для прошлых дней,
            // поэтому i идет от 0 до n - day
            for (int i = 0; i <= n - day; i++)
            {
                int maxDifficulty = 0;

                // Перебираем, где закончить текущий день (индекс j).
                // j — это индекс последней задачи в текущем дне.
                for (int j = i; j <= n - day; j++)
                {
                    maxDifficulty = Math.Max(maxDifficulty, jobDifficulty[j]);

                    // Если последующее состояние валидно (не бесконечность)
                    if (dp[day - 1, j + 1] != int.MaxValue)
                    {
                        dp[day, i] = Math.Min(dp[day, i], maxDifficulty + dp[day - 1, j + 1]);
                    }
                }
            }
        }

        // Ответ: минимальная сложность для d дней, начиная с 0-й задачи
        return dp[d, 0];
    }
}
