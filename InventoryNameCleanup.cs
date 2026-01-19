public class InventoryNameCleanup
{
    public static void Main(string[] args)
    {
        string input1 = Console.ReadLine() ?? "";
        string output1 = RemoveDublicates(input1);
        string output2 = output1.Trim();


        string output3 = ConvertTOTitleCase(output2);
        Console.WriteLine(output3);
    }

    public static string RemoveDublicates(string input)
    {
        if (input.Length == 0) return "";
        string temp = "" + input[0];
        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] != input[i - 1])
            {
                temp += input[i];
            }
        }
        return temp;
    }

    public static string ConvertTOTitleCase(string input)
    {
        if (input.Length == 0) return "";

        string result = "";

        for (int i = 0; i < input.Length; i++)
        {
            char current = input[i];

            if (i == 0 || input[i - 1] == ' ')
            {
                result += char.ToUpper(current);
            }
            else
            {
                result += char.ToLower(current);
            }
        }

        return result;
    }
}