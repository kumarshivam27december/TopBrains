public class MahirlandAlphabetsandVowels
{
    public static void Main(string[] args)
    {
        string string1 = Console.ReadLine() ?? "";
        string string2 = Console.ReadLine() ?? "";

        string afterRemovingConsonant = RemoveCommonConsonant(string1, string2);
        string finalResult = RemoveConsecutiveDuplicate(afterRemovingConsonant);

        Console.WriteLine(finalResult);
    }

    public static string RemoveCommonConsonant(string string1, string string2)
    {
        string result = "";
        string secondLower = string2.ToLower();

        foreach (char ch in string1)
        {
            if (IsVowel(ch))
            {
                result += ch;
            }
            else
            {
                if (!secondLower.Contains(char.ToLower(ch)))
                {
                    result += ch;
                }
            }
        }

        return result;
    }

    public static string RemoveConsecutiveDuplicate(string input)
    {
        if (input.Length == 0)
            return "";

        string result = "" + input[0];

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] != input[i - 1])
            {
                result += input[i];
            }
        }

        return result;
    }

    public static bool IsVowel(char ch)
    {
        if(ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u')
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
