using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingSystem
{
    public class InvalidTradeException : Exception
    {
        public InvalidTradeException(string message) : base(message) { }
    }

    public delegate void PriceChangedHandler(Stock stock);

    public class Stock
    {
        public string Symbol { get; set; }
        public decimal Price { get; private set; }

        public event PriceChangedHandler OnPriceChanged;

        public Stock(string symbol, decimal price)
        {
            Symbol = symbol;
            Price = price;
        }

        public void UpdatePrice(decimal newPrice)
        {
            Price = newPrice;
            OnPriceChanged?.Invoke(this);
        }
    }

    public class Transaction
    {
        public string StockSymbol { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public bool IsBuy { get; set; }
    }

    public class Investor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public decimal NetProfitLoss()
        {
            return Transactions.Aggregate(0m, (total, t) =>
            {
                if (t.IsBuy)
                    return total - (t.Price * t.Quantity);
                else
                    return total + (t.Price * t.Quantity);
            });
        }
    }

    public interface IRiskStrategy
    {
        string CalculateRisk(Investor investor);
    }

    public class SimpleRiskStrategy : IRiskStrategy
    {
        public string CalculateRisk(Investor investor)
        {
            var totalInvestment = investor.Transactions
                .Where(t => t.IsBuy)
                .Sum(t => t.Price * t.Quantity);

            if (totalInvestment > 100000)
                return "High Risk";
            else if (totalInvestment > 50000)
                return "Medium Risk";
            else
                return "Low Risk";
        }
    }

    class Program
    {
        static List<Investor> investors = new List<Investor>();
        static List<Stock> stocks = new List<Stock>();
        static List<Transaction> allTransactions = new List<Transaction>();
        static Dictionary<string, List<Transaction>> transactionByStock =
            new Dictionary<string, List<Transaction>>();

        static IRiskStrategy riskStrategy = new SimpleRiskStrategy();

        static void Main(string[] args)
        {
            SeedData();

            ExecuteTrade(1, "TCS", 10, 3500, true);
            ExecuteTrade(1, "TCS", 5, 3600, false);
            ExecuteTrade(2, "INFY", 20, 1500, true);

            RunLinqReports();
        }

        static void SeedData()
        {
            stocks.Add(new Stock("TCS", 3500));
            stocks.Add(new Stock("INFY", 1500));

            investors.Add(new Investor { Id = 1, Name = "Rahul" });
            investors.Add(new Investor { Id = 2, Name = "Ritika" });

            foreach (var stock in stocks)
            {
                stock.OnPriceChanged += s =>
                {
                    Console.WriteLine($"Price Updated: {s.Symbol} - {s.Price}");
                };
            }
        }

        static void ExecuteTrade(int investorId, string symbol, int qty, decimal price, bool isBuy)
        {
            if (DateTime.Now > DateTime.Now)
                throw new InvalidTradeException("Transaction date cannot be future.");

            var investor = investors.First(i => i.Id == investorId);

            if (!isBuy)
            {
                int owned = investor.Transactions
                    .Where(t => t.StockSymbol == symbol && t.IsBuy)
                    .Sum(t => t.Quantity)
                    -
                    investor.Transactions
                    .Where(t => t.StockSymbol == symbol && !t.IsBuy)
                    .Sum(t => t.Quantity);

                if (qty > owned)
                    throw new InvalidTradeException("Cannot sell more shares than owned.");
            }

            Transaction transaction = new Transaction
            {
                StockSymbol = symbol,
                Quantity = qty,
                Price = price,
                Date = DateTime.Now,
                IsBuy = isBuy
            };

            investor.Transactions.Add(transaction);
            allTransactions.Add(transaction);

            if (!transactionByStock.ContainsKey(symbol))
                transactionByStock[symbol] = new List<Transaction>();

            transactionByStock[symbol].Add(transaction);
        }

        static void RunLinqReports()
        {
            Console.WriteLine("\nMost Profitable Investor:");
            var profitable = investors
                .OrderByDescending(i => i.NetProfitLoss())
                .FirstOrDefault();

            if (profitable != null)
                Console.WriteLine(profitable.Name + " - " + profitable.NetProfitLoss());

            Console.WriteLine("\nStock with Highest Volume Traded:");
            var highestVolume = allTransactions
                .GroupBy(t => t.StockSymbol)
                .OrderByDescending(g => g.Sum(t => t.Quantity))
                .FirstOrDefault();

            if (highestVolume != null)
                Console.WriteLine(highestVolume.Key);

            Console.WriteLine("\nGroup Transactions by Stock:");
            foreach (var kvp in transactionByStock)
            {
                Console.WriteLine(kvp.Key);
                foreach (var t in kvp.Value)
                    Console.WriteLine("  Qty: " + t.Quantity);
            }

            Console.WriteLine("\nInvestors with Negative Returns:");
            var negative = investors
                .Where(i => i.NetProfitLoss() < 0);

            foreach (var i in negative)
                Console.WriteLine(i.Name);

            Console.WriteLine("\nRisk Calculation:");
            foreach (var i in investors)
            {
                Console.WriteLine(i.Name + " - " + riskStrategy.CalculateRisk(i));
            }
        }
    }
}
