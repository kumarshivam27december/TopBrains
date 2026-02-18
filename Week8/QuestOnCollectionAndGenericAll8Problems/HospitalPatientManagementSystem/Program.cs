using HospitalPatientManagementSystem;

var manager = new HospitalManager();
manager.RegisterPatient(1, "John Doe", 45, "Hypertension");
manager.RegisterPatient(2, "Jane Smith", 32, "Diabetes");
manager.ScheduleAppointment(1);
manager.ScheduleAppointment(2);

var nextPatient = manager.ProcessNextAppointment();
Console.WriteLine(nextPatient?.Name ?? "None"); // Should output: John Doe

var diabeticPatients = manager.FindPatientsByCondition("Diabetes");
Console.WriteLine(diabeticPatients.Count); // Should output: 1
