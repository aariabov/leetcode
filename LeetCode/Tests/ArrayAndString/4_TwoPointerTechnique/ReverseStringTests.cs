namespace Tests.ArrayAndString._4_TwoPointerTechnique;

/// <summary>
/// Реверс строки https://leetcode.com/explore/learn/card/array-and-string/205/array-two-pointer-technique/1183/
/// </summary>
public class ReverseStringTests
{
    [Theory]
    [InlineData(new char[] { 'h', 'e', 'l', 'l', 'o' }, new char[] { 'o', 'l', 'l', 'e', 'h' })]
    [InlineData(
        new char[] { 'H', 'a', 'n', 'n', 'a', 'h' },
        new char[] { 'h', 'a', 'n', 'n', 'a', 'H' }
    )]
    public void Test(char[] s, char[] expected)
    {
        ReverseString(s);
        Assert.Equal(expected, s);
    }

    public void ReverseString(char[] s)
    {
        var i = 0;
        var j = s.Length - 1;
        while (i < j)
        {
            (s[i], s[j]) = (s[j], s[i]);
            i++;
            j--;
        }
    }
}
