using System.Collections.Generic;
using System.Linq;

namespace HospitalPatientManagementSystem;

public class HospitalManager
{
    private readonly Dictionary<int, Patient> _patients = new Dictionary<int, Patient>();
    private readonly Queue<Patient> _appointmentQueue = new Queue<Patient>();

    public void RegisterPatient(int id, string name, int age, string condition)
    {
        var patient = new Patient(id, name, age, condition);
        _patients[id] = patient;
    }

    public void ScheduleAppointment(int patientId)
    {
        if (_patients.TryGetValue(patientId, out var patient))
        {
            _appointmentQueue.Enqueue(patient);
        }
    }

    public Patient? ProcessNextAppointment()
    {
        return _appointmentQueue.Count > 0 ? _appointmentQueue.Dequeue() : null;
    }

    public List<Patient> FindPatientsByCondition(string condition)
    {
        return _patients.Values
            .Where(p => string.Equals(p.Condition, condition, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void AddMedicalRecord(int patientId, string record)
    {
        if (_patients.TryGetValue(patientId, out var patient))
        {
            patient.AddToMedicalHistory(record);
        }
    }
}
