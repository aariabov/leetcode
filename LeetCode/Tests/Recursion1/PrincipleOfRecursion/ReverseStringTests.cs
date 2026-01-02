namespace Tests.Recursion1.PrincipleOfRecursion;

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
        Reverse(s, 0, s.Length - 1);

        void Reverse(char[] str, int left, int right)
        {
            if (left >= right)
                return;

            // меняем местами символы
            char temp = str[left];
            str[left] = str[right];
            str[right] = temp;

            // рекурсивный вызов
            Reverse(str, left + 1, right - 1);
        }
    }

    // работает, но медленно, зачем-то придумал какие-то спаны
    public void ReverseStringMy(char[] s)
    {
        Rec(s);

        void Rec(Span<char> str)
        {
            if (str.Length != 1)
            {
                var temp = str[0];
                str[0] = str[^1];
                str[^1] = temp;
            }
            if (str.Length < 3)
            {
                return;
            }

            var res = str.Slice(1, str.Length - 2);
            Rec(res);
        }
    }
}
