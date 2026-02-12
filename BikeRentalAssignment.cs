using System;
using System.Collections.Generic;

public class Bike
{
    public string Model { get; set; }
    public int PricePerDay { get; set; }
    public string Brand { get; set; }
}

public class BikeUtility
{
    public void AddBikeDetails(string model, string brand, int pricePerDay)
    {
        Bike bike = new Bike
        {
            Model = model,
            Brand = brand,
            PricePerDay = pricePerDay
        };

        int key = Program.bikeDetails.Count + 1;
        Program.bikeDetails.Add(key, bike);
    }

    public SortedDictionary<string, List<Bike>> GroupBikesByBrand()
    {
        SortedDictionary<string, List<Bike>> grouped =
            new SortedDictionary<string, List<Bike>>();

        foreach (var item in Program.bikeDetails)
        {
            Bike bike = item.Value;

            if (!grouped.ContainsKey(bike.Brand))
            {
                grouped[bike.Brand] = new List<Bike>();
            }

            grouped[bike.Brand].Add(bike);
        }

        return grouped;
    }
}

public class Program
{
    public static SortedDictionary<int, Bike> bikeDetails =
        new SortedDictionary<int, Bike>();

    public static void Main()
    {
        BikeUtility utility = new BikeUtility();
        int choice = 0;

        do
        {
            Console.WriteLine("1. Add Bike Details");
            Console.WriteLine("2. Group Bikes By Brand");
            Console.WriteLine("3. Exit");

            Console.Write("Enter your choice ");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine();

                    Console.Write("Enter the model: ");
                    string model = Console.ReadLine();

                    Console.Write("Enter the brand: ");
                    string brand = Console.ReadLine();

                    Console.Write("Enter the price per day: ");
                    int price = Convert.ToInt32(Console.ReadLine());

                    utility.AddBikeDetails(model, brand, price);

                    Console.WriteLine();
                    Console.WriteLine("Bike details added successfully");
                    Console.WriteLine();
                    break;

                case 2:
                    Console.WriteLine();

                    var grouped = utility.GroupBikesByBrand();

                    foreach (var brandGroup in grouped)
                    {
                        foreach (var bike in brandGroup.Value)
                        {
                            Console.WriteLine($"{brandGroup.Key} {bike.Model}");
                        }
                    }

                    Console.WriteLine();
                    break;

                case 3:
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

        } while (choice != 3);
    }
}
