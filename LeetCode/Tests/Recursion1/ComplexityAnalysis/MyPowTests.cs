namespace Tests.Recursion1.ComplexityAnalysis;

/// <summary>
/// [Возведение в степень](https://leetcode.com/explore/learn/card/recursion-i/256/complexity-analysis/2380/)
/// </summary>
public class MyPowTests
{
    [Theory]
    [InlineData(2, 10, 1024)]
    [InlineData(2, 3, 8)]
    [InlineData(2.1, 3, 9.261000000000001)]
    [InlineData(2, -2, 0.25)]
    [InlineData(0.44528, 0, 1)]
    [InlineData(2, -2147483648, 0)]
    public void Test(double x, int n, double expected)
    {
        var result = MyPow(x, n);
        Assert.Equal(expected, result);
    }

    public double MyPow(double x, int n)
    {
        long power = n; // защита от int.MinValue

        if (power < 0)
        {
            return 1 / Pow(x, -power);
        }

        return Pow(x, power);
    }

    private double Pow(double x, long n)
    {
        // базовый случай
        if (n == 0)
            return 1.0;

        // рекурсивный вызов
        double half = Pow(x, n / 2);

        // если n чётное
        if (n % 2 == 0)
            return half * half;

        // если n нечётное
        return half * half * x;
    }

    public double MyPowIter(double x, int n)
    {
        long power = n;

        // если степень отрицательная
        if (power < 0)
        {
            x = 1 / x;
            power = -power;
        }

        double result = 1.0;

        while (power > 0)
        {
            if (power % 2 == 1)
            {
                result *= x;
                power--;
            }
            else
            {
                x *= x; // используем свойства степеней: если степень положительная, то основание можно возвести в квадрат, а степень разделить на 2
                power = power / 2;
            }
        }

        return result;
    }

    // работает, но медленно на больших n
    public double MyPow1(double x, int n)
    {
        if (n == 0)
        {
            return 1;
        }

        long y = n;
        if (n < 0)
        {
            x = 1 / x;
            y = -(long)n;
        }

        var res = x;
        while (y > 1)
        {
            res *= x;
            y--;
        }
        return res;
    }

    // работает, но падает на stack overflow
    public double MyPowMyRec(double x, int n)
    {
        if (n < 0)
        {
            x = 1 / x;
            n *= -1;
        }

        return Rec(x, n);

        double Rec(double x, int n)
        {
            if (n == 1)
            {
                return x;
            }

            var res = MyPowMyRec(x, n - 1) * x;
            return res;
        }
    }
}
