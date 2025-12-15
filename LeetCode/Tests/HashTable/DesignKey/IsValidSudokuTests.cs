namespace Tests.HashTable.DesignKey;

/// <summary>
/// Проверить судоку на валидность https://leetcode.com/explore/learn/card/hash-table/185/hash_table_design_the_key/1126/
/// </summary>
public class IsValidSudokuTests
{
    public static IEnumerable<object[]> MatrixData =>
        new List<object[]>
        {
            new object[]
            {
                new char[][] { ['5','3','.','.','7','.','.','.','.']
                    ,['6','.','.','1','9','5','.','.','.']
                    ,['.','9','8','.','.','.','.','6','.']
                    ,['8','.','.','.','6','.','.','.','3']
                    ,['4','.','.','8','.','3','.','.','1']
                    ,['7','.','.','.','2','.','.','.','6']
                    ,['.','6','.','.','.','.','2','8','.']
                    ,['.','.','.','4','1','9','.','.','5']
                    ,['.','.','.','.','8','.','.','7','9'] },
                true
            },
            new object[]
            {
                new char[][] { ['8','3','.','.','7','.','.','.','.']
                    ,['6','.','.','1','9','5','.','.','.']
                    ,['.','9','8','.','.','.','.','6','.']
                    ,['8','.','.','.','6','.','.','.','3']
                    ,['4','.','.','8','.','3','.','.','1']
                    ,['7','.','.','.','2','.','.','.','6']
                    ,['.','6','.','.','.','.','2','8','.']
                    ,['.','.','.','4','1','9','.','.','5']
                    ,['.','.','.','.','8','.','.','7','9'] },
                false
            }
        };

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void Test(char[][] mat, bool expected)
    {
        var result = IsValidSudoku(mat);
        Assert.Equal(expected, result);
    }
    
    public bool IsValidSudoku(char[][] board)
    {
        var hashSet = new HashSet<char>();
        for (int i = 0; i < board.Length; i++)
        {
            var row = board[i];
            hashSet = new HashSet<char>();
            for (int j = 0; j < row.Length; j++)
            {
                if (row[j] != '.' && !hashSet.Add(row[j]))
                {
                    return false;
                }
            }
            
            hashSet = new HashSet<char>();
            for (int j = 0; j < board.Length; j++)
            {
                if (board[j][i] != '.' && !hashSet.Add(board[j][i]))
                {
                    return false;
                }
            }
        }
         
        for (int k = 0; k < board.Length / 3; k++)
        {
            for (int h = 0; h < board.Length / 3; h++)
            {
                hashSet = new HashSet<char>();
                for (int i = k * 3; i < k * 3 + 3; i++)
                {
                    for (int j = h * 3; j < h * 3 + 3; j++)
                    {
                        if (board[j][i] != '.' && !hashSet.Add(board[j][i]))
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }
}