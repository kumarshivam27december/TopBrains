public class Parsing
{
    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine() ?? "0");
        string[] tokens = new string[n];

        for (int i = 0; i < n; i++)
        {
            tokens[i] = Console.ReadLine() ?? "";
        }

        int sum = SumValidIntegers(tokens);
        Console.WriteLine(sum);
    }

    static int SumValidIntegers(string[] tokens)
    {
        int sum = 0;

        foreach (string token in tokens)
        {
            if (int.TryParse(token, out int value))
            {
                sum += value;
            }
        }

        return sum;
    }
}