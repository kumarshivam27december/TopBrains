using Services;
using System;
using System.Net.Sockets;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SupportTicketUtility service = new SupportTicketUtility();

            while (true)
            {


                Console.WriteLine("1. Display Ticket by Priority");
                Console.WriteLine("2. Escalate Ticket");
                Console.WriteLine("3. Add Ticket");
                Console.WriteLine("4. Exit");

                

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        try
                        {
                            service.DisplayTicketByPriority();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                        break;
                    case 2:
                        try
                        {
                            service.EscalateTicket();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                        break;
                    case 3:
                        try
                        {
                            service.AddTicket();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                        break;
                    case 4:
                        return;
                    default:
                        break;
                }
            }
        }
    }
}
