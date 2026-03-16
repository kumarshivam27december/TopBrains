using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

namespace Services
{
    public class OrderUtility
    {
        private SortedDictionary<int, List<Order>> _data
            = new SortedDictionary<int, List<Order>>(
                Comparer<int>.Create((x, y) => y.CompareTo(x)));

        public void AddEntity(int key, Order entity)
        {
            if (entity.OrderAmount <= 0)
                throw new InvalidOrderAmountException("Order amount must be greater than 0.");

            foreach (var pair in _data)
            {
                if (pair.Value.Any(x => x.OrderId == entity.OrderId))
                    throw new InvalidOrderAmountException("Duplicate Order Id not allowed.");
            }

            if (!_data.ContainsKey(key))
                _data[key] = new List<Order>();

            _data[key].Add(entity);
        }

        public void UpdateEntity(int key)
        {
            Console.WriteLine("Enter Order Id:");
            string id = Console.ReadLine();

            Order found = null;
            int oldKey = 0;

            foreach (var pair in _data)
            {
                found = pair.Value.FirstOrDefault(x => x.OrderId == id);
                if (found != null)
                {
                    oldKey = pair.Key;
                    break;
                }
            }

            if (found == null)
                throw new OrderNotFoundException("Order not found.");

            Console.WriteLine("Enter New Order Amount:");
            int newAmount = Convert.ToInt32(Console.ReadLine());

            if (newAmount <= 0)
                throw new InvalidOrderAmountException("Order amount must be greater than 0.");

            _data[oldKey].Remove(found);
            if (_data[oldKey].Count == 0)
                _data.Remove(oldKey);

            found.OrderAmount = newAmount;

            if (!_data.ContainsKey(newAmount))
                _data[newAmount] = new List<Order>();

            _data[newAmount].Add(found);
        }

        public IEnumerable<Order> GetAll()
        {
            List<Order> result = new List<Order>();

            foreach (var pair in _data)
            {
                result.AddRange(pair.Value);
            }

            return result;
        }
    }
}
