public class ExtentionMethod
{
    public static void Main(string[] args)
    {
        int size = Convert.ToInt32(Console.ReadLine());
        string[] items = new string[size];
        for(int i = 0; i < size; i++)
        {
            items[i] = Console.ReadLine() ?? "";
        }

        string[] distinctNames = items.DistinctByID();
        foreach (var item in distinctNames)
        {
            Console.WriteLine(item);
        }

    }
}
static class Extention
{
    public static string[] DistinctByID(this string[] items)
    {
        HashSet<string> seenId = new HashSet<string>();
        List<string> output = new List<string>();

        foreach (string  item in items)
        {
            string[] parts = item.Split(':');
            string id = parts[0];
            string name = parts[1];
            if (!seenId.Contains(id))
            {
                seenId.Add(id);
                output.Add(name);
            }
        }
        return output.ToArray();
    }
}