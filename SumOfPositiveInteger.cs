public class SumOfPositiveInteger
{
    public static void Main(string[] args)
    {
        int sizeOfArr = Convert.ToInt32(Console.ReadLine());
        int[] arr = new int[sizeOfArr];
        for(int i = 0; i < sizeOfArr; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        int sum = 0;
        foreach (var item in arr)
        {
            if (item == 0)
            {
                break;
            }else if (item < 0)
            {
                continue;
            }
            else
            {
                sum+=item;
            }
        }

        Console.WriteLine(sum);
    }
}