public class BankingTransactionModule
{
    public static void Main(string[] args)
    {
        Account userAccount = new Account();
        Console.WriteLine("1. Deposit");
        Console.WriteLine("2. WithDraw");
        Console.WriteLine("Enter the choice");
        int choice = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the account number");
        userAccount.AccountNumber = Console.ReadLine();
        Console.WriteLine("Enter the balance");
        userAccount.Balance = Convert.ToDecimal(Console.ReadLine());

        try
        {
            if (choice == 1)
            {
                Console.WriteLine("Enter the amount to be deposit");
                decimal amount = Convert.ToInt32(Console.ReadLine());
                decimal currentBalance = userAccount.Deposit(amount);
                Console.WriteLine("Balance amount " + currentBalance);
            }
            else if (choice == 2)
            {
                Console.WriteLine("Enter the amount to be withDraw");
                decimal amount = Convert.ToInt32(Console.ReadLine());
                decimal currentBalance = userAccount.WithDraw(amount);
                Console.WriteLine("Balance amount " + currentBalance);
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }catch(ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

public class Account
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }

    public decimal Deposit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
        }
        else
        {
            throw new ArgumentException("Deposit amount must be positive");
        }
        return Balance;
    }

    public decimal WithDraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
        }
        else if (amount <= 0)
        {
            throw new ArgumentException("WithDrawal amount must be positive.");
        }
        else
        {
            throw new InvalidOperationException("Insufficient funds.");
        }
        return Balance;
    }
}