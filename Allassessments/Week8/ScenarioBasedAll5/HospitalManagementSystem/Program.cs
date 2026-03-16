using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagementSystem
{
    public class DoctorNotAvailableException : Exception
    {
        public DoctorNotAvailableException(string message) : base(message) { }
    }

    public class InvalidAppointmentException : Exception
    {
        public InvalidAppointmentException(string message) : base(message) { }
    }

    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException(string message) : base(message) { }
    }

    public class DuplicateMedicalRecordException : Exception
    {
        public DuplicateMedicalRecordException(string message) : base(message) { }
    }

    public interface IBillable
    {
        decimal CalculateBill();
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Doctor : Person, IBillable
    {
        public string Specialization { get; set; }
        public decimal ConsultationFee { get; set; }
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public decimal CalculateBill()
        {
            return Appointments.Count * ConsultationFee;
        }
    }

    public class Patient : Person
    {
        public string Disease { get; set; }
    }

    public class Appointment
    {
        public int AppointmentId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime Date { get; set; }
    }

    public class MedicalRecord
    {
        private string Diagnosis;
        private string Treatment;

        public int RecordId { get; set; }
        public Patient Patient { get; set; }

        public void SetRecord(string diagnosis, string treatment)
        {
            Diagnosis = diagnosis;
            Treatment = treatment;
        }

        public string GetRecord()
        {
            return $"Diagnosis: {Diagnosis}, Treatment: {Treatment}";
        }
    }

    class Program
    {
        static List<Doctor> doctors = new List<Doctor>();
        static List<Patient> patients = new List<Patient>();
        static List<Appointment> appointments = new List<Appointment>();
        static Dictionary<int, MedicalRecord> medicalRecords = new Dictionary<int, MedicalRecord>();

        static void Main(string[] args)
        {
            SeedData();

            ScheduleAppointment(1, 1, DateTime.Now.AddDays(-5));
            ScheduleAppointment(1, 2, DateTime.Now.AddDays(-2));
            ScheduleAppointment(2, 1, DateTime.Now.AddDays(-1));

            RunLinqReports();
        }

        static void SeedData()
        {
            doctors.Add(new Doctor { Id = 1, Name = "Dr. Sharma", Specialization = "Cardiology", ConsultationFee = 1000 });
            doctors.Add(new Doctor { Id = 2, Name = "Dr. Mehta", Specialization = "Orthopedic", ConsultationFee = 800 });

            patients.Add(new Patient { Id = 1, Name = "Rahul", Disease = "Heart" });
            patients.Add(new Patient { Id = 2, Name = "Ritika", Disease = "Bone" });
        }

        static void ScheduleAppointment(int doctorId, int patientId, DateTime date)
        {
            var doctor = doctors.FirstOrDefault(d => d.Id == doctorId);
            var patient = patients.FirstOrDefault(p => p.Id == patientId);

            if (doctor == null)
                throw new DoctorNotAvailableException("Doctor not found.");

            if (patient == null)
                throw new PatientNotFoundException("Patient not found.");

            if (appointments.Any(a => a.Doctor.Id == doctorId && a.Date == date))
                throw new InvalidAppointmentException("Doctor already booked at this time.");

            Appointment appointment = new Appointment
            {
                AppointmentId = appointments.Count + 1,
                Doctor = doctor,
                Patient = patient,
                Date = date
            };

            appointments.Add(appointment);
            doctor.Appointments.Add(appointment);
        }

        static void RunLinqReports()
        {
            Console.WriteLine("\nDoctors with more than 1 appointment:");
            var busyDoctors = doctors.Where(d => d.Appointments.Count > 1);
            foreach (var d in busyDoctors)
                Console.WriteLine(d.Name);

            Console.WriteLine("\nPatients treated in last 30 days:");
            var recentPatients = appointments
                .Where(a => a.Date >= DateTime.Now.AddDays(-30))
                .Select(a => a.Patient.Name)
                .Distinct();

            foreach (var p in recentPatients)
                Console.WriteLine(p);

            Console.WriteLine("\nGroup appointments by doctor:");
            var grouped = appointments.GroupBy(a => a.Doctor.Name);
            foreach (var g in grouped)
            {
                Console.WriteLine(g.Key);
                foreach (var a in g)
                    Console.WriteLine("  Patient: " + a.Patient.Name);
            }

            Console.WriteLine("\nTop 3 highest earning doctors:");
            var topDoctors = doctors
                .OrderByDescending(d => d.CalculateBill())
                .Take(3);

            foreach (var d in topDoctors)
                Console.WriteLine(d.Name + " - " + d.CalculateBill());

            Console.WriteLine("\nPatients by disease (Heart):");
            var heartPatients = patients.Where(p => p.Disease == "Heart");
            foreach (var p in heartPatients)
                Console.WriteLine(p.Name);

            Console.WriteLine("\nTotal Revenue Generated:");
            Console.WriteLine(doctors.Sum(d => d.CalculateBill()));
        }
    }
}
