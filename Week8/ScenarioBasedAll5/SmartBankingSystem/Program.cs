using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartBankingSystem
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message) : base(message) { }
    }

    public class MinimumBalanceException : Exception
    {
        public MinimumBalanceException(string message) : base(message) { }
    }

    public class InvalidTransactionException : Exception
    {
        public InvalidTransactionException(string message) : base(message) { }
    }

    public abstract class BankAccount
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal Balance { get; protected set; }
        public List<string> TransactionHistory { get; set; } = new List<string>();

        protected BankAccount(string accNo, string name, decimal balance)
        {
            AccountNumber = accNo;
            CustomerName = name;
            Balance = balance;
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidTransactionException("Invalid deposit amount.");

            Balance += amount;
            TransactionHistory.Add($"Deposited: {amount}");
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount > Balance)
                throw new InsufficientBalanceException("Insufficient balance.");

            Balance -= amount;
            TransactionHistory.Add($"Withdrawn: {amount}");
        }

        public void Transfer(BankAccount target, decimal amount)
        {
            this.Withdraw(amount);
            target.Deposit(amount);
            TransactionHistory.Add($"Transferred {amount} to {target.AccountNumber}");
        }

        public abstract decimal CalculateInterest();
    }

    public class SavingsAccount : BankAccount
    {
        private const decimal MinimumBalance = 1000;

        public SavingsAccount(string accNo, string name, decimal balance)
            : base(accNo, name, balance) { }

        public override void Withdraw(decimal amount)
        {
            if (Balance - amount < MinimumBalance)
                throw new MinimumBalanceException("Minimum balance violated.");

            base.Withdraw(amount);
        }

        public override decimal CalculateInterest()
        {
            return Balance * 0.04m;
        }
    }

    public class CurrentAccount : BankAccount
    {
        private const decimal OverdraftLimit = 20000;

        public CurrentAccount(string accNo, string name, decimal balance)
            : base(accNo, name, balance) { }

        public override void Withdraw(decimal amount)
        {
            if (Balance - amount < -OverdraftLimit)
                throw new InsufficientBalanceException("Overdraft limit exceeded.");

            Balance -= amount;
            TransactionHistory.Add($"Withdrawn: {amount}");
        }

        public override decimal CalculateInterest()
        {
            return 0;
        }
    }

    public class LoanAccount : BankAccount
    {
        public LoanAccount(string accNo, string name, decimal balance)
            : base(accNo, name, balance) { }

        public override void Deposit(decimal amount)
        {
            throw new InvalidTransactionException("Deposit not allowed in Loan Account.");
        }

        public override decimal CalculateInterest()
        {
            return Balance * 0.10m;
        }
    }

    class Program
    {
        static List<BankAccount> accounts = new List<BankAccount>();

        static void Main(string[] args)
        {
            SeedData();

            while (true)
            {
                Console.WriteLine("\n1. Display Accounts");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. LINQ Reports");
                Console.WriteLine("6. Exit");

                int choice = int.Parse(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1: DisplayAll(); break;
                        case 2: Deposit(); break;
                        case 3: Withdraw(); break;
                        case 4: Transfer(); break;
                        case 5: RunLinq(); break;
                        case 6: return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        static void SeedData()
        {
            accounts.Add(new SavingsAccount("S1", "Ramesh", 80000));
            accounts.Add(new CurrentAccount("C1", "Rahul", 120000));
            accounts.Add(new LoanAccount("L1", "Amit", 50000));
            accounts.Add(new SavingsAccount("S2", "Ritika", 30000));
        }

        static BankAccount Find(string accNo)
        {
            return accounts.FirstOrDefault(a => a.AccountNumber == accNo);
        }

        static void DisplayAll()
        {
            foreach (var acc in accounts)
            {
                Console.WriteLine($"{acc.AccountNumber} | {acc.CustomerName} | {acc.Balance}");
            }
        }

        static void Deposit()
        {
            Console.Write("Account Number: ");
            var acc = Find(Console.ReadLine());

            Console.Write("Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            acc.Deposit(amount);
        }

        static void Withdraw()
        {
            Console.Write("Account Number: ");
            var acc = Find(Console.ReadLine());

            Console.Write("Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            acc.Withdraw(amount);
        }

        static void Transfer()
        {
            Console.Write("From Account: ");
            var from = Find(Console.ReadLine());

            Console.Write("To Account: ");
            var to = Find(Console.ReadLine());

            Console.Write("Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            from.Transfer(to, amount);
        }

        static void RunLinq()
        {
            Console.WriteLine("\nAccounts with Balance > 50000");
            var high = accounts.Where(a => a.Balance > 50000);
            foreach (var a in high)
                Console.WriteLine(a.CustomerName);

            Console.WriteLine("\nTotal Bank Balance:");
            Console.WriteLine(accounts.Sum(a => a.Balance));

            Console.WriteLine("\nTop 3 Highest Balance Accounts:");
            var top3 = accounts.OrderByDescending(a => a.Balance).Take(3);
            foreach (var a in top3)
                Console.WriteLine(a.CustomerName);

            Console.WriteLine("\nGroup By Account Type:");
            var grouped = accounts.GroupBy(a => a.GetType().Name);
            foreach (var group in grouped)
            {
                Console.WriteLine(group.Key);
                foreach (var acc in group)
                    Console.WriteLine("  " + acc.CustomerName);
            }

            Console.WriteLine("\nCustomers starting with R:");
            var rCustomers = accounts.Where(a => a.CustomerName.StartsWith("R"));
            foreach (var a in rCustomers)
                Console.WriteLine(a.CustomerName);
        }
    }
}
