namespace Tests.DP.CommonPatterns;

// [Собрать сумму из монет](https://leetcode.com/explore/learn/card/dynamic-programming/632/common-patterns-in-dp-problems/4111/)
public class CoinChangeTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 5 }, 11, 3)]
    [InlineData(new int[] { 2 }, 3, -1)]
    [InlineData(new int[] { 1 }, 0, 0)]
    public void Test(int[] nums, int d, int expected)
    {
        var result = CoinChange(nums, d);
        Assert.Equal(expected, result);
    }

    public int CoinChange(int[] coins, int amount)
    {
        // Создаем массив для хранения результатов для каждой суммы от 0 до amount
        int[] dp = new int[amount + 1];

        // Заполняем массив "бесконечностью" (amount + 1 точно больше любого возможного ответа)
        Array.Fill(dp, amount + 1);

        // Базовый случай: для суммы 0 нужно 0 монет
        dp[0] = 0;

        // Перебираем все суммы от 1 до amount
        for (int i = 1; i <= amount; i++)
        {
            // Проверяем каждую монету
            foreach (int coin in coins)
            {
                // Если монету можно использовать для текущей суммы
                if (i >= coin)
                {
                    dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
                }
            }
        }

        // Если значение не изменилось, значит сумму собрать нельзя
        return dp[amount] > amount ? -1 : dp[amount];
    }

    // работает, как и идеальное
    public int CoinChangeMy(int[] coins, int amount)
    {
        var dp = new int[amount + 1];
        var bigVal = int.MaxValue - 10;
        Array.Fill(dp, bigVal);
        dp[0] = 0;

        for (int i = 1; i < amount + 1; i++)
        {
            foreach (var coin in coins)
            {
                if (coin <= i)
                {
                    if (dp[i - coin] + 1 < dp[i])
                    {
                        dp[i] = dp[i - coin] + 1;
                    }
                }
            }
        }
        return dp[amount] == bigVal ? -1 : dp[amount];
    }

    // падает по времени
    public int CoinChangeIter(int[] coins, int amount)
    {
        var result = int.MaxValue;

        // Стек хранит пары: (текущий остаток суммы, количество использованных монет)
        var stack = new Stack<(int Current, int Count)>();

        // Инициализация стартовым состоянием
        stack.Push((amount, 0));

        while (stack.Count > 0)
        {
            var (current, count) = stack.Pop();

            // Базовый случай 1: сумма ушла в минус
            if (current < 0)
            {
                continue;
            }

            // Базовый случай 2: сумма успешно набрана
            if (current == 0)
            {
                if (count < result)
                {
                    result = count;
                }
                continue;
            }

            // Оптимизация: если текущий шаг уже хуже или равен лучшему результату, пропускаем ветку
            if (count >= result - 1)
            {
                continue;
            }

            // Помещаем в стек все возможные переходы
            foreach (var coin in coins)
            {
                stack.Push((current - coin, count + 1));
            }
        }

        return result == int.MaxValue ? -1 : result;
    }

    // падает по времени
    public int CoinChangeRec(int[] coins, int amount)
    {
        var memo = new int[amount + 1];
        Array.Fill(memo, int.MaxValue);
        var result = int.MaxValue;
        Rec(amount, 0);
        return result == int.MaxValue ? -1 : result;

        void Rec(int current, int count)
        {
            if (current < 0)
            {
                return;
            }

            if (memo[current] < count)
            {
                return;
            }

            if (current == 0)
            {
                if (count < result)
                {
                    result = count;
                }
                return;
            }

            memo[current] = count;

            foreach (var coin in coins)
            {
                Rec(current - coin, count + 1);
            }
        }
    }
}
