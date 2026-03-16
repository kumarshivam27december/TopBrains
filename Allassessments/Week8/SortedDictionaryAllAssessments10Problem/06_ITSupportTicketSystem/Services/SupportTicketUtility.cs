using System;
using System.Collections.Generic;
using Domain;
using Exceptions;

namespace Services
{
    public class SupportTicketUtility
    {
        private SortedDictionary<int, Queue<SupportTicket>> _data
            = new SortedDictionary<int, Queue<SupportTicket>>();

        public void DisplayTicketByPriority()
        {
            if (_data.Count == 0)
                throw new TicketNotFoundException("No tickets available.");

            foreach (var kvp in _data)
            {
                int priority = kvp.Key;
                Queue<SupportTicket> queue = kvp.Value;

                Console.WriteLine($"Priority: {priority}");

                foreach (var ticket in queue)
                {
                    Console.WriteLine($"TicketId: {ticket.TicketId}");
                    Console.WriteLine($"Issue: {ticket.IssueDescription}");
                    Console.WriteLine($"Severity: {ticket.SeverityLevel}");
                }
            }
        }

        public void EscalateTicket()
        {
            Console.WriteLine("Enter Ticket Id to escalate:");
            string id = Console.ReadLine();

            foreach (var kvp in _data)
            {
                var queue = kvp.Value;
                int size = queue.Count;

                for (int i = 0; i < size; i++)
                {
                    var ticket = queue.Dequeue();

                    if (ticket.TicketId == id)
                    {
                        int newPriority = kvp.Key - 1;

                        if (newPriority < 1)
                            throw new Exception("Ticket already at highest priority.");

                        if (!_data.ContainsKey(newPriority))
                            _data[newPriority] = new Queue<SupportTicket>();

                        _data[newPriority].Enqueue(ticket);

                        Console.WriteLine("Ticket escalated successfully.");
                        return;
                    }
                    else
                    {
                        queue.Enqueue(ticket);
                    }
                }
            }

            throw new TicketNotFoundException("Ticket not found.");
        }


        public void AddTicket()
        {
            Console.WriteLine("Enter the TicketId");
            string ticketid = Console.ReadLine();

            Console.WriteLine("Enter the Issue Description");
            string issueDescription = Console.ReadLine();

            Console.WriteLine("Enter the Severity Level (High/Medium/Low)");
            string severityLevel = Console.ReadLine();

            int priority;

            switch (severityLevel.ToLower())
            {
                case "high":
                    priority = 1;
                    break;
                case "medium":
                    priority = 2;
                    break;
                case "low":
                    priority = 3;
                    break;
                default:
                    throw new InvalidSeverityException("Invalid severity level.");
            }

            SupportTicket ticket = new SupportTicket
            {
                TicketId = ticketid,
                IssueDescription = issueDescription,
                SeverityLevel = severityLevel,
                Priority = priority
            };

            if (!_data.ContainsKey(priority))
                _data[priority] = new Queue<SupportTicket>();

            _data[priority].Enqueue(ticket);

            Console.WriteLine("Ticket added successfully.");
        }
    }
}
