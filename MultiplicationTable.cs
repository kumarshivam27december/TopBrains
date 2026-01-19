public class MultiplicationTable
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine());
        int upto = Convert.ToInt32(Console.ReadLine());

        int[] arr = new int[upto];
        for(int i = 1; i <= upto; i++)
        {
            arr[i-1] = n*i;
        }

        foreach (var item in arr)
        {
            Console.WriteLine(item);
        }
    }
}