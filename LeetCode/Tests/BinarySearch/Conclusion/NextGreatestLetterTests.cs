namespace Tests.BinarySearch.Conclusion;

// [Найти следующий символ](https://leetcode.com/explore/learn/card/binary-search/137/conclusion/977/)
public class NextGreatestLetterTests
{
    [Theory]
    [InlineData(new char[] { 'c', 'f', 'j' }, 'a', 'c')]
    [InlineData(new char[] { 'c', 'f', 'j' }, 'c', 'f')]
    [InlineData(new char[] { 'c', 'f', 'j' }, 'j', 'c')]
    [InlineData(new char[] { 'x', 'x', 'y', 'y' }, 'z', 'x')]
    public void Test(char[] s, char target, char expected)
    {
        var result = NextGreatestLetter(s, target);
        Assert.Equal(expected, result);
    }

    public char NextGreatestLetter(char[] letters, char target)
    {
        if (target < letters[0] || target == 'z' || target >= letters[^1])
        {
            return letters[0];
        }

        int left = 0;
        int right = letters.Length - 1;

        while (left + 1 < right)
        {
            int mid = left + (right - left) / 2;

            if (letters[mid] <= target)
            {
                left = mid;
            }
            else
            {
                right = mid;
            }
        }

        return letters[right];
    }
}
