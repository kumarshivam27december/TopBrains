using System;
using Services;
using Exceptions;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ViolationUtility service = new ViolationUtility();

            while (true)
            {
                Console.WriteLine("1 → Display Violations");
                Console.WriteLine("2 → Pay Fine");
                Console.WriteLine("3 → Add Violation");
                Console.WriteLine("4 → Exit");

                Console.WriteLine("Enter Choice:");
                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            service.DisplayViolations();
                            break;

                        case 2:
                            service.PayFine();
                            break;

                        case 3:
                            service.AddViolation();
                            break;

                        case 4:
                            Console.WriteLine("Thank You");
                            return;

                        default:
                            Console.WriteLine("Invalid Choice.");
                            break;
                    }
                }
                catch (InvalidFineAmountException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (DuplicateViolationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
