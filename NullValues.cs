public class NullValues
{
    public static void Main(string[] args)
    {
        double?[] doubles = {10.01,69.96,null,0.0,8,90.0};
        double total = 0 ;
        int size = 0;

        foreach (var item in doubles)
        {
            if(item != null)
            {
                size++;
                total+=item.Value;
            }
        }
        double output = total/size;
        Console.WriteLine(Math.Round(output,2));
    }
}