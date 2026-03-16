using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

namespace Services
{
    public class PatientUtility
    {
        private SortedDictionary<int, Queue<Patient>> _data
            = new SortedDictionary<int, Queue<Patient>>();

        public void AddPatient(Patient patient)
        {
            if (patient.SeverityLevel < 1)
                throw new InvalidSeverityLevelException("Severity level must be 1 or greater.");

            if (!_data.ContainsKey(patient.SeverityLevel))
                _data[patient.SeverityLevel] = new Queue<Patient>();

            _data[patient.SeverityLevel].Enqueue(patient);
        }

        public void UpdateSeverity()
        {
            Console.WriteLine("Enter Patient Id:");
            string id = Console.ReadLine();

            Patient found = null;
            int oldKey = 0;

            foreach (var pair in _data)
            {
                found = pair.Value.FirstOrDefault(x => x.PatientId == id);
                if (found != null)
                {
                    oldKey = pair.Key;
                    break;
                }
            }

            if (found == null)
                throw new PatientNotFoundException("Patient not found.");

            Console.WriteLine("Enter New Severity Level:");
            int newSeverity = Convert.ToInt32(Console.ReadLine());

            if (newSeverity < 1)
                throw new InvalidSeverityLevelException("Severity level must be 1 or greater.");

            Queue<Patient> tempQueue = new Queue<Patient>();

            while (_data[oldKey].Count > 0)
            {
                var p = _data[oldKey].Dequeue();
                if (p.PatientId != id)
                    tempQueue.Enqueue(p);
            }

            _data[oldKey] = tempQueue;

            if (_data[oldKey].Count == 0)
                _data.Remove(oldKey);

            found.SeverityLevel = newSeverity;

            if (!_data.ContainsKey(newSeverity))
                _data[newSeverity] = new Queue<Patient>();

            _data[newSeverity].Enqueue(found);
        }

        public IEnumerable<Patient> GetAll()
        {
            List<Patient> result = new List<Patient>();

            foreach (var pair in _data)
            {
                foreach (var patient in pair.Value)
                {
                    result.Add(patient);
                }
            }

            return result;
        }
    }
}
