namespace Tests.BitManipulation.StateCompression;

// [Расположить студентов на экзамене](https://leetcode.com/explore/learn/card/bit-manipulation/671/bit-manipulation-state-compression/4270/)
public class MaxStudentsTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new char[][]
                {
                    ['#', '.', '#', '#', '.', '#'],
                    ['.', '#', '#', '#', '#', '.'],
                    ['#', '.', '#', '#', '.', '#'],
                },
                4,
            },
            new object[]
            {
                new char[][] { ['.', '#'], ['#', '#'], ['#', '.'], ['#', '#'], ['.', '#'] },
                3,
            },
            new object[]
            {
                new char[][]
                {
                    ['#', '.', '.', '.', '#'],
                    ['.', '#', '.', '#', '.'],
                    ['.', '.', '#', '.', '.'],
                    ['.', '#', '.', '#', '.'],
                    ['#', '.', '.', '.', '#'],
                },
                10,
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(char[][] mat, int expected)
    {
        var result = MaxStudents(mat);
        Assert.Equal(expected, result);
    }

    public int MaxStudents(char[][] seats)
    {
        int rows = seats.Length;
        int cols = seats[0].Length;

        // Шаг 1: Представление сломанных сидений в виде битовой маски для каждого ряда
        int[] validityMasks = new int[rows];
        for (int i = 0; i < rows; i++)
        {
            int mask = 0;
            for (int j = 0; j < cols; j++)
            {
                if (seats[i][j] == '.')
                {
                    mask |= (1 << j); // 1 означает, что место исправно
                }
            }
            validityMasks[i] = mask;
        }

        // Подготовка списка всех возможных корректных масок рассадки внутри ОДНОГО ряда
        // (студенты не должны сидеть на соседних креслах в одном ряду)
        List<int> rowPatterns = new List<int>();
        int totalStates = 1 << cols;
        for (int mask = 0; mask < totalStates; mask++)
        {
            if ((mask & (mask << 1)) == 0)
            {
                rowPatterns.Add(mask);
            }
        }

        // dp[mask] хранит максимальное число студентов для текущего ряда при заданной маске
        int[] dp = new int[totalStates];
        Array.Fill(dp, -1);
        dp[0] = 0; // Базовое состояние перед обработкой рядов

        // Шаг 2: Построчный расчет динамического программирования
        for (int i = 0; i < rows; i++)
        {
            int[] nextDp = new int[totalStates];
            Array.Fill(nextDp, -1);
            int validSeats = validityMasks[i];

            foreach (int currMask in rowPatterns)
            {
                // Студенты могут садиться только на исправные стулья
                if ((currMask & validSeats) != currMask)
                    continue;

                int studentCount = IntegerCountBits(currMask);

                // Сравниваем с масками предыдущего ряда
                for (int prevMask = 0; prevMask < totalStates; prevMask++)
                {
                    if (dp[prevMask] == -1)
                        continue;

                    // Проверка подглядывания по диагонали:
                    // Текущий студент слева вверху: (currMask & (prevMask << 1))
                    // Текущий студент справа вверху: (currMask & (prevMask >> 1))
                    if ((currMask & (prevMask << 1)) == 0 && (currMask & (prevMask >> 1)) == 0)
                    {
                        nextDp[currMask] = Math.Max(nextDp[currMask], dp[prevMask] + studentCount);
                    }
                }
            }
            dp = nextDp;
        }

        // Нахождение максимума среди всех финальных состояний последнего ряда
        int maxStudents = 0;
        foreach (int score in dp)
        {
            maxStudents = Math.Max(maxStudents, score);
        }

        return maxStudents;
    }

    // Вспомогательный метод для подсчета установленных битов (количества студентов)
    private int IntegerCountBits(int mask)
    {
        int count = 0;
        while (mask > 0)
        {
            count += (mask & 1);
            mask >>= 1;
        }
        return count;
    }
}
