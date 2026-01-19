public class GreatestCommonDivisor
{
    static void Main(string[] args)
    {
        string[] input = (Console.ReadLine() ?? "").Split(' ');

        int a = Convert.ToInt32(input[0]);
        int b = Convert.ToInt32(input[1]);

        int result = GCD(a, b);
        Console.WriteLine(result);
    }

    static int GCD(int a, int b)
    {
        if (b == 0)
            return a;

        return GCD(b, a % b);
    }
}