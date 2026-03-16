namespace HospitalPatientManagementSystem;

public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Condition { get; set; } = string.Empty;
    public List<string> MedicalHistory { get; } = new List<string>();

    public Patient(int id, string name, int age, string condition)
    {
        Id = id;
        Name = name;
        Age = age;
        Condition = condition;
    }

    public void AddToMedicalHistory(string record)
    {
        MedicalHistory.Add(record);
    }
}
