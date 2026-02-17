using System;
using Services;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            InvestementUtility service = new InvestementUtility();

            while (true)
            {
                Console.WriteLine("1. Display Investement");
                Console.WriteLine("2. Update Risk");
                Console.WriteLine("3. Add Investment");
                Console.WriteLine("4. Exit");



                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        service.DisplayInvestement();
                        break;
                    case 2:
                        try
                        {
                            service.UpdateRisk();
                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 3:
                        try
                        {
                            service.AddInvestment();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 4:
                        Console.WriteLine("Exiting");
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}
