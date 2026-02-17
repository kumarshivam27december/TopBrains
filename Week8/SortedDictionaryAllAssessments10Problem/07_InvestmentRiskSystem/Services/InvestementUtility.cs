using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

namespace Services
{
    public class InvestementUtility
    {
        private SortedDictionary<int, List<Investment>> _data
            = new SortedDictionary<int, List<Investment>>();

        public void DisplayInvestement()
        {
            if (_data.Count == 0)
            {
                Console.WriteLine("No investments available.");
                return;
            }

            foreach (KeyValuePair<int, List<Investment>> item in _data)
            {
                Console.WriteLine($"\nRisk Rating: {item.Key}");

                foreach (Investment inv in item.Value)
                {
                    Console.WriteLine($"Id: {inv.InvestmentId}, " +
                                      $"Asset: {inv.AssetName}, " +
                                      $"Risk: {inv.RiskRating}");
                }
            }
        }

        public void UpdateRisk()
        {
            Console.WriteLine("Enter Investment Id to update:");
            string id = Console.ReadLine();

            Investment foundInvestment = null;
            int oldRisk = 0;

            foreach (var pair in _data)
            {
                foundInvestment = pair.Value.FirstOrDefault(x => x.InvestmentId == id);

                if (foundInvestment != null)
                {
                    oldRisk = pair.Key;
                    break;
                }
            }

            if (foundInvestment == null)
            {
                Console.WriteLine("Investment not found.");
                return;
            }

            Console.WriteLine("Enter New Risk Rating (1-5):");
            int newRisk = Convert.ToInt32(Console.ReadLine());

            if (newRisk < 1 || newRisk > 5)
                throw new InvalidRiskRatingException("Risk rating must be between 1 and 5.");

            _data[oldRisk].Remove(foundInvestment);

            if (_data[oldRisk].Count == 0)
                _data.Remove(oldRisk);

            foundInvestment.RiskRating = newRisk;

            if (!_data.ContainsKey(newRisk))
                _data[newRisk] = new List<Investment>();

            _data[newRisk].Add(foundInvestment);

            Console.WriteLine("Risk updated successfully.");
        }

        public void AddInvestment()
        {
            Console.WriteLine("Enter Investment Id:");
            string id = Console.ReadLine();

            foreach (var pair in _data)
            {
                if (pair.Value.Any(x => x.InvestmentId == id))
                    throw new DuplicateInvestmentException("Investment Id already exists.");
            }

            Console.WriteLine("Enter Asset Name:");
            string asset = Console.ReadLine();

            Console.WriteLine("Enter Risk Rating (1-5):");
            int risk = Convert.ToInt32(Console.ReadLine());

            if (risk < 1 || risk > 5)
                throw new InvalidRiskRatingException("Risk rating must be between 1 and 5.");

            Investment investment = new Investment
            {
                InvestmentId = id,
                AssetName = asset,
                RiskRating = risk
            };

            if (!_data.ContainsKey(risk))
                _data[risk] = new List<Investment>();

            _data[risk].Add(investment);

            Console.WriteLine("Investment added successfully.");
        }
    }
}
