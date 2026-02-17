using System;
using System.Collections.Generic;
using Domain;
using Exceptions;

namespace Services
{
    public class TicketUtility
    {
        private SortedDictionary<int, List<Ticket>> _data =
            new SortedDictionary<int, List<Ticket>>();

        public void DisplayTicket()
        {
            if (_data.Count == 0)
            {
                Console.WriteLine("No tickets available.");
                return;
            }

            foreach (KeyValuePair<int, List<Ticket>> keyValuePair in _data)
            {
                int fare = keyValuePair.Key;

                foreach (Ticket ticket in keyValuePair.Value)
                {
                    Console.WriteLine($"Ticket ID: {ticket.TicketId}");
                    Console.WriteLine($"Passenger Name: {ticket.PassengerName}");
                    Console.WriteLine($"Fare: {ticket.Fare}");
                    Console.WriteLine("-----------------------------");
                }
            }
        }

        public void UpdateFare()
        {
            Console.WriteLine("Enter Ticket Id to update:");
            string id = Console.ReadLine();

            Ticket foundTicket = null;
            int oldFareKey = 0;

            // Find ticket
            foreach (var pair in _data)
            {
                foreach (var ticket in pair.Value)
                {
                    if (ticket.TicketId == id)
                    {
                        foundTicket = ticket;
                        oldFareKey = pair.Key;
                        break;
                    }
                }
                if (foundTicket != null)
                    break;
            }

            if (foundTicket == null)
            {
                Console.WriteLine("Ticket not found.");
                return;
            }

            Console.WriteLine("Enter New Fare:");
            int newFare = Convert.ToInt32(Console.ReadLine());

            if (newFare < 0)
                throw new InvalidFareException("Fare must be greater than 0");

            _data[oldFareKey].Remove(foundTicket);

            if (_data[oldFareKey].Count == 0)
                _data.Remove(oldFareKey);

            foundTicket.Fare = newFare;

            if (!_data.ContainsKey(newFare))
                _data[newFare] = new List<Ticket>();

            _data[newFare].Add(foundTicket);

            Console.WriteLine("Fare updated successfully.");
        }

        public void AddTicket()
        {
            Console.WriteLine("Enter the ticket id");
            string ticketid = Console.ReadLine();

            Console.WriteLine("Enter the passenger name");
            string passengername = Console.ReadLine();

            Console.WriteLine("Enter the fare");
            int fare = Convert.ToInt32(Console.ReadLine());

            if (fare < 0)
                throw new InvalidFareException("Fare price must be greater than 0");

            foreach (var pair in _data)
            {
                foreach (var item in pair.Value)
                {
                    if (item.TicketId == ticketid)
                        throw new DuplicateTicketException("Ticket already exists");
                }
            }

            Ticket ticket = new Ticket()
            {
                TicketId = ticketid,
                PassengerName = passengername,
                Fare = fare
            };

            if (!_data.ContainsKey(fare))
                _data[fare] = new List<Ticket>();

            _data[fare].Add(ticket);

            Console.WriteLine("Ticket added successfully.");
        }
    }
}
