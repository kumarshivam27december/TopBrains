public class BankTransactionProgram
{
    public static void Main(string[] args)
    {
        int initialBalance = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the size of transaction array");
        int size = Convert.ToInt32(Console.ReadLine());
        int[] transactions = new int[size];
        for(int i = 0; i < size; i++)
        {
            transactions[i] = Convert.ToInt32(Console.ReadLine());
        }

        foreach (var item in transactions)
        {
            if(item <= 0)
            {
                initialBalance-=(Math.Abs(item));
            }
            else
            {
                initialBalance+=item;
            }
        }

        
        Console.WriteLine(initialBalance);

    }
}