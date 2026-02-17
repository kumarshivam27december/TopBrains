using System;
using Services;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TicketUtility service = new TicketUtility();

            while (true)
            {
                Console.WriteLine("1. Display Ticket");
                Console.WriteLine("2. update fare");
                Console.WriteLine("3. Add ticket");
                Console.WriteLine("4. Exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        service.DisplayTicket();
                        break;
                    case 2:
                        service.UpdateFare();
                        break;
                    case 3:
                        service.AddTicket();
                        break;
                    case 4:
                        Console.WriteLine("Thank You");
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}
