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
            PatientUtility service = new PatientUtility();

            while (true)
            {
                Console.WriteLine("1  Display Patients by Priority");
                Console.WriteLine("2  Update Severity");
                Console.WriteLine("3  Add Patient");
                Console.WriteLine("4  Exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            foreach (var p in service.GetAll())
                            {
                                Console.WriteLine($"{p.PatientId} | {p.Name} | {p.SeverityLevel}");
                            }
                            break;

                        case 2:
                            service.UpdateSeverity();
                            break;

                        case 3:
                            Console.WriteLine("Enter Patient Id:");
                            string id = Console.ReadLine();

                            Console.WriteLine("Enter Name:");
                            string name = Console.ReadLine();

                            Console.WriteLine("Enter Severity Level:");
                            int severity = Convert.ToInt32(Console.ReadLine());

                            Patient patient = new Patient
                            {
                                PatientId = id,
                                Name = name,
                                SeverityLevel = severity
                            };

                            service.AddPatient(patient);
                            break;

                        case 4:
                            Console.WriteLine("Thank You");
                            return;

                        default:
                            Console.WriteLine("Invalid Choice");
                            break;
                    }
                }
                catch (InvalidSeverityLevelException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (PatientNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
