using System;
using System.Collections.Generic;
using System.Text.Json;
public class StringFormat
{
    public static string BuildAndSerialize(string[] items,int minScore)
    {
        List<Student> students = new List<Student>();


        foreach (string item in items)
        {
            string[] parts = item.Split(' ');
            string name = parts[0];
            int score = Convert.ToInt32(parts[1]);

            if (score >= minScore)
            {
                students.Add(new Student(name,score));
            }
        }

        students.Sort((a, b) =>
        {
            if (a.Score != b.Score)
            {
                return b.Score.CompareTo(a.Score);
            }
            return string.Compare(a.Name,b.Name,StringComparison.Ordinal);
        });

        return JsonSerializer.Serialize(students);
    }
    public static void Main(string[] args)
    {
        int size = Convert.ToInt32(Console.ReadLine());
        string[] items = new string[size];

        for(int i = 0; i < size; i++)
        {
            items[i] = Console.ReadLine() ?? "";
        }

        int minScore = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(BuildAndSerialize(items,minScore));
    }
}

public class Student
{
    public string Name {get;init;}
    public int Score {get;init;}
    public Student(string Name,int Score)
    {
        this.Name= Name;
        this.Score = Score;
    }
}