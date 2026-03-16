using System;
using Services;
using Domain;
using Exceptions;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderUtility service = new OrderUtility();

            while (true)
            {
                Console.WriteLine("1 → Display Orders");
                Console.WriteLine("2 → Update Order");
                Console.WriteLine("3 → Add Order");
                Console.WriteLine("4 → Exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            foreach (var order1 in service.GetAll())
                            {
                                Console.WriteLine($"{order1.OrderId} | {order1.CustomerName} | {order1.OrderAmount}");
                            }
                            break;

                        case 2:
                            service.UpdateEntity(0);
                            break;

                        case 3:
                            Console.WriteLine("Enter Order Id:");
                            string id = Console.ReadLine();

                            Console.WriteLine("Enter Customer Name:");
                            string name = Console.ReadLine();

                            Console.WriteLine("Enter Order Amount:");
                            int amount = Convert.ToInt32(Console.ReadLine());

                            Order order = new Order
                            {
                                OrderId = id,
                                CustomerName = name,
                                OrderAmount = amount
                            };

                            service.AddEntity(amount, order);
                            break;

                        case 4:
                            Console.WriteLine("Thank You");
                            return;

                        default:
                            Console.WriteLine("Invalid Choice");
                            break;
                    }
                }
                catch (InvalidOrderAmountException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (OrderNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
