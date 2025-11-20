namespace Tests.ArrayAndString._3_IntroductionToString;

/// <summary>
/// Сложение бинарных чисел https://leetcode.com/explore/learn/card/array-and-string/203/introduction-to-string/1160/
/// </summary>
public class AddBinaryTests
{
    [Theory]
    [InlineData("11", "1", "100")]
    [InlineData("1010", "1011", "10101")]
    public void Test(string a, string b, string expected)
    {
        var result = AddBinary(a, b);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("11", "1", "100")]
    [InlineData("1010", "1011", "10101")]
    public void Test1(string a, string b, string expected)
    {
        var result = AddBinary1(a, b);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("11", "1", "100")]
    [InlineData("1010", "1011", "10101")]
    public void Test2(string a, string b, string expected)
    {
        var result = AddBinary2(a, b);
        Assert.Equal(expected, result);
    }

    // решение gpt
    public string AddBinary2(string a, string b)
    {
        int i = a.Length - 1;
        int j = b.Length - 1;
        int carry = 0;
        var result = new System.Text.StringBuilder();

        while (i >= 0 || j >= 0 || carry > 0)
        {
            int sum = carry;

            if (i >= 0)
                sum += a[i--] - '0';

            if (j >= 0)
                sum += b[j--] - '0';

            result.Insert(0, (sum % 2).ToString());
            carry = sum / 2;
        }

        return result.ToString();
    }

    // второе решение
    public string AddBinary1(string a, string b)
    {
        var len = a.Length > b.Length ? a.Length : b.Length;
        var result = new System.Text.StringBuilder();
        var adelta = len - a.Length;
        var bdelta = len - b.Length;
        var prev = 0;
        for (int i = len - 1; i > -1; i--)
        {
            var aIdx = i - adelta;
            var bIdx = i - bdelta;
            var aItem = aIdx > -1 ? a[aIdx] - '0' : 0;
            var bItem = bIdx > -1 ? b[bIdx] - '0' : 0;

            var sum = aItem + bItem + prev;
            prev = sum > 1 ? 1 : 0;
            result.Insert(0, (sum % 2).ToString());
        }

        if (prev > 0)
        {
            result.Insert(0, '1');
        }

        return result.ToString();
    }

    // первое решение
    public string AddBinary(string a, string b)
    {
        var result = new int[a.Length > b.Length ? a.Length : b.Length];
        var adelta = result.Length - a.Length;
        var bdelta = result.Length - b.Length;
        var prev = 0;
        for (int i = result.Length - 1; i > -1; i--)
        {
            var aIdx = i - adelta;
            var bIdx = i - bdelta;
            var aItem = aIdx > -1 ? a[aIdx] - '0' : 0; //  - '0' - хак, чтобы char привести к числу
            var bItem = bIdx > -1 ? b[bIdx] - '0' : 0;

            var sum = aItem + bItem + prev;
            prev = sum > 1 ? 1 : 0;
            result[i] = sum % 2;
        }

        var resultString = string.Join(null, result);
        return prev > 0 ? '1' + resultString : resultString;
    }
}
