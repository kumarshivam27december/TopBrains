public class ObjectArray
{
    public static void Main(string[] args)
    {
        object[] objArray = new object[] {"shivam",60,"rahul",90,88,"Rohit"};

        int total = 0;
        foreach (var item in objArray)
        {
            if(item is int x)
            {
                total+=x;
            }   
        }
        Console.WriteLine(total);
    }
}