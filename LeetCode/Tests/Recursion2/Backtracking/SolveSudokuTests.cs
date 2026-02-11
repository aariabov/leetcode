namespace Tests.Recursion2.Backtracking;

/// <summary>
/// [Решить судоку](https://leetcode.com/explore/learn/card/recursion-ii/472/backtracking/2796/)
/// </summary>
public class SolveSudokuTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new char[][]
                {
                    ['5', '3', '.', '.', '7', '.', '.', '.', '.'],
                    ['6', '.', '.', '1', '9', '5', '.', '.', '.'],
                    ['.', '9', '8', '.', '.', '.', '.', '6', '.'],
                    ['8', '.', '.', '.', '6', '.', '.', '.', '3'],
                    ['4', '.', '.', '8', '.', '3', '.', '.', '1'],
                    ['7', '.', '.', '.', '2', '.', '.', '.', '6'],
                    ['.', '6', '.', '.', '.', '.', '2', '8', '.'],
                    ['.', '.', '.', '4', '1', '9', '.', '.', '5'],
                    ['.', '.', '.', '.', '8', '.', '.', '7', '9'],
                },
                new char[][]
                {
                    ['5', '3', '4', '6', '7', '8', '9', '1', '2'],
                    ['6', '7', '2', '1', '9', '5', '3', '4', '8'],
                    ['1', '9', '8', '3', '4', '2', '5', '6', '7'],
                    ['8', '5', '9', '7', '6', '1', '4', '2', '3'],
                    ['4', '2', '6', '8', '5', '3', '7', '9', '1'],
                    ['7', '1', '3', '9', '2', '4', '8', '5', '6'],
                    ['9', '6', '1', '5', '3', '7', '2', '8', '4'],
                    ['2', '8', '7', '4', '1', '9', '6', '3', '5'],
                    ['3', '4', '5', '2', '8', '6', '1', '7', '9'],
                },
            },
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(char[][] board, char[][] expected)
    {
        SolveSudoku(board);
        Assert.Equal(expected, board);
    }

    public void SolveSudoku(char[][] board)
    {
        Solve(board);
    }

    private bool Solve(char[][] board)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row][col] == '.')
                {
                    for (char num = '1'; num <= '9'; num++)
                    {
                        if (IsValid(board, row, col, num))
                        {
                            board[row][col] = num;

                            if (Solve(board))
                                return true;

                            // Backtrack
                            board[row][col] = '.';
                        }
                    }
                    return false;
                }
            }
        }

        return true;
    }

    private bool IsValid(char[][] board, int row, int col, char num)
    {
        for (int i = 0; i < 9; i++)
        {
            // Проверка строки
            if (board[row][i] == num)
                return false;

            // Проверка колонки
            if (board[i][col] == num)
                return false;

            // Проверка блока 3x3
            int boxRow = 3 * (row / 3) + i / 3;
            int boxCol = 3 * (col / 3) + i % 3;

            if (board[boxRow][boxCol] == num)
                return false;
        }

        return true;
    }

    // работает, но много кода и медленно, конверчу числа и char туда сюда
    public void MySolveSudoku(char[][] board)
    {
        var list = new List<(int row, int col)>();
        var rowsDict = new HashSet<int>[9];
        var colsDict = new HashSet<int>[9];
        var squareDict = new HashSet<int>[9];

        for (int i = 0; i < 9; i++)
        {
            rowsDict[i] = new HashSet<int>();
            colsDict[i] = new HashSet<int>();
            squareDict[i] = new HashSet<int>();
        }

        for (int row = 0; row < board.Length; row++)
        {
            for (int col = 0; col < board[0].Length; col++)
            {
                var val = board[row][col];
                if (val == '.')
                {
                    list.Add((row, col));
                }
                else
                {
                    Place(row, col, val - '0');
                }
            }
        }

        var bingo = false;
        Backtrack(0);

        void Backtrack(int cellIdx)
        {
            if (cellIdx > list.Count - 1)
            {
                bingo = true;
                return;
            }

            var (row, col) = list[cellIdx];
            for (int i = 1; i < 10; i++)
            {
                if (!IsValid(row, col, i))
                {
                    continue;
                }

                // ставим
                board[row][col] = (char)('0' + i);
                Place(row, col, i);
                Backtrack(cellIdx + 1);

                if (!bingo)
                {
                    // backtracking
                    board[row][col] = '.';
                    UnPlace(row, col, i);
                }
            }
        }

        bool IsValid(int row, int col, int val)
        {
            if (rowsDict[row].Contains(val))
            {
                return false;
            }

            if (colsDict[col].Contains(val))
            {
                return false;
            }

            var squareId = GetSquareId(row, col);
            if (squareDict[squareId].Contains(val))
            {
                return false;
            }

            return true;
        }

        void UnPlace(int row, int col, int val)
        {
            rowsDict[row].Remove(val);
            colsDict[col].Remove(val);
            var squareId = GetSquareId(row, col);
            squareDict[squareId].Remove(val);
        }

        void Place(int row, int col, int val)
        {
            rowsDict[row].Add(val);
            colsDict[col].Add(val);
            var squareId = GetSquareId(row, col);
            squareDict[squareId].Add(val);
        }

        int GetSquareId(int row, int col)
        {
            return (row / 3) * 3 + (col / 3);
        }
    }
}
