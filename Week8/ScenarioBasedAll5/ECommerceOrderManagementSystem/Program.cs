using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem
{
    public class OutOfStockException : Exception
    {
        public OutOfStockException(string message) : base(message) { }
    }

    public class OrderAlreadyShippedException : Exception
    {
        public OrderAlreadyShippedException(string message) : base(message) { }
    }

    public class CustomerBlacklistedException : Exception
    {
        public CustomerBlacklistedException(string message) : base(message) { }
    }

    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal totalAmount);
    }

    public class PercentageDiscount : IDiscountStrategy
    {
        private decimal _percentage;

        public PercentageDiscount(decimal percentage)
        {
            _percentage = percentage;
        }

        public decimal ApplyDiscount(decimal totalAmount)
        {
            return totalAmount - (totalAmount * _percentage / 100);
        }
    }

    public class FlatDiscount : IDiscountStrategy
    {
        private decimal _amount;

        public FlatDiscount(decimal amount)
        {
            _amount = amount;
        }

        public decimal ApplyDiscount(decimal totalAmount)
        {
            return totalAmount - _amount;
        }
    }

    public class FestivalDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal totalAmount)
        {
            return totalAmount * 0.85m;
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBlacklisted { get; set; }
        public decimal TotalSpent { get; set; }
    }

    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice()
        {
            return Product.Price * Quantity;
        }
    }

    public enum OrderStatus
    {
        Pending,
        Shipped,
        Cancelled
    }

    public class Order
    {
        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public IDiscountStrategy DiscountStrategy { get; set; }

        public decimal CalculateTotal()
        {
            decimal total = Items.Sum(i => i.TotalPrice());

            if (DiscountStrategy != null)
                total = DiscountStrategy.ApplyDiscount(total);

            return total;
        }

        public void Ship()
        {
            Status = OrderStatus.Shipped;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Shipped)
                throw new OrderAlreadyShippedException("Cannot cancel shipped order.");

            Status = OrderStatus.Cancelled;
        }
    }

    class Program
    {
        static List<Product> products = new List<Product>();
        static List<Customer> customers = new List<Customer>();
        static List<Order> orders = new List<Order>();
        static Dictionary<int, Product> productDictionary = new Dictionary<int, Product>();

        static void Main(string[] args)
        {
            SeedData();

            CreateOrder(1, new Dictionary<int, int> { { 1, 2 }, { 2, 1 } }, new PercentageDiscount(10));
            CreateOrder(2, new Dictionary<int, int> { { 3, 5 } }, new FestivalDiscount());

            RunLinqReports();
        }

        static void SeedData()
        {
            products.Add(new Product { Id = 1, Name = "Laptop", Price = 60000, Stock = 10 });
            products.Add(new Product { Id = 2, Name = "Phone", Price = 30000, Stock = 20 });
            products.Add(new Product { Id = 3, Name = "Headphones", Price = 2000, Stock = 50 });

            foreach (var p in products)
                productDictionary[p.Id] = p;

            customers.Add(new Customer { Id = 1, Name = "Rahul", IsBlacklisted = false });
            customers.Add(new Customer { Id = 2, Name = "Ritika", IsBlacklisted = false });
        }

        static void CreateOrder(int customerId, Dictionary<int, int> items, IDiscountStrategy discount)
        {
            var customer = customers.First(c => c.Id == customerId);

            if (customer.IsBlacklisted)
                throw new CustomerBlacklistedException("Customer is blacklisted.");

            Order order = new Order
            {
                OrderId = orders.Count + 1,
                Customer = customer,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                DiscountStrategy = discount
            };

            foreach (var item in items)
            {
                var product = productDictionary[item.Key];

                if (product.Stock < item.Value)
                    throw new OutOfStockException($"{product.Name} out of stock.");

                product.Stock -= item.Value;

                order.Items.Add(new OrderItem
                {
                    Product = product,
                    Quantity = item.Value
                });
            }

            decimal total = order.CalculateTotal();
            customer.TotalSpent += total;

            orders.Add(order);

            Console.WriteLine($"Order Created. Total: {total}");
        }

        static void RunLinqReports()
        {
            Console.WriteLine("\nOrders in last 7 days:");
            var recent = orders.Where(o => o.OrderDate >= DateTime.Now.AddDays(-7));
            foreach (var o in recent)
                Console.WriteLine(o.OrderId);

            Console.WriteLine("\nTotal Revenue:");
            Console.WriteLine(orders.Sum(o => o.CalculateTotal()));

            Console.WriteLine("\nMost Sold Product:");
            var mostSold = orders
                .SelectMany(o => o.Items)
                .GroupBy(i => i.Product.Name)
                .OrderByDescending(g => g.Sum(i => i.Quantity))
                .FirstOrDefault();

            if (mostSold != null)
                Console.WriteLine(mostSold.Key);

            Console.WriteLine("\nTop 5 Customers by Spending:");
            var topCustomers = customers
                .OrderByDescending(c => c.TotalSpent)
                .Take(5);

            foreach (var c in topCustomers)
                Console.WriteLine(c.Name);

            Console.WriteLine("\nGroup Orders by Status:");
            var grouped = orders.GroupBy(o => o.Status);
            foreach (var g in grouped)
            {
                Console.WriteLine(g.Key);
                foreach (var o in g)
                    Console.WriteLine("  Order " + o.OrderId);
            }

            Console.WriteLine("\nProducts with Stock < 10:");
            var lowStock = products.Where(p => p.Stock < 10);
            foreach (var p in lowStock)
                Console.WriteLine(p.Name);
        }
    }
}
