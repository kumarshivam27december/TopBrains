using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Applicant
{
    public string ApplicantId { get; set; }
    public string ApplicantName { get; set; }
    public string CurrentLocation { get; set; }
    public string PreferredLocation { get; set; }
    public string CoreCompetency { get; set; }
    public int PassingYear { get; set; }
}

class Program
{
    static List<Applicant> applicants = new List<Applicant>();
    static string filePath = "applicants.json";

    static void Main()
    {
        LoadFromFile();

        while (true)
        {
            Console.WriteLine("1.Add Applicant");
            Console.WriteLine("2.Display All");
            Console.WriteLine("3.Search by ID");
            Console.WriteLine("4.Update Applicant");
            Console.WriteLine("5.Delete Applicant");
            Console.WriteLine("6.Exit");
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddApplicant(); break;
                case "2": DisplayAll(); break;
                case "3": SearchApplicant(); break;
                case "4": UpdateApplicant(); break;
                case "5": DeleteApplicant(); break;
                case "6": SaveToFile(); return;
                default: Console.WriteLine("Invalid choice"); break;
            }
        }
    }

    static void AddApplicant()
    {
        Applicant a = new Applicant();

        Console.Write("Applicant ID: ");
        string id = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(id) || id.Length != 8 || !id.StartsWith("CH"))
        {
            Console.WriteLine("Invalid Applicant ID");
            return;
        }
        if (applicants.Exists(x => x.ApplicantId == id))
        {
            Console.WriteLine("Applicant ID already exists");
            return;
        }
        a.ApplicantId = id;

        Console.Write("Applicant Name: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name) || name.Length < 4 || name.Length > 15)
        {
            Console.WriteLine("Invalid Name");
            return;
        }
        a.ApplicantName = name;

        Console.Write("Current Location (Mumbai/Pune/Chennai): ");
        string cl = Console.ReadLine();
        if (cl != "Mumbai" && cl != "Pune" && cl != "Chennai")
        {
            Console.WriteLine("Invalid Current Location");
            return;
        }
        a.CurrentLocation = cl;

        Console.Write("Preferred Location (Mumbai/Pune/Chennai/Delhi/Kolkata/Bangalore): ");
        string pl = Console.ReadLine();
        if (pl != "Mumbai" && pl != "Pune" && pl != "Chennai" &&
            pl != "Delhi" && pl != "Kolkata" && pl != "Bangalore")
        {
            Console.WriteLine("Invalid Preferred Location");
            return;
        }
        a.PreferredLocation = pl;

        Console.Write("Core Competency (.NET/JAVA/ORACLE/Testing): ");
        string cc = Console.ReadLine();
        if (cc != ".NET" && cc != "JAVA" && cc != "ORACLE" && cc != "Testing")
        {
            Console.WriteLine("Invalid Core Competency");
            return;
        }
        a.CoreCompetency = cc;

        Console.Write("Passing Year: ");
        if (!int.TryParse(Console.ReadLine(), out int year) || year > DateTime.Now.Year)
        {
            Console.WriteLine("Invalid Passing Year");
            return;
        }
        a.PassingYear = year;

        applicants.Add(a);
        SaveToFile();
        Console.WriteLine("Applicant Added Successfully");
    }

    static void DisplayAll()
    {
        if (applicants.Count == 0)
        {
            Console.WriteLine("No records found");
            return;
        }

        foreach (var a in applicants)
        {
            Console.WriteLine($"{a.ApplicantId} {a.ApplicantName} {a.CurrentLocation} {a.PreferredLocation} {a.CoreCompetency} {a.PassingYear}");
        }
    }

    static void SearchApplicant()
    {
        Console.Write("Enter Applicant ID: ");
        string id = Console.ReadLine();
        var a = applicants.Find(x => x.ApplicantId == id);
        if (a == null)
        {
            Console.WriteLine("Applicant Not Found");
            return;
        }
        Console.WriteLine($"{a.ApplicantId} {a.ApplicantName} {a.CurrentLocation} {a.PreferredLocation} {a.CoreCompetency} {a.PassingYear}");
    }

    static void UpdateApplicant()
    {
        Console.Write("Enter Applicant ID: ");
        string id = Console.ReadLine();
        var a = applicants.Find(x => x.ApplicantId == id);
        if (a == null)
        {
            Console.WriteLine("Applicant Not Found");
            return;
        }

        Console.Write("New Name: ");
        string name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name) && name.Length >= 4 && name.Length <= 15)
            a.ApplicantName = name;

        Console.Write("New Preferred Location: ");
        string pl = Console.ReadLine();
        if (pl == "Mumbai" || pl == "Pune" || pl == "Chennai" ||
            pl == "Delhi" || pl == "Kolkata" || pl == "Bangalore")
            a.PreferredLocation = pl;

        Console.Write("New Core Competency: ");
        string cc = Console.ReadLine();
        if (cc == ".NET" || cc == "JAVA" || cc == "ORACLE" || cc == "Testing")
            a.CoreCompetency = cc;

        Console.Write("New Passing Year: ");
        if (int.TryParse(Console.ReadLine(), out int year) && year <= DateTime.Now.Year)
            a.PassingYear = year;

        SaveToFile();
        Console.WriteLine("Applicant Updated Successfully");
    }

    static void DeleteApplicant()
    {
        Console.Write("Enter Applicant ID: ");
        string id = Console.ReadLine();
        var a = applicants.Find(x => x.ApplicantId == id);
        if (a == null)
        {
            Console.WriteLine("Applicant Not Found");
            return;
        }

        applicants.Remove(a);
        SaveToFile();
        Console.WriteLine("Applicant Deleted Successfully");
    }

    static void SaveToFile()
    {
        string json = JsonSerializer.Serialize(applicants);
        File.WriteAllText(filePath, json);
    }

    static void LoadFromFile()
    {
        if (!File.Exists(filePath)) return;

        string json = File.ReadAllText(filePath);
        applicants = JsonSerializer.Deserialize<List<Applicant>>(json) ?? new List<Applicant>();
    }
}
