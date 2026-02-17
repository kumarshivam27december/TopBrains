using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

namespace Services
{
    public class ViolationUtility
    {
        private SortedDictionary<int, List<Violation>> _data
            = new SortedDictionary<int, List<Violation>>(
                Comparer<int>.Create((x, y) => y.CompareTo(x)));

        public void DisplayViolations()
        {
            if (_data.Count == 0)
            {
                Console.WriteLine("No violations found.");
                return;
            }

            foreach (var pair in _data)
            {
                Console.WriteLine($"\nFine Amount: {pair.Key}");

                foreach (var v in pair.Value)
                {
                    Console.WriteLine($"Vehicle: {v.VehicleNumber}, " +
                                      $"Owner: {v.OwnerName}, " +
                                      $"Fine: {v.FineAmount}");
                }
            }
        }

        public void PayFine()
        {
            Console.WriteLine("Enter Vehicle Number:");
            string vehicle = Console.ReadLine();

            Violation found = null;
            int oldFine = 0;

            foreach (var pair in _data)
            {
                found = pair.Value.FirstOrDefault(x => x.VehicleNumber == vehicle);

                if (found != null)
                {
                    oldFine = pair.Key;
                    break;
                }
            }

            if (found == null)
            {
                Console.WriteLine("Violation not found.");
                return;
            }

            _data[oldFine].Remove(found);

            if (_data[oldFine].Count == 0)
                _data.Remove(oldFine);

            Console.WriteLine("Fine paid successfully. Violation removed.");
        }

        public void AddViolation()
        {
            Console.WriteLine("Enter Vehicle Number:");
            string vehicle = Console.ReadLine();

            foreach (var pair in _data)
            {
                if (pair.Value.Any(x => x.VehicleNumber == vehicle))
                    throw new DuplicateViolationException("Vehicle already has a violation.");
            }

            Console.WriteLine("Enter Owner Name:");
            string owner = Console.ReadLine();

            Console.WriteLine("Enter Fine Amount:");
            int fine = Convert.ToInt32(Console.ReadLine());

            if (fine <= 0)
                throw new InvalidFineAmountException("Fine amount must be greater than 0.");

            Violation violation = new Violation
            {
                VehicleNumber = vehicle,
                OwnerName = owner,
                FineAmount = fine
            };

            if (!_data.ContainsKey(fine))
                _data[fine] = new List<Violation>();

            _data[fine].Add(violation);

            Console.WriteLine("Violation added successfully.");
        }
    }
}
